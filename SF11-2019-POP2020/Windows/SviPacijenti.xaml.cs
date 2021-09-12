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

            view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            DataGridPacijenti.ItemsSource = view;
            DataGridPacijenti.IsSynchronizedWithCurrentItem = true;
            DataGridPacijenti.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


        }


     

        private void MenuItemDodajPacijenta_Click(object sender, RoutedEventArgs e)
        {

            Korisnik noviKorisnik = new Korisnik();
            DodavanjeIzmenaPacijenta addPacijent = new DodavanjeIzmenaPacijenta(noviKorisnik, EStatus.Dodaj) ;
            addPacijent.Show();

            this.Hide();
            //if ((bool)addAdmin.ShowDialog())
            //{

            //}
            this.Show();
            view.Refresh();
        }

        private void MenuItemIzmeniPacijenta_Click(object sender, RoutedEventArgs e)
        {
            Korisnik izabraniPacijent = view.CurrentItem as Korisnik;
            Korisnik stariPacijent = izabraniPacijent.Clone();
            DodavanjeIzmenaLekar add = new DodavanjeIzmenaLekar(izabraniPacijent, EStatus.Izmeni);

            this.Hide();
            if ((bool)add.ShowDialog())
            {
                int index = Util.Instance.Korisnici.ToList().FindIndex(k => k.Jmbg.Equals(izabraniPacijent.Jmbg));

                //Util.Instance.Korisnici[index] = stariLekar;
                Util.Instance.Korisnici[index].Id = izabraniPacijent.Id;
                Util.Instance.Korisnici[index].Ime = izabraniPacijent.Ime;
                Util.Instance.Korisnici[index].Prezime = izabraniPacijent.Prezime;
                Util.Instance.Korisnici[index].Email = izabraniPacijent.Email;
                Util.Instance.Korisnici[index].AdresaId = izabraniPacijent.AdresaId;
                Util.Instance.Korisnici[index].Pol = izabraniPacijent.Pol;
                Util.Instance.Korisnici[index].Lozinka = izabraniPacijent.Lozinka;
                Util.Instance.Korisnici[index].TipKorisnika = izabraniPacijent.TipKorisnika;
                //Util.Instance.Korisnici[index].Aktivan = izabraniPacijent.Aktivan;

                //Util.Instance.UpdateEntiteta(izabraniLekar);
           
            }
            Util.Instance.SacuvajEntitet(stariPacijent);
            Util.Instance.DeletePacijent(izabraniPacijent.Jmbg);


            this.Show();
            view.Refresh();

        }

        private void MenuItemObrisiPacijenta_Click(object sender, RoutedEventArgs e)
        {
            Korisnik izabraniPacijent = view.CurrentItem as Korisnik;
            Util.Instance.DeletePacijent(izabraniPacijent.Jmbg);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }

     
            private void DataGridPacijenti_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
            {
                if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
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
    }
}
