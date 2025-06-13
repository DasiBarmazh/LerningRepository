using Dal.Entities;

namespace BL.Models
{
    public class LessonRequest
    {
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public string UserPrompt { get; set; }
        public int UserId { get; set; }
    }
}
