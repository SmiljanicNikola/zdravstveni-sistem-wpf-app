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
    /// Interaction logic for SveAdrese.xaml
    /// </summary>
    public partial class SveAdrese : Window
    {
        ICollectionView view;
        public SveAdrese()
        {
            InitializeComponent();

            UpdateView();
        }

        private void UpdateView()
        {
            view = CollectionViewSource.GetDefaultView(Util.Instance.Adrese);
            DataGridAdrese.ItemsSource = view;
            DataGridAdrese.IsSynchronizedWithCurrentItem = true;
            DataGridAdrese.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void MenuItemDodajAdresu_Click(object sender, RoutedEventArgs e)
        {
            Adresa novaAdresa = new Adresa();
            DodavanjeIzmenaAdrese addAdresa = new DodavanjeIzmenaAdrese(novaAdresa);
            addAdresa.Show();

            this.Hide();
            this.Show();
            UpdateView();
            view.Refresh();
        }

        private void MenuItemIzmeniAdresu_Click(object sender, RoutedEventArgs e)
        {
            Adresa izabranaAdresa = view.CurrentItem as Adresa;

            if(izabranaAdresa != null)
            {
                Adresa staraAdresa = izabranaAdresa.Clone();
                DodavanjeIzmenaAdrese addadrese = new DodavanjeIzmenaAdrese(izabranaAdresa, EStatus.Izmeni);
                
                if ((bool)addadrese.ShowDialog())
                {

                }
                UpdateView();
            }
        }

        private void MenuItemObrisiAdresu_Click(object sender, RoutedEventArgs e)
        {
            Adresa izabranaAdresa = view.CurrentItem as Adresa;
            Util.Instance.DeleteAdresa(izabranaAdresa.Id);

            Util.Instance.Adrese.Remove(izabranaAdresa);
            UpdateView();
            view.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
            this.Hide();
            gsa.Show();
        }

        private void DataGridAdrese_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void DataGridAdrese_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
