using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapper_crud
{
    public class ProductDTO
    {
        public Product product { get; set; } = new Product();
        public Category category { get; set; } = new Category();
    }
}
