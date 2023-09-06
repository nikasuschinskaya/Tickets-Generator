using DAL.Entities;
using DAL.Readers.Base;
using System.Text.Json;

namespace DAL.Readers;
public class JsonReader : IReader<Question>
{
    public IEnumerable<Question> Read(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File doesn't exist!");
        }

        using var fs = new FileStream(path, FileMode.OpenOrCreate);

        var questions = JsonSerializer.Deserialize<IEnumerable<Question>>(fs);

        return questions;
    }
}