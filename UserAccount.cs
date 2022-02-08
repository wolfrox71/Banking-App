namespace UserAccount
{
    using BankAccount;
    using FileChange;
    using System;
    using System.Linq;
    class userAccount : BankAccount.account
    {
        protected string name;
        protected string password;
        public string Name { get { return name; } } // allow the name to be get but not set
        protected FileChange.FileWriter usersDB = new FileChange.FileWriter("users.txt"); // add the code to edit text files
        protected FileChange.FileWriter userStatus = new FileChange.FileWriter("status.csv");
        public userAccount(string enteredName, string enteredPassword)
        {
            name = enteredName;
            password = HashString(enteredPassword);
            if (getStatus() != "-1")
            {
                string storedStringMoney = getStatus().Split(',')[1];
                Int32.TryParse(storedStringMoney, out money);
            }
        }
        public bool userExists()
        {
            return usersDB.readFile().Contains(name); // if the username is in the file
        }
        public bool addUser()
        {
            if (!userExists()) // if the user does not exists
            {
                Console.WriteLine($"Added name {name}"); // say that the user 
                usersDB.updateFile(name); // add the name to the file
                return true; // return true to show that the user has been added
            }
            else
            {
                Console.WriteLine($"{name} already exists"); // say that the user is already in the file
                return false; // return false to show that the user has not been added to the file
            }
        }
        public void updateStatus()
        {
            string[] currentRows = userStatus.readFile();
            if (getStatus() == "-1") // if the user does not already have a status
            {
                userStatus.updateFile($"{name},{password},{money},{accountNumber}"); // this should add a row to the csv file with the username, current money and the account number
                return; // get out of the subroutine 
            }
            if (!loggedIn) { return; }
            int numberOfStatuses = currentRows.Count();
            string[] updatedRows = new string[numberOfStatuses];
            for (int i = 0; i < numberOfStatuses; i++)
            {
                string row = currentRows[i];
                if (row.StartsWith($"{name},"))  // if the row is the row for the username
                {
                    Console.WriteLine($"Status Updated to {name},{password},{money},{accountNumber}"); // display what the status has been updated to
                    updatedRows[i] = $"{name},{password},{money},{accountNumber}"; // set the row to be the correct values
                    continue; // restart the loop
                }
                else
                {
                    updatedRows[i] = currentRows[i];
                    continue; // restart the loop
                }
            }
            userStatus.updateFile("", true, false);
            for (int i = 0; i < numberOfStatuses; i++) // go through each row in the updatedRows
            {
                string row = updatedRows[i];
                Console.WriteLine($"Adding row {row}");
                userStatus.updateFile(row); // add the new rows to the file
            }
        }
        protected string getStatus()
        {
            string[] currentRows = userStatus.readFile(); // get the rows from the array
            foreach (string row in currentRows) // cycle through each row in the array
            {
                if (row.StartsWith($"{name},")) // if the row is the row for the user
                {
                    return row; // return the row 
                }
            }
            return "-1"; // the user is not in the file
        }
        protected bool createUser()
        {
            Console.WriteLine("Username not found");
            Console.WriteLine("Do you want to create it?");
            string wantToCreateUser = Console.ReadLine().ToLower();
            if (yesAnswers.Contains(wantToCreateUser))
            {
                Console.WriteLine("User Created");
                updateStatus();
                return true;
            }
            Console.WriteLine("User not created");
            return false;
        }
        public bool login()
        {
            if (getStatus() == "-1") // if the user does not have a status
            {
                createUser();
            }
            if (name == getStatus().Split(',')[0] && password == getStatus().Split(',')[1])
            {
                Console.WriteLine("Logged In");
                money = Convert.ToInt32(getStatus().Split(',')[2]);
                accountNumber = Convert.ToInt32(getStatus().Split(',')[3]);
                loggedIn = true;
                return true;
            }
            return false;
        }
    }
}