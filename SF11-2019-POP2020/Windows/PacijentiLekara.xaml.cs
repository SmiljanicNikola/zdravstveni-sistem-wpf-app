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
    /// Interaction logic for PacijentiLekara.xaml
    /// </summary>
    public partial class PacijentiLekara : Window
    {
        ICollectionView viewPacijentiLekara;
        public static Pacijent izabraniPacijent;
        public static string jmbg;
        public static Pacijent pacijentt;

        public PacijentiLekara()
        {
            InitializeComponent();

            string jmbg = GlavnaStranicaLekar.jmbg;
            ObservableCollection<Pacijent> pacijentiLekara = Util.Instance.pacijentiPoLekarJmbg(jmbg);



            viewPacijentiLekara = CollectionViewSource.GetDefaultView(pacijentiLekara);

            UpdateView();
        }

       

        private void dataGridPacijentiLekara_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error") || e.PropertyName.Equals("Termini") || e.PropertyName.Equals("ListaTerapija"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void UpdateView()
        {
            string jmbg = GlavnaStranicaLekar.jmbg;
            
            //viewTermini = CollectionViewSource.GetDefaultView();
            dataGridPacijentiLekara.ItemsSource = viewPacijentiLekara;
            dataGridPacijentiLekara.IsSynchronizedWithCurrentItem = true;
            dataGridPacijentiLekara.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            //view.Filter = customFilter;

        }

        private void dataGridPacijentiLekara_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGridPacijentiLekara_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void MenuItemKarton_Click(object sender, RoutedEventArgs e)
        {
            Pacijent izabranPacijent = viewPacijentiLekara.CurrentItem as Pacijent;
            //jmbg = GlavnaStranicaLekar.jmbg;

            if (izabranPacijent != null)
            {
                pacijentt = izabraniPacijent;
                //izabraniPacijent = izabranPacijent;
                SveTerapije st = new SveTerapije();
                st.Show();

            }
        }

        private void MenuItemDodajTerapiju_Click(object sender, RoutedEventArgs e)
        {
          
            Pacijent izabranPacijent = viewPacijentiLekara.CurrentItem as Pacijent;
            jmbg = GlavnaStranicaLekar.jmbg;
            if (izabranPacijent != null)
            {
                izabraniPacijent = izabranPacijent;
                Terapija novaTerapija = new Terapija();
                DodavanjeTerapijaVerzija2 addTerapija = new DodavanjeTerapijaVerzija2(novaTerapija, EStatus.Dodaj);
                addTerapija.Show();

            }
            UpdateView();
            viewPacijentiLekara.Refresh();
        }
    }
}
