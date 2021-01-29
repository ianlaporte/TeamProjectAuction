using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectAuction
{
    public static class MyEnums
    {
        public enum PreferredPaymentType
        {
            Deposit, Cheque, CreditCard
        }

        public enum SexEnum { Male, Female }

        public enum SortEnum { ClientNumber, FirstName, LastName }

        public enum ProductCategory
        {
            Clothing, Accessories, Appliances, Antiques, Collectibles, Deals, Electronics,
            Entertainment, Family, Farming, Furniture, Hobbies, Garden, Sports, Vehicles, Other
        }

    }
}