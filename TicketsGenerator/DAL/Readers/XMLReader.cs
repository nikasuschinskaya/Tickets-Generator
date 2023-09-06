using DAL.Entities;
using DAL.Readers.Base;
using System.Xml.Linq;

namespace DAL.Readers;
public class XMLReader : IReader<Question>
{
    public IEnumerable<Question> Read(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File doesn't exist!");
        }

        var xdoc = XDocument.Load(path);
        var root = xdoc.Element("questions");

        var questions = new List<Question>();
        if (root is not null)
        {
            foreach (var qElem in root.Elements("question"))
            {
                try
                {
                    var text = qElem.Element("text").Value;
                    var difficulty = int.Parse(qElem.Element("difficulty").Value);
                    var volume = int.Parse(qElem.Element("volume").Value);
                    var chapter = int.Parse(qElem.Element("chapter").Value);

                    questions.Add(new Question(text, volume, difficulty, chapter));
                }
                catch (Exception) { }
            }
        }

        return questions;
    }
}
