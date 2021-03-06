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
    /// Interaction logic for DodavanjaIzmenaDoktora.xaml
    /// </summary>
    public partial class DodavanjaIzmenaDoktora : Window
    {
        private EStatus odabranStatus;
        private Lekar odabranDoktor;
        private DomZdravlja selektovanDomZdravljaComboBox;
        ObservableCollection<DomZdravlja> domoviZdravlja;


        public DodavanjaIzmenaDoktora(Lekar lekar, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = lekar;

            odabranDoktor = lekar;
            odabranStatus = status;

            this.domoviZdravlja = Util.Instance.DomoviZdravlja;
            cmbDomoviZdravlja.ItemsSource = this.domoviZdravlja;


            if (status.Equals(EStatus.Izmeni) && lekar != null)
            {
                this.Title = "Izmeni doktora";
         
                lekar.DomZdravlja = cmbDomoviZdravlja.SelectedItem as DomZdravlja;
            }
            else
            {
                this.Title = "Dodaj doktora";
            }
        }

        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            odabranDoktor.DomZdravlja = cmbDomoviZdravlja.SelectedItem as DomZdravlja;
            odabranDoktor.Aktivan = true;

           
            if (odabranStatus.Equals(EStatus.Izmeni))
                {
                    Util.Instance.UpdateEntiteta(odabranDoktor);
                }
                this.Close();
            }
        

        private void cmbDomoviZdravlja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selektovanDomZdravljaComboBox = cmbDomoviZdravlja.SelectedItem as DomZdravlja;

        }
    }
}