using System.Collections.Generic;
using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.ViewModels;

namespace AbstractCookShopServiceDAL.Interfaces
{
    public interface ISClientService
    {
        List<SClientViewModel> GetList();
        SClientViewModel GetElement(int id);
        void AddElement(SClientBindingModel model);
        void UpdElement(SClientBindingModel model);
        void DelElement(int id);
    }
}
