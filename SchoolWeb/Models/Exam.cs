using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWeb.Models
{
    public class Exam
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Range(1, 10)]
        public int Grade { get; set; }

        [Required]
        public string StudentID { get; set; }
        [ForeignKey("ID")]
        [ValidateNever]
        public virtual Student Student { get; set; } = null!;

        public int SubjectID { get; set; }
        [ForeignKey("ID")]
        [ValidateNever]
        public virtual Subject Subject { get; set; }

    }
}
