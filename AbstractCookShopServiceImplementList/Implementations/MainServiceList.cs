using AbstractCookShopModel;
using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.Interfaces;
using AbstractCookShopServiceDAL.ViewModels;
using AbstractCookShopServiceImplement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractShopServiceImplementList.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SOrderViewModel> GetList()
        {
            List<SOrderViewModel> result = source.SOrders
            .Select(rec => new SOrderViewModel
            {
                Id = rec.Id,
                SClientId = rec.SClientId,
                CookId = rec.CookId,
                DateCreate = rec.DateCreate.ToLongDateString(),
                DateImplement = rec.DateImplement?.ToLongDateString(),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                SClientFIO = source.SClients.FirstOrDefault(recC => recC.Id ==
     rec.SClientId)?.SClientFIO,
                CookName = source.Cooks.FirstOrDefault(recP => recP.Id ==
    rec.CookId)?.CookName,
            })
            .ToList();
            return result;
        }
        public void CreateOrder(SOrderBindingModel model)
        {
            int maxId = source.SOrders.Count > 0 ? source.SOrders.Max(rec => rec.Id) : 0;
            source.SOrders.Add(new SOrder
            {
                Id = maxId + 1,
                SClientId = model.SClientId,
                CookId = model.CookId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = SOrderStatus.Принят
            });
        }
        public void TakeOrderInWork(SOrderBindingModel model)
        {
            SOrder element = source.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству компонентов на складах
            var CookIngridientss = source.CookIngridientss.Where(rec => rec.CookId
           == element.CookId);
            foreach (var CookIngridients in CookIngridientss)
            {
                int countOnStocks = source.StockIngridientss
                .Where(rec => rec.IngridientsId ==
               CookIngridients.IngridientsId)
               .Sum(rec => rec.Count);
                if (countOnStocks < CookIngridients.Count * element.Count)
                {
                    var IngridientsName = source.Ingridientss.FirstOrDefault(rec => rec.Id ==
                   CookIngridients.IngridientsId);
                    throw new Exception("Не достаточно Ингридиента " +
                   IngridientsName?.IngridientsName + " требуется " + (CookIngridients.Count * element.Count) +
                   ", в наличии " + countOnStocks);
                }
            }
            // списываем
            foreach (var CookIngridients in CookIngridientss)
            {
                int countOnStocks = CookIngridients.Count * element.Count;
                var stockIngridientss = source.StockIngridientss.Where(rec => rec.IngridientsId
               == CookIngridients.IngridientsId);
                foreach (var stockIngridients in stockIngridientss)
                {
                    // Ингридиентов на одном слкаде может не хватать
                    if (stockIngridients.Count >= countOnStocks)
                    {
                        stockIngridients.Count -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= stockIngridients.Count;
                        stockIngridients.Count = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.Status = SOrderStatus.Выполняется;
        }

        public void FinishOrder(SOrderBindingModel model)
        {
            SOrder element = source.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = SOrderStatus.Готов;
        }

        public void PayOrder(SOrderBindingModel model)
        {
            SOrder element = source.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = SOrderStatus.Оплачен;
        }
        public void PutIngridientsOnStock(StockIngridientsBindingModel model)
        {
            StockIngridients element = source.StockIngridientss.FirstOrDefault(rec =>
           rec.SStockId == model.SStockId && rec.IngridientsId == model.IngridientsId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StockIngridientss.Count > 0 ?
               source.StockIngridientss.Max(rec => rec.Id) : 0;
                source.StockIngridientss.Add(new StockIngridients
                {
                    Id = ++maxId,
                    SStockId = model.SStockId,
                    IngridientsId = model.IngridientsId,
                    Count = model.Count
                });
            }
        }
    }
}