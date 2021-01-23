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
    /// Interaction logic for PrikazDomovaBezCRUD.xaml
    /// </summary>
    public partial class PrikazDomovaBezCRUD : Window
    {
        ICollectionView view;
        public PrikazDomovaBezCRUD()
        {
            InitializeComponent();

            UpdateView();


           // ComboBoxMesto.ItemsSource = typeof(Adresa).Cast<Adresa>();
        }
        private void UpdateView()
        {

            view = CollectionViewSource.GetDefaultView(Util.Instance.DomoviZdravlja);
            DataGridDomZdravlja.ItemsSource = view;
            DataGridDomZdravlja.IsSynchronizedWithCurrentItem = true;
            DataGridDomZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


        }

        private void DataGridTermini_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }




    
    }
}
