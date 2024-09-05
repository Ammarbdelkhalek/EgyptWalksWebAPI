using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.DTOS.FavioriteWalkDTOS
{
    public class AddFaviouriteWalkDto
    {
        public string Name { get; set; }
        public Guid WalkId { get; set; }
        public string UserId { get; set; }
    }
}
