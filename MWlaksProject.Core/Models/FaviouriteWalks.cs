using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Models
{
    public class FaviouriteWalks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WalkId { get; set; }
        public string UserId { get; set; }

        public Walks Walk { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
