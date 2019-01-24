using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; } // listinha de departamento pra poder criar a caixinha pra selecionar o departamento do vendedor
    }
}
