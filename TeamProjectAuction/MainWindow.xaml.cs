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
        private static StartWindow _startWindowValues = new StartWindow();
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

            var JoinTable = from cl in Globals.AuctionContext.Clients
                join cc in Globals.AuctionContext.ClientsContacts
                    on cl.ClientId equals cc.ClientId
                select new
                {
                    ClientId = cc.ClientId, ClientFirstName = cl.ClientFirstName, ClientLastName = cl.ClientLastName,
                    ClientPhoneNumber = cc.ClientPhoneNumber
                };

            //var Table = new List<StartWindow>(JoinTable);
            lvClientInfo.ItemsSource = JoinTable.ToList();
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            ClientInfo ci = new ClientInfo();
            bool? ReturnValue = ci.ShowDialog();
            if (ReturnValue == true)
            {
                OnStartWindowLoad();
            }
        }

        private void BtnDetail_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvClientInfo.SelectedIndex == -1)
            {
                MessageBox.Show("You have to choose a client!", "No client chosen warning");
                return;
            }

            
            //StartWindow TargetItem = (StartWindow)lvClientInfo.SelectedItem ;

            //Client TargetClient =
            //    (from cl in Globals.AuctionContext.Clients where cl.ClientId == TargetItem.Id select cl)
            //    .FirstOrDefault<Client>();

            //ClientInfo ci = new ClientInfo(TargetClient);
            //bool? ReturnValue = ci.ShowDialog();
            //if (ReturnValue == true)
            //{
            //    OnStartWindowLoad();
            //}
        }
    }
}
