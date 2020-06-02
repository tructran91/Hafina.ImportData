using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hafina.ImportData
{
    public abstract class BaseEntity
    {
        public virtual string CreatedBy { get; set; }

        public virtual DateTime? CreatedAt { get; set; }

        public virtual string UpdatedBy { get; set; }

        public virtual DateTime? UpdatedAt { get; set; }

        public virtual bool IsDeleted { get; set; }
    }
}
