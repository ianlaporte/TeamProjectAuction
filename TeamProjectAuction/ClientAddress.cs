using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public class ClientAddress
    {
        [Key] [ForeignKey("Client")] public int ClientId { get; set; }
        public int ClientStreetNumber { get; set; }
        public string ClientStreetName { get; set; }
        public string ClientCity { get; set; }
        public string ClientProvince { get; set; }
        public string ClientCountry { get; set; }
        public string ClientPostCode { get; set; }

        [Required] public virtual Client Client { get; set; }
    }
}
