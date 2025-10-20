using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetAndCSharp.Zoo;
public record Monkey
{
    public required string? Name { get; init; }
    public int Age { get; init; }
}

public record Gorilla(string Name, int Age);
