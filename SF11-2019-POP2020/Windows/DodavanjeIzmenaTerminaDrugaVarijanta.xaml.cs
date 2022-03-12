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
    /// Interaction logic for DodavanjeIzmenaTerminaDrugaVarijanta.xaml
    /// </summary>
    public partial class DodavanjeIzmenaTerminaDrugaVarijanta : Window
    {
        private EStatus odabranStatus;
        private Termin odabranTermin;
        ObservableCollection<Lekar> lekari;
        public DodavanjeIzmenaTerminaDrugaVarijanta(Termin termin, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();
            this.DataContext = termin;
            string jmbg = TerminiPojedinacnogLekara.jmbg;

            
            odabranTermin = termin;
            ComboBoxStatusTermina.ItemsSource = Enum.GetValues(typeof(EStatusTermina)).Cast<EStatusTermina>();
            odabranStatus = status;

            this.lekari = Util.Instance.Lekari;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string jmbg = TerminiPojedinacnogLekara.jmbg;
            odabranTermin.Lekar = Util.Instance.lekarPoJmbg(jmbg);
            odabranTermin.Datum = (DateTime)dateDatum.SelectedDate;
            odabranTermin.StatusTermina = EStatusTermina.SLOBODAN;
            odabranTermin.Aktivan = true;

            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                Termin t = new Termin()
                {
                    Lekar = Util.Instance.lekarPoJmbg(jmbg),
                    Datum = (DateTime)dateDatum.SelectedDate,
                    StatusTermina = EStatusTermina.SLOBODAN,
                    Aktivan = true
                };
                
                int id = Util.Instance.SacuvajEntitet(t);
                t.Id = id;
                Util.Instance.Termini.Add(t);
   
                this.Close();
            }
            else
            {
                Util.Instance.UpdateEntiteta(odabranTermin);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxStatusTermina_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
