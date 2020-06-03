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

namespace TestCalculatrice
{
    /// <summary>
    /// Logique d'interaction pour EcranUtilisateur.xaml
    /// </summary>
    public partial class EcranUtilisateur : Window
    {
        public EcranUtilisateur()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ShowEditionButton()
        {
            MasquerBoutton(BoutonReset);
            MasquerBoutton(BouttonAjout);
            //MasquerBoutton(BouttonQuitter);
            //MasquerBoutton(BouttonSauvegarde);
            //MasquerBoutton(BouttonSupprimer);
        }

        public void ShowAjoutButton()
        {
            //MasquerBoutton(BoutonReset);
            //MasquerBoutton(BouttonAjout);
            //MasquerBoutton(BouttonQuitter);
            MasquerBoutton(BouttonSauvegarde);
            MasquerBoutton(BouttonSupprimer);
        }

        public void MasquerBoutton(Button mybutton)
        {
            mybutton.IsEnabled = false;
            mybutton.Visibility = Visibility.Hidden;
        }
    }
}
