using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TeamProjectAuction
{
    static class Utils
    {
        public static BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            var image = new BitmapImage();
            if (array != null)
            {
                using (var ms = new System.IO.MemoryStream(array))
                {
                    
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad; // here
                    image.StreamSource = ms;
                    image.EndInit();
                    
                }
            }
            return image;
        }

    }
}