using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class OrderItem
    {
        public int? id {  get; set; }
        public string? xuid { get; set; }
        public int? product_id { get; set; }
        public int? quantity { get; set; }
        public bool? delivered { get; set; }
        public string? date { get; set; }
    }
}
