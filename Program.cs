using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace Bank
{
    class AccType
    {
        protected double Amount;
        protected string Type;
        public AccType()
        {
            Amount = 0;
        }

        public void View()
        {
            Console.WriteLine("\n\nCurrently have $" + Amount+"\n\n");
            Console.ReadLine();
        }
        public virtual void Withdraw(string w)
        {
            double amt;

            //Turn string amount into double so that is can be formatted with two decimal places then change back
            Double.TryParse(w, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out amt);
            w = amt.ToString("C", CultureInfo.CurrentCulture);
            w = w.Replace("$", "").Trim();

            if (Double.TryParse(w, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out amt) &&
                Amount > amt)
            {
                if (Amount > amt)
                {
                    Amount = Amount - amt;
                }
                else
                {
                    Console.WriteLine("Amount entered to large.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Unable to process amount.");
                Console.ReadLine();

            }
        }
        public void Deposit(string w)
        {
            double amt;

            //Turn string amount into double so that is can be formatted with two decimal places then change back
            Double.TryParse(w, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out amt);
            w = amt.ToString("C", CultureInfo.CurrentCulture);
            w = w.Replace("$", "").Trim();            
            
            if (Double.TryParse(w, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out amt))
            {
                Amount += amt;
            }
            else
            {
                Console.WriteLine("Unable to process amount.");
                Console.ReadLine();

            }
        }
        public void HomeScreen()
        {
            string input;
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("Viewing Checking Account" +
                                  "\nSelect an action:" +
                                  "\n1) View Amount" +
                                  "\n2) Withdraw" +
                                  "\n3) Deposit" +
                                  "\n4) Exit");
                input = Console.ReadLine();

                if (input == "1")
                {
                    View();
                }
                else if (input == "2")
                {
                    Console.WriteLine("How much do you want to take out?");
                    Withdraw(Console.ReadLine());
                }
                else if (input == "3")
                {
                    Console.WriteLine("How much you want to put in?");
                    Deposit(Console.ReadLine());
                }
                else if (input == "4" ||input == "")
                {
                    loop = false;
                }
            }
        }
    }
    class Checking : AccType
    {
        private string Type = "Checking";
    }
    class CorpInvest : AccType
    {
        private string Type = "Corporate Investment";
    }
    class IndivInvest : AccType
    {
        private string Type = "Individual Investment";
      

        public override void Withdraw(string w)
        {
            double amt;

            //Turn string amount into double so that is can be formatted with two decimal places then change back
            Double.TryParse(w, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out amt);
            w = amt.ToString("C", CultureInfo.CurrentCulture);
            w = w.Replace("$", "").Trim();

            if (Double.TryParse(w, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out amt) &&
                Amount > amt)
            {
                if (Amount > amt && amt <= 1000)
                {
                    Amount = Amount - amt;
                }
                else
                {
                    Console.WriteLine("Amount entered to large.");
                    Console.ReadLine();                }
            }
            else
            {
                Console.WriteLine("Unable to process amount.");
                Console.ReadLine();
            }
        }
    }
    class Account
    {
        private string Name;
        //private string Gender; Demographic information
        private Checking AccChecking;
        private CorpInvest AccCorporate;
        private IndivInvest AccIndividual;
        private IDictionary<string,AccType> AccountType = new Dictionary<string,AccType>();
        public Account(string name)
        {
            Name = name;
            AccChecking = new Checking();
            AccCorporate = new CorpInvest();
            AccIndividual = new IndivInvest();

            AccountType.Add("1", AccChecking);
            AccountType.Add("2", AccCorporate);
            AccountType.Add("3", AccIndividual);
        }

        

        public void HomeScreen()
        {
            string input;
            

            bool loop = true;
            while (loop)
            {    
                Console.Clear();
                Console.WriteLine("Hello "+Name+
                                  "\nWhat accounts do you want to view or transfer money between:\n");
                Console.WriteLine("1) Checking");
                Console.WriteLine("2) Corporate Investment");
                Console.WriteLine("3) Individual Investment");
                Console.WriteLine("4) Transfer");
                Console.WriteLine("5) Exit");
                input = Console.ReadLine();

                if(AccountType.ContainsKey(input))
                {
                    AccType Acc = AccountType[input];
                    Acc.HomeScreen();
                }
                else if(input == "4")
                {
                    string a1;
                    string a2;
                    string amt;
                    double amount;
                    Console.WriteLine("\nSelect two accounts:");
                    Console.WriteLine("1) Checking");
                    Console.WriteLine("2) Corporate Investment");
                    Console.WriteLine("3) Individual Investment");
                    Console.WriteLine("\n Select first account:");
                    a1 = Console.ReadLine();
                    Console.WriteLine("Select second account:");
                    a2 = Console.ReadLine();
                    Console.WriteLine("How much do you want to move from the first account to the second account:");
                    amt = Console.ReadLine();
                    Double.TryParse(amt, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out amount);
                    
                    //Check both accounts aren't the same and that they exist
                    if ( (AccountType.ContainsKey(a1) && AccountType.ContainsKey(a2)) &&
                         AccountType[a1] != AccountType[a2]) 
                    {
                        //A try should be used here in case of failure to undo anything
                        AccountType[a1].Withdraw(amt);
                        AccountType[a2].Deposit(amt);
                    }
                }
                else if(input == "5" || input == "")
                {
                    loop = false;
                }

            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IDictionary<string, Account> AccountList = new Dictionary<string, Account>();
            AccountList.Add("Jeremiah", new Account("Jeremiah"));



            string input;
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("*****************" +
                                  "*****BankApp*****" +
                                  "*****************");
                Console.WriteLine("\n\nWelcome!" +
                                  "\nSign In or reigster for an account" +
                                  "\n1) Sign In" +
                                  "\n2) Register" +
                                  "\n3 Exit");
                input = Console.ReadLine();

                if (input == "1")
                {
                    while(loop)
                    {
                        Console.WriteLine("\n\nAccount Name:");
                        input = Console.ReadLine();

                        if (AccountList.ContainsKey(input))
                        {
                            Account Acc = AccountList[input];
                            Acc.HomeScreen();
                        }
                        else if(input == "")
                        {
                            loop = false;
                        }                                          
                    }
                    loop = true;
                }
                else if (input == "2")
                {
                    Console.WriteLine("Enter a name for the account:");
                    input = Console.ReadLine();

                    if (AccountList.ContainsKey(input))
                    {
                        Console.WriteLine("Name already exists");
                    }
                    else
                    {
                        AccountList.Add(input, new Account(input));
                    }
                }
                else if (input == "3" || input == "")
                {
                    loop = false;
                }
                Console.Clear();
            }    
        }
    }
} 
