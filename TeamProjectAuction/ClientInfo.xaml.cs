using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TeamProjectAuction
{
    /// <summary>
    /// Interaction logic for ClientInfo.xaml
    /// </summary>
    public partial class ClientInfo : Window
    {
        private int _clientTrackingId = 0;
        public ClientInfo()
        {
            InitializeComponent();
            LoadNewClientWindow();
        }

        public ClientInfo(Client TargetClient)
        {
            InitializeComponent();
            
        }

        private void LoadNewClientWindow()
        {
            btnUpdate.IsEnabled = false;
            btnSave.IsEnabled = true;
            txtFirstName.IsEnabled = true;
            txtLastName.IsEnabled = true;
            rdoMale.IsEnabled = true;
            rdoFemale.IsEnabled = true;
            txtClientNumber.Text = "None";
            txtStreetNumber.IsEnabled = true;
            txtStreetName.IsEnabled = true;
            txtCity.IsEnabled = true;
            txtProvince.IsEnabled = true;
            txtCountry.IsEnabled = true;
            btnAddressUpdate.IsEnabled = false;
            btnAddressSave.IsEnabled = true;
            txtPostCode.IsEnabled = true;
            txtPhoneNumber.IsEnabled = true;
            txtEmail.IsEnabled = true;
            rdoDeposit.IsEnabled = true;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _clientTrackingId = Globals.AuctionContext.Clients.ToList().Count + 2;
                Client NewClient = new Client
                {
                    ClientFirstName = txtFirstName.Text,
                    ClientLastName = txtLastName.Text,
                    ClientTrackingId = _clientTrackingId
                };
                Globals.AuctionContext.Clients.Add(NewClient);
                Globals.AuctionContext.SaveChanges();

                bool IsFound = false;
                
                foreach (Client TempClient in Globals.AuctionContext.Clients.ToList())
                {
                    if (TempClient.ClientTrackingId == _clientTrackingId)
                    {
                        NewClient = TempClient;
                        IsFound = true;
                        break;
                    }
                }

                if (IsFound)
                {
                    ClientContact NewClientContact = new ClientContact
                    {
                        ClientPhoneNumber = txtPhoneNumber.Text,
                        ClientEmail = txtEmail.Text,
                        ClientId = NewClient.ClientId
                    };
                    Globals.AuctionContext.ClientsContacts.Add(NewClientContact);
                    Globals.AuctionContext.SaveChanges();
                }

                DialogResult = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void ExpAddress_OnExpanded(object sender, RoutedEventArgs e)
        {

        }

        private void LoadUpdateClientWindow(Client TargetClient)
        {
            ClientContact TargetClientContact = (from cc in Globals.AuctionContext.ClientsContacts
                where cc.ClientId == TargetClient.ClientId
                select cc).FirstOrDefault<ClientContact>();
            txtFirstName.Text = TargetClient.ClientFirstName;
            txtLastName.Text = TargetClient.ClientLastName;
            txtPhoneNumber.Text = TargetClientContact.ClientPhoneNumber;
            txtEmail.Text = TargetClientContact.ClientEmail;
            txtClientNumber.Text = TargetClient.ClientId.ToString();
            

        }
    }
}
