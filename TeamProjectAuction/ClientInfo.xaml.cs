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
        private Client MyTargetClient = new Client();
        private bool IsNewClient = false;
        private ClientPayment MyClientPayment = new ClientPayment();
        public ClientInfo()
        {
            IsNewClient = true;
            InitializeComponent();
            LoadNewClientWindow();
        }

        public ClientInfo(Client TargetClient)
        {
            IsNewClient = false;
            MyTargetClient = TargetClient;
            InitializeComponent();
            LoadUpdateClientWindow(MyTargetClient);
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
            btnAddressSave.IsEnabled = false;
            txtPostCode.IsEnabled = true;
            txtPhoneNumber.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtFaceBook.IsEnabled = true;
            cboPreferredPayment.IsEnabled = true;
            cboPreferredPayment.ItemsSource = Enum.GetValues(typeof(MyEnums.PreferredPaymentType));
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsNewClient)
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
                            ClientFaceBook = txtFaceBook.Text,
                            ClientId = NewClient.ClientId
                        };
                        NewClient.ClientContact = NewClientContact;
                        // Globals.AuctionContext.ClientsContacts.Add(NewClientContact);


                        // TODO: Check Address Fields  
                        //if (txtStreetNumber.Text.Length == 0 ||
                        //    txtStreetName.Text.Length == 0 ||
                        //    txtCity.Text.Length == 0 ||
                        //    txtProvince.Text.Length == 0 ||
                        //    txtCountry.Text.Length == 0 ||
                        //    txtPostCode.Text.Length == 0)
                        //{
                        //    MessageBox.Show("Have to complete all fields.", "Field empty warning");
                        //    return;
                        //}

                        ClientAddress NewClientAddress = new ClientAddress
                        {
                            ClientStreetNumber = txtStreetNumber.Text,
                            ClientStreetName = txtStreetName.Text,
                            ClientCity = txtCity.Text,
                            ClientProvince = txtProvince.Text,
                            ClientCountry = txtCountry.Text,
                            ClientPostCode = txtPostCode.Text,
                            ClientId = NewClient.ClientId
                        };
                        NewClient.ClientAddress = NewClientAddress;
                        // Globals.AuctionContext.ClientsAddresses.Add(NewClientAddress);

                        ClientPayment newClientPayment = new ClientPayment
                        {
                            ClientDeposit = MyClientPayment.ClientDeposit,
                            ClientCheque = MyClientPayment.ClientCheque,
                            ClientCreditCardNumber = MyClientPayment.ClientCreditCardNumber,
                            ClientCreditCardExpireDate = MyClientPayment.ClientCreditCardExpireDate,
                            ClientCreditCardSecurityCode = MyClientPayment.ClientCreditCardSecurityCode,
                            ClientPreferredPaymentType = (MyEnums.PreferredPaymentType)cboPreferredPayment.SelectedItem,
                            ClientId = NewClient.ClientId
                        };
                        NewClient.ClientPayment = newClientPayment;
                        
                        Globals.AuctionContext.SaveChanges();
                    }
                }
                else
                {
                    MyTargetClient.ClientFirstName = txtFirstName.Text;
                    MyTargetClient.ClientLastName = txtLastName.Text;
                    if (rdoMale.IsChecked == true)
                    {
                        MyTargetClient.Sex = MyEnums.SexEnum.Male;
                    }
                    else if (rdoFemale.IsChecked == true)
                    {
                        MyTargetClient.Sex = MyEnums.SexEnum.Female;
                    }

                    MyTargetClient.ClientContact.ClientEmail = txtEmail.Text;
                    MyTargetClient.ClientContact.ClientPhoneNumber = txtPhoneNumber.Text;
                    MyTargetClient.ClientContact.ClientFaceBook = txtFaceBook.Text;

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


        private void LoadUpdateClientWindow(Client TargetClient)
        {
            cboPreferredPayment.ItemsSource = Enum.GetValues(typeof(MyEnums.PreferredPaymentType));
            try
            {
                // General Part
                txtFirstName.Text = TargetClient.ClientFirstName;
                txtLastName.Text = TargetClient.ClientLastName;
                txtClientNumber.Text = TargetClient.ClientId.ToString();
                switch (TargetClient.Sex)
                {
                    case MyEnums.SexEnum.Male:
                        rdoMale.IsChecked = true;
                        break;
                    case MyEnums.SexEnum.Female:
                        rdoFemale.IsChecked = true;
                        break;
                }
                
                // Contact Part
                ClientContact TargetClientContact = (from cc in Globals.AuctionContext.ClientsContacts
                    where cc.ClientId == TargetClient.ClientId
                    select cc).FirstOrDefault<ClientContact>();
                if (TargetClientContact != null)
                {
                    txtPhoneNumber.Text = TargetClientContact.ClientPhoneNumber;
                    txtEmail.Text = TargetClientContact.ClientEmail;
                    txtFaceBook.Text = TargetClientContact.ClientFaceBook;
                }

                // Address Part
                ClientAddress TargetClientAddress = (from ca in Globals.AuctionContext.ClientsAddresses
                    where ca.ClientId == TargetClient.ClientId
                    select ca).FirstOrDefault<ClientAddress>();
                if (TargetClientAddress != null)
                {
                    txtStreetNumber.Text = TargetClientAddress.ClientStreetNumber;
                    txtStreetName.Text = TargetClientAddress.ClientStreetName;
                    txtCity.Text = TargetClientAddress.ClientCity;
                    txtProvince.Text = TargetClientAddress.ClientProvince;
                    txtCountry.Text = TargetClientAddress.ClientCountry;
                    txtPostCode.Text = TargetClientAddress.ClientPostCode;
                }
                
                // Payment Part
                ClientPayment TargetClientPayment = (from cp in Globals.AuctionContext.ClientsPayments
                    where cp.ClientId == TargetClient.ClientId
                    select cp).FirstOrDefault<ClientPayment>();
                if (TargetClientPayment != null)
                {
                    MyClientPayment = TargetClientPayment;
                    int selectedIndex = (int) TargetClientPayment.ClientPreferredPaymentType;
                    cboPreferredPayment.SelectedIndex = selectedIndex;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void BtnAddressUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            txtStreetNumber.IsEnabled = true;
            txtStreetName.IsEnabled = true;
            txtCity.IsEnabled = true;
            txtProvince.IsEnabled = true;
            txtCountry.IsEnabled = true;
            btnAddressUpdate.IsEnabled = false;
            btnAddressSave.IsEnabled = true;
            txtPostCode.IsEnabled = true;
        }

        private void BtnAddressSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (txtStreetNumber.Text.Length == 0 ||
                txtStreetName.Text.Length == 0 ||
                txtCity.Text.Length == 0 ||
                txtProvince.Text.Length == 0 ||
                txtCountry.Text.Length == 0 ||
                txtPostCode.Text.Length == 0)
            {
                MessageBox.Show("Have to complete all fields.", "Field empty warning");
                return;
            }

            ClientAddress TargetClientAddress = (from ca in Globals.AuctionContext.ClientsAddresses
                where ca.ClientId == MyTargetClient.ClientId
                select ca).FirstOrDefault<ClientAddress>();

            TargetClientAddress.ClientStreetNumber = txtStreetNumber.Text;
            TargetClientAddress.ClientStreetName = txtStreetName.Text;
            TargetClientAddress.ClientCity = txtCity.Text;
            TargetClientAddress.ClientProvince = txtProvince.Text;
            TargetClientAddress.ClientCountry = txtCountry.Text;
            TargetClientAddress.ClientPostCode = txtPostCode.Text;
            Globals.AuctionContext.SaveChanges();
            MessageBox.Show("Address saved!", "Address saved");
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            btnUpdate.IsEnabled = false;
            btnSave.IsEnabled = true;
            txtFirstName.IsEnabled = true;
            txtLastName.IsEnabled = true;
            rdoMale.IsEnabled = true;
            rdoFemale.IsEnabled = true;
            txtPhoneNumber.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtFaceBook.IsEnabled = true;
            cboPreferredPayment.IsEnabled = true;
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnProduct_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsNewClient)
            {
                MessageBox.Show("You have to save a new client before adding selling products.", "New client warning");
                return;
            }

            ProductWindow pw = new ProductWindow(MyTargetClient);
            bool? ReturnValue = pw.ShowDialog();
        }

        private void BtnPaymentDetail_OnClick(object sender, RoutedEventArgs e)
        {
            PaymentDetailsWindow pdw = new PaymentDetailsWindow(MyClientPayment, IsNewClient);
            bool? returnValue = pdw.ShowDialog();
        }
    }
}
