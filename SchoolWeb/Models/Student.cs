using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWeb.Models
{
    public class Student
    {

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Student Index")]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",
            ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Exam>? Exams { get; set; }

    }
}
