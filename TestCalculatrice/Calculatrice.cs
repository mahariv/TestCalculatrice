using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestCalculatrice
{
    class Calculatrice : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChanged([CallerMemberName] String str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        private String previousResult;

        public String PreviouResult
        {
            get
            {
                if (previousResult == "0")
                    return "";

                return previousResult;
            }
            set
            {
                previousResult = value;
                NotifyChanged();
            }
        }

        private String currentResult;

        public String CurrentResult {
            get
            {
                return currentResult;
            }
            set
            {
                currentResult = value;
                NotifyChanged();
            }
        }

        private int indicateurCurrentResult;

        public int IndicateurCurrentResult
        {
            get
            {
                return indicateurCurrentResult = currentResult.Length;
            }
            set
            {
                indicateurCurrentResult = value;
                NotifyChanged();
            }
        }

        private Stack<String> listeOperation;

        public Stack<String> stackOperation {
            get
            {
                return listeOperation;
            }
            set
            {
                listeOperation = value;
                NotifyChanged();
            }
        }

        public Calculatrice()
        {
            this.previousResult = "0";
            this.currentResult = "0";
            this.indicateurCurrentResult = 0;
            this.stackOperation = new Stack<string>();
        }
    }
}
