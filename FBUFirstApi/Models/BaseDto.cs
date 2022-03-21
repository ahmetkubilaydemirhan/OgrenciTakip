using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBUFirstApi.Models
{
    public class BaseDto
    {
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }
    }
}
