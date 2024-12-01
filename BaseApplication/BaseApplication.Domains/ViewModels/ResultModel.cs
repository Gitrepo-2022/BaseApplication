using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApplication.Domains.ViewModels
{
    public class ResultModel
    {
        public string? response { get; set; }
        public string? returnvalue { get; set; }
        public bool success { get; set; }
        public int? id { get; set; }
    }
}
