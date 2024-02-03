using System;
using System.ComponentModel.DataAnnotations;

namespace BugBuddy.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Display(Name = "Date Created")]
        public DateTime Date { get; set; }

        public Note()
        {
            Date = DateTime.Now;
        }
    }
}