using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractCookShopServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveCookPrice(ReportBindingModel model);
        List<StocksLoadViewModel> GetStocksLoad();
        void SaveStocksLoad(ReportBindingModel model);
        List<SClientOrdersModel> GetClientOrders(ReportBindingModel model);
        void SaveClientOrders(ReportBindingModel model);
    }
}
