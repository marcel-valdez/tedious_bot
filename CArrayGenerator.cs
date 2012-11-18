using System;
using System.IO;
using System.Collections.Generic;

namespace CodeGeneration {
    public class CArrayGenerator {
        private static Random rand = new Random();

        // Expects:
        // [N][M][O].. etc
        // --type: (type)
        public static void Main(string[] args) {
            if(args.Length < 2) {
                Console.WriteLine("Usage: CArrayGenerator [type] [N][M][O]...");
                return;
            }

            string dimensions = args[1];
            List<int> sizes = ParseDimensions(dimensions);
            string arrayContent = CreateHeader(args[0], sizes);
            arrayContent += CreateContent(sizes, 0);
            arrayContent += CreateFooter();
            Console.WriteLine(arrayContent);
        }

        private static string CreateHeader(string type, List<int> sizes) {
            string header = string.Format("{0} data ", type);
            foreach(int size in sizes) {
                header += string.Format("[{0}]", size);
            }

            return header + " = {\n    ";
        }

        private static string CreateContent(List<int> sizes, int index) {
            string content = "";

            if (index == sizes.Count - 1) {
                content += "{ ";
                for(int i = 0; i < sizes[index]; i++) {
                    content += rand.Next(100) + ", ";
                }

                content += "},";
            } else {
                for(int i = 0; i < sizes[index]; i++) {
                    content += "{\n";
                    content += Indent(index+2);
                    content += CreateContent(sizes, index + 1) + "\n";
                    content += Indent(index+1);
                    content += "},";
                }
            }

            return content;
        }

        private static string Indent(int level) {
            string result = "";
            for(int i = 0; i < level; i++) {
                result += "    ";
            }

            return result;
        }

        private static string CreateFooter() {
            return "\n};";
        }

        private static List<int> ParseDimensions(string dimensions) {
            string dimPrepared = dimensions.Replace("[", "");

            List<int> dimensionSizes = new List<int>();

            foreach(string dimensionSize in dimPrepared.Split(']')) {
                if(dimensionSize.Length > 0) {
                    try {
                        Console.WriteLine("Parsing: " + dimensionSize);
                        dimensionSizes.Add(int.Parse(dimensionSize));
                    } catch(Exception e) {
                        Console.WriteLine("Error: Dimension sizes must be numbers.");
                        throw e;
                    }
                }
            }

            return dimensionSizes;
        }
    }

}