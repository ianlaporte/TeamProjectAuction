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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {

        byte[] productImage;

        public ProductWindow()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            txtOwnerId.Text = "";
            txtOwnerName.Text = "";
            txtLotId.Text = "";
            txtCategoryId.Text = "";
            txtProductId.Text = "";
            txtOwnerName.Text = "";
            txtProductName.Text = "";
            txtProductDescription.Text = "";  // make enum ???
            txtProductStartPrice.Text = "";
            txtProductSoldPrice.Text = "";

            List<Product> productOwners = Globals.AuctionContext.Clients.DbSet<Product>();  //Correct this line
            lvProductOwners.ItemsSource = productOwners;
            lvProductOwners.Items.Refresh();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Validate the data
            if (!ValidateData())
            {
                MessageBox.Show("the fields are not set properly");
                return;
            }

        }

        public bool ValidateData()
        {
            return true;
        }

        private void lvProductOwners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnImageUpcoming_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddManyProducts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSeeNextProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteOneProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnManyProducts_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
