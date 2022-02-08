namespace Menu
{
    using System;
    using System.Linq;
    class menu
    {
        protected string[] escapeStrings = { "escape", "quit", "close", "exit" };
        protected string[] yesAnswers = { "yes", "y" };

        public string[] EscapeStrings { get { return escapeStrings; } }
        public string[] YesAnswers { get { return yesAnswers; } }

        public menu()
        {

        }

        protected string HashString(string text, string salt = "") // this is from https://www.sean-lloyd.com/post/hash-a-string/
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
        public string displayMenu()
        {
            string[] expectedResults = { "b", "w", "q" , "d" };
            string enteredString;
            do
            {
                Console.WriteLine("------------------");
                Console.WriteLine("Welcome to Natwest!");
                Console.WriteLine("b: Display balance");
                Console.WriteLine("w: Withdraw money");
                Console.WriteLine("d: Diposite money");
                Console.WriteLine("q: quit");
                enteredString = Console.ReadLine().ToLower(); // get a string input from the user and convert it to lowercase
                if (escapeStrings.Contains(enteredString)) // they want to quit the program
                {
                    return "q"; // return that they want to quit
                }
            } while (!expectedResults.Contains(enteredString)); // repeat the loop until the user enters something expected
            return enteredString; // return what the user entered 

        }
    }

}