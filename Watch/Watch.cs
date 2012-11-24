using System;
using System.Linq;
using System.Threading;

namespace CodeGeneration
{
    public class Watch
    {
        private const string usage = "usage: watch -f [frequency] -n [count] [command]" +
                                    "\n\tfrequency: frequency with which to execute the command in seconds" +
                                    "\n\tcount: amount of times to execute the command";

        public static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine(usage);
                return 1;
            }

            int fOptionIndex = -1;

            if (!args.Any(item =>
            {
                fOptionIndex++;
                return item == "-f";
            }))
            {
                Console.WriteLine(usage);
                return 1;
            }

            int frequency = 1;

            try
            {
                frequency = Int32.Parse(args[fOptionIndex + 1]);
            }
            catch (Exception)
            {
                Console.WriteLine("Expected a number for frequency. Instead found: {0}", args[fOptionIndex + 1]);
                return 1;
            }


            int nOptionIndex = -1;
            int spinCount = int.MaxValue;
            if (args.Any(item =>
            {
                nOptionIndex++;
                return item == "-n";
            }))
            {
                spinCount = Int32.Parse(args[nOptionIndex + 1]);
            }
            else
            {
                nOptionIndex = fOptionIndex;
            }

            try
            {
                int count = 0;
                while (count++ < spinCount)
                {
                    Console.Clear();
                    string command = args.Skip(nOptionIndex + 2).Aggregate((acum, current) => acum + " " + current);
                    Console.WriteLine(CommandLine.RunCmd(command.Trim()));
                    count++;
                    Thread.Sleep(frequency * 1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error ocurred while executing the command: \n <{0}>",
                    args[args.Length - 1]);
                Console.WriteLine(e.Message);
            }

            return 0;
        }
    }

}