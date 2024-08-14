using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Helper
{
    public class PaginationMetaData
    {
        public int TotalRecords { get; set; }
        public int TotalPages {  get; set; }
        public int currentpage { get; set; }
        public int ? Nextpaget {  get; set; }
        public int ? PrevPage { get; set; }
    }
}
