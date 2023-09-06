namespace DAL.Entities;
public class Question
{
    public string Text { get; set; }
    public int Volume { get; set; }
    public int Difficulty { get; set; }
    public int Chapter { get; set; }

    public Question(string text, int volume, int difficulty, int chapter)
    {
        Text = text;
        Volume = volume;
        Difficulty = difficulty;
        Chapter = chapter;
    }

    public double GetSquaredMeanError(double meanDifficulty, double meanVolume)
    {
        return Math.Pow(meanDifficulty - Difficulty, 2) + Math.Pow(meanVolume - Volume, 2);
    }

    public override string ToString()
    {
        return $"Question [Text: {Text}, Volume: {Volume}, Difficulty: {Difficulty}, Chapter: {Chapter}]";
    }

    public override bool Equals(object? obj)
    {
        return obj is Question question &&
               Text == question.Text &&
               Volume == question.Volume &&
               Difficulty == question.Difficulty &&
               Chapter == question.Chapter;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Text, Volume, Difficulty, Chapter);
    }
}
