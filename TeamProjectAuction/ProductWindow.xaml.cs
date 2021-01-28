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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
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
        byte[] productImage;  // Is this needed

        public string filename;
        public ProductWindow()
        {
            InitializeComponent();

            cbCategory.ItemsSource = Enum.GetValues(typeof(MyEnums.ProductCategory));

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Product aProduct = new Product
            {
                CategoryName = (MyEnums.ProductCategory)cbCategory.SelectedItem,
                ProductName = txtProductName.Text,
                ProductDescription = txtProductDescription.Text,
                ProductStartPrice = double.Parse(txtProductStartPrice.Text)
            };
            //string owner_name = txtOwnerName.Text;
            //string product_name = txtProductName.Text;
            //string product_descr = txtProductDescription.Text;

            // ComboBox emumVariablesFromCb = cbCategory;
            //MyEnums.ProductCategory emumVariablesFromCb = (MyEnums.ProductCategory)cbCategory.SelectedItem;
            
            int product_start_price, product_sold_price;
            int.TryParse(txtProductStartPrice.Text, out product_start_price);
            int.TryParse(txtProductSoldPrice.Text, out product_sold_price);
            
            Stream stream = File.OpenRead(filename);
            byte[] product_image = new byte[stream.Length];
            stream.Read(product_image, 0, (int)stream.Length);
            
            tblProduct product = new tblProduct();
            product.tblOwner.OwnerName = owner_name;
            product.ProductName = product_name;
            product.ProductDescription = product_descr;
            product.ProductStartPrice = product_start_price;
            product.ProductSoldPrice = product_sold_price;
            product.ProductImage = product_image;
            ProjectAuctionEntities entities = new ProjectAuctionEntities();
            entities.tblProducts.Add(product);
            entities.SaveChanges();

            // ? Not needed? 
            // List<Product> productOwners = Globals.AuctionContext.Clients.DbSet<Product>();  //Correct this line
            // lvProductOwners.ItemsSource = productOwners;
            // lvProductOwners.Items.Refresh();

        }

        /*public bool ValidateData()
        {
            return true;
        }*/

        private void lvProductOwners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnImageUpcoming_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.Filter = "All Files (*.*)|*.*|JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg";
            if (fd.ShowDialog() == true)
            {
                imageViewerUpcoming.Source = new BitmapImage(new Uri(fd.FileName));
                filename = fd.FileName;
            }
        }

        private void btnAddManyProducts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            foreach (tblProduct x in lvProductOwners.Items)
            {
                int owner_id, lot_id, cat_id, prod_id;
                int.TryParse(txtOwnerId.Text, out owner_id);
                int.TryParse(txtCategoryId.Text, out cat_id);
                int.TryParse(txtLotId.Text, out lot_id);
                int.TryParse(txtProductId.Text, out prod_id);
                if (x.OwnerID == owner_id || x.ProductID == prod_id || x.CategoryID == cat_id)
                {
                    tblProduct found = x;
                    lvProductOwners.Items.Clear();
                    lvProductOwners.Items.Add(found);
                    break;
                }
            }
        }

        private void btnSeeNextProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            string owner_name = txtOwnerName.Text;
            string product_name = txtProductName.Text;
            string product_descr = txtProductDescription.Text;
            int product_start_price, product_sold_price;
            int.TryParse(txtProductStartPrice.Text, out product_start_price);
            int.TryParse(txtProductSoldPrice.Text, out product_sold_price);
            Stream stream = File.OpenRead(filename);
            byte[] product_image = new byte[stream.Length];
            stream.Read(product_image, 0, (int)stream.Length);
            tblProduct product = new tblProduct();
            product.tblOwner.OwnerName = owner_name;
            product.ProductName = product_name;
            product.ProductDescription = product_descr;
            product.ProductStartPrice = product_start_price;
            product.ProductSoldPrice = product_sold_price;
            product.ProductImage = product_image;
            lvProductOwners.SelectedItem = product;
            lvProductOwners.Items.Refresh();
            ProjectAuctionEntities ent = new ProjectAuctionEntities();
            foreach (tblProduct x in ent.tblProducts)
            {
                int owner_id, lot_id, cat_id, prod_id;
                int.TryParse(txtOwnerId.Text, out owner_id);
                int.TryParse(txtCategoryId.Text, out cat_id);
                int.TryParse(txtLotId.Text, out lot_id);
                int.TryParse(txtProductId.Text, out prod_id);
                if (x.OwnerID == owner_id || x.ProductID == prod_id || x.CategoryID == cat_id)
                {
                    x.tblOwner.OwnerName = owner_name;
                    x.ProductDescription = product_descr;
                    x.ProductStartPrice = product_start_price;
                    x.ProductSoldPrice = product_sold_price;
                    x.ProductImage = product_image;
                    break;
                }
            }
        }

        private void btnDeleteOneProduct_Click(object sender, RoutedEventArgs e)
        {
            lvProductOwners.Items.Remove(lvProductOwners.SelectedItem);
            lvProductOwners.Items.Refresh();
        }

        private void btnManyProducts_Click(object sender, RoutedEventArgs e)
        {
            lvProductOwners.Items.Remove(lvProductOwners.SelectedItems);
            lvProductOwners.Items.Refresh();
        }
    }
}
