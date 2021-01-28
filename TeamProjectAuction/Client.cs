using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public class Client
    {
        private string _clientName;
        [Key] public int ClientId { get; set; }
        [Required] public string ClientFirstName { get; set; }
        [Required] public string ClientLastName { get; set; }
        public MyEnums.SexEnum Sex { get; set; }
        [NotMapped]
        public string ClientName => _clientName = ClientFirstName + ClientLastName;

        [NotMapped] public int ClientTrackingId { get; set; }

        public int LotCount { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ClientContact ClientContact { get; set; }
        public virtual ClientAddress ClientAddress { get; set; }
        public virtual ClientPayment ClientPayment { get; set; }
    }
}
