global using DotnetAndCSharp.Zoo;

Console.WriteLine("Hello, World!");

var monkey1 = new Monkey()
{
    Name = "George"
};
Console.WriteLine(monkey1);

var monkey2 = new Monkey()
{
    Name = "George",
    Age = 3
};
Console.WriteLine(monkey2);

var kong = new Gorilla("King", 32);
Console.WriteLine(kong);
var updatedKong = kong with { Name = "King Kong" };
Console.WriteLine(updatedKong);

if (monkey1.Name is not null)
{
    Console.WriteLine($"{monkey1.Name.ToUpper()}");
}
else 
{
    Console.WriteLine("Monkey has no name");
}

if (monkey1 == monkey2)
{
    Console.WriteLine("They are equivalent");
}
else 
{
    Console.WriteLine("Not the same Monkey");
}
