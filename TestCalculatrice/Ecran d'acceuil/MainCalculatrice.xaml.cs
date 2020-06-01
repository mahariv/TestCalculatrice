using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TestCalculatrice.BDDservice;

namespace TestCalculatrice
{
    /// <summary>
    /// Logique d'interaction pour MainCalculatrice.xaml
    /// </summary>
    public partial class MainCalculatrice : Window, INotifyPropertyChanged

    {
        Calculate page;


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChange([CallerMemberName] string str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public MainCalculatrice()
        {
            
            InitializeComponent();
        }

        private void OpenCalculatrice_Click(object sender, RoutedEventArgs e)
        {
            page = new Calculate();
            using (var bdd = new Service1Client())
            {
                String nomUtilisateur = ((MainCalculatriceViewModel)this.DataContext).Utilisateur.Nom;
                ((CalculatriceViewModel)page.DataContext).Utilisateur = ((MainCalculatriceViewModel)this.DataContext).Utilisateur;
                ((CalculatriceViewModel)page.DataContext).ListeOperation = new ObservableCollection<FicheOperations>(bdd.GetOperation(nomUtilisateur));
            }
            page.Show();
            
        }

        private void ListBoxItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem ab = (ListBoxItem)sender;
            FicheUtilisateurs fa = (FicheUtilisateurs)ab.Content;
            
            ((MainCalculatriceViewModel)this.DataContext).Utilisateur = fa;

        }
    }
}
