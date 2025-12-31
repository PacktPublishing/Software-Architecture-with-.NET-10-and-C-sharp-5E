using System;

const int value = 10;

Console.WriteLine(value switch
{
    10 => "variable equals 10",
    0 => "variable equals 0",
    _ => "Unknown behavior"
});