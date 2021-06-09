using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOOMINGTREES_WpfApp
{
    public partial class BloomingTrees
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }        
        public string ItemDescription { get; set; }
        public string ItemImagePath { get; set; }
        public int ItemQty { get; set; }
        public string ItemPrice { get; set; }
        public string ItemSupplier { get; set; }
        public DateTime ItemPurchaseDate { get; set; }
    }
}
