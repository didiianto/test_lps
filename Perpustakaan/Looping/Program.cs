// See https://aka.ms/new-console-template for more information
Console.Write("Input N: ");
string n = Console.ReadLine();

for(int i = 1; i <= int.Parse(n); i++)
{
    if(i%5 == 0 && i != 5)
    {
        Console.Write("IDIC ");
    }
    else if(i%6 == 0 && i != 6)
    {
        Console.Write("LPS ");
    }
    else
    {
        Console.Write($"{i} ");
    }
}
