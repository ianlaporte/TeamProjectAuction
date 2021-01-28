using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public class ClientContact
    {
        [Key] [ForeignKey("Client")] public int ClientId { get; set; }
        [Required] public string ClientEmail { get; set; }
        [Required] public string ClientPhoneNumber { get; set; }
        public string ClientFaceBook { get; set; }

        public virtual Client Client { get; set; }
    }
}
