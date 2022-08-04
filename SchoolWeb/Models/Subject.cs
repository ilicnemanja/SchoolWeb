using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWeb.Models
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Exam>? Exams { get; set; }

        public Subject()
        {
            Exams = new HashSet<Exam>();
        }
    }
}