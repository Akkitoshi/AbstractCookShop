using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractCookShopServiceDAL.Interfaces
{
    public interface ICookService
    {
        List<CookViewModel> GetList();
        CookViewModel GetElement(int id);
        void AddElement(CookBindingModel model);
        void UpdElement(CookBindingModel model);
        void DelElement(int id);
    }
}
