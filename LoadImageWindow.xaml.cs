using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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

namespace BLOOMINGTREES_WpfApp
{
    /// <summary>
    /// Interaction logic for LoadImageWindow.xaml
    /// </summary>
    public partial class LoadImageWindow : Window
    {
        string displayimg, filePath, location;
        
        public LoadImageWindow()
        {
            InitializeComponent();
        }

        public BitmapImage Source { get; internal set; }

        private void selectImageButton_Click(object sender, RoutedEventArgs e)
            {


                OpenFileDialog open = new OpenFileDialog();
                open.Title = "Select a picture";
                open.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";


                if (open.ShowDialog() == true)
                {


                    MainWindow thisImage = new MainWindow();
                    displayimg = open.SafeFileName;
                    //following will display image inside UI Image tag = "displayImage".
                    displayImage.Source = new BitmapImage(new Uri(open.FileName));
                    //thisImage.uxAddImagePath.IsEnabled = true;
                    thisImage.uxAddImagePath.Text = displayimg;

                    //thisImage.uxAddImagePath.Text = open.FileName;

                    // following two lines will add image's path text to Textbox = "uxAddImagePath" in MainWindow but does not work here - only works from MainWindow event ha
                    BindingExpression be = thisImage.uxAddImagePath.GetBindingExpression(TextBox.TextProperty);
                    be.UpdateSource();
                    filePath = open.FileName;
                    location = open.FileName;
                }//end of if statement

            } //end of Event Handler selectImageButton    

    }//end of Partial Class LoadImageWindow : Window

} //end of Namespace BLOOMINGTREES_wpfApp














       