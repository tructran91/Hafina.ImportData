using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hafina.ImportData
{
    public class Company : BaseEntity
    {
        [Required]
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public virtual List<BalanceSheet> BalanceSheets { get; set; }

        public virtual List<BusinessResult> BusinessResults { get; set; }
    }
}
