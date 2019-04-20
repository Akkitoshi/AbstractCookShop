using AbstractCookShopServiceDAL.Interfaces;
using AbstractCookShopServiceImplementDataBase;
using AbstractCookShopView;
using AbstractShopServiceImplementDataBase.Implementations;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractCookShopView
{
    static class Program
    {/// <summary>
     /// Главная точка входа для приложения.
     /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractCookShopDbContext>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISClientService, SClientServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngridientsService, IngridientsServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICookService, CookServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISStockService, SStockServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
