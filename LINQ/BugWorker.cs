using LINQ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class BugWorker 
    {
        DBBankBugs bankBugs = new DBBankBugs();

        public void Run()
        {
            bankBugs.LoadFromFile();

            bankBugs.DicTeamsKey();

            bankBugs.BuildReport();

            bankBugs.MakeReportDictionary();

            bankBugs.PrintReport();

            Console.WriteLine(new string ('-', 99));

            bankBugs.PrintByError();


            // bankBugs.InDic();
            // bankBugs.PrintAllb();
        }
    }
}
