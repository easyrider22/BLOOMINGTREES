
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        private void editImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select a picture";
            open.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            "Portable Network Graphic (*.png)|*.png";

            // display image in Image Tag
            if (open.ShowDialog() == true)
            {
                //displayimg = open.SafeFileName;
                
                location = open.FileName;
                Process msPaint = new();
                msPaint.StartInfo.FileName = "mspaint.exe";
                //msPaint.StartInfo.Arguments = location; // if you need some arguments to open
                msPaint.Start();
                displayImage.Source = null;
            }
        }

        public BitmapImage Source { get; internal set; }

        private void selectImageButton_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select a picture";
            open.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            "Portable Network Graphic (*.png)|*.png";

            // display image in Image Tag
            if (open.ShowDialog() == true)
            {
                MainWindow thisImage = new MainWindow();
                displayimg = open.SafeFileName;
                //following will display image inside UI Image tag = "displayImage".
                displayImage.Source = new BitmapImage(new Uri(open.FileName));
                thisImage.uxAddImagePath.IsEnabled = true;
                thisImage.uxAddImagePath.Text = displayimg;
                //thisImage.uxAddImagePath.Text = open.FileName;                
                BindingExpression be = thisImage.uxAddImagePath.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
                filePath = open.FileName;
                location = open.FileName;               


            }//end of if statement

        } //end of Event Handler selectImageButton                   

    }//end of Partial Class LoadImageWindow : Window

} //end of Namespace BLOOMINGTREES_wpfApp
















