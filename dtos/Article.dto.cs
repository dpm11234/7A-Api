namespace H7A_Api.Models
{

    public class ArticleDTO
    {
        public uint id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string imgUrl { get; set; }
        public int createdAt { get; set; }
    }

    public class ArticleDetailsDTO
    {
        public uint id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string content{ get; set; }
        public string imgUrl { get; set; }
        public string thumbUrl{ get; set; }
        public int views { get; set; }
        public int createdAt { get; set; }
        public int updatedAt { get; set; }
    }
}