using AbstractCookShopModel;
using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.Interfaces;
using AbstractCookShopServiceDAL.ViewModel;
using AbstractCookShopServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceImplementDataBase.Implementations
{
    public class SStockServiceDB : ISStockService
    {
        private AbstractCookShopDbContext context;
        public SStockServiceDB(AbstractCookShopDbContext context)
        {
            this.context = context;
        }
        public List<SStockViewModel> GetList()
        {
            List<SStockViewModel> result = context.Stocks.Select(rec => new
           SStockViewModel
            {
                Id = rec.Id,
                SStockName = rec.SStockName
            })
            .ToList();
            return result;
        }
        public SStockViewModel GetElement(int id)
        {
            SStock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SStockViewModel
                {
                    Id = element.Id,
                    SStockName = element.SStockName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SStockBindingModel model)
        {
            SStock element = context.Stocks.FirstOrDefault(rec => rec.SStockName ==
           model.SStockName);
            if (element != null)
            {
                throw new Exception("Уже есть Склад с таким Названием");
            }
            context.Stocks.Add(new SStock
            {
                SStockName = model.SStockName
            });
            context.SaveChanges();
        }
        public void UpdElement(SStockBindingModel model)
        {
            SStock element = context.Stocks.FirstOrDefault(rec => rec.SStockName ==
           model.SStockName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SStockName = model.SStockName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            SStock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Stocks.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
