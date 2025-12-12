// See https://aka.ms/new-console-template for more information

//string input = "hello world";
Console.Write("Enter a string: ");
string input = Console.ReadLine();
var dict = new Dictionary<char, int>();

foreach (char c in input.Replace(" ", ""))
{
    if (dict.ContainsKey(c))
    {
        dict[c]++;
    }
    else
    {
        dict[c] = 1;
    }
}

foreach (var item in dict)
{
    Console.WriteLine($"{item.Key} : {item.Value}");
}   
