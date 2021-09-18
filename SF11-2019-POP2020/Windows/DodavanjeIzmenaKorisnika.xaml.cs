using SF11_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DodavanjeIzmenaKorisnika : Window
    {
        private EStatus odabranStatus;
        private Korisnik odabranKorisnik;
        private Adresa novaAdresa;
        public DodavanjeIzmenaKorisnika(Korisnik korisnik, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = korisnik;

            odabranKorisnik = korisnik;
            odabranStatus = status;

            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                ComboBoxTipKorisnika.IsEnabled = true;
                ComboBoxPol.IsEnabled = true;
                novaAdresa = new Adresa();

            }
            else
            {
                novaAdresa = korisnik.Adresa;
                ComboBoxTipKorisnika.IsEnabled = false;
                ComboBoxPol.IsEnabled = false;
            }

            ComboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();
            ComboBoxPol.ItemsSource = Enum.GetValues(typeof(EPol)).Cast<EPol>();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        public Adresa adresaPoId(int id)
        {
            ObservableCollection<Adresa> adrese = Util.Instance.Adrese;
            foreach (Adresa adr in adrese)
            {
                if (adr.Id == id)
                {
                    return adr;
                }
            }
            return null;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid()) 
            {
                /*odabranKorisnik.Aktivan = true;
                odabranKorisnik.Email = txtEmail.Text;
                odabranKorisnik.Ime = txtIme.Text;
                odabranKorisnik.Prezime = txtPrezime.Text;
                odabranKorisnik.Jmbg = txtJMBG.Text;
                Console.WriteLine($"Id: {novaAdresa.Id}");
                odabranKorisnik.Adresa = Util.Instance.adresaPoId(novaAdresa.Id);
                odabranKorisnik.Pol = (EPol)Enum.Parse(typeof(EPol), ComboBoxPol.SelectedItem.ToString());
                odabranKorisnik.TipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), ComboBoxTipKorisnika.SelectedItem.ToString());*/

                if (odabranStatus.Equals(EStatus.Dodaj))
            {


                    Korisnik k = new Korisnik
                    {

                        Ime = txtIme.Text,
                        Prezime = txtPrezime.Text,
                        Jmbg = txtJMBG.Text,
                        Email = txtEmail.Text,
                        Adresa = DodavanjeIzmenaAdrese.adresa,
                        Pol = (EPol)Enum.Parse(typeof(EPol), ComboBoxPol.SelectedItem.ToString()),
                        Lozinka = txtLozinka.Text,
                        TipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), ComboBoxTipKorisnika.SelectedItem.ToString()),
                        Aktivan = true

                };

                    //Util.Instance.Lekari.Add(lekar);
                    //int id = Util.Instance.SacuvajEntitet(k);
                    Util.Instance.SacuvajEntitet(k);
                    //k.Id = id;
                    Util.Instance.Korisnici.Add(k);

                }
            else if (odabranStatus.Equals(EStatus.Izmeni))
                {
                    

                    /*Adresa adresa = new Adresa()
                    {
                        Ulica = txtUlica.Text,
                        Broj = txtBroj.Text,
                        Grad = txtGrad.Text,
                        Drzava = txtDrzava.Text,
                        Aktivan = true

                    };*/
                    Util.Instance.UpdateEntiteta(odabranKorisnik);
                }
                //this.DialogResult = true;
                this.Close();
            }
        }

        /* Util.Instance.SacuvajEntitet(obj);*/
        //Util.Instance.SacuvajEntite("lekari.txt");


        //this.DialogResult = false;

        private bool IsValid()
        {
            return !Validation.GetHasError(txtEmail) && !Validation.GetHasError(txtJMBG) && !Validation.GetHasError(txtIme) && !Validation.GetHasError(txtPrezime) && !Validation.GetHasError(txtLozinka);
        }

        private void btnAdresa_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeIzmenaAdrese addAdresa = new DodavanjeIzmenaAdrese(novaAdresa,odabranStatus);
            //addAdresa.Show();
            if ((bool)addAdresa.ShowDialog())
            {

            }
        }
    }
}
