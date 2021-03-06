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
    /// Interaction logic for DodavanjeIzmenaTermina.xaml
    /// </summary>
    public partial class DodavanjeIzmenaTermina : Window
    {
        private EStatus odabranStatus;
        private Termin odabranTermin;
        ObservableCollection<Lekar> lekari;

        private Lekar selektovanLekarCmb;

        public DodavanjeIzmenaTermina(Termin termin,  EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = termin;

            odabranTermin = termin;
            odabranStatus = status;

            this.lekari = Util.Instance.Lekari;
            cmbLekari.ItemsSource = this.lekari;

            ComboBoxStatusTermina.ItemsSource = Enum.GetValues(typeof(EStatusTermina)).Cast<EStatusTermina>();

            if (status.Equals(EStatus.Izmeni) && termin != null)
            {
                cmbLekari.SelectedItem = odabranTermin.Lekar;
                this.Title = "Izmena termina";
            }
            else
            {
                cmbLekari.SelectedItem = odabranTermin.Lekar;
                this.Title = "Dodavanje termina";
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            odabranTermin.Lekar = cmbLekari.SelectedItem as Lekar;
            odabranTermin.Datum = (DateTime)dateDatum.SelectedDate;
            odabranTermin.StatusTermina = EStatusTermina.SLOBODAN;
            odabranTermin.Aktivan = true;

            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                Termin t = new Termin()
                {
                    Lekar = cmbLekari.SelectedItem as Lekar,
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
            this.Close();
        }

        private void cmbLekari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selektovanLekarCmb = cmbLekari.SelectedItem as Lekar;
        }
    }
}
