using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public class ProductOwner
    {
        [Key]
        public string ProductId { get; set; }
       

        public virtual Client Client { get; set; }
    }
}
