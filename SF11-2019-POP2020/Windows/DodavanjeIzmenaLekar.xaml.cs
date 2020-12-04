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
    /// </summary>
    public partial class DodavanjeIzmenaLekar : Window
    {

        private EStatus odabranStatus;
        private Lekar odabranLekar;
        public DodavanjeIzmenaLekar(Lekar lekar, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            odabranLekar = lekar;
            odabranStatus = status;

            if(status.Equals(EStatus.Izmeni) && lekar != null)
            {
                this.Title = "Izmeni lekara";
                txtEmail.Text = lekar.Email;
                txtKorisnickoIme.Text = lekar.KorisnickoIme;
                txtIme.Text = lekar.Ime;
                txtPrezime.Text = lekar.Prezime;
                txtKorisnickoIme.IsEnabled = false;
            }
            else
            {
                this.Title = "Dodaj lekara";
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)comboBoxTipKorisnika.SelectedItem;
            string value = item.Content.ToString();
            Enum.TryParse(value, out ETipKorisnika tip);

            Korisnik k = new Korisnik
            {
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                KorisnickoIme = txtKorisnickoIme.Text,
                Email = txtEmail.Text,
                TipKorisnika = tip,
                Aktivan = true,
                JMBG = "1234",
                Lozinka = "1234"
            };

            Lekar lekar = new Lekar
            {
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                KorisnickoIme = txtKorisnickoIme.Text,
                Email = txtEmail.Text,
                TipKorisnika = tip,
                Aktivan = true,
                JMBG = "1234",
                Lozinka = "1234",
                DomZdravlja = "DomZdravlja 1",
                Korisnicko = k 
            };
            if(odabranStatus.Equals(EStatus.Dodaj))
            {
                Util.Instance.Korisnici.Add(k);
                Util.Instance.Lekari.Add(lekar);
            }
            else
            {
                int izmenaLekara = Util.Instance.Lekari.ToList().FindIndex(u => u.KorisnickoIme.Equals(txtKorisnickoIme.Text));
                int izmenaKorisnika = Util.Instance.Korisnici.ToList().FindIndex(u => u.KorisnickoIme.Equals(txtKorisnickoIme.Text));

                Util.Instance.Korisnici[izmenaKorisnika] = k;
                Util.Instance.Lekari[izmenaLekara] = lekar;
            }


            Util.Instance.SacuvajEntite("korisnici.txt");
            Util.Instance.SacuvajEntite("lekari.txt");


            this.DialogResult = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
