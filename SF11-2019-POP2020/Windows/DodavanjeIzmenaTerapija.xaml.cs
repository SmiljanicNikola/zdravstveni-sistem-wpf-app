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
    /// Interaction logic for DodavanjeIzmenaTerapija.xaml
    /// </summary>
    ///  
   
    public partial class DodavanjeIzmenaTerapija : Window
    {

        private EStatus odabranStatus;
        private Terapija odabranaTerapija;
        ObservableCollection<Lekar> lekari;
        ObservableCollection<Pacijent> pacijenti;


        private Lekar selektovanLekarCmb;
        private Pacijent selektovanPacijentCmb;



        public DodavanjeIzmenaTerapija(Terapija terapija, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = terapija;

            odabranaTerapija = terapija;
            odabranStatus = status;

            this.lekari = Util.Instance.Lekari;
            cmbLekari.ItemsSource = this.lekari;

            this.pacijenti = Util.Instance.Pacijenti;
            cmbPacijenti.ItemsSource = this.pacijenti;


            /*string jmbg = PacijentiLekara.jmbg;
            Lekar lekar = Util.Instance.lekarPoJmbg(jmbg);
            if(lekar != null)
            {
                lblLekarId.Visibility = Visibility.Hidden;
                cmbLekari.Visibility = Visibility.Hidden;
            }
            else
            {
                jmbg = SveTerapije.jmbg;
                if (jmbg != null)
                {
                    cmbLekari.Visibility = Visibility.Visible;
                    lblLekarId.Visibility = Visibility.Visible;
                }
            }*/




            if (status.Equals(EStatus.Izmeni) && terapija != null)
            {
                this.Title = "Izmeni Terapiju";
                cmbLekari.SelectedItem = terapija.Lekar;

                //Util.Instance.SacuvajEntitet(lekar);
                //Util.Instance.UpdateEntiteta(lekar);
                //Util.Instance.SacuvajEntitet(lekar);
                //Util.Instance.UpdateEntiteta(termin);

            }
            else
            {
                cmbLekari.SelectedItem = terapija.Lekar;

                this.Title = "Dodaj terapiju";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            /*string jmbg = PacijentiLekara.jmbg;
            Lekar lekar = Util.Instance.lekarPoJmbg(jmbg);
            Pacijent pacijent = PacijentiLekara.izabraniPacijent;
            if (lekar != null && pacijent != null)
            {
                Terapija terapija = new Terapija()
                {
                    Opis = txtOpis.Text,
                    Lekar = lekar,
                    Pacijent = pacijent,
                    Aktivan = true

                };
               
                int id = Util.Instance.SacuvajEntitet(terapija);
                terapija.Id = id;
                Util.Instance.Terapije.Add(terapija);

                this.Close();
            }*/
            
                if (odabranStatus.Equals(EStatus.Dodaj))
                {
                Terapija terap = new Terapija()
                {
                    Opis = txtOpis.Text,
                    Lekar = cmbLekari.SelectedItem as Lekar,
                    Pacijent = cmbPacijenti.SelectedItem as Pacijent,
                    Aktivan = true

                    };
                    int id = Util.Instance.SacuvajEntitet(terap);
                    terap.Id = id;
                    Util.Instance.Terapije.Add(terap);


                    this.Close();
                }
                else if (odabranStatus.Equals(EStatus.Izmeni))
                {
                    Util.Instance.UpdateEntiteta(odabranaTerapija);
                }

                this.Close();
           
        }


        private void cmbLekari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selektovanLekarCmb = cmbLekari.SelectedItem as Lekar;


        }

        private void cmbPacijenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selektovanPacijentCmb = cmbPacijenti.SelectedItem as Pacijent;

        }
    }
}
