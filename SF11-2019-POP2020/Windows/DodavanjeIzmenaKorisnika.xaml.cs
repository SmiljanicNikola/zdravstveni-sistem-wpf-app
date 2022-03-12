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
        ObservableCollection<DomZdravlja> domoviZdravlja;
        private DomZdravlja selektovanDomZdravljaComboBox;


        public DodavanjeIzmenaKorisnika(Korisnik korisnik, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = korisnik;

            odabranKorisnik = korisnik;
            odabranStatus = status;

            this.domoviZdravlja = Util.Instance.DomoviZdravlja;
            cmbDomoviZdravlja.ItemsSource = this.domoviZdravlja;

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

            if (checkBox1.IsChecked == true)
            {
                cmbDomoviZdravlja.Visibility = Visibility.Hidden;
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
                   
                    int id = Util.Instance.SacuvajEntitet(k);
                    k.Id = id;
                    Util.Instance.Korisnici.Add(k);
                    if (k.TipKorisnika.Equals(ETipKorisnika.LEKAR))
                    {
                        Lekar lekar = new Lekar();
                        lekar.Korisnik = k;
                        lekar.DomZdravlja = cmbDomoviZdravlja.SelectedItem as DomZdravlja;
                        lekar.Termini = "|";
                        lekar.Aktivan = true;
                        int id2 = Util.Instance.SacuvajEntitet(lekar);
                        lekar.Id = id2;
                        Util.Instance.Lekari.Add(lekar);
                    }
                }
            else if (odabranStatus.Equals(EStatus.Izmeni))
                {
                    Util.Instance.UpdateEntiteta(odabranKorisnik);
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
            //addAdresa.Show();
            if ((bool)addAdresa.ShowDialog())
            {

            }
        }

        private void cmbDomoviZdravlja_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            cmbDomoviZdravlja.Visibility = Visibility.Visible;
        }

        private void cmbDomoviZdravlja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selektovanDomZdravljaComboBox = cmbDomoviZdravlja.SelectedItem as DomZdravlja;

        }
    }
}
