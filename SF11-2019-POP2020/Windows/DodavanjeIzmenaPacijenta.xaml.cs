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
    /// Interaction logic for DodavanjeIzmenaPacijenta.xaml
    /// </summary>
    public partial class DodavanjeIzmenaPacijenta : Window
    {
        private EStatus odabranStatus;
        private Korisnik odabranKorisnik;
        private Adresa novaAdresa;
        public DodavanjeIzmenaPacijenta(Korisnik korisnik, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = korisnik;

            odabranKorisnik = korisnik;
            odabranStatus = status;

            ComboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();
            ComboBoxPol.ItemsSource = Enum.GetValues(typeof(EPol)).Cast<EPol>();

            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                novaAdresa = new Adresa();
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid()) { 
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
                        TipKorisnika = ETipKorisnika.PACIJENT,
                        Aktivan = true
                    };

                    int id = Util.Instance.SacuvajEntitet(k);
                    k.Id = id;

                    Util.Instance.Korisnici.Add(k);

                    Pacijent pacijent = new Pacijent();
                    pacijent.Korisnik = k;
                    pacijent.ListaTerapija = new ObservableCollection<Terapija>();
                    pacijent.Aktivan = true;
                    int id2 = Util.Instance.SacuvajEntitet(pacijent);
                    pacijent.Id = id2;

                    Util.Instance.Pacijenti.Add(pacijent);
                }
            this.Close();
            }
        }


        private bool IsValid()
        {
            return !Validation.GetHasError(txtEmail) && !Validation.GetHasError(txtJMBG) && !Validation.GetHasError(txtIme) && !Validation.GetHasError(txtPrezime) && !Validation.GetHasError(txtLozinka);
        }


        private void btnAdresa_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeIzmenaAdrese addAdresa = new DodavanjeIzmenaAdrese(novaAdresa,odabranStatus);
            if ((bool)addAdresa.ShowDialog())
            {

            }
        }
    }
}
