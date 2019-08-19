using System;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //generate random number between 1 and 1000 to save in file
            Random rand = new Random();
            string random = rand.Next(1000).ToString();

            // generate file name like cityname_timestamp.txt
            string sAttr = ConfigurationManager.AppSettings.Get("City");
            String timeStamp = GetTimestamp(DateTime.Now);
            string FILE_NAME = sAttr + '_' + timeStamp;

            //create the text file at the specified PATH
            const string path = "C:\\temp\\";
            string file = @path + FILE_NAME + ".txt";
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(random);  
            }
            Console.WriteLine(FILE_NAME + " created successfully!");

            //get latest file
            //Provide folder path from which you like to get latest file
            string Folder = @path;
            var files = new DirectoryInfo(Folder).GetFiles("*.txt");
            string latestfile = "";

            DateTime lastModified = DateTime.MinValue;

            foreach (FileInfo fileX in files)
            {
                if (fileX.LastWriteTime > lastModified)
                {
                    lastModified = fileX.LastWriteTime;
                    latestfile = fileX.Name;
                }
            }
            //Show the name of the latestfile
            Console.Write("Latest File Name: " + latestfile);

            //open latest file, append the city's name and a random number to the end of the contents, save the file to the new name of the city plus latest timestamp
            string docPath = (path);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, latestfile), true))
            {
                outputFile.WriteLine("Hello, appendage! " + ConfigurationManager.AppSettings.Get("City") + " " + random);
                Console.WriteLine("\n" + FILE_NAME + " appended successfully!");
            }

            Console.ReadLine();

        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("MM-dd-yyyy_hhmmtt").ToLower();
        }
    }
}
