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
        string FileName = "bugs-2002.csv";
        private List<BankBug> bugs = new List<BankBug>();

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
    }
}
