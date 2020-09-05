namespace H7A_Api.Models
{

    public class QAndADTO
    {
        public uint id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public int createdAt { get; set; }
    }
}