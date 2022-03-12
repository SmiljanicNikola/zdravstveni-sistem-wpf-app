using SF11_2019_POP2020.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SF11_2019_POP2020.Windows
{
    /// <summary>
    /// Interaction logic for PrikazDezurstava.xaml
    /// </summary>
    public partial class PrikazDezurstava : Window
    {
        ICollectionView view;
  
        public PrikazDezurstava()
        {
            InitializeComponent();
            Util.Instance.CitanjeEntiteta("dezurstva");
            view = CollectionViewSource.GetDefaultView(Util.Instance.Dezurstva);

            UpdateView();
        }

        private void UpdateView()
        {
            dataGridDezurstva.ItemsSource = view;
            dataGridDezurstva.IsSynchronizedWithCurrentItem = true;
            dataGridDezurstva.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void dataGridDezurstva_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void dataGridDezurstva_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        private void dataGridDezurstva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
