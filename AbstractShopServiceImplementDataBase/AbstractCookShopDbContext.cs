using AbstractCookShopModel;
using System.Data.Entity;

namespace AbstractCookShopServiceImplementDataBase
{
    public class AbstractCookShopDbContext : DbContext
    {
        public AbstractCookShopDbContext() : base("AbstractCookShopDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
           System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<SClient> Clients { get; set; }
        public virtual DbSet<Ingridients> Ingridientss { get; set; }
        public virtual DbSet<SOrder> SOrders { get; set; }
        public virtual DbSet<Cook> Cooks { get; set; }
        public virtual DbSet<CookIngridients> CookIngridientss { get; set; }
        public virtual DbSet<SStock> Stocks { get; set; }
        public virtual DbSet<StockIngridients> StockIngridientss { get; set; }
    }
}