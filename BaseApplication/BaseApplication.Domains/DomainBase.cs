using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Domains
{
    public class DomainBase
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public int? ModifiedById { get; set; }

        public int? DeletedBy { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
    }
}
