using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hafina.ImportData
{
    public class FinancialReport
    {
        public List<BalanceSheet> BalanceSheets { get; set; } = new List<BalanceSheet>();

        public List<BusinessResult> BusinessResults { get; set; } = new List<BusinessResult>();
    }
}
