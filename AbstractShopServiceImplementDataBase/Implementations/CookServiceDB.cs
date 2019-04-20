using AbstractCookShopModel;
using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.Interfaces;
using AbstractCookShopServiceDAL.ViewModels;
using AbstractCookShopServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractShopServiceImplementDataBase.Implementations
{
    public class CookServiceDB : ICookService
    {
        private AbstractCookShopDbContext context;
        public CookServiceDB(AbstractCookShopDbContext context)
        {
            this.context = context;
        }
        public List<CookViewModel> GetList()
        {
            List<CookViewModel> result = context.Cooks.Select(rec => new
           CookViewModel
            {
                Id = rec.Id,
                CookName = rec.CookName,
                Price = rec.Price,
                CookIngridients = context.CookIngridientss
            .Where(recPC => recPC.CookId == rec.Id)
           .Select(recPC => new CookIngridientsViewModel
           {
               Id = recPC.Id,
               CookId = recPC.CookId,
               IngridientsId = recPC.IngridientsId,
               IngridientsName = recPC.Ingridients.IngridientsName,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public CookViewModel GetElement(int id)
        {
            Cook element = context.Cooks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CookViewModel
                {
                    Id = element.Id,
                    CookName = element.CookName,
                    Price = element.Price,
                    CookIngridients = context.CookIngridientss
    .Where(recPC => recPC.CookId == element.Id)
     .Select(recPC => new CookIngridientsViewModel
     {
         Id = recPC.Id,
         CookId = recPC.CookId,
         IngridientsId = recPC.IngridientsId,
         IngridientsName = recPC.Ingridients.IngridientsName,
         Count = recPC.Count
     })
    .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(CookBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Cook element = context.Cooks.FirstOrDefault(rec =>
                   rec.CookName == model.CookName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть Блюдо с таким названием");
                    }
                    element = new Cook
                    {
                        CookName = model.CookName,
                        Price = model.Price
                    };
                    context.Cooks.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.CookIngridientss
                     .GroupBy(rec => rec.IngridientsId)
                    .Select(rec => new
                    {
                        IngridientsId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.CookIngridientss.Add(new CookIngridients
                        {
                            CookId = element.Id,
                            IngridientsId = groupComponent.IngridientsId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(CookBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Cook element = context.Cooks.FirstOrDefault(rec =>
                   rec.CookName == model.CookName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть Блюдо с таким названием");
                    }
                    element = context.Cooks.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.CookName = model.CookName;
                    element.Price = model.Price;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.CookIngridientss.Select(rec =>
                   rec.IngridientsId).Distinct();
                    var updateComponents = context.CookIngridientss.Where(rec =>
                   rec.CookId == model.Id && compIds.Contains(rec.IngridientsId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count =
                       model.CookIngridientss.FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.CookIngridientss.RemoveRange(context.CookIngridientss.Where(rec =>
                    rec.CookId == model.Id && !compIds.Contains(rec.IngridientsId)));
                    context.SaveChanges();
                    // новые записи
                    var groupComponents = model.CookIngridientss
                    .Where(rec => rec.Id == 0)
                   .GroupBy(rec => rec.IngridientsId)
                   .Select(rec => new
                   {
                       IngridientsId = rec.Key,
                       Count = rec.Sum(r => r.Count)
                   });
                    foreach (var groupComponent in groupComponents)
                    {
                        CookIngridients elementPC =
                       context.CookIngridientss.FirstOrDefault(rec => rec.CookId == model.Id &&
                       rec.IngridientsId == groupComponent.IngridientsId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.CookIngridientss.Add(new CookIngridients
                            {
                                CookId = model.Id,
                                IngridientsId = groupComponent.IngridientsId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Cook element = context.Cooks.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.CookIngridientss.RemoveRange(context.CookIngridientss.Where(rec =>
                        rec.CookId == id));
                        context.Cooks.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
