namespace Perpustakaan.Models
{
    public class MasterJenisBuku
    {
        public int Id { get; set; }
        public string JenisBuku { get; set; } = string.Empty;

        public ICollection<Buku> Bukus { get; set; } = new List<Buku>();
    }
}
