using System.Collections.Frozen;

List<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

var fs = numbers.ToFrozenSet();

System.Console.WriteLine(numbers.Count);
System.Console.WriteLine(fs.Count);

numbers.Add(11);

System.Console.WriteLine(numbers.Count);
System.Console.WriteLine(fs.Count);


var dictionary = new Dictionary<int, string>
{
    { 1, "one" },
    { 2, "two" },
    { 3, "three" }
};

var fd = dictionary.ToFrozenDictionary();

fd.TryAdd(4, "four");
