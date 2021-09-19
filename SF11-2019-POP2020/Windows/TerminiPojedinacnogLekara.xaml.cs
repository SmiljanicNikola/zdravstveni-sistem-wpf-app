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
    /// Interaction logic for TerminiPojedinacnogLekara.xaml
    /// </summary>
    public partial class TerminiPojedinacnogLekara : Window
    {
        ICollectionView viewTermini;
        //ObservableCollection<Termin> termini;
        ObservableCollection<string> datumi;
        public static string jmbg;
        public TerminiPojedinacnogLekara()
        {
            InitializeComponent();

            jmbg = GlavnaStranicaLekar.jmbg;
            Console.WriteLine(jmbg);
            ObservableCollection<Termin> privatniTermini = Util.Instance.terminiByLekarJmbg(jmbg);

            this.datumi = new ObservableCollection<string>(privatniTermini
                .Select(lekar => lekar.Datum.ToString())
                .Distinct()
                .Prepend("Izaberite datum")
                );

            cmbDatum.ItemsSource = this.datumi;
            cmbDatum.SelectedIndex = 0;


            

           
            viewTermini = CollectionViewSource.GetDefaultView(privatniTermini);

            UpdateView();

            //view.Filter = customFilter;
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaLekar gsl = new GlavnaStranicaLekar();

            gsl.Show();
            this.Close();
        }
        /*public ObservableCollection<Termin> terminiByLekarJmbg(string jmbg)
        {
            ObservableCollection<Termin> privatniTermini = new ObservableCollection<Termin>();

            foreach (Termin t in Util.Instance.Termini)
            {
                if (t.Lekar.Korisnik.Jmbg == jmbg)
                {
                    privatniTermini.Add(t);
                }
            }
            return privatniTermini;
        }*/

        private void UpdateView()
        {
            string jmbg = GlavnaStranicaLekar.jmbg;
            /*ObservableCollection<Termin> termini = Util.Instance.Termini;
            ObservableCollection<Lekar> lekari = Util.Instance.Lekari;
            ObservableCollection<Termin> privatni = new ObservableCollection<Termin>();

            foreach (Termin terminn in termini)
            {
                foreach(Lekar lekarr in lekari)
                {
                    if(terminn.Lekar == lekarr)
                    {
                        if(lekarr.Korisnik.Jmbg == jmbg)
                        {
                            privatni.Add(terminn);
                        }
                    }
                }
                
            }*/
            //viewTermini = CollectionViewSource.GetDefaultView();
            DataGridTerminiLekara.ItemsSource = viewTermini;
            DataGridTerminiLekara.IsSynchronizedWithCurrentItem = true;
            DataGridTerminiLekara.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            //view.Filter = customFilter;

        }

        private void DataGridTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridTerminiLekara_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }


        private void MenuItemObrisiTermin_Click(object sender, RoutedEventArgs e)
        {
            Termin izabraniTermin = viewTermini.CurrentItem as Termin;
            if (izabraniTermin.StatusTermina.Equals(EStatusTermina.SLOBODAN))
            {

                Util.Instance.DeleteTermin(izabraniTermin.Id);
                Util.Instance.Termini.Remove(izabraniTermin);
            }
            else { 
            }
            string jmbg = GlavnaStranicaLekar.jmbg;
            Console.WriteLine(jmbg);
            ObservableCollection<Termin> privatniTermini = Util.Instance.terminiByLekarJmbg(jmbg);
            viewTermini = CollectionViewSource.GetDefaultView(privatniTermini);
            UpdateView();
            viewTermini.Refresh();
        }

        private void MenuItemDodajTermin_Click(object sender, RoutedEventArgs e)
        {
            Termin noviTermin = new Termin();
            DodavanjeIzmenaTerminaDrugaVarijanta addTermin = new DodavanjeIzmenaTerminaDrugaVarijanta(noviTermin, EStatus.Dodaj);
            addTermin.Show();

            this.Hide();
            //if ((bool)addAdmin.ShowDialog())
            //{

            //}
            this.Show();
            UpdateView();
            viewTermini.Refresh();
        }

        private void cmbDatum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbDatum_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string datum = cmbDatum.SelectedItem as string;
            string jmbg = GlavnaStranicaLekar.jmbg;


            if (datum.Contains("Izaberite datum"))
            {
                viewTermini = CollectionViewSource.GetDefaultView(Util.Instance.terminiByLekarJmbg(jmbg));
            }
            else
            {
                viewTermini = CollectionViewSource.GetDefaultView(Util.Instance.nadjiTerminePoDatumu(datum));
            }

            UpdateView();
            viewTermini.Refresh();
        }

     
    }
}
