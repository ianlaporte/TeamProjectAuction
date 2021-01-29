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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace TeamProjectAuction
{
    /// <summary>
    /// Interaction logic for Auction.xaml
    /// </summary>
    public partial class Auction : Window
    {
        // byte[] currOwnerImage;
        private byte[] ProductImage;

        public Auction()
        {
            InitializeComponent();
            try
            {
                Globals.AuctionContext = new AuctionDbContext();
                FetchRecord();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private int currentElement = 0;

        private void FetchRecord()
        {
            // Include means force eager loading- used for collection in one-to-many relationship
            lvAuctionList.ItemsSource = Globals.AuctionContext.Clients.Include("Products").ToList();
            // List<Product> tempProduct = Globals.AuctionContext.Products.ToList();
        }

        // On _SelectionChange = load single selection to "page 2 image" and load all text fields from that row in Grid view
        private void lvAuctionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lvAuctionList.SelectedIndex == -1)
            {
                ClearInputs();
                return;
            }

            if ((bool) lvAuctionList.SelectedItem == true)  // or use « SelectionMode="Single" »
            {
                // If single selection made; load all text fields with GridView row values AND display image on"page 2 image" .
            }
            if (lvAuctionList.SelectedItems.Contains(true))
            {
                ClearInputs();
                return;
            }
           
        }

        
    public void ClearInputs()
        {
            txtAuctionedProductOwnerId.Text = "";
            txtAuctionedProductOwnerName.Text = "";
            txtLotId.Text = "";
            cbCategory.Text = ""; // even if it's a ComboBox? Not « cbCategory.ItemsSource = ""; »
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtProductDescription.Text = "";
            txtProductStartPrice.Text = "";
            txtProductSoldPrice.Text = "";
            txtCurrentBidAmount.Text = "";
            txtCurrentBidderClientId.Text = "";
            
            imageViewer.Source = null;
            
            btnSold.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnCurrent.IsEnabled = false;
            btnNext.IsEnabled = false;
            btnLeft.IsEnabled = false;
            btnRight.IsEnabled = false;
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (currentElement < 2)
            {
                currentElement++;
                AnimateCarousel();
            }
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (currentElement > 0)
            {
                currentElement--;
                AnimateCarousel();
            }
        }

        private void AnimateCarousel()
        {
            Storyboard storyboard = (this.Resources["CarouselStoryboard"] as Storyboard);
            DoubleAnimation animation = storyboard.Children.First() as DoubleAnimation;
            animation.To = -this.Width * currentElement;
            storyboard.Begin();
        }

        private void btnSold_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCurrent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {

            /*
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            */


            /*
            if (dlg.ShowDialog() == true)
            {
            
            */
            int targetId = Int32.Parse(txtAuctionedProductOwnerId.Text);

            if (IsIdValid(targetId))
            {
                try
                {
                    List<Product> tempProduct = Globals.AuctionContext.Products.ToList();

                    Product targetProduct =
                        (from p in Globals.AuctionContext.Products where p.ProductId == targetId select p)
                        .FirstOrDefault();
                    if (targetProduct != null)
                    {
                        ProductImage = targetProduct.ProductImage;
                    }
                    tbImage.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = Utils.ByteArrayToBitmapImage(ProductImage); // ex: SystemException
                    imageViewer.Source = bitmap;
                }
                catch (Exception ex) when (ex is SystemException || ex is IOException)
                {
                    MessageBox.Show(ex.Message, "File reading failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private bool IsIdValid(int id)
        {
            Client targetClient = (from c in Globals.AuctionContext.Clients where c.ClientId == id select c)
                .FirstOrDefault();
            if (targetClient != null)
            {
                if (targetClient.LotCount != 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}


