using dnlib.DotNet;
using dnlib.DotNet.Writer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntptrPoint
{
    class Program
    {
        public static string Asmpath;
        public static ModuleDefMD module;
        public static Assembly asm;
        static void Main(string[] args)
        {
            Console.Title = "Point Remover by OFF_LINE";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n");
            string lol = @" /$$$$$$$           /$$             /$$           /$$$$$$$$ /$$                              
| $$__  $$         |__/            | $$          | $$_____/|__/                              
| $$  \ $$ /$$$$$$  /$$ /$$$$$$$  /$$$$$$        | $$       /$$ /$$   /$$  /$$$$$$   /$$$$$$ 
| $$$$$$$//$$__  $$| $$| $$__  $$|_  $$_/        | $$$$$   | $$|  $$ /$$/ /$$__  $$ /$$__  $$
| $$____/| $$  \ $$| $$| $$  \ $$  | $$          | $$__/   | $$ \  $$$$/ | $$$$$$$$| $$  \__/
| $$     | $$  | $$| $$| $$  | $$  | $$ /$$      | $$      | $$  >$$  $$ | $$_____/| $$      
| $$     |  $$$$$$/| $$| $$  | $$  |  $$$$/      | $$      | $$ /$$/\  $$|  $$$$$$$| $$      
|__/      \______/ |__/|__/  |__/   \___/        |__/      |__/|__/  \__/ \_______/|__/      
                                                                                             
                                                                                             
                                                                                             ";
            Console.WriteLine(lol);
            string diretorio = args[0];
            try
            {
                Program.module = ModuleDefMD.Load(diretorio, (ModuleCreationOptions) null);
                Program.asm = Assembly.LoadFrom(diretorio);
                Program.Asmpath = diretorio;
            }

            catch (Exception)
            {
                Console.WriteLine("Not .NET Assembly...");
            }
            string text = Path.GetDirectoryName(diretorio);
            bool flag = !text.EndsWith("\\");
            bool flag2 = flag;
            if (flag2)
            {
                text += "\\";
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully loaded assembly!");
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                IntptrPoint.PointRemover.Clean();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Removing mutations!");

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Failed to remove. ORIGINAL EXCEPTION: " + ex.ToString());
            }
            string filename = string.Format("{0}{1}_Unpacked{2}", text, Path.GetFileNameWithoutExtension(diretorio), Path.GetExtension(diretorio));
            ModuleWriterOptions writerOptions = new ModuleWriterOptions(Program.module);
            writerOptions.MetaDataOptions.Flags |= MetaDataFlags.PreserveAll;
            writerOptions.Logger = DummyLogger.NoThrowInstance;
            NativeModuleWriterOptions NativewriterOptions = new NativeModuleWriterOptions(Program.module);
            NativewriterOptions.MetaDataOptions.Flags |= MetaDataFlags.PreserveAll;
            NativewriterOptions.Logger = DummyLogger.NoThrowInstance;
            bool isILOnly = Program.module.IsILOnly;
            if (isILOnly)
            {
                Program.module.Write(filename, writerOptions);
            }
            else
            {
                Program.module.NativeWrite(filename, NativewriterOptions);
            }
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully removed " + IntptrPoint.PointRemover.amount + " point mutations");
            Console.ReadLine();
        }
    }
}
