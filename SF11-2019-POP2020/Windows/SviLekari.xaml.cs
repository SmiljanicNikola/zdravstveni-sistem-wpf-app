using SF11_2019_POP2020.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SviLekari.xaml
    /// </summary>
    public partial class SviLekari : Window
    {
        public SviLekari()
        {
            InitializeComponent();

            UpdateView();
        }

        private void UpdateView()
        {
            DataGridLekari.ItemsSource = null;
            DataGridLekari.ItemsSource = Util.Instance.Lekari;
            DataGridLekari.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DataGridLekari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridLekari_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }
    }
}
