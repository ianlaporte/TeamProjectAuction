using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace TeamProjectAuction
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public string filename;
        private byte[] productImage;
        private Client _targetClient = new Client();
        private HashSet<Product> ProductWindowProducts = new HashSet<Product>();
        private Random rand = new Random();

        public ProductWindow()
        {
            InitializeComponent();
        }

        public ProductWindow(Client ClientFromRegistration)
        {
            _targetClient = ClientFromRegistration;
            InitializeComponent();
            cbCategory.ItemsSource = Enum.GetValues(typeof(MyEnums.ProductCategory));
            LoadProductWindow();
        }

        public void LoadProductWindow()
        {
            txtOwnerId.Text = _targetClient.ClientId.ToString();
            txtOwnerName.Text = _targetClient.ClientName;

            //foreach (Product tempProduct in Globals.AuctionContext.Products)
            //{
            //    if (tempProduct.ClientId == _targetClient.ClientId)
            //    {
            //        ProductWindowProducts.Add(tempProduct);
            //    }
            //}

            //lvProductOwners.ItemsSource = ProductWindowProducts;
            lvProductOwners.ItemsSource = Globals.AuctionContext.Products.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int lotId = rand.Next(1, 15);
            int OwnerId = -1;

            if (!Int32.TryParse(txtOwnerId.Text, out OwnerId))
            {
                MessageBox.Show("Owner Id entered invalid.", "Owner Id warning");
                return;
            }

            LotType targetLot =
                (from l in Globals.AuctionContext.Lots where l.LotId == lotId select l).FirstOrDefault();

            if (targetLot == null)
            {
                MessageBox.Show("Adding failed, cannot find a proper lot.", "Lot not found warning");
                return;
            }

            Product aProduct = new Product
            {
                CategoryName = (MyEnums.ProductCategory) cbCategory.SelectedItem,
                ProductName = txtProductName.Text,
                ProductDescription = txtProductDescription.Text,
                ProductStartPrice = double.Parse(txtProductStartPrice.Text),
                LotTypeId = targetLot.LotId,
                LotType = targetLot
            };

            aProduct.ProductImage = productImage;
            aProduct.ClientId = OwnerId;

            Client TargetClient = (from c in Globals.AuctionContext.Clients
                where c.ClientId == OwnerId
                select c).FirstOrDefault();

            aProduct.Client = TargetClient;

            Globals.AuctionContext.Products.Add(aProduct);
            Globals.AuctionContext.SaveChanges();

            LoadProductWindow();
            lvProductOwners.Items.Refresh();
            ClearFields();
        }

        private void btnImageUpcoming_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    productImage = File.ReadAllBytes(dlg.FileName);
                    tbImageUpcoming.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = Utils.ByteArrayToBitmapImage(productImage);
                    imageViewerUpcoming.Source = bitmap;

                }
                catch (Exception ex) when (ex is SystemException || ex is IOException)
                {
                    MessageBox.Show(ex.Message, "File reading failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private void BtnClearFields_OnClick(object sender, RoutedEventArgs e)
        {
            LoadProductWindow();
            ClearFields();
        }

        private void ClearFields()
        {
            txtLotId.Text = "";
            cbCategory.SelectedIndex = -1;
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtProductDescription.Text = "";
            txtProductStartPrice.Text = "";
            txtProductSoldPrice.Text = "";
            txtLotId.IsEnabled = false;
            txtProductId.IsEnabled = false;
            chbLotId.IsChecked = false;
            chbProductId.IsChecked = false;
            lvProductOwners.UnselectAll();
            imageViewerUpcoming.Source = null;
            tbImageUpcoming.Visibility = Visibility.Visible;
        }

        private void ChbLotId_OnClick(object sender, RoutedEventArgs e)
        {
            if (chbLotId.IsChecked == true)
            {
                txtLotId.IsEnabled = true;
            }
            else
            {
                txtLotId.IsEnabled = false;
            }
        }

        private void ChbProductId_OnClick(object sender, RoutedEventArgs e)
        {

            if (chbProductId.IsEnabled == true)
            {
                txtProductId.IsEnabled = true;
            }
            else
            {
                txtProductId.IsEnabled = false;
            }
        }

        private void LvProductOwners_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProductOwners.SelectedIndex == -1)
            {
                ClearFields();
            }
            else
            {
                btnUpdateProduct.IsEnabled = true;
                btnDeleteProduct.IsEnabled = true;
                Product TargetProduct = (Product) lvProductOwners.SelectedItem;
                txtLotId.Text = TargetProduct.LotTypeId.ToString();
                txtProductId.Text = TargetProduct.ProductId.ToString();
                txtProductDescription.Text = TargetProduct.ProductDescription;
                int selectedCategoryEnum = (int) TargetProduct.CategoryName;
                cbCategory.SelectedIndex = selectedCategoryEnum;
                txtProductStartPrice.Text = TargetProduct.ProductStartPrice.ToString();
                txtProductSoldPrice.Text = TargetProduct.ProductSoldPrice.ToString();
                txtProductName.Text = TargetProduct.ProductName;
                BitmapImage bitmap = Utils.ByteArrayToBitmapImage(TargetProduct.ProductImage);
                imageViewerUpcoming.Source = bitmap;
                tbImageUpcoming.Visibility = Visibility.Hidden;
            }
        }

        private void BtnUpdateProduct_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvProductOwners.SelectedIndex == -1)
            {
                ClearFields();
            }
            else
            {
                Product TargetProduct = (Product) lvProductOwners.SelectedItem;
                TargetProduct.ProductImage = productImage;
                TargetProduct.CategoryName = (MyEnums.ProductCategory) cbCategory.SelectedItem;
                TargetProduct.ProductName = txtProductName.Text;
                TargetProduct.ProductDescription = txtProductDescription.Text;
                TargetProduct.ProductStartPrice = double.Parse(txtProductStartPrice.Text);
                TargetProduct.ProductSoldPrice = double.Parse(txtProductSoldPrice.Text);
                Globals.AuctionContext.SaveChanges();
                ClearFields();
                lvProductOwners.Items.Refresh();
                MessageBox.Show("Updated!", "Update Message");
            }
        }

        private void BtnDeleteProduct_OnClick(object sender, RoutedEventArgs e)
        {
            Product TargetProduct = (Product)lvProductOwners.SelectedItem;
            Globals.AuctionContext.Products.Remove(TargetProduct);
            Globals.AuctionContext.SaveChanges();
            LoadProductWindow();
            lvProductOwners.Items.Refresh();
        }

        private void BtnSearchProduct_OnClick(object sender, RoutedEventArgs e)
        {
            if (chbLotId.IsChecked != true && chbProductId.IsChecked != true)
            {
                MessageBox.Show("Select at least 1 search entity to search.");
                return;
            }
            int lotNumber;
            int productNumber;
            List<Product> TargetProductList = new List<Product>();
            if (chbLotId.IsChecked == true && chbProductId.IsChecked == true)
            {
                if (txtLotId.Text.Length == 0 || txtProductId.Text.Length == 0)
                {
                    MessageBox.Show("Lot Id and product id cannot be empty!", "Search Field Empty!");
                    return;
                }
                if (!Int32.TryParse(txtLotId.Text, out lotNumber))
                {
                    MessageBox.Show("Lot id parsing failed!");
                    return;
                }
                if (!Int32.TryParse(txtProductId.Text, out productNumber))
                {
                    MessageBox.Show("Product id parsing failed!");
                    return;
                }

                foreach (Product tempProduct in Globals.AuctionContext.Products)
                {
                    if (tempProduct.ProductId == productNumber && tempProduct.LotTypeId == lotNumber)
                    {
                        TargetProductList.Add(tempProduct);
                    }
                }
                lvProductOwners.ItemsSource = TargetProductList;
                lvProductOwners.Items.Refresh();
            }
            else if (chbLotId.IsChecked == true)
            {
                if (txtLotId.Text.Length == 0)
                {
                    MessageBox.Show("Lot Id cannot be empty! Or un-select lot id checkbox!", "Search Field Empty!");
                    return;
                }

                if (! Int32.TryParse(txtLotId.Text, out lotNumber))
                {
                    MessageBox.Show("Lot id parsing failed!");
                    return;
                }

                foreach (Product tempProduct in Globals.AuctionContext.Products)
                {
                    if (tempProduct.LotTypeId == lotNumber)
                    {
                        TargetProductList.Add(tempProduct);
                    }
                }

                lvProductOwners.ItemsSource = TargetProductList;
                lvProductOwners.Items.Refresh();
            }
            else if (chbProductId.IsChecked == true)
            {
                if (txtProductId.Text.Length == 0)
                {
                    MessageBox.Show("Lot Id cannot be empty! Or un-select product id checkbox!", "Search Field Empty!");
                    return;
                }

                if (!Int32.TryParse(txtProductId.Text, out productNumber))
                {
                    MessageBox.Show("Product id parsing failed!");
                    return;
                }

                foreach (Product tempProduct in Globals.AuctionContext.Products)
                {
                    if (tempProduct.ProductId == productNumber)
                    {
                        TargetProductList.Add(tempProduct);
                    }
                }

                lvProductOwners.ItemsSource = TargetProductList;
                lvProductOwners.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Search Failed!");
            }
            ClearFields();
        }
    }
}
