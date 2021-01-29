using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [StringLength(100)]
        public string ProductName { get; set; }
        public MyEnums.ProductCategory CategoryName { get; set; }
        public string ProductDescription { get; set; }
        public byte[] ProductImage { get; set; }
        public double ProductStartPrice { get; set; }
        public double ProductSoldPrice { get; set; }
        
        public int ClientId { get; set; }
        public virtual Client Client { set; get; }

        public int LotTypeId { get; set; }
        public virtual LotType LotType { get; set; }
    }
}