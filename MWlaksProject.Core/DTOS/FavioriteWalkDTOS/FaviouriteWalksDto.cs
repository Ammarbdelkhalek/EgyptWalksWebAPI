using MWlaksProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.DTOS.FavioriteWalk
{
    public class FaviouriteWalksDto
    {
        public string Name { get; set; }
        public Guid WalkId { get; set; }
        public string ApplicationUserId { get; set; }

        public Walks Walk { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
