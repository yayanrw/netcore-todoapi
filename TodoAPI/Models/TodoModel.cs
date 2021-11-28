namespace TodoAPI.Models
{
    public class TodoModel
    {
        public Guid pid { get; set; }
        public String? title { get; set; }
        public String? description { get; set; }
        public DateTime date { get; set; }
        public DateTime created_at { get; set; }
        public String? created_by { get; set; }
        public DateTime updated_at { get; set; }
        public String? updated_by { get; set; }
    }
}
