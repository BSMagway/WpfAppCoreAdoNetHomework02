using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAdoNetHomework02.Model
{
    public class CompactDiscOnWarehouse
    {
        public int Id { get; set; }
        
        public int CompactDiscId { get; set; }
        public CompactDisc CompactDisc { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public CompactDiscOnWarehouse()
        {

        }

        public CompactDiscOnWarehouse(int compactDiscId, int quantity, double price)
        {
            CompactDiscId = compactDiscId;
            Quantity = quantity;
            Price = price;
        }
    }
}
