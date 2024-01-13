int[] numbers = Enumerable.Range(1, 100).ToArray();
int[] winners = Random.Shared.GetItems(numbers, 2);
winners.ToList().ForEach(Console.WriteLine);

Random.Shared.Shuffle(numbers);

System.Console.WriteLine();

numbers.ToList().ForEach(Console.WriteLine);