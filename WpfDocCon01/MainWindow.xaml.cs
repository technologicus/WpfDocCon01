using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDocCon01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBox00.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                return;
            }

            if (e.Key == Key.Enter)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }

                e.Handled = true;
            }
        }

        private void Window_KeyDown_Document(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                return;
            }

            if (e.Key == Key.Enter)
            {
//----------------------------
                barcode dc_bc = new barcode(textBox00.Text, textBox01.Text, textBox02.Text, textBox03.Text, textBox04.Text, textBox05.Text, textBox06.Text, textBox07.Text);
                string dc_rework_file;

                string dc_partnumber_file;

                dc_bc.field01 = dc_bc.field01.Replace("/", string.Empty);

                if (dc_bc.field01.Length > 3) if (dc_bc.field01.Substring(dc_bc.field01.Length - 3, 3) == "_XX") dc_bc.field01 = dc_bc.field01.Substring(0, dc_bc.field01.Length - 3);

                dc_rework_file = dc_getfilename(@"C:\Profiles\Alan\Programming\C# 6.0 and the .NET 4.6 Framework, Seventh Edition\DocumentControl\DocCtlRewks", dc_bc.field00 + "-" + dc_bc.field01);

                if (dc_rework_file != null)
                {
//-------------------                    Console.WriteLine("Found file {0}", dc_rework_file);
                    System.Diagnostics.Process.Start(dc_rework_file);
                }
                else
                {

                    dc_partnumber_file = dc_getfilename(@"C:\Profiles\Alan\Programming\C# 6.0 and the .NET 4.6 Framework, Seventh Edition\DocumentControl\DocCtlParts", dc_bc.field01);
                    if (dc_partnumber_file != null)
                    {
//-------------------                        Console.WriteLine("Found file {0}", dc_partnumber_file);
                        System.Diagnostics.Process.Start(dc_partnumber_file);
                    }

                    else
                    {
//-------------------                        Console.WriteLine("File not found");
                    }
                }
//----------------------------
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }

                e.Handled = true;
            }
        }

        static string dc_getfilename(string dc_path, string dc_file)
        {
            DirectoryInfo diTop = new DirectoryInfo(@dc_path);
            string dc_fullfilename = null;

            try
            {
                foreach (var fi in diTop.EnumerateFiles())
                {
                    try
                    {
                        // Display each file;
                        Console.WriteLine("FullName {0}\t\t{1}", fi.FullName, fi.Length.ToString("N0"));
                        Console.WriteLine("Name {0}\t\t{1}", fi.Name, fi.Length.ToString("N0"));
                    }
                    catch (UnauthorizedAccessException UnAuthTop)
                    {
                        Console.WriteLine("{0}", UnAuthTop.Message);
                    }
                }

                foreach (var di in diTop.EnumerateDirectories("*"))
                {
                    try
                    {
                        foreach (var fi in di.EnumerateFiles("*", SearchOption.AllDirectories))
                        {
                            try
                            {
                                //// Display each file;
                                //Console.WriteLine("FullName {0}\t\t{1}", fi.FullName, fi.Length.ToString("N0"));
                                //Console.WriteLine("Name {0}\t\t{1}", fi.Name, fi.Length.ToString("N0"));
                                if (System.IO.Path.GetFileNameWithoutExtension(fi.FullName) == dc_file)
                                {
                                    dc_fullfilename = fi.FullName;
                                }
                            }
                            catch (UnauthorizedAccessException UnAuthFile)
                            {
//---------------                                Console.WriteLine("UnAuthFile: {0}", UnAuthFile.Message);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException UnAuthSubDir)
                    {
//---------------                        Console.WriteLine("UnAuthSubDir: {0}", UnAuthSubDir.Message);
                    }
                }
            }
            catch (DirectoryNotFoundException DirNotFound)
            {
//---------------                Console.WriteLine("{0}", DirNotFound.Message);
            }
            catch (UnauthorizedAccessException UnAuthDir)
            {
//---------------                Console.WriteLine("UnAuthDir: {0}", UnAuthDir.Message);
            }
            catch (PathTooLongException LongPath)
            {
//---------------                Console.WriteLine("{0}", LongPath.Message);
            }
            return dc_fullfilename;
        }

        public class barcode
        {
            public string field00;
            public string field01;
            public string field02;
            public string field03;
            public string field04;
            public string field05;
            public string field06;
            public string field07;

            public barcode(string text00, string text01, string text02, string text03, string text04, string text05, string text06, string text07)
            {
                field00 = text00;

                field01 = text01;

                field02 = text02;

                field03 = text03;

                field04 = text04;

                field05 = text05;

                field06 = text06;

                field07 = text07;


            }

        }

    }
}
