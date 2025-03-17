using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTest.Core.Services.Models
{
    public class PlayerUpdateRequestModel : PlayerAddRequestModel
    {
        public int Id { get; set; }
    }
}
