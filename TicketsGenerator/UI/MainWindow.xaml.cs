using BLL.Generators;
using DAL.Entities;
using DAL.Readers;
using DAL.Readers.Base;
using DAL.Writers;
using DAL.Writers.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace UI;
public partial class MainWindow : Window
{
    private List<Ticket> _tickets;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenFileButton_Click(object sender, RoutedEventArgs e)
    {
        int countQuestions;

        try
        {
            countQuestions = Convert.ToInt32(countQuestionsTB.Text);
        }
        catch (Exception)
        {
            MessageBox.Show("Введите число!");
            return;
        }

        if (countQuestions <= 1)
        {
            MessageBox.Show("Количество вопросов не может быть меньше нуля!");
            return;
        }

        var dialog = new OpenFileDialog();

        if (!IsInputFileValid(dialog))
        {
            return;
        }

        saveFileBut.IsEnabled = true;

        var fileName = dialog.FileName;
        var extension = Path.GetExtension(dialog.FileName);

        IReader<Question> reader = GetReader(extension);

        var questions = reader.Read(fileName).ToList();

        var generator = new TicketsGeneratorRandom(questions);
        _tickets = generator.Generate(countQuestions);

        var builder = new StringBuilder();
        int i = 1;
        foreach (var ticket in _tickets)
        {
            builder.AppendLine($"Билет №{i}");
            builder.AppendLine(ticket.ToString());
            i++;
        }

        //MessageBox.Show(_tickets.Count().ToString());

        newFileNameTB.Text = builder.ToString();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        IWriter writer = new WordTicketsWriter();

        var builder = new StringBuilder();
        int i = 1;
        foreach (var ticket in _tickets)
        {
            builder.AppendLine($"Билет: {i++}");
            builder.AppendLine(ticket.ToString());
        }

        writer.Write(builder.ToString());

        MessageBox.Show("Успешно!");
    }

    private static IReader<Question> GetReader(string extension)
    {
        return extension switch
        {
            ".json" => new JsonReader(),
            ".xml" => new XMLReader(),
            ".csv" => new CSVReader(),
            ".xlsx" => new XLSReader(),
            _ => new XLSReader()
        };
    }

    private bool IsInputFileValid(OpenFileDialog dialog)
    {
        if (dialog.ShowDialog() == false)
        {
            MessageBox.Show("Выберите файл!");
            return false;
        }

        var extension = Path.GetExtension(dialog.FileName);

        if (string.Compare(extension, ".json") == 0 ||
            string.Compare(extension, ".xml") == 0 ||
            string.Compare(extension, ".csv") == 0 ||
            string.Compare(extension, ".xls") == 0 ||
            string.Compare(extension, ".xlsx") == 0)
        {
            return true;
        }

        MessageBox.Show("Неверный формат файла. Выберите (.json/.xml/.csv/.xlsx/.xls)");

        return false;
    }
}
