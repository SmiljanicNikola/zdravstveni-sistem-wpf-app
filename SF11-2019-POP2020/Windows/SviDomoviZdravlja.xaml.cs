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
    /// Interaction logic for SviDomoviZdravlja.xaml
    /// </summary>
    public partial class SviDomoviZdravlja : Window
    {
        ICollectionView view;

        ObservableCollection<string> gradovi;

        public SviDomoviZdravlja()
        {
            InitializeComponent();

            textBLock2.Text = GlavnaStranicaLekar.jmbg;

            string jmbg = GlavnaStranicaLekar.jmbg;
            if(jmbg == null)
            {
                jmbg = GlavnaStranicaPacijent.jmbg;
                if(jmbg == null)
                {
                    jmbg = GlavnaStranicaAdministrator.jmbg;
                }
            }
            Korisnik kor = Util.Instance.korisnikPoJmbg(jmbg);

            if (kor == null)
            {
                menuAkcije.Visibility = Visibility.Hidden;
                btnPocetnaLekar.Visibility = Visibility.Hidden;
                btnPocetnaAdmin.Visibility = Visibility.Hidden;
                btnPocetnaPacijent.Visibility = Visibility.Hidden;
            }
            else
            { //ODAVDE
                if (kor.TipKorisnika.Equals(ETipKorisnika.LEKAR) || kor.TipKorisnika.Equals(ETipKorisnika.PACIJENT))
                {
                    txtPretragaPoNazivuInstitucije.Visibility = Visibility.Hidden;
                    txtPretragaPoUlici.Visibility = Visibility.Hidden;
                    menuAkcije.Visibility = Visibility.Hidden;
                    lblUlica.Visibility = Visibility.Hidden;
                    lblNazivInstituta.Visibility = Visibility.Hidden;
                }
                if (kor.TipKorisnika.Equals(ETipKorisnika.LEKAR))
                {
                    btnPocetnaNeulogovan.Visibility = Visibility.Hidden;
                    btnPocetnaAdmin.Visibility = Visibility.Hidden;
                    btnPocetnaPacijent.Visibility = Visibility.Hidden;
                }
                if (kor.TipKorisnika.Equals(ETipKorisnika.PACIJENT))
                {
                    btnPocetnaAdmin.Visibility = Visibility.Hidden;
                    btnPocetnaLekar.Visibility = Visibility.Hidden;
                    btnPocetnaNeulogovan.Visibility = Visibility.Hidden;
                }
                if (kor.TipKorisnika.Equals(ETipKorisnika.ADMINISTRATOR))
                {
                    btnPocetnaLekar.Visibility = Visibility.Hidden;
                    btnPocetnaPacijent.Visibility = Visibility.Hidden;
                    btnPocetnaNeulogovan.Visibility = Visibility.Hidden;
                }

            }//DOVDE

            view = CollectionViewSource.GetDefaultView(Util.Instance.DomoviZdravlja);


            this.gradovi = new ObservableCollection<string>(Util.Instance.DomoviZdravlja
                 .Select(domZdravlja => domZdravlja.Adresa.Grad)
                 .Distinct()
                 .Prepend("Izaberite grad...")
                 );

            cmbMesto.ItemsSource = this.gradovi;
            cmbMesto.SelectedIndex = 0;

            UpdateView();
        }

        private bool CustomFilter(object obj)
        {
            DomZdravlja domZdravlja = obj as DomZdravlja;

            if (domZdravlja.Aktivan)
                if (txtPretragaPoNazivuInstitucije.Text != "")
                {
                    return domZdravlja.NazivInstitucije.Contains(txtPretragaPoNazivuInstitucije.Text);
                }
            if(domZdravlja.Aktivan)
                if(txtPretragaPoUlici.Text != "")
                {
                    return domZdravlja.Adresa.Ulica.Contains(txtPretragaPoUlici.Text);
                }
                else
                    return true;

            return false;
        }

        private void UpdateView()
        {  
            DataGridDomoviZdravlja.ItemsSource = view;
            DataGridDomoviZdravlja.IsSynchronizedWithCurrentItem = true;
            DataGridDomoviZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            view.Filter = CustomFilter; 
        }

        private void MenuItemDodajDomZdravlja_Click(object sender, RoutedEventArgs e)
        {
            DomZdravlja noviDomZdravlja = new DomZdravlja();
            DodavanjeIzmenaDomovaZdravlja addDomZdravlja= new DodavanjeIzmenaDomovaZdravlja(noviDomZdravlja, EStatus.Dodaj);
            addDomZdravlja.Show();

            this.Hide();
            this.Show();
            UpdateView();
        }

        private void MenuItemIzmeniDomZdravlja_Click(object sender, RoutedEventArgs e)
        {
            DomZdravlja izabraniDom = view.CurrentItem as DomZdravlja;
            if (izabraniDom != null)
            {
                DomZdravlja stariDom = izabraniDom.Clone();
                DodavanjeIzmenaDomovaZdravlja addDomZdravlja = new DodavanjeIzmenaDomovaZdravlja(izabraniDom, EStatus.Izmeni);

                if ((bool)addDomZdravlja.ShowDialog())
                {
                   
                }
                UpdateView();
            }
        }

        private void MenuItemObrisiDomZdravlja_Click(object sender, RoutedEventArgs e)
        {

            DomZdravlja izabraniDom = view.CurrentItem as DomZdravlja;
            Util.Instance.DeleteDomZdravlja(izabraniDom.Id);
            Util.Instance.DomoviZdravlja.Remove(izabraniDom);

            UpdateView();
            view.Refresh();
        }

        private void DataGridDomoviZdravlja_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void btnPocetnaAdmin_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
            this.Hide();
            gsa.Show();
        }

        private void txtPretragaPoNazivuInstitucije_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtPretragaPoUlici_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void cmbMesto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string grad = cmbMesto.SelectedItem as string;

            if (grad.Contains("Izaberite grad"))
            {
                view = CollectionViewSource.GetDefaultView(Util.Instance.nadjiDomovePoMestu(""));
            }
            else
            {
                view = CollectionViewSource.GetDefaultView(Util.Instance.nadjiDomovePoMestu(grad));
            }

            UpdateView();
            view.Refresh();
        }

        private void btnPocetnaLekar_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaLekar gsl = new GlavnaStranicaLekar();
            this.Hide();
            gsl.Show();
        }

        private void btnPocetnaPacijent_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaPacijent gsp = new GlavnaStranicaPacijent();
            this.Hide();
            gsp.Show();
        }

        private void btnPocetnaNeulogovan_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow glavna = new HomeWindow();
            this.Hide();
            glavna.Show();
        }

        private void DataGridDomoviZdravlja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPocetnaNeulogovan_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
