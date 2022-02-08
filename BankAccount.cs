namespace BankAccount
{
    using System;
    using System.Linq;
    using Menu;
    class account : Menu.menu
    {
        protected int money;
        protected int maxWithdrawal = 200;
        protected int accountNumber;
        protected bool loggedIn = false;
        public bool LoggedIn { get { return loggedIn; } }

        public account()
        {
        }

        public void displayBalance()
        {
            if (!loggedIn)
            { // if the user is not logged in, dont let the diposite money
                Console.WriteLine("You need to be logged in to display");
                return;
            }
            Console.WriteLine($"You have a balance of £{money}.");
        }

        public void diposite(int ammountToAdd)
        {
            if (!loggedIn) 
            { // if the user is not logged in, dont let the diposite money
                Console.WriteLine("You need to be logged in to diposite money");
                return; 
            } 
            money += ammountToAdd;
            Console.WriteLine($"Dipositing £{ammountToAdd} brings your balance up to £{money}");
        }

        public bool askForDiposite()
        {
            if (!loggedIn)
            { // if the user is not logged in, dont let the diposite money
                Console.WriteLine("You need to be logged in to diposite money");
                return false;
            }
            string dipositeString;
            int dipositeInt;
            do
            {
                Console.WriteLine("How much are you depositing?");
                dipositeString = Console.ReadLine();
                if (escapeStrings.Contains(dipositeString)) // if the user wants to quit
                {
                    return false ; // quit
                }
            } while (!Int32.TryParse(dipositeString, out dipositeInt));
            diposite(dipositeInt);
            return true;

        }

        public bool canWithdraw(int ammountToWithdraw)
        {
            if (!loggedIn)
            { // if the user is not logged in, dont let the diposite money
                Console.WriteLine("You need to be logged in to withdraw money");
                return false;
            }
            if (money >= ammountToWithdraw) // they have more money that the ammount they want to withdraw
            {
                return true;
            }
            return false; // the do not have enough money to withdraw
        }
        public bool widthdraw(int ammountToWithdraw)
        {
            if (!loggedIn)
            { // if the user is not logged in, dont let the diposite money
                Console.WriteLine("You need to be logged in to diposite money");
                return false;
            }
            if (!canWithdraw(ammountToWithdraw)) // if they cannot withdraw the ammount the want to
            {
                Console.WriteLine($"You do not have enought money to withdraw £{ammountToWithdraw}."); // tell the user that they dont have the money
                return false; // show that they dont have enough money
            }
            money -= ammountToWithdraw; // minus the ammount they have withdrawn from the balance
            Console.WriteLine($"You have withdrawn £{ammountToWithdraw}, so you have £{money} left");
            return true;
        }
        public bool askForWithdraw()
        {
            if (!loggedIn)
            { // if the user is not logged in, dont let the diposite money
                Console.WriteLine("You need to be logged in to withdraw money");
                return false;
            }
            string stringEntered;
            int ammountToWithdraw; // initilize varables

            do // repeat until they enter an int
            {
                Console.WriteLine("How much do you want to withdraw?");
                stringEntered = Console.ReadLine(); // get a string input from the user
                if (escapeStrings.Contains(stringEntered)) // if the user wants to leave the program
                {
                    Console.WriteLine("Exiting the subroutine");
                    return false; // leave the subroutine
                }
            } while (!Int32.TryParse(stringEntered, out ammountToWithdraw));  // try convert to an intiger and if it fails repeat the loop


            if (ammountToWithdraw > maxWithdrawal) // if they want to withdraw more that the maximum withdrawal ammount
            {
                Console.WriteLine($"You cannot withdraw £{ammountToWithdraw} as it is more that the maximum withdrawal ammount of £{maxWithdrawal}"); // tell them
                return false; // stop the subroutine
            }

            if (!canWithdraw(ammountToWithdraw))
            {
                Console.WriteLine($"You do not have enough money to withdraw £{ammountToWithdraw}");
                return false;
            }
            bool successful = widthdraw(ammountToWithdraw); // widthraw the ammount they want
            return successful; // return true to show the withdrawl was sucsessfull or false if it wasnt
        }
    }
}