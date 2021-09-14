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
        ObservableCollection<Termin> termini;
        public TerminiPojedinacnogLekara(ObservableCollection<Termin> termini)
        {
            InitializeComponent();

            this.termini = termini;
            string jmbg = textBlock1.Text.Trim();
            Console.WriteLine(jmbg);

            this.termini = Util.Instance.terminiByLekarJmbg(jmbg);
            //view = CollectionViewSource.GetDefaultView(Util.Instance.terminiByLekarJmbg(jmbg));

            UpdateView();

            //view.Filter = customFilter;
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaLekar gsl = new GlavnaStranicaLekar();

            gsl.Show();
            this.Close();
        }
        public ObservableCollection<Termin> terminiByLekarJmbg(string jmbg)
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
        }

        private void UpdateView()
        {
            string jmbg = textBlock1.Text.Trim();
            popuni();
            ObservableCollection<Termin> termini = Util.Instance.Termini;
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
                
            }
            viewTermini = CollectionViewSource.GetDefaultView(privatni);
            DataGridTerminiLekara.ItemsSource = viewTermini;
            DataGridTerminiLekara.IsSynchronizedWithCurrentItem = true;
            DataGridTerminiLekara.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


        }

        private void popuni()
        {
            string jmbg = textBlock1.Text.Trim();
            Console.WriteLine(jmbg);

            this.termini = Util.Instance.terminiByLekarJmbg(jmbg);
           
        }

        private void DataGridTermini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridTermini_AutoGeneratingColumn(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItemObrisiTermin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDodajTermin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
