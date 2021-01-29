using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public class LotType
    {
        [Key] public int LotId { get; set; }
        public string LotName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
