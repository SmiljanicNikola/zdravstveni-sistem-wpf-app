using SF11_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SF11_2019_POP2020.Windows
{
    /// <summary>
    /// Interaction logic for SviDomoviZdravlja.xaml
    /// </summary>
    public partial class SviDomoviZdravlja : Window
    {
        ICollectionView view;
        public SviDomoviZdravlja()
        {
            InitializeComponent();

            UpdateView();
        }

        private void UpdateView()
        {
            view = CollectionViewSource.GetDefaultView(Util.Instance.DomoviZdravlja);
            DataGridDomoviZdravlja.ItemsSource = view;
            DataGridDomoviZdravlja.IsSynchronizedWithCurrentItem = true;
            DataGridDomoviZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void MenuItemDodajDomZdravlja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemIzmeniDomZdravlja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemObrisiDomZdravlja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridDomoviZdravlja_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }
    }
}
