using AbstractCookShopServiceDAL.BindingModels;
using AbstractCookShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractCookShopServiceDAL.Interfaces
{
    public interface IIngridientsService
    {
        List<IngridientsViewModel> GetList();
        IngridientsViewModel GetElement(int id);
        void AddElement(IngridientsBindingModel model);
        void UpdElement(IngridientsBindingModel model);
        void DelElement(int id);
    }
}
