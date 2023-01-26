using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WordProcessingApplication
{
    public partial class MainWindow : Window
    {
        private List<string> lines;
        public MainWindow()
        {
            lines = new List<string>();
            InitializeComponent();
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filepathTxtBox.Text;
            if (string.IsNullOrWhiteSpace(filepath))
            {
                MessageBox.Show("File path text box is empty!");
                return;
            }

            loadEvent(filepath);
        }

        private void loadEvent(string filepath)
        {
            try
            {
                StreamReader reader = new StreamReader(filepath);
                string wordToRead;
                while ((wordToRead = reader.ReadLine()) != null)
                {
                    lines.Add(wordToRead);
                }

                showLinesOnScreen(lines);
                reader.Close();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File is not found!");
                return;
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filepathTxtBox.Text;
            var isAvailable = (from line in lines
                               select line).Any();

            if (!isAvailable)
            {
                MessageBox.Show("Cannot save. There is no available data");
                return;
            }

            saveFile(filepath);
        }

        private void saveFile(string filepath)
        {
            File.Delete(filepath);
            StreamWriter writer = new StreamWriter(filepath);
            string parsedLines = "";
            foreach (string line in lines)
            {
                parsedLines = line + "\n";
                writer.Write(parsedLines);
                writer.Flush();
            }

            writer.Close();
            MessageBox.Show("File was saved successfully!");
            return;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void swapWordsBtn_Click(object sender, RoutedEventArgs e)
        {
            swapWords();
        }

        private void swapWords()
        {
            string firstIndexLineInput = firstLineTxtBox.Text;
            string secondIndexLineInput = secondLineTxtBox.Text;
            string firstIndexWordInput = firstWordTxtBox.Text;
            string secondIndexWordInput = secondWordTxtBox.Text;
            int firstIndexLine = int.Parse(firstIndexLineInput) - 1;
            int secondIndexLine = int.Parse(secondIndexLineInput) - 1;
            int firstIndexWord = int.Parse(firstIndexWordInput) - 1;
            int secondIndexWord = int.Parse(secondIndexWordInput) - 1;
            string firstWordData = "";


            if ((firstIndexLine > lines.Count) || (secondIndexLine >= lines.Count))
            {
                MessageBox.Show("You entered a line that doesn't exist! The file has" + lines.Count + " lines!");
                return;
            }

            string[] firstLineWords = lines[firstIndexLine].Split(" ");
            if (firstIndexLine != secondIndexLine)
            {
                string[] secLineWords = lines[secondIndexLine].Split(" ");
                firstWordData = firstLineWords[firstIndexWord];
                firstLineWords[firstIndexWord] = secLineWords[secondIndexWord];
                secLineWords[secondIndexWord] = firstWordData;
                lines[firstIndexLine] = string.Join(" ", firstLineWords);
                lines[secondIndexLine] = string.Join(" ", secLineWords);

                showLinesOnScreen(lines);
                return;
            }

            firstWordData = firstLineWords[firstIndexWord];
            firstLineWords[firstIndexWord] = firstLineWords[secondIndexWord];
            firstLineWords[secondIndexWord] = firstWordData;
            lines[firstIndexLine] = string.Join(" ", firstLineWords);

            showLinesOnScreen(lines);
        }

        private void swapLinesBtn_Click(object sender, RoutedEventArgs e)
        {
            string firstIndexLineInput = firstLineTxtBox.Text;
            string secondIndexLineInput = secondLineTxtBox.Text;
            int firstIndexLine = int.Parse(firstIndexLineInput) - 1;
            int secondIndexLine = int.Parse(secondIndexLineInput) - 1;

            if ((firstIndexLine > lines.Count) || (secondIndexLine >= lines.Count))
            {
                MessageBox.Show("You entered an invalid number! The file has " + lines.Count + " lines!");
                return;
            }

            if (firstIndexLine == secondIndexLine)
            {
                MessageBox.Show("Enter two different indexes to swap lines!");
                return;
            }

            lines.Swap(firstIndexLine, secondIndexLine);
            showLinesOnScreen(lines);
        }

        private void showLinesOnScreen(List<string> lines)
        {
            string parsedLines = "";
            foreach (string word in lines)
                parsedLines += word.ToString() + "\n";

            linesTxtBlock.Text = "";
            linesTxtBlock.Text = parsedLines;
        }
    }
}
