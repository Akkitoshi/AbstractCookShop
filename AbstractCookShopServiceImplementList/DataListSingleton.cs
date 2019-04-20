using AbstractCookShopModel;
using System.Collections.Generic;

namespace AbstractCookShopServiceImplement
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<SClient> SClients { get; set; }
        public List<Ingridients> Ingridientss { get; set; }
        public List<SOrder> SOrders { get; set; }
        public List<Cook> Cooks { get; set; }
        public List<CookIngridients> CookIngridientss { get; set; }
        public List<SStock> Stocks { get; set; }
        public List<StockIngridients> StockIngridientss { get; set; }

        private DataListSingleton()
        {
            SClients = new List<SClient>();
            Ingridientss = new List<Ingridients>();
            SOrders = new List<SOrder>();
            Cooks = new List<Cook>();
            CookIngridientss = new List<CookIngridients>();
            Stocks = new List<SStock>();
            StockIngridientss = new List<StockIngridients>();

        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
