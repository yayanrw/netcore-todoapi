using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoAPI.Models
{
    [Table("t_todos")]
    public class TodoModel
    {
        [Key]
        public String pid { get; set; }
        public String? title { get; set; }
        public String? description { get; set; }
        public DateTime? date { get; set; }
        public DateTime? created_at { get; set; }
        public String? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public String? updated_by { get; set; }
    }
}
