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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestCalculatrice.BDDservice;

namespace TestCalculatrice
{
    /// <summary>
    /// Logique d'interaction pour Page2.xaml
    /// </summary>
    public partial class Calculate : Window
    {
        public Calculate()
        {
            InitializeComponent();
        }

        private void ListBoxItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem ab = (ListBoxItem)sender;
            FicheOperations fa = (FicheOperations)ab.Content;

            ((CalculatriceViewModel)this.DataContext).MyCalculatrice.CurrentResult = fa.Operation;
        }
    }
}
