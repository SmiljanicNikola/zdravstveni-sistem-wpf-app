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
    /// Logic for DodavanjeIzmenaLekar.xaml
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

            ComboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();

            ComboBoxPol.ItemsSource = Enum.GetValues(typeof(EPol)).Cast<EPol>();

            if(status.Equals(EStatus.Izmeni) && lekar != null)
            {
                this.Title = "Izmeni lekara";
                /*lekar.Ime = txtIme.Text;
                lekar.Prezime = txtPrezime.Text;
                txtJmbg.IsEnabled = false;
                lekar.Email = txtEmail.Text;
                txtAdresaId.IsEnabled = false;               
                lekar.Lozinka = txtLozinka.Text;
                Util.Instance.SacuvajEntitet(lekar);*/
                txtJmbg.IsEnabled = false;
                //Util.Instance.SacuvajEntitet(lekar);
                //Util.Instance.UpdateEntiteta(lekar);
                //Util.Instance.SacuvajEntitet(lekar);
                

            }
            else
            {
                this.Title = "Dodaj lekara";
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            if (!Validation.GetHasError(txtEmail) && !Validation.GetHasError(txtIme))
            {
                if (odabranStatus.Equals(EStatus.Dodaj))
                {
                    odabranLekar.Aktivan = true;
                    Lekar lekar = new Lekar
                    {
                        Id = odabranLekar.Id,
                        //DomZdravljaId = 4,
                        Termini = "nema",
                        Aktivan = true


                    };

                    Util.Instance.Korisnici.Add(odabranLekar);
                    Util.Instance.Lekari.Add(lekar);

                    int id = Util.Instance.SacuvajEntitet(odabranLekar);
                    lekar.Id = id;

                    Util.Instance.SacuvajEntitet(lekar);
                }

                this.DialogResult = true;
                this.Close();
            }

        }
            /*
            ComboBoxItem item = (ComboBoxItem)comboBoxTipKorisnika.SelectedItem;
            string value = item.Content.ToString();
            Enum.TryParse(value, out ETipKorisnika tip);
            // comboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();

            ComboBoxItem item1 = (ComboBoxItem)ComboBoxPol.SelectedItem;
            string value1 = item.Content.ToString();
            Enum.TryParse(value1, out EPol pol);


            Korisnik k = new Korisnik
            {
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                Jmbg = txtJmbg.Text,
                Email = txtEmail.Text,
                AdresaId = int.Parse(txtAdresaId.Text),
                Pol = pol,
                Lozinka = txtLozinka.Text,
                TipKorisnika = tip,
                Aktivan = true
            };
            Util.Instance.Korisnici.Add(k);
            Util.Instance.SacuvajEntitet(k);
            */


            /*
            if (IsValid())
            {

                if (odabranStatus.Equals(EStatus.Dodaj))
                {
                    odabranLekar.Aktivan = true;
                    Lekar lekar = new Lekar
                    {
                        Id = odabranLekar.Id,
                        DomZdravljaId = 4
                   
                    };
                    Util.Instance.Korisnici.Add(odabranLekar);
                    Util.Instance.Lekari.Add(lekar);

                    int id = Util.Instance.SacuvajEntitet(odabranLekar);
                    Util.Instance.SacuvajEntitet(lekar);

                }
                else
                {
                    //int izmenaLekara = Util.Instance.Lekari.ToList().FindIndex(u => u.Korisnicko.KorisnickoIme.Equals(txtKorisnickoIme.Text));
                    //int izmenaKorisnika = Util.Instance.Korisnici.ToList().FindIndex(u => u.KorisnickoIme.Equals(txtKorisnickoIme.Text));

                    //Util.Instance.Korisnici[izmenaKorisnika] = k;
                    //Util.Instance.Lekari[izmenaLekara] = lekar;
                }


               


                this.DialogResult = false;
                this.Close();
            }*/
        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /*private bool IsValid()
        {
            return !Validation.GetHasError(txtEmail)
                && !Validation.GetHasError(txtIme);
        }*/

    }
}
