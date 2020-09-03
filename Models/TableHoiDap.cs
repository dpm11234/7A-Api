namespace H7A_Api.Models
{
    public partial class TableHoiDap
    {
        public uint Id { get; set; }
        public int NgayDang { get; set; }
        public string NoiDung { get; set; }
        public bool HienThi { get; set; }

        public string Ten { get; set; }

        // public string HoTen {get; set;}
    }
}