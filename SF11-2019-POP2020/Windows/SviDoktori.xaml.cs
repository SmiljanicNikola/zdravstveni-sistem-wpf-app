using SF11_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SviDoktori.xaml
    /// </summary>
    public partial class SviDoktori : Window
    {
        ICollectionView view;

        ObservableCollection<string> naziviInstitucija;
        public static Lekar izabraniLekar;
        public SviDoktori()
        {
            InitializeComponent();
            string jmbg = GlavnaStranicaPacijent.jmbg;
            if (jmbg == null)
            {
                jmbg = GlavnaStranicaAdministrator.jmbg;
            }
            Korisnik kor = Util.Instance.korisnikPoJmbg(jmbg);

            if (kor == null)
            {
                menuItemAkcije.Visibility = Visibility.Hidden;
                btnPocetnaAdmin.Visibility = Visibility.Hidden;

            }
            if (kor.TipKorisnika.Equals(ETipKorisnika.PACIJENT))
            {
                menuItemAkcije.Visibility = Visibility.Hidden;
                btnPocetnaAdmin.Visibility = Visibility.Hidden;
            }

            btnPocetnaApp.Visibility = Visibility.Hidden;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Lekari);

            this.naziviInstitucija = new ObservableCollection<string>(Util.Instance.DomoviZdravlja
                .Select(domZdravlja => domZdravlja.NazivInstitucije)
                .Distinct()
                .Prepend("Izaberite instituciju..."));


            cmbInstitucije.ItemsSource = this.naziviInstitucija;
            cmbInstitucije.SelectedIndex = 0;

            UpdateView2();
        }

        private void UpdateView2()
        {
            //DataGridLekari.ItemsSource = null;
            //view = CollectionViewSource.GetDefaultView(Util.Instance.Lekari);
            DataGridDoktori.ItemsSource = view; // Util.Instance.Korisnici;
            DataGridDoktori.IsSynchronizedWithCurrentItem = true;
            DataGridDoktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            //view.Filter = CustomFilter;

        }

        private void DataGridDoktori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error") || e.PropertyName.Equals("Termini") || e.PropertyName.Equals("ListaTerapija"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void DataGridDoktori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItemDodajDoktora_Click(object sender, RoutedEventArgs e)
        {
            Lekar noviDoktor = new Lekar();
            DodavanjaIzmenaDoktora addDoktora = new DodavanjaIzmenaDoktora(noviDoktor, EStatus.Dodaj);

            this.Hide();
            if ((bool)addDoktora.ShowDialog())
            {

            }
            this.Show();
            view.Refresh();
        }

        private void MenuItemIzmeniDoktora_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemObrisiDoktora_Click(object sender, RoutedEventArgs e)
        {
            Lekar izabraniDoktor = view.CurrentItem as Lekar;
            Util.Instance.DeleteDoktora(izabraniDoktor.Id);

            Util.Instance.Lekari.Remove(izabraniDoktor);

            UpdateView2();
            view.Refresh();
        }

        private void btnPocetnaAdmin_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
            this.Hide();
            gsa.Show();
        }

        private void btnPocetnaApp_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }

        private void cmbInstitucije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string institucija = cmbInstitucije.SelectedItem as string;

            if(institucija.Contains("Izaberite instituciju..."))
            {
                view = CollectionViewSource.GetDefaultView(Util.Instance.nadjiLekarePoInstituciji(""));
            }
            else
            {
                view = CollectionViewSource.GetDefaultView(Util.Instance.nadjiLekarePoInstituciji(institucija));

            }

            UpdateView2();
            view.Refresh();
        }

        private void MenuItemTerminiDoktora_Click(object sender, RoutedEventArgs e)
        {
            Lekar izabraniDoktor = view.CurrentItem as Lekar;
            izabraniLekar = izabraniDoktor;
            SviTermini st = new SviTermini();
            st.Show();
        }

       

        private void menuItemAkcije_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
