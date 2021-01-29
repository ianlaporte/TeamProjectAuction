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
    /// Interaction logic for LotWindow.xaml
    /// </summary>
    public partial class LotWindow : Window
    {
        public LotWindow()
        {
            InitializeComponent();
        }

        private void TxtLotNumber_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int lotNumber;
                if (!Int32.TryParse(txtLotNumber.Text, out lotNumber))
                {
                    MessageBox.Show("Lot number invalid!", "Lot number invalid warning");
                    return;
                }

                LotType TargetLot = (from l in Globals.AuctionContext.Lots where l.LotId == lotNumber select l)
                    .FirstOrDefault();
                if (TargetLot.Products.Count != 0)
                {
                    lvLotDetails.ItemsSource = TargetLot.Products;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        //private void LoadLotWindow()
        //{

        //}
    }
}