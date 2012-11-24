using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeGeneration
{
    class FileFormatter
    {
        public const string USAGE = "Usage:\nformatter [filename] -pattern [regex] -format [format] -verbose -only-capture" +
                                         "\n filename: The file from which to read the lines." +
                                         "\n -pattern: The regex pattern from which to replace GROUPS. \n\tIf no groups are specified, the command will fail" +
                                         "\n -format: The String.Format format to use for the captured groups. \n\t(Consult MSDN, each captured group is a value)" +
                                         "\n -only-capture: Only return the formatted captures, and ignore the rest of the string." +
                                         "\nExample:" +
                                         "\n  formatter names.txt -pattern \\([a-zA-Z]+\\)\\s\\([a-zA-Z]+\\) -format \\({1},\\ {2}\\)" +
                                         "\n  If names.txt contains:" +
                                         "\n    1. Marcel Valdez is me." +
                                         "\n    2. John Doe is you." +
                                         "\n  The result will be:" +
                                         "\n    1. (Marcel, Valdez) is me." +
                                         "\n    2. (John, Doe) is you.";
        static int Main(string[] args)
        {
            if (!IsInputCorrent(args))
            {
                return 1;
            }

            try
            {
                string filename = args[0];

                string regexStr = args[args.IndexOf("-pattern") + 1];
                string format = args[args.IndexOf("-format") + 1];

                Regex regex = new Regex(regexStr, RegexOptions.Singleline);
                
                if (!FileExists(ref filename))
                {
                    return 1;
                }

                string[] lines = File.ReadAllLines(filename);
                string[] resultingLines = new string[lines.Length];

                int count = 0;
                bool verbose = args.IndexOf("-verbose") != -1;

                if (verbose)
                {
                    Console.WriteLine("Using regex: " + regex);
                    Console.WriteLine("Using format: " + format);
                }

                foreach (string line in lines)
                {
                    // Run regex over lines
                    // For each group matched, replace is with the format string.
                    if (regex.IsMatch(line))
                    {
                        Match match = regex.Match(line);
                        List<string> captures = new List<string>();
                        foreach (Group group in match.Groups)
                        {
                            foreach (Capture capture in group.Captures)
                            {
                                captures.Add(capture.Value);
                            }
                        }

                        bool onlyCapture = args.IndexOf("-only-capture") != -1;

                        string beginning = onlyCapture ? "" : line.Substring(0, line.IndexOf(match.Value, 0));
                        string end = onlyCapture ? "" : line.Substring(line.IndexOf(match.Value, 0) + match.Value.Length);

                        // First group is the combination of all caught groups, we want to ignore this.                        
                        string formatted = "";
                        try
                        {
                            formatted = string.Format(format, (object[])captures.ToArray());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("ERROR: \n  Your regex pattern did not find enough captures for the given format or the format is not a valid MSDN String.Format.\n");
                            throw;
                        }

                        Console.WriteLine(beginning + formatted + end);
                    }
                    else
                    {
                        resultingLines[count++] = line;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Debug: " + e.Message + "\n");
                Console.WriteLine(USAGE);
                return 1;
            }


            return 0;
        }

        private static bool IsInputCorrent(string[] args)
        {
            if (args.Length < 5)
            {
                Console.WriteLine(USAGE);
                Console.WriteLine("It must have at least 5 arguments.");
                return false;
            }

            if (args.IndexOf("-pattern") == -1 || args.IndexOf("-format") == -1)
            {
                Console.WriteLine(USAGE);
                Console.WriteLine("It must contain a pattern and a format");
                Console.Write("Instruction parsed: \n  ");
                foreach (string arg in args)
                {
                    Console.Write(arg + " ");
                }

                return false;
            }

            return true;
        }

        private static bool FileExists(ref string filename)
        {
            bool exists = new FileInfo(filename).Exists;
            if (!exists)
            {

                filename = Path.Combine(Environment.CurrentDirectory, filename);
                exists = new FileInfo(filename).Exists;
                if (!exists)
                {
                    Console.WriteLine(USAGE);
                    Console.WriteLine("File: {0} does not exist.", filename);
                }

                exists = !new FileInfo(filename).Exists;
            }

            return exists;
        }
    }
}