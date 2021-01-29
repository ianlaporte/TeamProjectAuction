using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public class ClientPayment
    {
        [Key] [ForeignKey("Client")] public int ClientId { get; set; }

        // Shouldn't we have a field to "set" the "prefered method of payment?" like : "ClientsPayments" (see: DbSet<ClientPayment> )
        public MyEnums.PreferredPaymentType ClientPreferredPaymentType { get; set; }
        
        public double ClientDeposit { get; set; }
        public bool ClientCheque { get; set; }
        public string ClientCreditCardNumber { get; set; }
        public string ClientCreditCardExpireDate { get; set; }
        public int ClientCreditCardSecurityCode { get; set; }
        

        public virtual Client Client { get; set; }
    }
}
