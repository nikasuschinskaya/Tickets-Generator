using DAL.Entities;

namespace BLL;
public class TicketsGenerator
{
    private readonly List<Question> _questions;

    public TicketsGenerator(List<Question> questions)
    {
        _questions = questions;
    }

    /// <summary>
    /// Generates tickets
    /// </summary>
    /// <param name="count">Count questions in a ticket</param>
    /// <returns>Generated tickets</returns>
    public IEnumerable<Ticket> GenerateFirstIdea(int count)
    {
        var ticketsCount = _questions.Count / count;
        var tickets = new List<Ticket>();

        for (int i = 0; i < ticketsCount; i++)
        {
            var ticket = new Ticket();

            //Добавляем первые n-1 вопросов в билет
            for (int j = 0; j < count - 1; j++)
            {
                ticket.Questions.Add(_questions[j]);
            }

            //Удаляем n-1 вопросов из списка вопросов
            for (int j = 0; j < count - 1; j++)
            {
                _questions.RemoveAt(0);
            }

            //Ищем наиболее подходящий из оставшихся вопрос
            var previouslyDifficulty = _questions.Average(x => x.Difficulty) * count;
            var previouslyVolume = _questions.Average(x => x.Volume) * count;

            int indexOfMostSuitable = 0;
            double globalValueOfMostSuitable = 0; //Сумма разниц по модулю между средними и текущими значениями
            globalValueOfMostSuitable += 
                Math.Abs(previouslyDifficulty - ticket.Questions.Sum(x => x.Difficulty) - _questions[0].Difficulty);

            globalValueOfMostSuitable += 
                Math.Abs(previouslyVolume - ticket.Questions.Sum(x => x.Volume) - _questions[0].Volume);
            for (int j = 1; j < _questions.Count; j++)
            {
                double valueOfMostSuitable = 
                    Math.Abs(previouslyDifficulty - ticket.Questions.Sum(x => x.Difficulty) - _questions[j].Difficulty);

                valueOfMostSuitable += 
                    Math.Abs(previouslyVolume - ticket.Questions.Sum(x => x.Volume) - _questions[j].Volume);

                if (globalValueOfMostSuitable > valueOfMostSuitable)
                {
                    globalValueOfMostSuitable = valueOfMostSuitable;
                    indexOfMostSuitable = j;
                }
            }

            ticket.Questions.Add(_questions[indexOfMostSuitable]);
            _questions.RemoveAt(indexOfMostSuitable);

            tickets.Add(ticket);
        }

        return tickets;
    }

    public IEnumerable<Ticket> GenerateFirstIdea2(int count)
    {
        var ticketsCount = _questions.Count / count;
        var previouslyAvgDifficulty = _questions.Average(x => x.Difficulty) * count;
        var previouslyAvgVolume = _questions.Average(x => x.Volume) * count;
        var tickets = new List<Ticket>();

        for (int i = 0; i < ticketsCount; i++)
        {
            var ticket = new Ticket();

            //Добавляем первые n-1 вопросов в билет
            for (int j = 0; j < count - 1; j++)
            {
                ticket.Questions.Add(_questions[j]);
            }

            //Удаляем n-1 вопросов из списка вопросов
            for (int j = 0; j < count - 1; j++)
            {
                _questions.RemoveAt(0);
            }

            //Ищем наиболее подходящий из оставшихся вопрос
            int indexOfMostSuitable = 0;
            double globalValueOfMostSuitable = 0; //Сумма разниц по модулю между средними и текущими значениями
            globalValueOfMostSuitable += Math.Abs(previouslyAvgDifficulty - ticket.Questions.Sum(x => x.Difficulty) - _questions[0].Difficulty);
            globalValueOfMostSuitable += Math.Abs(previouslyAvgVolume - ticket.Questions.Sum(x => x.Volume) - _questions[0].Volume);
            for (int j = 1; j < _questions.Count; j++)
            {
                double valueOfMostSuitable = Math.Abs(previouslyAvgDifficulty - ticket.Questions.Sum(x => x.Difficulty) - _questions[j].Difficulty);
                valueOfMostSuitable += Math.Abs(previouslyAvgVolume - ticket.Questions.Sum(x => x.Volume) - _questions[j].Volume);

                if (globalValueOfMostSuitable > valueOfMostSuitable)
                {
                    globalValueOfMostSuitable = valueOfMostSuitable;
                    indexOfMostSuitable = j;
                }
            }

            ticket.Questions.Add(_questions[indexOfMostSuitable]);
            _questions.RemoveAt(indexOfMostSuitable);

            tickets.Add(ticket);
        }

        return tickets;
    }

    /// <summary>
    /// Generates tickets
    /// </summary>
    /// <param name="count">Count quenstions in a ticket</param>
    /// <returns>Generated tickets</returns>
    public IEnumerable<Ticket> GenerateSecondIdea(int count)
    {
        //var questions = _questions.OrderBy(q => q.Difficulty + q.Volume).ToList();
        var avgDiff = _questions.Average(q => q.Difficulty);
        var avgVolume = _questions.Average(q => q.Volume);
        var questions = _questions.OrderBy(q => (avgDiff - q.Difficulty) + (avgVolume - q.Volume)).ToList();
        var ticketsCount = questions.Count / count;
        var tickets = new List<Ticket>();

        for (int i = 0; i < ticketsCount; i++)
        {
            var ticket = new Ticket();

            //Добавляем n-1 вопросов в билет
            for (int j = 0; j < count - 1; j++)
            {
                if (j % 2 == 0)
                {
                    ticket.Questions.Add(questions[0]);
                    questions.RemoveAt(0);
                }
                else
                {
                    ticket.Questions.Add(questions[^1]);
                    questions.RemoveAt(questions.Count - 1);
                }
            }

            //Ищем наиболее подходящий из оставшихся билет
            var previouslyAvgDifficulty = questions.Average(x => x.Difficulty) * count;
            var previouslyAvgVolume = questions.Average(x => x.Volume) * count;

            int indexOfMostSuitable = 0;
            double globalValueOfMostSuitable = 0; //Сумма разниц по модулю между средними и текущими значениями
            globalValueOfMostSuitable += Math.Abs(previouslyAvgDifficulty - (ticket.Questions.Sum(x => x.Difficulty) + _questions[0].Difficulty) / count);
            globalValueOfMostSuitable += Math.Abs(previouslyAvgVolume - (ticket.Questions.Sum(x => x.Volume) + _questions[0].Volume) / count);
            for (int j = 1; j < questions.Count; j++)
            {
                double valueOfMostSuitable = Math.Abs(previouslyAvgDifficulty - (ticket.Questions.Sum(x => x.Difficulty) + _questions[j].Difficulty) / count);
                valueOfMostSuitable += Math.Abs(previouslyAvgVolume - (ticket.Questions.Sum(x => x.Volume) + _questions[j].Volume) / count);

                if (globalValueOfMostSuitable > valueOfMostSuitable)
                {
                    globalValueOfMostSuitable = valueOfMostSuitable;
                    indexOfMostSuitable = j;
                }
            }

            ticket.Questions.Add(questions[indexOfMostSuitable]);
            questions.RemoveAt(indexOfMostSuitable);

            tickets.Add(ticket);
        }

        return tickets;
    }

    public IEnumerable<Ticket> GenerateSecondIdea2(int count)
    {
        //var questions = _questions.OrderBy(q => q.Difficulty + q.Volume).ToList();
        var avgDiff = _questions.Average(q => q.Difficulty);
        var avgVolume = _questions.Average(q => q.Volume);
        var questions = _questions.OrderBy(q => Math.Abs(avgDiff - q.Difficulty) + Math.Abs(avgVolume - q.Volume))
                                  .ToList();
        var ticketsCount = questions.Count / count;
        var tickets = new List<Ticket>();

        for (int i = 0; i < ticketsCount; i++)
        {
            var ticket = new Ticket();

            //Добавляем n-1 вопросов в билет
            for (int j = 0; j < count - 1; j++)
            {
                if (j % 2 == 0)
                {
                    ticket.Questions.Add(questions[0]);
                    questions.RemoveAt(0);
                }
                else
                {
                    ticket.Questions.Add(questions[^1]);
                    questions.RemoveAt(questions.Count - 1);
                }
            }

            //Ищем наиболее подходящий из оставшихся билет
            var previouslyAvgDifficulty = questions.Average(x => x.Difficulty) * count;
            var previouslyAvgVolume = questions.Average(x => x.Volume) * count;

            int indexOfMostSuitable = 0;
            double globalValueOfMostSuitable = 0; //Сумма разниц по модулю между средними и текущими значениями
            globalValueOfMostSuitable += Math.Abs(previouslyAvgDifficulty - (ticket.Questions.Sum(x => x.Difficulty) + _questions[0].Difficulty) / count);
            globalValueOfMostSuitable += Math.Abs(previouslyAvgVolume - (ticket.Questions.Sum(x => x.Volume) + _questions[0].Volume) / count);
            for (int j = 1; j < questions.Count; j++)
            {
                double valueOfMostSuitable = Math.Abs(previouslyAvgDifficulty - (ticket.Questions.Sum(x => x.Difficulty) + _questions[j].Difficulty) / count);
                valueOfMostSuitable += Math.Abs(previouslyAvgVolume - (ticket.Questions.Sum(x => x.Volume) + _questions[j].Volume) / count);

                if (globalValueOfMostSuitable > valueOfMostSuitable)
                {
                    globalValueOfMostSuitable = valueOfMostSuitable;
                    indexOfMostSuitable = j;
                }
            }

            ticket.Questions.Add(questions[indexOfMostSuitable]);
            questions.RemoveAt(indexOfMostSuitable);

            tickets.Add(ticket);
        }

        return tickets;
    }

    public IEnumerable<Ticket> GenerateThirdIdea(int count)
    {
        //var questions = _questions.OrderBy(q => q.Difficulty + q.Volume).ToList();
        var avgDiff = _questions.Average(q => q.Difficulty);
        var avgVolume = _questions.Average(q => q.Volume);
        var questions = _questions.OrderBy(q => (avgDiff - q.Difficulty) + (avgVolume - q.Volume)).ToList();
        var ticketsCount = questions.Count / count;
        var tickets = new List<Ticket>();

        for (int i = 0; i < ticketsCount; i++)
        {
            var ticket = new Ticket();

            //Добавляем n-2 вопросов в билет
            for (int j = 0; j < count - 2; j++)
            {
                if (j % 2 == 0)
                {
                    ticket.Questions.Add(questions[0]);
                    questions.RemoveAt(0);
                }
                else
                {
                    ticket.Questions.Add(questions[^1]);
                    questions.RemoveAt(questions.Count - 1);
                }
            }

            //Ищем два наиболее подходящих вопроса
            (int, int) indexesOfMostSuitable = (0, 1);
            double globalValueOfMostSuitable = int.MaxValue; //Сумма разниц по модулю между средними и текущими значениями
            for (int j = 0; j < questions.Count; j++)
            {
                for (int k = 0; k < questions.Count; k++)
                {
                    if (j != k)
                    {
                        double valueOfMostSuitable = 0;
                        valueOfMostSuitable +=
                            Math.Abs(avgDiff -
                                     (ticket.Questions.Sum(x => x.Difficulty) +
                                     _questions[j].Difficulty + _questions[k].Difficulty) / count);

                        valueOfMostSuitable +=
                            Math.Abs(avgVolume -
                                     (ticket.Questions.Sum(x => x.Volume) +
                                     _questions[j].Volume + _questions[k].Volume) / count);

                        if (globalValueOfMostSuitable > valueOfMostSuitable)
                        {
                            globalValueOfMostSuitable = valueOfMostSuitable;
                            indexesOfMostSuitable = (j, k);
                        }
                    }
                }
            }

            ticket.Questions.Add(questions[indexesOfMostSuitable.Item1]); //5 2 5 8 9 6 5 4
            ticket.Questions.Add(questions[indexesOfMostSuitable.Item2]);

            if (indexesOfMostSuitable.Item1 > indexesOfMostSuitable.Item2)
            {
                questions.RemoveAt(indexesOfMostSuitable.Item1);
                questions.RemoveAt(indexesOfMostSuitable.Item2);
            }
            else
            {
                questions.RemoveAt(indexesOfMostSuitable.Item2);
                questions.RemoveAt(indexesOfMostSuitable.Item1);
            }

            tickets.Add(ticket);
        }

        return tickets;
    }

    public IEnumerable<Ticket> GenerateThirdIdea2(int count)
    {
        //var questions = _questions.OrderBy(q => q.Difficulty + q.Volume).ToList();
        var avgDiff = _questions.Average(q => q.Difficulty);
        var avgVolume = _questions.Average(q => q.Volume);
        //var questions = _questions.OrderBy(q => (avgDiff - q.Difficulty) + (avgVolume - q.Volume)).ToList();
        var ticketsCount = _questions.Count / count;
        var tickets = new List<Ticket>();

        for (int i = 0; i < ticketsCount; i++)
        {
            if (_questions.Count < count) return tickets;

            var ticket = new Ticket();

            //Добавляем первые n-1 вопросов в билет
            for (int j = 0; j < count - 1; j++)
            {
                ticket.Questions.Add(_questions[j]);
            }

            //Удаляем n-1 вопросов из списка вопросов
            for (int j = 0; j < count - 1; j++)
            {
                _questions.RemoveAt(0);
            }

            //Ищем два наиболее подходящих вопроса
            (int, int) indexesOfMostSuitable = (0, 1);
            double globalValueOfMostSuitable = int.MaxValue; //Сумма разниц по модулю между средними и текущими значениями
            for (int j = 0; j < _questions.Count; j++)
            {
                for (int k = 0; k < _questions.Count; k++)
                {
                    if (j != k)
                    {
                        double valueOfMostSuitable = 0;
                        valueOfMostSuitable +=
                            Math.Abs(avgDiff -
                                     (ticket.Questions.Sum(x => x.Difficulty) +
                                     _questions[j].Difficulty + _questions[k].Difficulty) / count);

                        valueOfMostSuitable +=
                            Math.Abs(avgVolume -
                                     (ticket.Questions.Sum(x => x.Volume) +
                                     _questions[j].Volume + _questions[k].Volume) / count);

                        if (globalValueOfMostSuitable > valueOfMostSuitable)
                        {
                            globalValueOfMostSuitable = valueOfMostSuitable;
                            indexesOfMostSuitable = (j, k);
                        }
                    }
                }
            }

            ticket.Questions.Add(_questions[indexesOfMostSuitable.Item1]);
            ticket.Questions.Add(_questions[indexesOfMostSuitable.Item2]);

            if (indexesOfMostSuitable.Item1 > indexesOfMostSuitable.Item2)
            {
                _questions.RemoveAt(indexesOfMostSuitable.Item1);
                _questions.RemoveAt(indexesOfMostSuitable.Item2);
            }
            else
            {
                _questions.RemoveAt(indexesOfMostSuitable.Item2);
                _questions.RemoveAt(indexesOfMostSuitable.Item1);
            }

            tickets.Add(ticket);
        }

        return tickets;
    }
}
