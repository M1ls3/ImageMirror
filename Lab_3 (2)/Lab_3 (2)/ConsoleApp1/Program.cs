using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Xml.Linq;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Menu()
        {
            Console.WriteLine($"1 - Delete all mirrired pics" +
                $"\n0 - Exit");
            int key = Int32.Parse(Console.ReadLine());

            switch (key)
            {
                case 1: { Deleter(); break; }
                case 0: { Environment.Exit(0); break; }
                default: { Console.WriteLine("\nWhat???\n"); Menu();break; }
            }
        } 

        static void Deleter()
        {
            try
            {
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
                foreach (string path in files)
                {
                    string name = Path.GetFileNameWithoutExtension(path);

                    bool IsMirrored = name.Contains("mirrored");
                    if (IsMirrored)
                    {
                        File.Delete(name + ".gif");
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"ERROR Deleter: {ex}"); }
        }

        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            Regex regexExtForImage = new Regex("^((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$", RegexOptions.IgnoreCase);
            foreach (string path in files)
            {
                string fileName = Path.GetFileName(path);
                string name = Path.GetFileNameWithoutExtension(path);
                try
                {
                    Bitmap bitmap = new Bitmap(path);
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    bitmap.Save(name + "-mirrored.gif");
                }
                catch (ArgumentException)
                {
                    if (regexExtForImage.IsMatch(Path.GetExtension(path))) { Console.WriteLine(fileName + " Incorrect path!"); }
                }
            }

            Menu();
        }
    }
}
