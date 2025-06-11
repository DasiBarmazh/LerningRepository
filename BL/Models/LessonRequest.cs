using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class LessonRequest
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string UserPrompt { get; set; }
    }
}
