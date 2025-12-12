namespace Perpustakaan.Models
{
    public class Buku
    {
        public int Id { get; set; }
        public string Judul { get; set; } = string.Empty;
        public string Penulis { get; set; } = string.Empty;
        public int JenisBukuId { get; set; }
        public DateTime TanggalRilis { get; set; }
        public int JumlahHalaman { get; set; }

        public MasterJenisBuku JenisBuku { get; set; }

    }
}
