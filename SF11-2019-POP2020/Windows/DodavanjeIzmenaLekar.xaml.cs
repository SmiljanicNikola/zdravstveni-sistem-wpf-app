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
        private Korisnik odabranLekar;
        public DodavanjeIzmenaLekar(Korisnik lekar, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = lekar;

            odabranLekar = lekar;
            odabranStatus = status;
            comboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();

            if(status.Equals(EStatus.Izmeni) && lekar != null)
            {
                this.Title = "Izmeni lekara";
                //txtEmail.Text = lekar.Email;
                //txtKorisnickoIme.Text = lekar.KorisnickoIme;
                //txtIme.Text = lekar.Ime;
                //txtPrezime.Text = lekar.Prezime;
                txtKorisnickoIme.IsEnabled = false;
            }
            else
            {
                this.Title = "Dodaj lekara";
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //ComboBoxItem item = (ComboBoxItem)comboBoxTipKorisnika.SelectedItem;
            //string value = item.Content.ToString();
            //Enum.TryParse(value, out ETipKorisnika tip);

            //Korisnik k = new Korisnik
            //{
            //    Ime = txtIme.Text,
            //    Prezime = txtPrezime.Text,
            //    KorisnickoIme = txtKorisnickoIme.Text,
            //    Email = txtEmail.Text,
            //    TipKorisnika = tip,
            //    Aktivan = true,
            //    JMBG = "1234",
            //    Lozinka = "1234"
            //};

            if (IsValid())
            {

                if (odabranStatus.Equals(EStatus.Dodaj))
                {
                    odabranLekar.Aktivan = true;
                    Lekar lekar = new Lekar
                    {
                        DomZdravlja = "DomZdravlja 1",
                        Korisnicko = odabranLekar
                    };
                    Util.Instance.Korisnici.Add(odabranLekar);
                    Util.Instance.Lekari.Add(lekar);
                }
                else
                {
                    //int izmenaLekara = Util.Instance.Lekari.ToList().FindIndex(u => u.Korisnicko.KorisnickoIme.Equals(txtKorisnickoIme.Text));
                    //int izmenaKorisnika = Util.Instance.Korisnici.ToList().FindIndex(u => u.KorisnickoIme.Equals(txtKorisnickoIme.Text));

                    //Util.Instance.Korisnici[izmenaKorisnika] = k;
                    //Util.Instance.Lekari[izmenaLekara] = lekar;
                }


                Util.Instance.SacuvajEntite("korisnici.txt");
                Util.Instance.SacuvajEntite("lekari.txt");


                this.DialogResult = false;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private bool IsValid()
        {
            return !Validation.GetHasError(txtKorisnickoIme) && !Validation.GetHasError(txtEmail)
                && !Validation.GetHasError(txtIme);
        }

    }
}
