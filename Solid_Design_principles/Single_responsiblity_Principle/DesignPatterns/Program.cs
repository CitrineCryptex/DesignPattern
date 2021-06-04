using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;


// 단일 책임 원칙 
//어떤 클래스를 변경해야 하는 이유는 오직 하나뿐이어야 한다" - 로버트 C. 마틴
namespace DesignPatterns
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;
        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;
        }
        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("I ate a bug");
            WriteLine(j);

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);
        }
    }
}
