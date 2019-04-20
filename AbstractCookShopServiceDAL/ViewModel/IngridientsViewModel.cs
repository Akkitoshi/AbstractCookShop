using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractCookShopServiceDAL.ViewModels
{
    public class IngridientsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Ингридиент")]
        public string IngridientsName { get; set; }
    }
}
