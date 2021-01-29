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
                cboSort.ItemsSource = Enum.GetValues(typeof(MyEnums.SortEnum));
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
                        (from cc in Globals.AuctionContext.ClientsContacts.Include("ProductsForSellByClient")
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
        
        private void SortTheList(string sortBy)
        {
            if (sortBy == "Last Name")
            {
                List<string> sortingString = new List<string>();
                List<StartWindow> sortingStartWindows = new List<StartWindow>();
                foreach (StartWindow sw in _startWindowValues)
                {
                    sortingStartWindows.Add(sw);
                }
                
                foreach (StartWindow tempStartWindow in _startWindowValues)
                {
                    sortingString.Add(tempStartWindow.LastName);
                }
                sortingString.Sort();
                _startWindowValues.Clear();
                foreach (string str in sortingString)
                {
                    foreach (StartWindow sort in sortingStartWindows)
                    {
                        if (sort.LastName == str)
                        {
                            _startWindowValues.Add(sort);
                            break;
                        }
                    }
                }
            }
            else if (sortBy == "First Name")
            {
                List<string> sortingString = new List<string>();
                List<StartWindow> sortingStartWindows = new List<StartWindow>();
                foreach (StartWindow sw in _startWindowValues)
                {
                    sortingStartWindows.Add(sw);
                }
                foreach (StartWindow tempStartWindow in _startWindowValues)
                {
                    sortingString.Add(tempStartWindow.FirstName);
                }
                _startWindowValues.Clear();
                sortingString.Sort();

                foreach (string str in sortingString)
                {
                    foreach (StartWindow sort in sortingStartWindows)
                    {
                        if (sort.FirstName == str)
                        {
                            _startWindowValues.Add(sort);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int p = 0; p <= _startWindowValues.Count - 2; p++)
                {
                    for (int i = 0; i <= _startWindowValues.Count - 2; i++)
                    {
                        if (_startWindowValues[i].Id > _startWindowValues[i + 1].Id)
                        {
                            StartWindow Temp = new StartWindow();
                            Temp.CopyOf(_startWindowValues[i + 1]);
                            _startWindowValues[i + 1].CopyOf(_startWindowValues[i]);
                            _startWindowValues[i].CopyOf(Temp);
                        }
                    }
                }
            }

        }

        private void CboSort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSort.SelectedIndex != -1)
            {
                switch ((MyEnums.SortEnum)cboSort.SelectedItem)
                {
                    case MyEnums.SortEnum.ClientNumber:
                        SortTheList("Client Number");
                        break;
                    case MyEnums.SortEnum.FirstName:
                        SortTheList("First Name");
                        break;
                    case MyEnums.SortEnum.LastName:
                        SortTheList("Last Name");
                        break;
                }
                lvClientInfo.ItemsSource = _startWindowValues;
                lvClientInfo.Items.Refresh();
            }

        }


        //private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        //{
        //    if (expr)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
