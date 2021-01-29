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
        private int _lotCount;
        private string _clientName;
        [Key] public int ClientId { get; set; }
        [Required] public string ClientFirstName { get; set; }
        [Required] public string ClientLastName { get; set; }
        public MyEnums.SexEnum Sex { get; set; }
        [NotMapped]
        public string ClientName => _clientName = ClientFirstName + ClientLastName;

        [NotMapped] public int ClientTrackingId { get; set; }

        [NotMapped]
        public int LotCount
        {
            get => _lotCount;
            set => _lotCount = ProductsForSellByClient.Count;
        }
        public virtual ICollection<Product> ProductsForSellByClient { get; set; }

        public virtual ICollection<Product> Products { get; set; } // never used

        public virtual ClientContact ClientContact { get; set; }
        public virtual ClientAddress ClientAddress { get; set; }
        public virtual ClientPayment ClientPayment { get; set; }
    }
}