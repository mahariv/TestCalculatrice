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

        private void ButtonAjout_Click(object sender, RoutedEventArgs e)
        {
            ButtonAjoutEdition("Ajout");

            #region supp
            //EcranUtilisateur ecran = new EcranUtilisateur();
            //((EcranUtilisateurViewModel)ecran.DataContext).Utilisateur = new FicheUtilisateurs();
            //this.Hide();
            //ecran.ShowAjoutButton();

            //ecran.ShowDialog();
            //using (var bdd = new Service1Client())
            //{
            //    ((MainCalculatriceViewModel)this.DataContext).ListeUtilisateur = new ObservableCollection<FicheUtilisateurs>(bdd.GetUtilisateurs());
            // }

            //this.Show();

            #endregion supp
        }

        private void ButtonEditer_Click(object sender, RoutedEventArgs e)
        {
            ButtonAjoutEdition("Edition");

            #region supp
            //FicheUtilisateurs utilisateur = ((MainCalculatriceViewModel)this.DataContext).Utilisateur;
            ////on verifie que l'utilisateur est selectionné ou qu'il existe
            //if (((MainCalculatriceViewModel)this.DataContext).ListeUtilisateur.Contains(utilisateur))
            //{
            //    EcranUtilisateur ecran = new EcranUtilisateur();
            //    ((EcranUtilisateurViewModel)ecran.DataContext).Utilisateur = utilisateur;
            //    this.Hide();
            //    ecran.ShowEditionButton();
            //    ecran.ShowDialog();
            //    using (var bdd = new Service1Client())
            //    {
            //        ((MainCalculatriceViewModel)this.DataContext).ListeUtilisateur = new ObservableCollection<FicheUtilisateurs>(bdd.GetUtilisateurs());
            //    }

            //    this.Show();
            //}
            //else
            //{
            //    MessageBox.Show("veuillez selectionner un client");
            //}

            #endregion supp
        }

        private void ButtonAjoutEdition(string mode)
        {
            EcranUtilisateur ecran = new EcranUtilisateur();
            if (mode == "Edition")
            {
                FicheUtilisateurs utilisateur = ((MainCalculatriceViewModel)this.DataContext).Utilisateur;
                //on verifie que l'utilisateur est selectionné ou qu'il existe
                if (((MainCalculatriceViewModel)this.DataContext).ListeUtilisateur.Contains(utilisateur) == false)
                {
                    MessageBox.Show("veuillez selectionner un client");
                    return;
                }
                else {
                    ((EcranUtilisateurViewModel)ecran.DataContext).Utilisateur = utilisateur;
                    ecran.ShowEditionButton();
                }
            }
            else // mode ajout
            {
                ((EcranUtilisateurViewModel)ecran.DataContext).Utilisateur = new FicheUtilisateurs();
                ecran.ShowAjoutButton();
            }
            
            this.Hide(); // masque l'ecran principal
            
            ecran.ShowDialog(); // attend la fermeture de ecran

            using (var bdd = new Service1Client())
            {
                ((MainCalculatriceViewModel)this.DataContext).ListeUtilisateur = new ObservableCollection<FicheUtilisateurs>(bdd.GetUtilisateurs());
            }

            this.Show();
        }
    }
}
