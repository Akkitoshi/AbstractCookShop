using AbstractCookShopModel;
using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.Interfaces;
using AbstractCookShopServiceDAL.ViewModels;
using AbstractCookShopServiceImplement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCookShopServiceImplementList.Implementations
{
    public class IngridientsServiceList : IIngridientsService
    {
        private DataListSingleton source;
        public IngridientsServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<IngridientsViewModel> GetList()
        {
            List<IngridientsViewModel> result = source.Ingridientss.Select(rec => new
           IngridientsViewModel
            {
                Id = rec.Id,
                IngridientsName = rec.IngridientsName
            })
            .ToList();
            return result;
        }
        public IngridientsViewModel GetElement(int id)
        {
            Ingridients element = source.Ingridientss.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new IngridientsViewModel
                {
                    Id = element.Id,
                    IngridientsName = element.IngridientsName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(IngridientsBindingModel model)
        {
            Ingridients element = source.Ingridientss.FirstOrDefault(rec => rec.IngridientsName
           == model.IngridientsName);
            if (element != null)
            {
                throw new Exception("Уже есть Ингридиент с таким названием");
            }
            int maxId = source.Ingridientss.Count > 0 ? source.Ingridientss.Max(rec =>
           rec.Id) : 0;
            source.Ingridientss.Add(new Ingridients
            {
                Id = maxId + 1,
                IngridientsName = model.IngridientsName
            });
        }
        public void UpdElement(IngridientsBindingModel model)
        {
            Ingridients element = source.Ingridientss.FirstOrDefault(rec => rec.IngridientsName
           == model.IngridientsName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть Ингридиент с таким названием");
            }
            element = source.Ingridientss.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.IngridientsName = model.IngridientsName;
        }
        public void DelElement(int id)
        {
            Ingridients element = source.Ingridientss.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Ingridientss.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}