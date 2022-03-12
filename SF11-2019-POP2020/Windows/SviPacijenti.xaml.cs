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
    /// Interaction logic for SviPacijenti.xaml
    /// </summary>
    public partial class SviPacijenti : Window
    {
        ICollectionView view;

        public SviPacijenti()
        {
            InitializeComponent();

            UpdateView();
        }

        private void UpdateView()
        {
            view = CollectionViewSource.GetDefaultView(Util.Instance.Pacijenti);
            DataGridPacijenti.ItemsSource = view;
            DataGridPacijenti.IsSynchronizedWithCurrentItem = true;
            DataGridPacijenti.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void MenuItemDodajPacijenta_Click(object sender, RoutedEventArgs e)
        {

            Korisnik noviKorisnik = new Korisnik();
            DodavanjeIzmenaPacijenta addPacijent = new DodavanjeIzmenaPacijenta(noviKorisnik, EStatus.Dodaj);
            addPacijent.Show();

            this.Hide();
            this.Show();
            view.Refresh();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }

        private void DataGridPacijenti_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error") || e.PropertyName.Equals("Termini") || e.PropertyName.Equals("ListaTerapija"))
                    e.Column.Visibility = Visibility.Collapsed;
        }

        private void btnAdminPocetna_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
            this.Hide();
            gsa.Show();
        }

        private void DataGridPacijenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridPacijenti_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
