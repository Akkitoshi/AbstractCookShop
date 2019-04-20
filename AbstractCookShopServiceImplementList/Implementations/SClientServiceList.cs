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
    public class ClientServiceList : ISClientService
    {
        private DataListSingleton source;
        public ClientServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SClientViewModel> GetList()
        {
            List<SClientViewModel> result = source.SClients.Select(rec => new SClientViewModel
            {
                Id = rec.Id,
                SClientFIO = rec.SClientFIO
            })
            .ToList();
            return result;
        }
        public SClientViewModel GetElement(int id)
        {
            SClient element = source.SClients.FirstOrDefault(rec => rec.Id == id);
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
            SClient element = source.SClients.FirstOrDefault(rec => rec.SClientFIO ==
           model.SClientFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.SClients.Count > 0 ? source.SClients.Max(rec => rec.Id) : 0;
            source.SClients.Add(new SClient
            {
                Id = maxId + 1,
                SClientFIO = model.SClientFIO
            });
        }
        public void UpdElement(SClientBindingModel model)
        {
            SClient element = source.SClients.FirstOrDefault(rec => rec.SClientFIO ==
           model.SClientFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.SClients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SClientFIO = model.SClientFIO;
        }
        public void DelElement(int id)
        {
            SClient element = source.SClients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.SClients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}