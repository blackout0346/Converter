using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;


namespace Converter
{

    public partial class MainWindow : Window
    {
     
        private string SelectedFormat = "jpg";

        BitmapImage image;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {
            image = new BitmapImage();
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Filter(*png;*.jpg;*bpm;*tiff)|*png;*.jpg;*bmp;*tiff";
            
            
            if (openFileDialog.ShowDialog() == true)
            {
                image= new BitmapImage(new Uri(openFileDialog.FileName));
             
                MyImage.Source = image;

            }
        


        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (image == null)
            {
                MessageBox.Show("Сначала загрузите изображение");
                return;
            }
            SaveFileDialog savedialog = new SaveFileDialog
            {
                Filter = $"{SelectedFormat.ToUpper()} Image|*.{SelectedFormat}",
                DefaultExt = SelectedFormat
            };

            BitmapEncoder encoder;

            switch (SelectedFormat)
            {
                case "png":
                    encoder = new PngBitmapEncoder();
                    break;
                case "bmp":
                    encoder = new BmpBitmapEncoder();
                    break;
                case "jpg":
                    encoder = new JpegBitmapEncoder();
                    break;
                case "tiff":
                    encoder = new TiffBitmapEncoder();
                    break;
         
               
                default:
                    encoder = new JpegBitmapEncoder();
                    break;

            }
            if (savedialog.ShowDialog() == true)
            {
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (FileStream stream = new FileStream(savedialog.FileName, FileMode.Create))
                {
                    encoder.Save(stream);
                }

            }
        }
        private void FormatItem_Click(object sender, RoutedEventArgs e)
        {
            JpgItem.IsChecked = false;
            PngItem.IsChecked = false;
            BmpItem.IsChecked = false;
            tiffItem.IsChecked = false;
            MenuItem clickedItem = sender as MenuItem;
            clickedItem.IsChecked = true;
            SelectedFormat = clickedItem.Header.ToString().ToLower();
           
        }
      
    
    }
}
