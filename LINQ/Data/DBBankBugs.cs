using CsvHelper;
using LINQ.Entities;
using LINQ.EntitiesMaping;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.Data
{
    class DBBankBugs
    {
        string[] BugTypes = new string[] { "Blocker", "Critical", "Major", "Medium", "Minor", "Normal" };

        string FileName = @"D:\Step\С#\HW_C#_8_LINQ\LINQ\LINQ\bugs-2002.csv";

        private List<BankBug> bugs = new List<BankBug>();

        private Dictionary<string, ErrorSum> errors = new Dictionary<string, ErrorSum>();

        private Dictionary<string, int> teamDic = new Dictionary<string, int>();

        private Dictionary<string, Dictionary<string, int>> bugsDic2 = new Dictionary<string, Dictionary<string, int>>();

        /// <summary>
        /// получение коллекции из файла csv
        /// </summary>
        public void LoadFromFile()
        {
            //открыть файл
            using (var reader = new StreamReader(FileName))

            //передача файла системе чтения
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //регистрация правил чтения
                csv.Context.RegisterClassMap<BankBugMapping>();
                //чтение коллекции
                bugs.AddRange(csv.GetRecords<BankBug>());
            }
        }

        public void PrintAll()
        {
            foreach (var b in bugs)
            {
                Console.WriteLine(b.Priority + "\t" + b.Labels);
            }
        }

        public void DicTeamsKey()
        {

            foreach (var item in from b in bugs select b.Labels)
            {
                var names = item.Split(',');

                foreach (var n in names)
                {
                    try
                    {
                        errors.Add(n.Trim(), new ErrorSum());
                    }
                    catch (Exception e)
                    {

                    }
                }
            }            
        }

        public void BuildReport()
        {
            foreach (var item in errors)
            {
                foreach (var bugTypes in BugTypes)
                {
                    int c = bugs.Where(b => b.Labels.Contains(item.Key) && b.Priority.Contains(bugTypes)).Count();                  
                    item.Value.GetType().GetProperty(bugTypes).SetValue(item.Value, c);
                }
            }
        }

        public void MakeReportDictionary()
        {
            foreach (var item in errors)
            {               
                int total = 0;               

                foreach (var p in item.Value.GetType().GetProperties())
                {                   
                    int c = (int)p.GetValue(item.Value);
                    total += c;              
                    teamDic[item.Key] = total;                   
                } 
            }

            foreach (var item in teamDic)
            {
                Dictionary<string, int> t = new Dictionary<string, int>();

                t.Add(item.Key, item.Value);

                bugsDic2.Add(item.Key + "_" + DateTime.Now.ToString(), t);
            }            
        }

        public void PrintReport()
        {
            Console.WriteLine($"Время генерации отчета:\t\t\t      | Команда\t\t\t| Количество ошибок (общее)");
            Console.WriteLine(new string('-', 99));
            foreach (var kvp in bugsDic2)
            {
                var innerDict = kvp.Value;

                foreach (var innerKvp in innerDict)
                {
                    Console.WriteLine($"{kvp.Key.PadRight(45)} | {innerKvp.Key.PadRight(23)} | {innerKvp.Value}");                   
                }
            } 
        }

        public void PrintByError()
        {
            foreach (var item in errors)
            {
                int total = 0;

                Console.WriteLine($"Команда: {item.Key}");

                foreach (var p in item.Value.GetType().GetProperties())
                {
                    int c = (int)p.GetValue(item.Value);
                    total += c;

                    Console.WriteLine(p.Name + " " + p.GetValue(item.Value));
                }

                Console.WriteLine($"Общее количество ошибок: {total}");
                Console.WriteLine(new string('-', 99));
            }
        }
    }
}
