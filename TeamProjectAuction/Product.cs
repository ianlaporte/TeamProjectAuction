using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }
        [StringLength(100)]
        public string ProductName { get; set; }
        [StringLength(100)]
        public MyEnums CategoryName { get; set; }
        [StringLength(255)]
        public string ProductDescription { get; set; }
        public byte[] ProductImage { get; set; }
        public double ProductStartPrice { get; set; }
        public virtual Client Client { set; get; }  // linked to Id of Client.ca, for or as FK to Client.cs (Clients_table in SQL)

        public override string ToString() // Not needed since we use a GridView instead of a ListView
        {
            return $"{ProductId}, {ProductName}";
        }
    }
}
