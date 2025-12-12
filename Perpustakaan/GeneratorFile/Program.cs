// See https://aka.ms/new-console-template for more information
string[,] tables = new string[,]
{
    {"MasterJenisBuku", "Id:int, JenisBuku:string" },
    {"Buku", "Id:int, JudulBuku:string, Penulis:string, JenisBukuId:int, TanggalRilis:datetime, JumlahHalaman:int" },
};

for(int i = 0; i < tables.GetLength(0); i++)
{
    string template =
    $@" // {tables[i, 0]}.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{{
    public class {tables[i, 0]}
    {{
{GenerateClass(tables[i, 1])}
    }}
}}";

    File.WriteAllText($"{tables[i,0]}.cs", template);
}

static string GenerateClass(string input)
{
    var lines = input.Split(',');

    string prop = "";
    foreach (var line in lines)
    {
        var parts = line.Split(':');
        prop += $"        public {parts[1]} {parts[0]} {{get; set;}} \n";
    }

    return prop;
}



Console.WriteLine("Done!");
