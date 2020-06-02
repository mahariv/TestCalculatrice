using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TestCalculatrice.BDDservice;

namespace TestCalculatrice
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow() 
        {
            InitializeComponent();
        }


        private void OpenCalculatrice_Click(object sender, RoutedEventArgs e)
        {

            FicheUtilisateurs utilisateur = ((MainCalculatriceViewModel)this.DataContext).Utilisateur;
            //on verifie que l'utilisateur est selectionné ou qu'il existe
            if (((MainCalculatriceViewModel)this.DataContext).ListeUtilisateur.Contains(utilisateur))
            {
                Calculate page = new Calculate();

                using (var bdd = new Service1Client())
                {
                    String nomUtilisateur = ((MainCalculatriceViewModel)this.DataContext).Utilisateur.Nom;
                    ((CalculatriceViewModel)page.DataContext).Utilisateur = utilisateur;
                    ((CalculatriceViewModel)page.DataContext).ListeOperation = new ObservableCollection<FicheOperations>(bdd.GetOperation(nomUtilisateur));
                }
                page.Show();
            }
            else
            {
                MessageBox.Show("Vous devez selectionner un utilisateur pour continuer");
            }

        }

        private void ListBoxItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem ab = (ListBoxItem)sender;
            FicheUtilisateurs fa = (FicheUtilisateurs)ab.Content;

            ((MainCalculatriceViewModel)this.DataContext).Utilisateur = fa;

        }
    }
}
