using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayerDemo2.Core.Entity
{
    class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescriptions { get; set; }
        public int CategoryID { get; set; }
        
        public Product() {
            this.ProductID = 0;
            this.ProductName = "";
            this.ProductDescriptions = "";
            this.CategoryID = 0;
        }
        public String ToString() => this.ProductName;
    }
}
