using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.exception
{
    public class NotAuthorizedException:Exception
    {
        public NotAuthorizedException(string message):base(message)
        {
            
        }
    }
}
