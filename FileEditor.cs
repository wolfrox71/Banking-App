namespace FileChange
{
    using File = System.IO.File; // import file management as file
    using System; // to use console
    public class FileWriter
    {
        public readonly string path; //readonly means can only be set in constructor
        public FileWriter(  // this is a constructor like __init__
                string file_path // pass through the path to the file
                )
        {
            path = file_path;
            if (!(File.Exists(path)))
            { // if the file does not exist
                File.CreateText(path); // create the file
            }
        }

        public string[] readFile(
                bool show = false // specificy if they want the contence of the file to be printed, this is off by default
                )
        {
            string[] lines = File.ReadAllLines(this.path); //get each line in the file
            if (show)
            { // if printing the lines 
                foreach (string line in lines)
                { // for each line
                    Console.WriteLine($"{line}"); // print it
                }
            }
            return lines; // return the lines as a string array
        }

        public void updateFile(
                string data, // get the data to write to the file
                bool write = false, // these have a default value so are not needed to be passed through
                bool newLine = true
                )
        {
            string createText = data + (newLine ? Environment.NewLine : ""); // if newline = true, add a newline to the end of the text, if not dont
            if (!write)
            { // if appending to the file
                File.AppendAllText(this.path, createText); // append the file
            }
            if (write)
            { // if overwriting the file
                File.WriteAllText(this.path, createText); // overwrite the data in the file
            }
        }
    }
}