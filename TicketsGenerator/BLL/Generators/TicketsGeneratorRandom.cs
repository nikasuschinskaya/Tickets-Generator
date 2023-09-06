using BLL.Generators.Base;
using DAL.Entities;

namespace BLL.Generators;
public class TicketsGeneratorRandom : ITicketGenerator
{
    private readonly IReadOnlyCollection<Question> _questions;
    private readonly IReadOnlyCollection<int> _uniqueChapers;
    private readonly Dictionary<int, List<Ticket>> _chapterTickets;
    private readonly List<Ticket> _tickets;
    private readonly int _epochs;

    public TicketsGeneratorRandom(IEnumerable<Question> questions, int epochs = 20)
    {
        _epochs = epochs;
        _tickets = new List<Ticket>();
        _questions = questions.ToList().AsReadOnly();
        _uniqueChapers = _questions.DistinctBy(x => x.Chapter)
                                   .Select(x => x.Chapter)
                                   .OrderBy(x => x)
                                   .ToList();

        _chapterTickets = new Dictionary<int, List<Ticket>>();
        foreach (var chapter in _uniqueChapers)
        {
            _chapterTickets.Add(chapter, new List<Ticket>());
        }
    }

    private double GetAvgDifficulty() => _questions.Average(q => q.Difficulty);
    private double GetAvgVolume() => _questions.Average(q => q.Volume);
    private double GetAvgMeanError() => _tickets.Any() ?
        _tickets.Sum(t => t.GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume())) : int.MaxValue;

    public List<Ticket> Generate(int count)
    {
        FirstIteration(count);

        for (int epoch = 0; epoch < _epochs; epoch++)
        {
            var tickets = new List<Ticket>();
            var questions = _questions.ToList();
            var ticketsCount = questions.GroupBy(x => x.Chapter).Min(x => x.Count());

            for (int i = 0; i < ticketsCount; i++)
            {
                var ticket = new Ticket();

                //Добавляем первый билет 
                var random = new Random();
                var randomIndex = random.Next(questions.Count);
                ticket.Questions.Add(questions[randomIndex]);
                questions.RemoveAt(randomIndex);

                for (int _ = 0; _ < count - 1; _++)
                {
                    var usedChapters = ticket.Questions.Select(q => q.Chapter).ToList();
                    var questionsToUse = questions.Where(x => !usedChapters.Contains(x.Chapter)).ToList();

                    //Ищем наиболее подходящий из оставшихся вопрос
                    int indexOfMostSuitable = 0;
                    double globalValueOfMostSuitable = questionsToUse[0].GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume()) +
                        ticket.GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume());

                    for (int j = 1; j < questionsToUse.Count; j++)
                    {
                        double valueOfMostSuitable = questionsToUse[j].GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume()) +
                            ticket.GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume());

                        if (globalValueOfMostSuitable > valueOfMostSuitable)
                        {
                            globalValueOfMostSuitable = valueOfMostSuitable;
                            indexOfMostSuitable = j;
                        }
                    }

                    //Добавляем наиболее подходящий в билет
                    ticket.Questions.Add(questionsToUse[indexOfMostSuitable]);

                    int indxToDel = questions.IndexOf(questionsToUse[indexOfMostSuitable]);
                    questions.RemoveAt(indxToDel);

                    //questions.RemoveAt(indexOfMostSuitable);
                }

                tickets.Add(ticket);
            }

            var ticketsMeanError = tickets.Sum(t => t.GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume()));
            if (GetAvgMeanError() > ticketsMeanError)
            {
                RewriteTickets(tickets);
            }
        }

        return _tickets;
    }

    private void RewriteTickets(IEnumerable<Ticket> tickets)
    {
        _tickets.Clear();
        foreach (var t in tickets)
        {
            _tickets.Add(t);
        }
    }

    private void FirstIteration(int count)
    {
        var questions = _questions.ToList();
        var ticketsCount = questions.GroupBy(x => x.Chapter).Min(x => x.Count());

        for (int i = 0; i < ticketsCount; i++)
        {
            var ticket = new Ticket();

            //Добавляем первый билет 
            var random = new Random();
            var randomIndex = random.Next(questions.Count);
            ticket.Questions.Add(questions[randomIndex]);
            questions.RemoveAt(randomIndex);

            for (int _ = 0; _ < count - 1; _++)
            {
                var usedChapters = ticket.Questions.Select(q => q.Chapter).ToList();
                var questionsToUse = questions.Where(x => !usedChapters.Contains(x.Chapter)).ToList();

                //Ищем наиболее подходящий из оставшихся вопрос
                int indexOfMostSuitable = 0;
                double globalValueOfMostSuitable = questionsToUse[0].GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume()) +
                    ticket.GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume());

                for (int j = 1; j < questionsToUse.Count; j++)
                {
                    double valueOfMostSuitable = questionsToUse[j].GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume()) +
                        ticket.GetSquaredMeanError(GetAvgDifficulty(), GetAvgVolume());

                    if (globalValueOfMostSuitable > valueOfMostSuitable)
                    {
                        globalValueOfMostSuitable = valueOfMostSuitable;
                        indexOfMostSuitable = j;
                    }
                }

                //Добавляем наиболее подходящий в билет
                ticket.Questions.Add(questionsToUse[indexOfMostSuitable]);

                int indxToDel = questions.IndexOf(questionsToUse[indexOfMostSuitable]);
                questions.RemoveAt(indxToDel);

                //questions.RemoveAt(indexOfMostSuitable);
            }

            _tickets.Add(ticket);
        }
    }
}
