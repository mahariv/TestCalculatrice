using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestCalculatrice.BDDservice;

namespace TestCalculatrice
{
    class EcranUtilisateurViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChange([CallerMemberName] String str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        private FicheUtilisateurs utilisateur;

        public FicheUtilisateurs Utilisateur {
            get { return utilisateur; }
            set {
                if (utilisateur != value)
                {
                    utilisateur = value;
                    NotifyChange();
                }
            }
        }
        public EcranUtilisateurViewModel()
        {
            utilisateur = new FicheUtilisateurs();
        }

        private ICommand ajoutUtilisateur;

        public ICommand AjoutUtilisateur
        {
            get {
                if (ajoutUtilisateur == null)
                {
                    ajoutUtilisateur = new RelayCommand<FicheUtilisateurs>(
                        (elt) =>
                        {
                            using (var entities = new Service1Client())
                            {
                                entities.AjoutUtilisateur(this.Utilisateur);
                            }
                            // fonction qui verifie que l'utilisateur n'existe pas deja
                            // fonction qui ajoute l'utilisateur
                        }
                        );
                }
                return ajoutUtilisateur;
            }
        }

        private ICommand supprimerUtilisateur;

        public ICommand SupprimerUtilisateur {
            get {
                if (supprimerUtilisateur == null)
                {
                    supprimerUtilisateur = new RelayCommand<FicheUtilisateurs>(
                        (elt) => {
                            using (var entities = new Service1Client()) {
                                entities.SupprimerUtilisateur(this.Utilisateur);
                                InitialisationFenetre();
                            }
                        }
                        );
                }
                return supprimerUtilisateur;
            }
        }

        private ICommand editerUtilisateur;

        public ICommand EditerUtilisateur
        {
            get
            {
                if (editerUtilisateur == null)
                {
                    editerUtilisateur = new RelayCommand<FicheUtilisateurs>(
                        (elt) => {
                            if (this.Utilisateur.Id > 0)
                            using (var entities = new Service1Client())
                            {
                                entities.EditerUtilisateur(this.Utilisateur);
                            }
                        }
                        );
                }
                return editerUtilisateur;
            }
        }

        private ICommand initFenetre;

        public ICommand InitFenetre
        {
            get
            {
                if (initFenetre == null)
                {
                    initFenetre = new RelayCommand<object>(
                        (elt) => {
                            InitialisationFenetre();
                        }
                        );
                }
                return initFenetre;
            }
        }

        public void InitialisationFenetre()
        {
            Utilisateur.Id = -1;
            Utilisateur.Login = "";
            Utilisateur.Nom = "";
            Utilisateur.Prenom = "";
        }



    }
}
