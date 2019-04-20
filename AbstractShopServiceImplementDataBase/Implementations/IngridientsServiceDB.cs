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
    public class IngridientsServiceDB : IIngridientsService
    {
        private AbstractCookShopDbContext context;
        public IngridientsServiceDB(AbstractCookShopDbContext context)
        {
            this.context = context;
        }
        public List<IngridientsViewModel> GetList()
        {
            List<IngridientsViewModel> result = context.Ingridientss.Select(rec => new
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
            Ingridients Ingridients = context.Ingridientss.FirstOrDefault(rec => rec.Id == id);
            if (Ingridients != null)
            {
                return new IngridientsViewModel
                {
                    Id = Ingridients.Id,
                    IngridientsName = Ingridients.IngridientsName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(IngridientsBindingModel model)
        {
            Ingridients Ingridients = context.Ingridientss.FirstOrDefault(rec => rec.IngridientsName ==
            model.IngridientsName);
            if (Ingridients != null)
            {
                throw new Exception("Уже есть Ингридиент с таким названием");
            }
            context.Ingridientss.Add(new Ingridients
            {
                IngridientsName = model.IngridientsName
            });
            context.SaveChanges();
        }
        public void UpdElement(IngridientsBindingModel model)
        {
            Ingridients Ingridients = context.Ingridientss.FirstOrDefault(rec => rec.IngridientsName ==
            model.IngridientsName && rec.Id != model.Id);
            if (Ingridients != null)
            {
                throw new Exception("Уже есть Ингридиент с таким названием");
            }
            Ingridients = context.Ingridientss.FirstOrDefault(rec => rec.Id == model.Id);
            if (Ingridients == null)
            {
                throw new Exception("Элемент не найден");
            }
            Ingridients.IngridientsName = model.IngridientsName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Ingridients Ingridients = context.Ingridientss.FirstOrDefault(rec => rec.Id == id);
            if (Ingridients != null)
            {
                context.Ingridientss.Remove(Ingridients);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}