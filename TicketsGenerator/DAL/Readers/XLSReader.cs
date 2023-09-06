using DAL.Entities;
using DAL.Readers.Base;
using IronXL;

namespace DAL.Readers
{
    public class XLSReader : IReader<Question>
    {
        public IEnumerable<Question> Read(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File doesn't exist!");
            }

            var questions = new List<Question>();

            WorkBook wb = WorkBook.Load(path);
            WorkSheet ws = wb.WorkSheets.FirstOrDefault();

            for (int i = 0; i < ws.RowCount; i++)
            {
                var text = ws[$"A{i + 1}"].Value.ToString();
                Int32 volume = ws[$"B{i + 1}"].Int32Value;
                Int32 difficulty = ws[$"C{i + 1}"].Int32Value;
                Int32 chapter = ws[$"D{i + 1}"].Int32Value;

                questions.Add(new Question(text, volume, difficulty, chapter));
            }

            return questions;
        }
    }
}
