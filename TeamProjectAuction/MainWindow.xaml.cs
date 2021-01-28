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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TeamProjectAuction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<StartWindow> _startWindowValues = new List<StartWindow>();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.AuctionContext = new AuctionDbContext();
                OnStartWindowLoad();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void OnStartWindowLoad()
        {
            _startWindowValues = new List<StartWindow>();
            foreach (Client client in Globals.AuctionContext.Clients)
            {
                StartWindow Temp = new StartWindow();
                Temp.Id = client.ClientId;
                Temp.FirstName = client.ClientFirstName;
                Temp.LastName = client.ClientLastName;
                _startWindowValues.Add(Temp);
            }

            foreach (StartWindow startWindow in _startWindowValues)
            {
                if (startWindow.PhoneNumber == null)
                {
                    startWindow.PhoneNumber =
                        (from cc in Globals.AuctionContext.ClientsContacts
                            where cc.ClientId == startWindow.Id
                            select cc.ClientPhoneNumber).FirstOrDefault();
                }
            }
            //var Table = new List<StartWindow>(JoinTable);
            lvClientInfo.ItemsSource = _startWindowValues;
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            ClientInfo ci = new ClientInfo();
            bool? ReturnValue = ci.ShowDialog();
            if (ReturnValue == true)
            {
                OnStartWindowLoad();
                lvClientInfo.Items.Refresh();
            }
        }

        private void BtnDetail_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvClientInfo.SelectedIndex == -1)
            {
                MessageBox.Show("You have to choose a client!", "No client chosen warning");
                return;
            }

            StartWindow TargetItem = (StartWindow)lvClientInfo.SelectedItem;

            Client TargetClient =
                (from cl in Globals.AuctionContext.Clients where cl.ClientId == TargetItem.Id select cl)
                .FirstOrDefault<Client>();

            ClientInfo ci = new ClientInfo(TargetClient);
            bool? ReturnValue = ci.ShowDialog();
            if (ReturnValue == true)
            {
                OnStartWindowLoad();
                lvClientInfo.Items.Refresh();
            }
        }
    }
}
