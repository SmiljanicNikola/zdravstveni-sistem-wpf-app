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
    /// Interaction logic for SviTermini.xaml
    /// </summary>
    public partial class SviTermini : Window
    {
        ICollectionView view;

        public SviTermini()
        {
            InitializeComponent();

            UpdateView();


        }

        private void UpdateView()
        {

            view = CollectionViewSource.GetDefaultView(Util.Instance.Termini);
            DataGridTermini.ItemsSource = view;
            DataGridTermini.IsSynchronizedWithCurrentItem = true;
            DataGridTermini.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


        }

        private void DataGridTermini_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MenuItemDodajTermin_Click(object sender, RoutedEventArgs e)
        {
            Termin noviTermin = new Termin();
            DodavanjeIzmenaTermina addTermin = new DodavanjeIzmenaTermina(noviTermin, EStatus.Dodaj);
            addTermin.Show();

            this.Hide();
            //if ((bool)addAdmin.ShowDialog())
            //{

            //}
            this.Show();
            view.Refresh();
        }

        private void MenuItemIzmeniTermin_Click(object sender, RoutedEventArgs e)
        {
            //Korisnik izabraniLekar = (Korisnik)DataGridLekari.SelectedItem;
            Termin izabraniTermin = view.CurrentItem as Termin;
            Termin stariTermin = izabraniTermin.Clone();
            DodavanjeIzmenaTermina addTermin = new DodavanjeIzmenaTermina(izabraniTermin, EStatus.Izmeni);

            this.Hide();
            if ((bool)addTermin.ShowDialog())
            {
                /*int index = Util.Instance.Termini.ToList().FindIndex(t => t.Id.Equals(izabraniTermin.Id));

                //Util.Instance.Korisnici[index] = stariLekar;
                Util.Instance.Termini[index].Id = izabraniTermin.Id;
                Util.Instance.Termini[index].LekarId = izabraniTermin.LekarId;
                Util.Instance.Termini[index].Datum = izabraniTermin.Datum;
                Util.Instance.Termini[index].StatusTermina = izabraniTermin.StatusTermina;
                Util.Instance.Termini[index].PacijentId = izabraniTermin.PacijentId;
                Util.Instance.Termini[index].Aktivan = izabraniTermin.Aktivan;*/

                //Util.Instance.UpdateEntiteta(izabraniTermin);
            }
                

           
            this.Show();
            view.Refresh();
        }

        private void MenuItemObrisiTermin_Click(object sender, RoutedEventArgs e)
        {
            Termin izabraniTermin = view.CurrentItem as Termin;
            Util.Instance.DeleteTermin(izabraniTermin.Id);

            Util.Instance.Termini.Remove(izabraniTermin);
            UpdateView();
            view.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
            this.Hide();
            gsa.Show();
        }
    }
}
