using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractCookShopServiceDAL.Interfaces
{
    public interface ISStockService
    {
        List<SStockViewModel> GetList();
        SStockViewModel GetElement(int id);
        void AddElement(SStockBindingModel model);
        void UpdElement(SStockBindingModel model);
        void DelElement(int id);
    }
}
