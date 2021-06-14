using System.Windows.Media.Imaging;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BLOOMINGTREES_WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ImagesFlowers NewFlowers = new ImagesFlowers();
        string displayimg, filePath, location;

        string selection, selectedValue;

        public MainWindow()
        {
            InitializeComponent();

            var bloom = new BloomingTreesContext();
            bloom.ImagesFlowers.Load();
            int recordCount = (int)bloom.ImagesFlowers.Count();
            uxRecordCount.Text = "Record Count: " + recordCount.ToString();
            GetFlowers();
            uxAddNewItemGrid.DataContext = NewFlowers;
        }

        private void GetFlowers()
        {
            var flowerGet = new BloomingTreesContext();
            flowerGet.ImagesFlowers.Load();
            FlowersDG.ItemsSource = flowerGet.ImagesFlowers.Local.ToObservableCollection();
        }

        private void Save_AddFlowers(object sender, ExecutedRoutedEventArgs e)
        {
            //My code to Add New Flowers
            var flowerAdd = new BloomingTreesContext();
            flowerAdd.ImagesFlowers.Add(NewFlowers);
            flowerAdd.SaveChanges();
            GetFlowers();
            NewFlowers = new ImagesFlowers();
            uxAddNewItemGrid.DataContext = NewFlowers;
            var bloomCount = new BloomingTreesContext();
            int recordCount = (int)bloomCount.ImagesFlowers.Count();
            uxRecordCount.Text = "Record Count: " + recordCount.ToString();
            displayImage.Source = null;
        }


        //Verify that all fields in Add New Flowers are populated except purchase date (allows Nulls)
        private void Save_CanAddFlowers(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(string.IsNullOrEmpty(uxAddProductId.Text) || string.IsNullOrEmpty(uxAddName.Text) ||
                string.IsNullOrEmpty(uxAddDescription.Text) || string.IsNullOrEmpty(uxAddImagePath.Text) ||
                string.IsNullOrEmpty(uxAddQty.Text) || string.IsNullOrEmpty(uxAddrice.Text) ||
                string.IsNullOrEmpty(uxAddSupplier.Text));
        }


        //private void AddFlowers(object sender, RoutedEventArgs e)
        //{
        //    var flowerAdd = new BloomingTreesContext();
        //    flowerAdd.ImagesFlowers.Add(NewFlowers);
        //    flowerAdd.SaveChanges();
        //    GetFlowers();
        //    NewFlowers = new ImagesFlowers();            
        //    uxAddNewItemGrid.DataContext = NewFlowers;
        //    var bloomCount = new BloomingTreesContext();            
        //    int recordCount = (int)bloomCount.ImagesFlowers.Count();
        //    uxRecordCount.Text = "Record Count: " + recordCount.ToString();
        //    displayImage.Source = null;
        //}


        ImagesFlowers itemSelected = new ImagesFlowers();

        private void UpdateItemFromEdit(object sender, RoutedEventArgs e)
        {
            itemSelected = (sender as FrameworkElement).DataContext as ImagesFlowers;
            uxUpdateItemGrid.DataContext = itemSelected;
            GetFlowers();
        }

        private void Save_UpdateFlowers(object sender, ExecutedRoutedEventArgs e)
        {
            //My code to Update Flowers
            var flowerUpdate = new BloomingTreesContext();
            flowerUpdate.Update(itemSelected);
            flowerUpdate.SaveChanges();
            GetFlowers();
            uxUpdateProductId.Text = null;
            uxUpdateItemName.Text = null;
            uxUpdateItemDescription.Text = null;
            uxEditImagePath.Text = null;
            uxUpdateItemQty.Text = null;
            uxUpdateItemPrice.Text = null;
            uxUpdateItemSupplier.Text = null;
            uxUpdateDatePicker.Text = null;
            displayImage.Source = null;
            var bloomCount = new BloomingTreesContext();
            int recordCount = (int)bloomCount.ImagesFlowers.Count();
            uxRecordCount.Text = "Record Count: " + recordCount.ToString();
            displayImage.Source = null;
        }

        //Verify that all fields in Update Flowers are populated except purchase date (allows Nulls)
        private void Save_CanUpdateFlowers(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(string.IsNullOrEmpty(uxUpdateProductId.Text) || string.IsNullOrEmpty(uxUpdateItemName.Text) ||
                string.IsNullOrEmpty(uxUpdateItemDescription.Text) || string.IsNullOrEmpty(uxEditImagePath.Text) ||
                string.IsNullOrEmpty(uxUpdateItemQty.Text) || string.IsNullOrEmpty(uxUpdateItemPrice.Text) ||
                string.IsNullOrEmpty(uxUpdateItemSupplier.Text));
        }


        //private void UpdateItems(object sender, RoutedEventArgs e)
        //{
        //    var flowerUpdate = new BloomingTreesContext();
        //    flowerUpdate.Update(itemSelected);
        //    flowerUpdate.SaveChanges();
        //    GetFlowers();
        //    uxUpdateProductId.Text = null;
        //    uxUpdateItemName.Text = null;
        //    uxUpdateItemDescription.Text = null;
        //    uxEditImagePath.Text = null;
        //    uxUpdateItemQty.Text = null;
        //    uxUpdateItemPrice.Text = null;
        //    uxUpdateItemSupplier.Text = null;
        //    uxUpdateDatePicker.Text = null;
        //    displayImage.Source = null;
        //    var bloomCount = new BloomingTreesContext();
        //    int recordCount = (int)bloomCount.ImagesFlowers.Count();
        //    uxRecordCount.Text = "Record Count: " + recordCount.ToString();
        //    displayImage.Source = null;
        //}

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            var deleteFlowers = new BloomingTreesContext();
            var flowerToDelete = (sender as FrameworkElement).DataContext as ImagesFlowers;
            deleteFlowers.ImagesFlowers.Remove(flowerToDelete);
            deleteFlowers.SaveChanges();
            GetFlowers();
            var bloomCount = new BloomingTreesContext();
            int recordCount = (int)bloomCount.ImagesFlowers.Count();
            uxRecordCount.Text = "Record Count: " + recordCount.ToString();
        }

        private void OnNewPreview_Click(object sender, RoutedEventArgs e)
        {
            var window = new LoadImageWindow();
            Application.Current.MainWindow = window;
            //Close();
            window.Show();
        }

        private void OnNewQuery_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.bhg.com/gardening/plant-dictionary/";

            ProcessStartInfo startInfo = new ProcessStartInfo();

            // To open url in Chrome, specify startInfo.File = "chrome.exe"
            // To open url in FireFox specify firefox.exe
            // To open url in Microsoft Edge specify msedge.exe
            // To open url in default browser specify explorer.exe

            startInfo.FileName = "explorer.exe";
            startInfo.Arguments = url;
            Process.Start(startInfo);
        }

        private void OnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void QueryItems(object sender, RoutedEventArgs e)
        {
            selection = this.CBSelectQueryField.SelectionBoxItem.ToString();
            var flowerGet = new BloomingTreesContext();
            flowerGet.ImagesFlowers.Load();
            switch (selection)
            {

                case "ProductID":
                    selectedValue = uxQueryProductId.Text.ToString();
                    FlowersDG.ItemsSource = flowerGet.ImagesFlowers.Local.ToObservableCollection().Where(x => x.ProductId.Contains(selectedValue));

                    break;
                case "ItemName":
                    selectedValue = uxQueryItemName.Text.ToString();
                    FlowersDG.ItemsSource = flowerGet.ImagesFlowers.Local.ToObservableCollection().Where(x => x.ItemName.Contains(selectedValue));
                    break;
                case "Description":
                    selectedValue = uxQueryItemDescription.Text.ToString();
                    FlowersDG.ItemsSource = flowerGet.ImagesFlowers.Local.ToObservableCollection().Where(x => x.ItemDescription.Contains(selectedValue));
                    break;
                case "Quantity":
                    selectedValue = uxQueryItemQty.Text.ToString();
                    FlowersDG.ItemsSource = flowerGet.ImagesFlowers.Local.ToObservableCollection().Where(x => x.ItemQty.Equals(Int32.Parse(uxQueryItemQty.Text.ToString())));
                    break;
                case "Supplier":
                    selectedValue = uxQueryItemSupplier.Text.ToString();
                    FlowersDG.ItemsSource = flowerGet.ImagesFlowers.Local.ToObservableCollection().Where(x => x.ItemSupplier.Contains(selectedValue));
                    break;
                case "Purchase Date":
                    selectedValue = uxQueryPurchaseDate.Text.ToString();

                    DateTime dateTimeSelected = Convert.ToDateTime(selectedValue);
                    selectedValue = dateTimeSelected.ToString("M/d/yyyy hh:mm:ss");

                    FlowersDG.ItemsSource = flowerGet.ImagesFlowers.Local.ToObservableCollection().Where(x => x.ItemPurchaseDate.Contains(selectedValue));
                    break;

                default:
                    break;

            }
        }


        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBox cmb = sender as ComboBox;
            //handle = !cmb.IsDropDownOpen;
            //Handle();
        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            var flowerGet = new BloomingTreesContext();
            flowerGet.ImagesFlowers.Load();
            FlowersDG.ItemsSource = flowerGet.ImagesFlowers.Local.ToObservableCollection();
            uxQueryProductId.Text = null;
            uxQueryProductId.IsEnabled = false;
            uxQueryItemName.Text = null;
            uxQueryItemName.IsEnabled = false;
            uxQueryItemDescription.Text = null;
            uxQueryItemDescription.IsEnabled = false;
            uxQueryItemQty.Text = null;
            uxQueryItemQty.IsEnabled = false;
            uxQueryItemSupplier.Text = null;
            uxQueryItemSupplier.IsEnabled = false;
            uxQueryPurchaseDate.Text = null;
            queryDatePicker.IsEnabled = false;
            uxQueryMessage.Text = null;
        }

        

        private void Handle()
        {
            if (CBSelectQueryField.SelectedItem != null)
            { 
                string comboBoxSelection = CBSelectQueryField.Text.ToString();
                switch (comboBoxSelection)
                {

                    case "ProductID":
                        {
                            uxQueryProductId.IsEnabled = true;
                            //else disabling all 
                            uxQueryItemName.IsEnabled = false;
                            uxQueryItemDescription.IsEnabled = false;
                            uxQueryItemQty.IsEnabled = false;
                            uxQueryItemSupplier.IsEnabled = false;
                            uxQueryPurchaseDate.IsEnabled = false;
                            selection = "ProductID";
                            queryDatePicker.IsEnabled = false;
                            break;
                        }

                    case "ItemName":
                        {
                            uxQueryItemName.IsEnabled = true;
                            //else disabling all 
                            uxQueryProductId.IsEnabled = false;
                            queryDatePicker.IsEnabled = false;
                            uxQueryItemDescription.IsEnabled = false;
                            uxQueryItemQty.IsEnabled = false;
                            uxQueryItemSupplier.IsEnabled = false;
                            uxQueryPurchaseDate.IsEnabled = false;
                            break;
                        }

                    case "Description":
                        {
                            uxQueryItemDescription.IsEnabled = true;
                            //else disabling all
                            uxQueryItemName.IsEnabled = false;
                            uxQueryProductId.IsEnabled = false;                            
                            queryDatePicker.IsEnabled = false;
                            uxQueryItemQty.IsEnabled = false;
                            uxQueryItemSupplier.IsEnabled = false;
                            uxQueryPurchaseDate.IsEnabled = false;
                            break;
                        }

                    case "Quantity":
                        {
                            uxQueryItemQty.IsEnabled = true;
                            //else disabling all 
                            uxQueryItemDescription.IsEnabled = false;
                            uxQueryItemName.IsEnabled = false;
                            uxQueryProductId.IsEnabled = false;                            
                            queryDatePicker.IsEnabled = false;
                            uxQueryItemSupplier.IsEnabled = false;
                            uxQueryPurchaseDate.IsEnabled = false;
                            break;
                        }


                    case "Supplier":
                        {
                            uxQueryItemSupplier.IsEnabled = true;
                            //else disabling all 
                            uxQueryItemQty.IsEnabled = false;
                            uxQueryItemDescription.IsEnabled = false;
                            uxQueryItemName.IsEnabled = false;
                            uxQueryProductId.IsEnabled = false;                            
                            queryDatePicker.IsEnabled = false;
                            uxQueryPurchaseDate.IsEnabled = false;
                            break;
                        }

                    case "Purchase Date":
                        {
                            queryDatePicker.IsEnabled = true;
                            uxQueryPurchaseDate.IsEnabled = true;
                            //else disabling all 
                            uxQueryItemSupplier.IsEnabled = false;
                            uxQueryItemQty.IsEnabled = false;
                            uxQueryItemDescription.IsEnabled = false;
                            uxQueryItemName.IsEnabled = false;
                            uxQueryProductId.IsEnabled = false;                     
                            break;
                        }

                    default:
                        break;

                }
            }
        }

        private void selectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select a picture";
            open.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (open.ShowDialog() == true)
            {
                displayimg = open.SafeFileName;
                displayImage.Source = new BitmapImage(new Uri(open.FileName));
                uxAddImagePath.IsEnabled = true;
                uxAddImagePath.Text = (string)(open.FileName);
                //_ = (string)(open.FileName);
                BindingExpression be = uxAddImagePath.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
                filePath = open.FileName;
                location = open.FileName;
            }
        }

        private void editImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select a picture";
            open.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (open.ShowDialog() == true)
            {
                displayimg = open.SafeFileName;
                displayImage.Source = new BitmapImage(new Uri(open.FileName));
                uxEditImagePath.IsEnabled = true;
                uxEditImagePath.Text = (string)(open.FileName);
                //_ = (string)(open.FileName);
                BindingExpression be = uxEditImagePath.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
                filePath = open.FileName;
                location = open.FileName;
            }
        }
    }
}

