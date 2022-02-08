using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAccount;
using FileChange;

namespace Banking_App
{
    internal class Program
    { 
        static void Main(string[] args)
        { 
            /* 
             * (cat, something)
             * (jamie, 10)
             */
            UserAccount.userAccount account;
            int attempts = 0;
            int maxLoginAttempts = 3;
            do {
                attempts++;
                Console.WriteLine("Username:");
                string username = Console.ReadLine();
                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                account = new UserAccount.userAccount(username, password);
                if (account.EscapeStrings.Contains(username) || account.EscapeStrings.Contains(password)) { return; } // if the user wants to quit, quit
                if (account.login()) { continue; } // if the username and password are correct, exit the loop
                Console.WriteLine("Username or Password Incorrect");
                if (attempts != maxLoginAttempts)
                {
                    Console.WriteLine("Do you want to try again?");
                    string signInAgain = Console.ReadLine();
                    if (!account.YesAnswers.Contains(signInAgain)) { return; } // if the user wants to exit, exit
                }
            } while (!account.LoggedIn && attempts < maxLoginAttempts);// if the user does not log in, end the program

            if (!account.LoggedIn) { return; } // if the user is not logged in, stop the program
            while (true) {
                string menuChoice = account.displayMenu();
                switch (menuChoice)
                {
                    case "q":  // if the user wants to quit
                        account.updateStatus();
                        return; // close the program
                    case "b":
                        account.displayBalance();
                        break;
                    case "w":
                        account.askForWithdraw();
                        break;
                    case "d":
                        account.askForDiposite();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
