using BLL;
using BLL.Generators;
using BLL.Helpers;
using DAL.Entities;

//var data = QuestionsGenerator.GetQuestions(100).ToList();

//var generator = new TicketsGeneratorRandom(data, 15);
//var chapterTickets = generator.Generate(2);

//foreach (var pair in chapterTickets)
//{
//    Console.WriteLine(pair.Value.Count);
//}

//PrintHelper.PrintDictionaryWithCollection(chapterTickets);

var list = Enumerable.Range(1, 100).ToList();
var list2 = list.Where(x => x % 5 == 0).ToList();

PrintHelper.Print(list2);
Console.WriteLine("\n\n\n");
PrintHelper.Print(list);