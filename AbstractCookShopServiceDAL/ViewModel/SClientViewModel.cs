using System.ComponentModel;

namespace AbstractCookShopServiceDAL.ViewModels
{
    public class SClientViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО Клиента")]
        public string SClientFIO { get; set; }
    }
}
