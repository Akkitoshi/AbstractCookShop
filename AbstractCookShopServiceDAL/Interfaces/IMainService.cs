using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractCookShopServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<SOrderViewModel> GetList();
        void CreateOrder(SOrderBindingModel model);
        void TakeOrderInWork(SOrderBindingModel model);
        void FinishOrder(SOrderBindingModel model);
        void PayOrder(SOrderBindingModel model);
        void PutIngridientsOnStock(StockIngridientsBindingModel model);
    }
}