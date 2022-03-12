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
    /// Interaction logic for SveTerapije.xaml
    /// </summary>
    public partial class SveTerapije : Window
    {

        ICollectionView view;

        ObservableCollection<string> lekari;

        public static string jmbg;
        public SveTerapije()
        {
            InitializeComponent();

            Pacijent pacijenttt = PacijentiLekara.pacijentt;
            if (pacijenttt != null)
            {
                string jmbg1 = pacijenttt.Korisnik.Jmbg;
                ObservableCollection<Terapija> terapijePacijenta = Util.Instance.nadjiTerapijePoJmbgPacijenta(jmbg1);
                view = CollectionViewSource.GetDefaultView(terapijePacijenta);
                menuItemAkcije.Visibility = Visibility.Hidden;
            }
            else
            {

                string jmbg = GlavnaStranicaPacijent.jmbg;
                if (jmbg != null)
                {
                    ObservableCollection<Terapija> terapijePacijenta = Util.Instance.nadjiTerapijePoJmbgPacijenta(jmbg);
                    view = CollectionViewSource.GetDefaultView(terapijePacijenta);

                    Korisnik kor = Util.Instance.korisnikPoJmbg(jmbg);

                    if (kor.TipKorisnika.Equals(ETipKorisnika.PACIJENT))
                    {
                        menuItemAkcije.Visibility = Visibility.Hidden;
                    }

                }
                else
                {
                    view = CollectionViewSource.GetDefaultView(Util.Instance.Terapije);
                } 
            }
            UpdateView();
        }

        private void UpdateView()
        {
            DataGridTerapije.ItemsSource = view;
            DataGridTerapije.IsSynchronizedWithCurrentItem = true;
            DataGridTerapije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            Terapija terapija = obj as Terapija;

            if (terapija.Aktivan)
                if (txtPretragaPoOpisu.Text != "")
                {
                    return terapija.Opis.Contains(txtPretragaPoOpisu.Text);
                }
            if (terapija.Aktivan)
                if (txtPretragaPoLekaru.Text != "")
                {
                    return terapija.Lekar.Korisnik.Ime.Contains(txtPretragaPoLekaru.Text);
                }
            if(terapija.Aktivan)
                if(txtPretragaPoLekaruPrezime.Text != "")
                {
                    return terapija.Lekar.Korisnik.Prezime.Contains(txtPretragaPoLekaruPrezime.Text);
                }
            if (terapija.Aktivan)
                if (txtPretragePoLekaruEmail.Text != "")
                {
                    return terapija.Lekar.Korisnik.Email.Contains(txtPretragePoLekaruEmail.Text);
                }
                else
                    return true;
            return false;
        }

        private void DataGridTerapije_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MenuItemObrisiTerapiju_Click(object sender, RoutedEventArgs e)
        {
            Terapija izabranaTerapija = view.CurrentItem as Terapija;
            Util.Instance.DeleteTerapija(izabranaTerapija.Id);
            Util.Instance.Terapije.Remove(izabranaTerapija);

            UpdateView();
            view.Refresh();
        }

        private void MenuItemIzmeniTerapiju_Click(object sender, RoutedEventArgs e)
        {
            Terapija izabranaTerapija = view.CurrentItem as Terapija;

            if (izabranaTerapija != null)
            {
                Terapija staraTerapija = izabranaTerapija.Clone();
                DodavanjeIzmenaTerapija addterapije = new DodavanjeIzmenaTerapija(izabranaTerapija, EStatus.Izmeni);
                if ((bool)addterapije.ShowDialog())
                {
                    
                }
                view.Refresh();
            }
        }

        private void txtPretragaPoOpisu_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void txtPretragaPoLekaru_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
        private void txtPretragaPoLekaruPrezime_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void MenuItemDodajTerapiju_Click(object sender, RoutedEventArgs e)
        {
            jmbg = GlavnaStranicaAdministrator.jmbg;
            Terapija novaTerapija = new Terapija();
            DodavanjeIzmenaTerapija addTerapija = new DodavanjeIzmenaTerapija(novaTerapija, EStatus.Dodaj);
            addTerapija.Show();

            this.Hide();
            this.Show();
            view.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
            this.Hide();
            gsa.Show();
        }

        private void txtPretragePoLekaruEmail_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void DataGridTerapije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void menuItemAkcije_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
