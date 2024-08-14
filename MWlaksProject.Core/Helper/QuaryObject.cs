using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Helper
{
    public class QuaryObject
    {
       public  string? filterOn { set; get; } =  null;
       public  string? filterQuery { set; get; } =  null;
       public  string? sortBy { set; get; } = null;
       public  bool? isAscending { get; set; } = false;
       public  int pageNumber { get; set; } = 1;
       public  int pageSize { get; set; } = 1000;
    }
}
