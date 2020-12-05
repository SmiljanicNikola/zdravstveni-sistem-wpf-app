using SF11_2019_POP2020.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DodavanjeIzmenaAdministratora.xaml
    /// </summary>
    public partial class DodavanjeIzmenaAdministratora : Window
    {
        private EStatus odabranStatus;
        private Korisnik odabranKorisnik;
        public DodavanjeIzmenaAdministratora(Korisnik korisnik, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = korisnik;

            odabranKorisnik = korisnik;
            odabranStatus = status;
            comboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                //odabranKorisnik.Aktivan = true;
                Korisnik k = new Korisnik
                {
                    Ime = txtIme.Text,
                    Prezime = txtPrezime.Text,
                    KorisnickoIme = txtKorisnickoIme.Text,
                    Email = txtEmail.Text,
                    Aktivan = true,
                    JMBG = txtJMBG.Text,
                    Lozinka = txtLozinka.Text
                };
                Util.Instance.Korisnici.Add(k);
                //Util.Instance.Lekari.Add(lekar);
            }
            else
            {
                //int izmenaLekara = Util.Instance.Lekari.ToList().FindIndex(u => u.Korisnicko.KorisnickoIme.Equals(txtKorisnickoIme.Text));
                //int izmenaKorisnika = Util.Instance.Korisnici.ToList().FindIndex(u => u.KorisnickoIme.Equals(txtKorisnickoIme.Text));

                //Util.Instance.Korisnici[izmenaKorisnika] = k;
                //Util.Instance.Lekari[izmenaLekara] = lekar;
            }


            Util.Instance.SacuvajEntite("korisnici.txt");
            //Util.Instance.SacuvajEntite("lekari.txt");


            //this.DialogResult = false;
            this.Close();
        }
    }
}
