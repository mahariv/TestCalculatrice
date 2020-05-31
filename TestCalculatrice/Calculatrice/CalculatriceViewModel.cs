using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestCalculatrice.BDDservice;

namespace TestCalculatrice
{
    class CalculatriceViewModel :INotifyPropertyChanged
    {

        // Attribus
        #region Attribus
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
                    NotifyChanged();
                }
            }
        }


        private Calculatrice myCalculatrice;

        public Calculatrice MyCalculatrice
        {
            get { return myCalculatrice; }
            set { 
                myCalculatrice = value;
                NotifyChanged();
            }
        }

        private ObservableCollection<FicheOperations> listeOperation;

        public ObservableCollection<FicheOperations> ListeOperation 
        {
            get { return listeOperation; }
            set
            {
                if (listeOperation != value)
                {
                    listeOperation = value;
                    NotifyChanged();
                }
            }
        }

        #endregion Attribus

        //evenement de notification

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChanged([CallerMemberName] String str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }
        public CalculatriceViewModel()
        {

            myCalculatrice = new Calculatrice();
            utilisateur = new FicheUtilisateurs();
            
        }


        //command

        private ICommand editerZone;
        public ICommand EditerZone
        {
            get
            {
                if (editerZone == null)
                {
                    editerZone = new RelayCommand<Calculatrice>((obj) =>
                    {
                        myCalculatrice.PreviouResult = "0";
                        myCalculatrice.CurrentResult = "0";
                        myCalculatrice.IndicateurCurrentResult = 0;
                        myCalculatrice.stackOperation.Clear();
                    });
                }
                return editerZone;
            }
        }

        private ICommand EditeButon(string number)
        {
            ICommand buttonEdition = new RelayCommand<Calculatrice>((obj) =>
            {
                string temp = MyCalculatrice.CurrentResult + number;

                if (IsNumber(number))
                {
                    temp = VerifZero(temp);
                }
                else if (!VerifDot(temp))
                {
                    temp = ClearLastChar(temp);
                }


                MyCalculatrice.CurrentResult = temp;
            }); 
            return buttonEdition;
        }

        private string VerifZero(string expression)
        {
            int indicateur = expression.Length;
           
            if (indicateur == 2)
            {
                if (expression[indicateur - 2] == '0' && (expression[indicateur - 1] != ',' || expression[indicateur - 1] == '0'))
                {
                    // on retire le premier chiffre
                     return ClearFirstChar(expression);
                }
            }
            else if (expression.Length > 2)
            {


                if (expression[indicateur - 3] == ' ' && expression[indicateur - 2] == '0' && (expression[indicateur - 1] != ',' || expression[indicateur - 1] == '0'))
                {
                    // on retire l'avant dernier chiffre
                    return ClearPreviousChar(expression);
                }
            }
            
            return expression ;
            
        }

        private bool VerifDot(string expression)
        {
            int indicateur = expression.Length - 1;
            
            if (indicateur > 0)
            {
                while (expression[indicateur] != ' ' && indicateur > 0)
                {
                    indicateur--;
                    if (expression[indicateur] == ',')
                    { 
                        return false;
                    }
                    
                }
            }
            return true;
        }

        #region button of the number


        private ICommand button1;
        
        public ICommand Button1
        {
            get
            {
                if (button1 == null)
                {
                    button1 = EditeButon("1");
                }
                return button1;
            }
        }


        private ICommand button2;
        public ICommand Button2
        {
            get
            {
                if (button2 == null)
                {
                    button2 = EditeButon("2");
                }
                return button2;
            }
        }


        private ICommand button3;
        public ICommand Button3
        {
            get
            {
                if (button3 == null)
                {
                    button3 = EditeButon("3");
                }
                return button3;
            }
        }


        private ICommand button4;
        public ICommand Button4
        {
            get
            {
                if (button4 == null)
                {
                    button4 = EditeButon("4");
                }
                return button4;
            }
        }


        private ICommand button5;
        public ICommand Button5
        {
            get
            {
                if (button5 == null)
                {
                    button5 = EditeButon("5");
                }
                return button5;
            }
        }


        private ICommand button6;
        public ICommand Button6
        {
            get
            {
                if (button6 == null)
                {
                    button6 = EditeButon("6");
                }
                return button6;
            }
        }


        private ICommand button7;
        public ICommand Button7
        {
            get
            {
                if (button7 == null)
                {
                    button7 = EditeButon("7");
                }
                return button7;
            }
        }


        private ICommand button8;
        public ICommand Button8
        {
            get
            {
                if (button8 == null)
                {
                    button8 = EditeButon("8");
                }
                return button8;
            }
        }


        private ICommand button9;
        public ICommand Button9
        {
            get
            {
                if (button9 == null)
                {
                    button9 = EditeButon("9");
                }
                return button9;
            }
        }


        private ICommand button0;
        public ICommand Button0
        {
            get
            {
                if (button0 == null)
                {
                    button0 = EditeButon("0");
                }
                return button0;
            }
        }

        private ICommand buttonDot;
        public ICommand ButtonDot
        {
            get
            {
                if (buttonDot == null)
                {
                    buttonDot = EditeButon(",");
                }
                return buttonDot;
            }
        }

        #endregion

        #region button of operation

        private ICommand buttonEgal;
        public ICommand ButtonEgal
        {
            get
            {
                if (buttonEgal == null)
                {
                    buttonEgal = new RelayCommand<Calculatrice>((obj) =>
                    {
                        AnalyseExpression(); // creer une liste de chiffre et d'operation
                        //indicateur a supprimer
                        myCalculatrice.stackOperation = ReverseStack(myCalculatrice.stackOperation);
                        //indicateur a supprimer
                        foreach (string ab in myCalculatrice.stackOperation)
                        {
                            Console.WriteLine(ab);
                        }
                        // avoir une fonctione qui gere les priorités dans une liste
                        myCalculatrice.stackOperation = PriorityGestion(myCalculatrice.stackOperation);
                        foreach (string ab in myCalculatrice.stackOperation)
                        {
                            Console.WriteLine(ab);
                        }
                        string result = IsNegative(TraitementStackOperation(myCalculatrice.stackOperation));
                        myCalculatrice.PreviouResult = myCalculatrice.CurrentResult;
                        myCalculatrice.CurrentResult = result;
                    });
                }
                return buttonEgal;
            }
        }

        String TraitementStackOperation(Stack<String> operation)
        {
            String result = "";
           
            string temp;
            
            while (operation.Count > 0)
            {
                temp = operation.Pop();
                Console.WriteLine("result : " + temp);
                if (IsNumber(temp[0].ToString()) || temp[0] == ',')
                {
                    if (result == "")
                    {
                        result = temp;
                        
                    }
                    else
                    {
                        // pas utile pour le moment
                    }
                }
                else if (IsOperation(temp))
                {

                    string peekStack;
                    if (operation.Count > 0)
                    {
                        peekStack = operation.Peek();
                    }
                    else
                    {
                        peekStack = "0";
                    }
                    result = Calcul(peekStack, temp, result);
                    Console.WriteLine(result);
                }

            }
            return result;
        }

        private String Calcul(string peekStack, string operation, string result)
        {
            string temp = peekStack ;
            if (result == "")
            {
                result = "0";
            }


            switch(operation)
            {
                case " + ":
                    {
                        if (!IsNumber(temp))
                        {
                            temp = "0";
                        }
                        result = (Convert.ToDouble(temp) + Convert.ToDouble(result)).ToString();
                        break;
                    }
                case " - ":
                    {
                        if (!IsNumber(temp))
                        {
                            temp = "0";
                        }
                        result =  (Convert.ToDouble(result) - Convert.ToDouble(temp)).ToString();
                        break;
                    }
                case " * ":
                    {
                        if (!IsNumber(temp))
                        {
                            temp = "0";
                        }
                        result = (Convert.ToDouble(result) * Convert.ToDouble(temp)).ToString();
                        break;

                    }
                case " / ":
                    {
                        if (!IsNumber(temp))
                        {
                            temp = "0";
                        }
                        result = (Convert.ToDouble(result) / Convert.ToDouble(temp)).ToString();
                        break;

                    }
                default:
                    break;
            }
            return result;
        }

        private ICommand buttonPlus;
        public ICommand ButtonPlus
        {
            get
            {
                if (buttonPlus == null)
                {
                    buttonPlus = EditeButon(" + ");
                }
                return buttonPlus;
            }
        }

        private ICommand buttonMoins;
        public ICommand ButtonMoins
        {
            get
            {
                if (buttonMoins == null)
                {
                    buttonMoins = EditeButon(" - ");
                    // rajouter le comportement du moins
                }
                return buttonMoins;
            }
        }

        private ICommand buttonMultiplication;
        public ICommand ButtonMultiplication
        {
            get
            {
                if (buttonMultiplication == null)
                {
                    buttonMultiplication = EditeButon(" * ");
                }
                return buttonMultiplication;
            }
        }

        private ICommand buttonDivision;
        public ICommand ButtonDivision
        {
            get
            {
                if (buttonDivision == null)
                {
                    buttonDivision = EditeButon(" / ");
                }
                return buttonDivision;
            }
        }

        private ICommand buttonClearPrevious;
        public ICommand ButtonClearPrevious
        {
            get
            {
                if (buttonClearPrevious == null)
                {
                    buttonClearPrevious = new RelayCommand<Calculatrice>((obj) =>
                    {
                        string temp = myCalculatrice.CurrentResult;
                        int indicateur = temp.Length;

                        if (indicateur > 0)
                        {
                            indicateur--;
                            string lastChar = temp[indicateur].ToString();
                            // on verifie que la derniere information saisie est un chiffre ou un point
                            if (IsNumber(lastChar) == true || lastChar==",")
                            {
                                myCalculatrice.CurrentResult = ClearLastChar(temp);
                            }
                            else if (lastChar == " ") // on supprime la dernier operation
                            {
                                myCalculatrice.CurrentResult = ClearPreviousOperation(temp);
                            }

                        }


                    });
                }
                return buttonClearPrevious;
            }
        }

        #endregion button of operation

        // incomplete
        private void AnalyseExpression()
        {
            String expression = myCalculatrice.CurrentResult;
            int count = expression.Length;
            int indicateur = 0;
            
            string temp;
            bool error = false;
            int indice;

            while (indicateur < count && error == false) // parcour tous les caracteres de la chaine
            {
                indice = 0;

                if (IsNumber(expression[indicateur].ToString()) || expression[indicateur] ==',')
                {
                    temp = RecupNumber(expression,indicateur, out indice).ToString();
                }
                else
                {
                    temp = RecupOperation(expression, indicateur, out indice);
                }

                indicateur += indice;
                // on insere dans une pile toutes les operations
                myCalculatrice.stackOperation.Push(temp);

                indicateur++;
            }
        }

        private bool IsOperation(String val)
        {
            switch (val)
            {
                case " + ":
                    {
                        return true;
                    }
                case " - ":
                    {
                        return true;
                    }
                case " / ":
                    {
                        return true;
                    }
                case " * ":
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            };
        }

        private bool IsNumber(String val)
        {

            if (val.Length > 1)
            {
                foreach (char valString in val)
                {
                    if (!IsNumber(valString.ToString()))
                    {
                        return false;
                    }
                }
                return true;
            }

            switch (val)
            {
                case "0":
                    {
                        return true;
                    }
                case "1":
                    {
                        return true;
                    }
                case "2":
                    {
                        return true;
                    }
                case "3":
                    {
                        return true;
                    }
                case "4":
                    {
                        return true;
                    }
                case "5":
                    {
                        return true;
                    }
                case "6":
                    {
                        return true;
                    }
                case "7":
                    {
                        return true;
                    }
                case "8":
                    {
                        return true;
                    }
                case "9":
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            };
        }

        private String ClearPreviousChar(string expression)
        {
            int taille = expression.Length;
            string temp = "";
            if (taille > 0 && expression[taille - 1] != ' ')
            {
                for (int i = 0; i < expression.Length - 2; i++)
                {
                    temp += expression[i];
                }
                temp += expression[taille - 1];
                return temp;
            }
            return expression;
        }

        private String ClearLastChar(string expression)
        {
            int taille = expression.Length;
            string temp = "";
            if (taille > 0 && expression[taille - 1] != ' ')
            {
                for (int i = 0; i < taille - 1; i++)
                {
                    temp += expression[i];
                }
                return temp;
            }
            return expression;
        }

        private String ClearFirstChar(string expression)
        {
            int taille = expression.Length;
            string temp = "";
            if (taille > 0)
            {
                for (int i = 0; i < expression.Length - 1; i++)
                {
                    temp += expression[i +1];
                }
                return temp;
            }
            return expression;
        }

        private String ClearPreviousOperation(string expression)
        {
            int indicateur = expression.Length;
            //Rappel: le dernier element d'une chaine = taille de la chiane - 1 car une chaine commence avec l'indice 0
            if (indicateur > 0 && expression[--indicateur] == ' ')
            {
                string temp="";

                while (expression.Length != 0 && expression[--indicateur] != ' ') { }

                for (int i = 0; i < indicateur; i++)
                {
                    temp += expression[i];
                }
                return temp;
            }
            return expression;
        }

        private double RecupNumber(String expression,int indicateur, out int indice)
        {
            int count = expression.Length;
            double number;
            string temp;
            indice = 1;
            temp = expression[indicateur].ToString();
            
            indicateur++;
            while (indicateur < count && expression[indicateur] != ' ')
            {
                
                temp += expression[indicateur].ToString();
                
                indicateur++;
                indice++;
            }
            indice--;
            Double.TryParse(temp, out number);

            return number;
        }

        private String RecupOperation(String expression, int indicateur, out int indice)
        {
            int count = expression.Length;
            string  operation="";
            
            indice = 1;
            if (expression[indicateur] == ' ')
            {
                operation = " ";
            }
            else
            {
                operation = expression[indicateur].ToString();
            }

            indicateur++;

            while (indicateur < count && expression[indicateur] != ' ')
            {

                if (expression[indicateur] == ' ')
                {
                    operation += " ";
                }
                else
                {
                    operation += expression[indicateur].ToString();
                }

                indicateur++;
                indice++;
            }
            if (expression[indicateur] == ' ')
            {
                operation += " ";
                indice++;
            }
            
            indice--;
            return operation;
        }

        private string IsNegative(string expression)
        {
            
            if (expression[0] == '-')
            {
                string temp = " - ";
                for (int i = 1; i < expression.Length; i++)
                {
                    temp += expression[i];
                }
                expression = temp;
            }
            return expression;
        }

        private Stack<String> ReverseStack(Stack<String> stackRevers)
        {
            Stack<String> temp = new Stack<string>();

            while (stackRevers.Count > 0)
            {
                temp.Push(stackRevers.Pop());
            }
            return stackRevers = temp;

        }

        private Stack<String> PriorityGestion(Stack<String> stackPriority)
        {
            Stack<String> tempStack = new Stack<string>();
            string temp;
            string tempPeek;
            while (stackPriority.Count > 0)
            {
                temp = stackPriority.Pop();
                if (stackPriority.Count > 0)
                {
                    tempPeek = stackPriority.Peek();


                    if (temp == " * ")
                    {
                        if (tempStack.Count > 0)
                        {
                            
                            string a = tempStack.Pop();
                            double ab = Convert.ToDouble(a) * Convert.ToDouble(tempPeek);
                            temp = ab.ToString();
                            stackPriority.Pop();
                        }
                    }
                    if (temp == " / ")
                    {
                        if (tempStack.Count > 0)
                        {
                            
                            string a = tempStack.Pop();
                            double ab = Convert.ToDouble(a) / Convert.ToDouble(tempPeek);
                            temp = ab.ToString();
                            stackPriority.Pop();
                        }
                    }
                }
                tempStack.Push(temp);
            }
            tempStack =  ReverseStack(tempStack);

            return tempStack;
        }
    }
}

