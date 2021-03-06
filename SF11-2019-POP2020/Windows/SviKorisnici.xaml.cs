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
    /// Interaction logic for SviKorisnici.xaml
    /// </summary>
    public partial class SviKorisnici : Window
    {
        ICollectionView view;

        ObservableCollection<Enum> uloge;
        public SviKorisnici()
        {
            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);


            List<string> data = new List<string>();
            data.Add("Izaberite ulogu");
            data.Add("ADMINISTRATOR");
            data.Add("LEKAR");
            data.Add("PACIJENT");
          


            cmbUloga.ItemsSource = data;
            cmbUloga.SelectedIndex = 0;

            UpdateView();
        }

        private bool CustomFilter(object obj)
        {
            Korisnik korisnik = obj as Korisnik;

                if (txtPretragaIme.Text != "")
                {
                    return korisnik.Ime.Contains(txtPretragaIme.Text);
                }
           
                if (txtPretragaPrezime.Text != "")
                {
                    return korisnik.Prezime.Contains(txtPretragaPrezime.Text);
                }
            
                if (txtPretragaEmail.Text != "")
                {
                    return korisnik.Email.Contains(txtPretragaEmail.Text);
                }
                else
                    return true;


            return false;
        }

        private void UpdateView()
        {
            //DataGridLekari.ItemsSource = null;
            //view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            DataGridKorisnici.ItemsSource = view; // Util.Instance.Korisnici;
            DataGridKorisnici.IsSynchronizedWithCurrentItem = true;
            DataGridKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            view.Filter = CustomFilter;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
            this.Hide();
            gsa.Show();
        }

        private void DataGridKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }


        private void MenuItemDodajKorisnika_Click(object sender, RoutedEventArgs e)
        {
            Korisnik noviKorisnik = new Korisnik();
            DodavanjeIzmenaKorisnika addKorisnik = new DodavanjeIzmenaKorisnika(noviKorisnik, EStatus.Dodaj);
            addKorisnik.Show();
            //addKorisnik.ShowDialog();
            view.Refresh();
        }

        private void MenuItemIzmeniKorisnika_Click(object sender, RoutedEventArgs e)
        {
            Korisnik izabraniKorisnik = view.CurrentItem as Korisnik;

            if (izabraniKorisnik != null)
            {
                Korisnik stariKorisnik = izabraniKorisnik.Clone();
                DodavanjeIzmenaKorisnika addIzmenaKorisnika = new DodavanjeIzmenaKorisnika(izabraniKorisnik, EStatus.Izmeni);
                //this.Hide();
                if ((bool)addIzmenaKorisnika.ShowDialog())
                {
                    /*int index = Util.Instance.Adrese.ToList().FindIndex(a => a.Id.Equals(izabranaAdresa.Id));

                    //Util.Instance.Korisnici[index] = stariLekar;
                    Util.Instance.Adrese[index].Ulica = izabranaAdresa.Ulica;
                    Util.Instance.Adrese[index].Broj = izabranaAdresa.Broj;
                    Util.Instance.Adrese[index].Drzava = izabranaAdresa.Drzava;
                    Util.Instance.Adrese[index].Grad = izabranaAdresa.Grad;
                    Util.Instance.Adrese[index].Aktivan = izabranaAdresa.Aktivan;*/
                }

                //view.Refresh();

            }

        }

        private void MenuItemObrisiKorisnika_Click(object sender, RoutedEventArgs e)
        {
            Korisnik izabranKorisnik = view.CurrentItem as Korisnik;
            Util.Instance.DeleteUser(izabranKorisnik.Jmbg);
            Util.Instance.Korisnici.Remove(izabranKorisnik);

            UpdateView();
            view.Refresh();
        }

        private void DataGridKorisnici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtPretragaIme_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtPretragaPrezime_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtPretragaEmail_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string uloga = cmbUloga.SelectedItem as string;

            if (uloga.Contains("Izaberite ulogu"))
            {
                view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            }
            if (uloga.Contains("LEKAR"))
            {
                view = CollectionViewSource.GetDefaultView(Util.Instance.nadjiKorisnikePoUlozi(uloga));
            }
            if(uloga.Contains("PACIJENT"))
            {
                view = CollectionViewSource.GetDefaultView(Util.Instance.nadjiKorisnikePoUlozi(uloga));

            }
            if (uloga.Contains("ADMINISTRATOR"))
            {
                view = CollectionViewSource.GetDefaultView(Util.Instance.nadjiKorisnikePoUlozi(uloga));

            }
            UpdateView();
            view.Refresh();
        }
    }
}
