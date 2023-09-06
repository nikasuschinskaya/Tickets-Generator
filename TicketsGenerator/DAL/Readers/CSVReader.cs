using CsvHelper;
using CsvHelper.Configuration;
using DAL.Entities;
using DAL.Readers.Base;
using System.Globalization;

namespace DAL.Readers;
public class CSVReader : IReader<Question>
{
    public IEnumerable<Question> Read(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File doesn't exist!");
        }

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };

        using var streamReader = File.OpenText(path);
        using var csvReader = new CsvReader(streamReader, csvConfig);

        var questions = new List<Question>();
        while (csvReader.Read())
        {
            try
            {
                var text = csvReader.GetField(0);
                var volume = csvReader.GetField<int>(1);
                var difficulty = csvReader.GetField<int>(2);
                var chapter = csvReader.GetField<int>(3);

                questions.Add(new Question(text, volume, difficulty, chapter));
            }
            catch (Exception) { }
        }

        return questions;
    }
}
