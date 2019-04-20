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
   public class SClientServiceDB : ISClientService
    {
        private AbstractCookShopDbContext context;
        public SClientServiceDB(AbstractCookShopDbContext context)
        {
            this.context = context;
        }
        public List<SClientViewModel> GetList()
        {
            List<SClientViewModel> result = context.Clients.Select(rec => new
           SClientViewModel
            {
                Id = rec.Id,
                SClientFIO = rec.SClientFIO
            })
            .ToList();
            return result;
        }
        public SClientViewModel GetElement(int id)
        {
            SClient element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SClientViewModel
                {
                    Id = element.Id,
                    SClientFIO = element.SClientFIO
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SClientBindingModel model)
        {
            SClient element = context.Clients.FirstOrDefault(rec => rec.SClientFIO ==
           model.SClientFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Clients.Add(new SClient
            {
                SClientFIO = model.SClientFIO
            });
            context.SaveChanges();
        }
        public void UpdElement(SClientBindingModel model)
        {
            SClient element = context.Clients.FirstOrDefault(rec => rec.SClientFIO ==
           model.SClientFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SClientFIO = model.SClientFIO;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            SClient element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
