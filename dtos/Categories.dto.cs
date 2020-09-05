
namespace H7A_Api.Models
{
    using System.Collections.Generic;
    public class CategoriesDTO
    {
        public int id { get; set; }

        public string name { get; set; }
        public string slug { get; set; }
        public List<CategoryChildDTO> children { get; set; }

    }
}