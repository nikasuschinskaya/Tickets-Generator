using DAL.Entities;

namespace BLL;
public static class QuestionsGenerator
{
    public static IEnumerable<Question> GetQuestions(int questionCount = 30)
    {
        var questions = new List<Question>();

        for (int i = 0; i < questionCount; i++)
        {
            string text = $"Lorem and bla-bla-bla...{i + 1}";
            int difficulty = Random.Shared.Next(1, 11);
            int volume = Random.Shared.Next(1, 11);
            int chapter = Random.Shared.Next(1, 16);
            questions.Add(new Question(text, volume, difficulty, chapter));
        }

        return questions;
    }
}
