using BLL;
using BLL.Generators;
using DAL.Entities;

Start(3, 24, 2);
Start(3, 24, 3);
//Start(15, 240, 4);
//Start(15, 240, 5);
//Start(5, 600, 6);
//Start(5, 600, 10);
//Start(5, 600, 15);
//Start(5, 600, 20);
//Start(5, 600, 30);
//Start(5, 600, 40);
//Start(5, 600, 50);
//Start(5, 600, 60);
//Start(5, 600, 100);

static void Start(int epochs, int questionsCount, int questionsCountInATicket)
{
    //double meanError1 = 0;
    //double meanError2 = 0;
    //double meanError3 = 0;

    //for (int i = 0; i < epochs; i++)
    //{
    //    var questions = QuestionsGenerator.GetQuestions(questionsCount);

    //    var previouslyAvgDifficulty = questions.Average(x => x.Difficulty);
    //    var previouslyAvgVolume = questions.Average(x => x.Volume);

    //    //Console.WriteLine($"Diff: {previouslyAvgDifficulty}. Volume: {previouslyAvgVolume}");

    //    var generator1 = new TicketsGenerator(questions.ToList());
    //    var tickets1 = generator1.GenerateFirstIdea(questionsCountInATicket);

    //    var generator2 = new TicketsGeneratorRandom(questions.ToList(), 10);
    //    var tickets2 = generator2.Generate(questionsCountInATicket);
    //    //var generator3 = new TicketsGenerator(questions.ToList());
    //    //var tickets3 = generator3.GenerateThirdIdea2(questionsCountInATicket);

    //    meanError1 += tickets1.Sum(x => x.GetSquaredMeanError(previouslyAvgDifficulty, previouslyAvgVolume));
    //    meanError2 += tickets2.Sum(x => x.Value.Sum(t => t.GetSquaredMeanError(previouslyAvgDifficulty, previouslyAvgVolume)));

    //    //meanError3 += tickets3.Sum(x => x.GetSquaredMeanError(previouslyAvgDifficulty, previouslyAvgVolume));

    //    //avgDiffOtklon1 += tickets1.Sum(t => Math.Abs(previouslyAvgDifficulty - t.AvgDifficulty)) / tickets1.Count();
    //    //avgVolumeOtklon1 += tickets1.Sum(t => Math.Abs(previouslyAvgVolume - t.AvgVolume)) / tickets1.Count();

    //    //avgDiffOtklon2 += tickets2.Sum(t => Math.Abs(previouslyAvgDifficulty - t.AvgDifficulty)) / tickets2.Count();
    //    //avgVolumeOtklon2 += tickets2.Sum(t => Math.Abs(previouslyAvgVolume - t.AvgVolume)) / tickets2.Count();

    //    //avgDiffOtklon3 += tickets3.Sum(t => Math.Abs(previouslyAvgDifficulty - t.AvgDifficulty)) / tickets3.Count();
    //    //avgVolumeOtklon3 += tickets3.Sum(t => Math.Abs(previouslyAvgVolume - t.AvgVolume)) / tickets3.Count();
    //}

    //Console.WriteLine($"Count questions in a ticket: {questionsCountInATicket}");
    //Console.WriteLine($"Old: {meanError1 / epochs}");
    //Console.WriteLine($"New: {meanError2 / epochs}\n\n");
    //Console.WriteLine($"1. avgDiffOtklon: {avgDiffOtklon1 / epochs}. avgVolumeOtklon: {avgVolumeOtklon1 / epochs}");
    //Console.WriteLine($"2. avgDiffOtklon: {avgDiffOtklon2 / epochs}. avgVolumeOtklon: {avgVolumeOtklon2 / epochs}");
    //Console.WriteLine($"3. avgDiffOtklon: {avgDiffOtklon3 / epochs}. avgVolumeOtklon: {avgVolumeOtklon3 / epochs}\n");
}