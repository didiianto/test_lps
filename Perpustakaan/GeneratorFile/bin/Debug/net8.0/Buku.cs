 // Buku.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class Buku
    {
        public int Id {get; set;} 
        public string  JudulBuku {get; set;} 
        public string  Penulis {get; set;} 
        public int  JenisBukuId {get; set;} 
        public datetime  TanggalRilis {get; set;} 
        public int  JumlahHalaman {get; set;} 

    }
}