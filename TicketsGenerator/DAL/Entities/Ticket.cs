using System.Text;

namespace DAL.Entities;
public class Ticket
{
    public List<Question> Questions { get; set; }

    public double AvgVolume => Math.Round(Questions.Average(q => q.Volume), 2);
    public double AvgDifficulty => Math.Round(Questions.Average(q => q.Difficulty), 2);

    public Ticket()
    {
        Questions = new List<Question>();
    }

    public double GetSquaredMeanError(double meanDifficulty, double meanVolume)
    {
        return Questions.Sum(q => q.GetSquaredMeanError(meanDifficulty, meanVolume));
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        //builder.Append($"Avg difficulty: {AvgDifficulty}. Avg volume: {AvgVolume}\n");
        for (int i = 0; i < Questions.Count; i++)
        {
            builder.Append($"{i + 1}. {Questions[i].Text} \n");
        }

        return builder.ToString();
    }
}
