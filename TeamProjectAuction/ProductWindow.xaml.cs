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
using Microsoft.Win32;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
        public string filename;
        private byte[] productImage;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
        byte[] productImage;  // Is this needed

                CategoryName = (MyEnums.ProductCategory)cbCategory.SelectedItem,
                ProductName = txtProductName.Text,
                ProductDescription = txtProductDescription.Text,
                ProductStartPrice = double.Parse(txtProductStartPrice.Text)
            };
            //Product product = new Product();
            //string owner_name = txtOwnerName.Text;
            //string product_name = txtProductName.Text;
            //string product_descr = txtProductDescription.Text;
            // product category
            //int product_start_price, product_sold_price;
            //int.TryParse(txtProductStartPrice.Text, out product_start_price);
            //int.TryParse(txtProductSoldPrice.Text, out product_sold_price);
            //Stream stream = File.OpenRead(filename);
            //byte[] product_image = new byte[stream.Length];
            //stream.Read(product_image, 0, (int)stream.Length);

            //product.tblOwner.OwnerName = owner_name;
            //product.ProductName = product_name;
            //product.ProductDescription = product_descr;
            //product.ProductStartPrice = product_start_price;
            //product.ProductSoldPrice = product_sold_price;
            //product.ProductImage = product_image;
            //ProjectAuctionEntities entities = new ProjectAuctionEntities();
            //entities.tblProducts.Add(product);
            //entities.SaveChanges();

            aProduct.ProductImage = productImage;
            aProduct.ClientId = Int32.Parse(txtOwnerId.Text);
            Client TargetClient = (from c in Globals.AuctionContext.Clients
                                   where c.ClientId == Int32.Parse(txtOwnerId.Text)
                                   select c).FirstOrDefault();
            aProduct.Client = TargetClient;
            Globals.AuctionContext.Products.Add(aProduct);
            Globals.AuctionContext.SaveChanges();

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

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    productImage = File.ReadAllBytes(dlg.FileName);
                    tbImageUpcoming.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = Utils.ByteArrayToBitmapImage(productImage); // ex: SystemException
                    imageViewerUpcoming.Source = bitmap;

                }
                catch (Exception ex) when (ex is SystemException || ex is IOException)
                {
                    MessageBox.Show(ex.Message, "File reading failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            //Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            //fd.Filter = "All Files (*.*)|*.*|JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg";
            //if (fd.ShowDialog() == true)
            //{
            //    imageViewerUpcoming.Source = new BitmapImage(new Uri(fd.FileName));
            //    filename = fd.FileName;
            //}
        }

            // ? Not needed? 
            // List<Product> productOwners = Globals.AuctionContext.Clients.DbSet<Product>();  //Correct this line
            // lvProductOwners.ItemsSource = productOwners;
            // lvProductOwners.Items.Refresh();

            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.Filter = "All Files (*.*)|*.*|JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg";
            if (fd.ShowDialog() == true)
            {
                imageViewerUpcoming.Source = new BitmapImage(new Uri(fd.FileName));
                filename = fd.FileName;
            }
        }
            // ? Not needed? 
            // List<Product> productOwners = Globals.AuctionContext.Clients.DbSet<Product>();  //Correct this line
            // lvProductOwners.ItemsSource = productOwners;
            // lvProductOwners.Items.Refresh();

        }
            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.Filter = "All Files (*.*)|*.*|JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg";
            if (fd.ShowDialog() == true)
            {
                imageViewerUpcoming.Source = new BitmapImage(new Uri(fd.FileName));
                filename = fd.FileName;
            }
        }

        }

        private void btnImageUpcoming_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.Filter = "All Files (*.*)|*.*|JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg";
            if (fd.ShowDialog() == true)
}                imageViewerUpcoming.Source = new BitmapImage(new Uri(fd.FileName));
                filename = fd.FileName;
            }
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
