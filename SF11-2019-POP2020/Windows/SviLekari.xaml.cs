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
    /// Interaction logic for SviLekari.xaml
    /// </summary>
    public partial class SviLekari : Window
    {
        ICollectionView view;
        public SviLekari()
        {

            InitializeComponent();

            UpdateView();

            view.Filter = CustomFilter;

        }

        private bool CustomFilter(object obj)
        {
            Korisnik korisnik = obj as Korisnik;

            if (korisnik.TipKorisnika.Equals(ETipKorisnika.LEKAR) && korisnik.Aktivan)
                if(textBoxPretraga.Text != "")
                {
                    return korisnik.Ime.Contains(textBoxPretraga.Text);
                }
                else
                    return true;
            return false;
        }

        private void UpdateView()
        {
            //DataGridLekari.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            DataGridLekari.ItemsSource = view; // Util.Instance.Korisnici;
            DataGridLekari.IsSynchronizedWithCurrentItem = true;
            DataGridLekari.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DataGridLekari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridLekari_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MenuItemDodaj_Click(object sender, RoutedEventArgs e)
        {
            Korisnik noviKorisnik = new Korisnik();
            DodavanjeIzmenaLekar add = new DodavanjeIzmenaLekar(noviKorisnik);

            this.Hide();
            if((bool)add.ShowDialog())
            {

            }
            this.Show();
            view.Refresh();
        }

        private void MenuItemIzmeni_Click(object sender, RoutedEventArgs e)
        {
            //Korisnik izabraniLekar = (Korisnik)DataGridLekari.SelectedItem;
            Korisnik izabraniLekar = view.CurrentItem as Korisnik;
            Korisnik stariLekar = izabraniLekar.Clone();
            DodavanjeIzmenaLekar add = new DodavanjeIzmenaLekar(izabraniLekar, EStatus.Izmeni);

            this.Hide();
            if ((bool)add.ShowDialog())
            {
                int index = Util.Instance.Korisnici.ToList().FindIndex(k => k.KorisnickoIme.Equals(izabraniLekar.KorisnickoIme));
                Util.Instance.Korisnici[index] = stariLekar;
            }
            this.Show();
            view.Refresh();
        }

        private void MenuItemObrisi_Click(object sender, RoutedEventArgs e)
        {
            Korisnik izabraniLekar = view.CurrentItem as Korisnik;
            Util.Instance.DeleteUser(izabraniLekar.KorisnickoIme);

            //int index = Util.Instance.Lekari.ToList().FindIndex(u => u.Korisnicko.KorisnickoIme.Equals(izabraniLekar.KorisnickoIme));
            //Util.Instance.Lekari[index].Korisnicko.Aktivan = false;

            //UpdateView();

            view.Refresh();

        }

        private void textBoxPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
    }
}
