namespace Perpustakaan.Models
{
    public class HistoryBuku
    {
        public int Id { get; set; }
        public int BukuId { get; set; }
        public string Aksi { get; set; } = string.Empty;
        public DateTime Waktu { get; set; }
        public string? DataSebelum { get; set; }
        public string? DataSesudah { get; set; }

    }
}
