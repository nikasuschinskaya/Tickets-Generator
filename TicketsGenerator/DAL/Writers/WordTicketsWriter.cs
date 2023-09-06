using DAL.Entities;
using DAL.Writers.Base;
using Microsoft.Office.Interop.Word;
using System.Text;

namespace DAL.Writers;
public class WordTicketsWriter : IWriter
{
    public void Write(string data)
    {
        var app = new Application();
        var doc = app.Documents.Add(Visible: true);
        var r = doc.Range();

        //r.Text = string.Join('\n', values);
        r.Text = data;

        doc.Save();
        try
        {
            doc.Close();
            app.Quit();
        }
        catch (Exception) { }
    }
}
