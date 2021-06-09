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


namespace BLOOMINGTREES_WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ImagesFlowers NewFlowers = new ImagesFlowers();
        string displayimg, filePath, location;        

        public MainWindow()
        {
            InitializeComponent();

            var flowers = new List<Models.BloomingTrees>();

            flowers.Add(new Models.BloomingTrees { ID = 1, ProductID = "FPB566", Name = "Acacia", Description = "Golden Yellow tree", Path = "E:\\'Users", Quantity = 13, Price = 12.77});
            flowers.Add(new Models.BloomingTrees { ID = 2, ProductID = "FPS212", Name = "Amaranth", Description = "Reddish Purple plant", Path = "E:\\Users", Quantity = 1, Price = 2.00 });
            flowers.Add(new Models.BloomingTrees { ID = 3, ProductID = "FPB417", Name = "Primrose", Description = "dazzling multi-colored flowers", Path = "E:\\'Users", Quantity = 27, Price = 7.99 });

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

        private void AddFlowers(object sender, RoutedEventArgs e)
        {
            var flowerAdd = new BloomingTreesContext();
            flowerAdd.ImagesFlowers.Add(NewFlowers);
            flowerAdd.SaveChanges();
            GetFlowers();
            NewFlowers = new ImagesFlowers();
            uxAddNewItemGrid.DataContext = NewFlowers;
            var bloomCount = new BloomingTreesContext();
            int recordCount = (int)bloomCount.ImagesFlowers.Count();
            uxRecordCount.Text = "Record Count: " + recordCount.ToString();
        }


        ImagesFlowers itemSelected = new ImagesFlowers();

        private void UpdateItemFromEdit(object sender, RoutedEventArgs e)
        {
            itemSelected = (sender as FrameworkElement).DataContext as ImagesFlowers;
            uxUpdateItemGrid.DataContext = itemSelected;
            GetFlowers();
        }

        private void UpdateItems(object sender, RoutedEventArgs e)
        {
            var flowerUpdate = new BloomingTreesContext();
            flowerUpdate.Update(itemSelected);
            flowerUpdate.SaveChanges();
            GetFlowers();
        }

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

        private void OnNew_Click(object sender, RoutedEventArgs e)
        {
            var window = new LoadImageWindow();
            Application.Current.MainWindow = window;
            //Close();
            window.Show();
        }

        private void OnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void QueryItems(object sender, RoutedEventArgs e)
        {

        }


        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void Handle()
        {
            if (CBSelectQueryField.SelectedItem != null)
            { //Do Somthing

                string comboBoxSelection = this.CBSelectQueryField.SelectionBoxItem.ToString();
                switch (comboBoxSelection)
                {

                    case "ProductID":
                        {
                            ProductID.IsEnabled = true;
                            MessageBox.Show(comboBoxSelection);
                            break;
                        }

                    case "ItemName":
                        {
                            ItemName.IsEnabled = true;
                            MessageBox.Show(comboBoxSelection);
                            break;
                        }

                    case "Description":
                        {
                            break;
                        }

                    case "Quantity":
                        {
                            break;
                        }


                    case "Supplier":
                        {
                            break;
                        }

                    case "Date":
                        {
                            break;
                        }
                    default:
                        break;

                }

            }

        }












        //    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //string comboBoxSelection;
        //    //Convert.ToString(comboBoxSelection);         
        //}

        private void selectImageButton_Click(object sender, RoutedEventArgs e)
        {

            //following code would open LoadImageWindow Class but never got it working to update uxAddImagePath with file path
            //var window = new LoadImageWindow();
            //Application.Current.MainWindow = window;
            ////Close();
            //window.Show();

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
