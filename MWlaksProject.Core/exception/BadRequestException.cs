using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.exception
{
    public class BadRequestException:Exception
    {
        public string[] Errors { get; }

        public BadRequestException(string message) : base(message)
        {
            Errors = Array.Empty<string>();

        }

        public BadRequestException(string message, string[] errors) : base(message)
        {
            Errors = errors ?? Array.Empty<string>();
        }
    }
}
