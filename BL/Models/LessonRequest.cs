using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class LessonRequest
    {
        public int Category { get; set; }
        public int SubCategory { get; set; }
        public string UserPrompt { get; set; }
        public int UserId { get; set; }
    }
}
