using AbstractCookShopModel;
using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.Interfaces;
using AbstractCookShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using System.Linq;
using AbstractCookShopServiceImplementDataBase;

namespace AbstractShopServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private AbstractCookShopDbContext context;
        public MainServiceDB(AbstractCookShopDbContext context)
        {
            this.context = context;
        }
        public List<SOrderViewModel> GetList()
        {
            List<SOrderViewModel> result = context.SOrders.Select(rec => new SOrderViewModel
            {
                Id = rec.Id,
                SClientId = rec.SClientId,
                CookId = rec.CookId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                SClientFIO = rec.SClient.SClientFIO,
                CookName = rec.Cook.CookName
            })
            .ToList();
            return result;
        }
        public void CreateOrder(SOrderBindingModel model)
        {
            context.SOrders.Add(new SOrder
            {
                SClientId = model.SClientId,
                CookId = model.CookId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = SOrderStatus.Принят
            });
            context.SaveChanges();
        }
        public void TakeOrderInWork(SOrderBindingModel model)
        {

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != SOrderStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var CookIngridientss = context.CookIngridientss.Include(rec => rec.Ingridients).Where(rec => rec.CookId == element.CookId);
                    // списываем 
                    foreach (var CookIngridients in CookIngridientss)
                    {
                        int countOnStocks = CookIngridients.Count * element.Count;
                        var stockIngridientss = context.StockIngridientss.Where(rec => rec.IngridientsId == CookIngridients.IngridientsId);
                        foreach (var stockIngridients in stockIngridientss)
                        {
                            // компонентов на одном слкаде может не хватать 
                            if (stockIngridients.Count >= countOnStocks)
                            {
                                stockIngridients.Count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockIngridients.Count;
                                stockIngridients.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " + CookIngridients.Ingridients.IngridientsName + " требуется " + CookIngridients.Count + ", не хватает " + countOnStocks);
                        }
                    }
                    element.DateImplement = DateTime.Now;
                    element.Status = SOrderStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void FinishOrder(SOrderBindingModel model)
        {
            SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)

            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = SOrderStatus.Готов;
            context.SaveChanges();
        }
        public void PayOrder(SOrderBindingModel model)
        {
            SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = SOrderStatus.Оплачен;
            context.SaveChanges();
        }
        public void PutIngridientsOnStock(StockIngridientsBindingModel model)
        {
            StockIngridients element = context.StockIngridientss.FirstOrDefault(rec => rec.SStockId == model.SStockId && rec.IngridientsId == model.IngridientsId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StockIngridientss.Add(new StockIngridients
                {
                    SStockId = model.SStockId,
                    IngridientsId = model.IngridientsId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}