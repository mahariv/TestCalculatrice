using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace TestMultiEcran
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Page1 : Page, INotifyPropertyChanged
    {
        public Page1()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged([CallerMemberName] string str ="")
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }


        private void ButtonConfig_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Page2());
            Information a = new Information();
            a.Name = "toto";
        }
    }
}
