using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public class StartWindow
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber  { get; set; }

        public void CopyOf(StartWindow copyStartWindow)
        {
            this.Id = copyStartWindow.Id;
            this.FirstName = copyStartWindow.FirstName;
            this.LastName = copyStartWindow.LastName;
            this.PhoneNumber = copyStartWindow.PhoneNumber;
        }
    }
}
