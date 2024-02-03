using System.ComponentModel.DataAnnotations;

namespace BugBuddy.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Project { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string? Resolution { get; set; }
        [Display(Name = "Date Created")]
        public DateTime CreatedDate { get; set; }
        public List<Note> Notes { get; set; }

        public Bug()
        {
            Status = "Open";
            CreatedDate = DateTime.Now;
            Resolution = "";
            Notes = new List<Note>();
        }
    }
}