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
    /// Interaction logic for SviTermini.xaml
    /// </summary>
    public partial class SviTermini : Window
    {
        ICollectionView view;

        public SviTermini()
        {
            InitializeComponent();

            Lekar lekar = SviDoktori.izabraniLekar;
            if (lekar != null)
            {
                ObservableCollection<Termin> terminiIzabranogLekara = Util.Instance.terminiIzabranogLekara(lekar);
                view = CollectionViewSource.GetDefaultView(terminiIzabranogLekara);
            }
            else
            {
                string jmbg = GlavnaStranicaPacijent.jmbg;
                if (jmbg == null)
                {
                    jmbg = GlavnaStranicaAdministrator.jmbg;
                }

                Korisnik kor = Util.Instance.korisnikPoJmbg(jmbg);

                if (kor.TipKorisnika.Equals(ETipKorisnika.PACIJENT))
                {
                    MenuItemDodajTermin.Visibility = Visibility.Hidden;
                    MenuItemIzmeniTermin.Visibility = Visibility.Hidden;
                    MenuItemObrisiTermin.Visibility = Visibility.Hidden;
                    MenuItemZakaziTermin.Visibility = Visibility.Visible;
                }
                view = CollectionViewSource.GetDefaultView(Util.Instance.Termini);
            }
            UpdateView();     
        }

        private void UpdateView()
        {
            DataGridTermini.ItemsSource = view;
            DataGridTermini.IsSynchronizedWithCurrentItem = true;
            DataGridTermini.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DataGridTermini_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MenuItemDodajTermin_Click(object sender, RoutedEventArgs e)
        {
            Termin noviTermin = new Termin();
            DodavanjeIzmenaTermina addTermin = new DodavanjeIzmenaTermina(noviTermin, EStatus.Dodaj);
            addTermin.Show();

            this.Hide();
            this.Show();
            view.Refresh();
        }

        private void MenuItemIzmeniTermin_Click(object sender, RoutedEventArgs e)
        {
            Termin izabraniTermin = view.CurrentItem as Termin;
            Termin stariTermin = izabraniTermin.Clone();
            DodavanjeIzmenaTermina addTermin = new DodavanjeIzmenaTermina(izabraniTermin, EStatus.Izmeni);

            this.Hide();
            if ((bool)addTermin.ShowDialog())
            {
                int index = Util.Instance.Termini.ToList().FindIndex(t => t.Id.Equals(izabraniTermin.Id));

                Util.Instance.Termini[index] = stariTermin;        
            }

            this.Show();
            view.Refresh();

        }

        private void MenuItemZakaziTermin_Click(object sender, RoutedEventArgs e)
        {
            Termin izabraniTermin = view.CurrentItem as Termin;
            if (izabraniTermin.StatusTermina.Equals(EStatusTermina.ZAKAZAN))
            {
                MessageBox.Show("Termin je vec zakazan, rezervisite slobodan");
            }
            else
            {
                izabraniTermin.StatusTermina = EStatusTermina.ZAKAZAN;

                string jmbg = GlavnaStranicaPacijent.jmbg;
                if (jmbg == null)
                {
                    jmbg = GlavnaStranicaAdministrator.jmbg;
                }

                Korisnik korisnik = Util.Instance.korisnikPoJmbg(jmbg);
                if (korisnik.TipKorisnika.Equals(ETipKorisnika.PACIJENT))
                {
                    izabraniTermin.Pacijent = Util.Instance.pacijentPoJmbg(jmbg);
                }
                Util.Instance.UpdateEntiteta(izabraniTermin);

                this.Show();
                view.Refresh();
            }
        }

        private void MenuItemObrisiTermin_Click(object sender, RoutedEventArgs e)
        {
            Termin izabraniTermin = view.CurrentItem as Termin;
            Util.Instance.DeleteTermin(izabraniTermin.Id);

            Util.Instance.Termini.Remove(izabraniTermin);
            UpdateView();
            view.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
            this.Hide();
            gsa.Show();
        }

        private void DataGridTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
