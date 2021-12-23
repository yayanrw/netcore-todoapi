using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoAPI.Models
{
    [Table("member")]
    public class MemberModel
    {
        [Key]
        public int id_member { get; set; }
        public String? kode { get; set; }
        public String? nama { get; set; }
        public String? kota { get; set; }
        public String? rekening { get; set; }
        public String? telepon { get; set; }
        public String? facebook { get; set; }
        public String? instagram { get; set; }
        public String? website { get; set; }
        public String? twitter { get; set; }
        public String? foto { get; set; }
        public String? status { get; set; }
    }
}
