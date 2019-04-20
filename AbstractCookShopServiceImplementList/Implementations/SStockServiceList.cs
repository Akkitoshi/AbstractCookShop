using AbstractCookShopModel;
using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.Interfaces;
using AbstractCookShopServiceDAL.ViewModel;
using AbstractCookShopServiceImplement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractCookShopServiceImplementList.Implementations
{
    public class StockServiceList : ISStockService
    {
        private DataListSingleton source;
        public StockServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SStockViewModel> GetList()
        {
            List<SStockViewModel> result = source.Stocks
            .Select(rec => new SStockViewModel
            {
                Id = rec.Id,
                SStockName = rec.SStockName,
                StockIngridientss = source.StockIngridientss
                .Where(recPC => recPC.SStockId == rec.Id)
           .Select(recPC => new StockIngridientsViewModel
           {
               Id = recPC.Id,
               SStockId = recPC.SStockId,
               IngridientsId = recPC.IngridientsId,
               IngridientsName = source.Ingridientss
            .FirstOrDefault(recC => recC.Id ==
           recPC.IngridientsId)?.IngridientsName,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public SStockViewModel GetElement(int id)
        {
            SStock element = source.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SStockViewModel
                {
                    Id = element.Id,
                    SStockName = element.SStockName,
                    StockIngridientss = source.StockIngridientss
                .Where(recPC => recPC.SStockId == element.Id)
               .Select(recPC => new StockIngridientsViewModel
               {
                   Id = recPC.Id,
                   SStockId = recPC.SStockId,
                   IngridientsId = recPC.IngridientsId,
                   IngridientsName = source.Ingridientss
                .FirstOrDefault(recC => recC.Id ==
               recPC.IngridientsId)?.IngridientsName,
                   Count = recPC.Count
               })
               .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SStockBindingModel model)
        {
            SStock element = source.Stocks.FirstOrDefault(rec => rec.SStockName ==
           model.SStockName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Stocks.Count > 0 ? source.Stocks.Max(rec => rec.Id) : 0;
            source.Stocks.Add(new SStock
            {
                Id = maxId + 1,
                SStockName = model.SStockName
            });
        }
        public void UpdElement(SStockBindingModel model)
        {
            SStock element = source.Stocks.FirstOrDefault(rec =>
            rec.SStockName == model.SStockName && rec.Id !=
           model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SStockName = model.SStockName;
        }
        public void DelElement(int id)
        {
            SStock element = source.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // при удалении удаляем все записи о компонентах на удаляемом складе
                source.StockIngridientss.RemoveAll(rec => rec.SStockId == id);
                source.Stocks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
