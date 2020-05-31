using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace TestCalculatrice
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainCalculatrice calculatrice;
        public MainWindow()
        {
            
            Loaded += MainWindow_Loaded;

            InitializeComponent();
            
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            calculatrice = new MainCalculatrice();
            
            calculatrice.Show();
            this.Close();

        }


    }
}