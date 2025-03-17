using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTest.Core.Services.Models
{
    public class TeamUpdateRequestModel : TeamAddRequestModel
    {
        public int Id { get; set; }
    }
}
