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
    public class CookServiceList : ICookService
    {
        private DataListSingleton source;
        public CookServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<CookViewModel> GetList()
        {
            List<CookViewModel> result = source.Cooks
            .Select(rec => new CookViewModel
            {
                Id = rec.Id,
                CookName = rec.CookName,
                Price = rec.Price,
                CookIngridients = source.CookIngridientss.Where(recPC => recPC.CookId == rec.Id)
                .Select(recPC => new CookIngridientsViewModel
                {
                    Id = recPC.Id,
                    CookId = recPC.CookId,
                    IngridientsId = recPC.IngridientsId,
                    IngridientsName = source.Ingridientss.FirstOrDefault(recC => recC.Id == recPC.IngridientsId)?.IngridientsName,
                    Count = recPC.Count
                })
                .ToList()
            })
            .ToList();
            return result;
        }
        public CookViewModel GetElement(int id)
        {
            Cook element = source.Cooks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CookViewModel
                {
                    Id = element.Id,
                    CookName = element.CookName,
                    Price = element.Price,
                    CookIngridients = source.CookIngridientss
                .Where(recPC => recPC.CookId == element.Id)
                .Select(recPC => new CookIngridientsViewModel
                {
                    Id = recPC.Id,
                    CookId = recPC.CookId,
                    IngridientsId = recPC.IngridientsId,
                    IngridientsName = source.Ingridientss.FirstOrDefault(recC =>
 recC.Id == recPC.IngridientsId)?.IngridientsName,
                    Count = recPC.Count
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(CookBindingModel model)
        {
            Cook element = source.Cooks.FirstOrDefault(rec => rec.CookName ==
           model.CookName);
            if (element != null)
            {
                throw new Exception("Уже есть Блюдо с таким названием");
            }
            int maxId = source.Cooks.Count > 0 ? source.Cooks.Max(rec => rec.Id) :
           0;
            source.Cooks.Add(new Cook
            {
                Id = maxId + 1,
                CookName = model.CookName,
                Price = model.Price
            });
            // ингридиенты для Блюда
            int maxPCId = source.CookIngridientss.Count > 0 ?
           source.CookIngridientss.Max(rec => rec.Id) : 0;
            // убираем дубли по Ингридиентами
            var groupIngridientss = model.CookIngridientss
            .GroupBy(rec => rec.IngridientsId)
           .Select(rec => new
           {
               IngridientsId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            // добавляем компоненты
            foreach (var groupIngridients in groupIngridientss)
            {
                source.CookIngridientss.Add(new CookIngridients
                {
                    Id = ++maxPCId,
                    CookId = maxId + 1,
                    IngridientsId = groupIngridients.IngridientsId,
                    Count = groupIngridients.Count
                });
            }
        }
        public void UpdElement(CookBindingModel model)
        {
            Cook element = source.Cooks.FirstOrDefault(rec => rec.CookName ==
           model.CookName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть Блюдо с таким названием");
            }
            element = source.Cooks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CookName = model.CookName;
            element.Price = model.Price;
            int maxPCId = source.CookIngridientss.Count > 0 ?
           source.CookIngridientss.Max(rec => rec.Id) : 0;
            // обновляем существуюущие ингридиенты
            var matIds = model.CookIngridientss.Select(rec =>
           rec.IngridientsId).Distinct();
            var updateIngridientss = source.CookIngridientss.Where(rec => rec.CookId ==
           model.Id && matIds.Contains(rec.IngridientsId));
            foreach (var updateIngridients in updateIngridientss)
            {
                updateIngridients.Count = model.CookIngridientss.FirstOrDefault(rec =>
               rec.Id == updateIngridients.Id).Count;
            }
            source.CookIngridientss.RemoveAll(rec => rec.CookId == model.Id &&
           !matIds.Contains(rec.IngridientsId));
            // новые записи
            var groupIngridientss = model.CookIngridientss
            .Where(rec => rec.Id == 0)
           .GroupBy(rec => rec.IngridientsId)
           .Select(rec => new
           {
               IngridientsId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            foreach (var groupIngridients in groupIngridientss)
            {
                CookIngridients elementPC = source.CookIngridientss.FirstOrDefault(rec
               => rec.CookId == model.Id && rec.IngridientsId == groupIngridients.IngridientsId);
                if (elementPC != null)
                {
                    elementPC.Count += groupIngridients.Count;
                }
                else
                {
                    source.CookIngridientss.Add(new CookIngridients
                    {
                        Id = ++maxPCId,
                        CookId = model.Id,
                        IngridientsId = groupIngridients.IngridientsId,
                        Count = groupIngridients.Count
                    });
                }
            }
        }
        public void DelElement(int id)
        {
            Cook element = source.Cooks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по Ингридиентам при удалении Блюда
                source.CookIngridientss.RemoveAll(rec => rec.CookId == id);
                source.Cooks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}