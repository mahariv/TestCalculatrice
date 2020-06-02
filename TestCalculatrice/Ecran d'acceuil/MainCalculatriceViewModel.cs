using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestCalculatrice.BDDservice;

namespace TestCalculatrice
{


    class MainCalculatriceViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChange([CallerMemberName] string str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        private FicheUtilisateurs utilisateur;

        public FicheUtilisateurs Utilisateur
        {
            get
            {
                return utilisateur;
            }
            set
            {
                if (utilisateur != value)
                {
                    utilisateur = value;
                    NotifyChange();
                }
            }
        }

        private ObservableCollection<FicheUtilisateurs> listeUtilisateur;

        public ObservableCollection<FicheUtilisateurs> ListeUtilisateur
        {
            get {
                
                return listeUtilisateur;
            }
            set
            {
                if (listeUtilisateur != value)
                {
                    listeUtilisateur = value;
                    NotifyChange();
                }
            }
        }

        public MainCalculatriceViewModel()
        {
            using (var bdd = new Service1Client())
            {
                listeUtilisateur = new ObservableCollection<FicheUtilisateurs>(bdd.GetUtilisateurs());
            }
            Utilisateur = new FicheUtilisateurs();
        }


    }
}
