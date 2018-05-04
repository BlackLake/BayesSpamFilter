using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BayesSpamFilter
{
    public partial class FrmMain : Form
    {
        Hashtable hashtableLexemeCounts = new Hashtable();
        Hashtable hashtableSpamicities = new Hashtable();
        Hashtable hashtableNewFileSpamicities = new Hashtable();

        string[] hamFiles;
        string[] spamFiles;
        string[] newFiles;
        int hamLexemeCount = 0;
        int spamLexemeCount = 0;
        int lexemesToBeAnalyzed = 16;
        double newLexemeSpamicity = 0.4;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnHashLexemes_Click(object sender, EventArgs e)
        {
            hamLexemeCount = 0;
            spamLexemeCount = 0;
            txtOutput.Clear();
            hashtableLexemeCounts.Clear();
            hashtableSpamicities.Clear();
            hashtableNewFileSpamicities.Clear();
            hamFiles = Directory.GetFiles(Environment.CurrentDirectory + "//Ham//", "*.txt");
            spamFiles = Directory.GetFiles(Environment.CurrentDirectory + "//Spam//", "*.txt");


            foreach (var file in hamFiles)
            {
                string fileContent = File.ReadAllText(file);
                string[] words = fileContent.Split(' ');
                foreach (var word in words)
                {
                    double[] counts = new double[] { 1, 0 };
                    if (hashtableLexemeCounts.Contains(word))
                    {
                        counts = (double[])hashtableLexemeCounts[word];
                        counts[0]++;
                        hashtableLexemeCounts[word] = counts;
                    }
                    else
                    {
                        hashtableLexemeCounts.Add(word, counts);
                        hamLexemeCount++;
                    }

                }
            }
            foreach (var file in spamFiles)
            {
                string fileContent = File.ReadAllText(file);
                string[] words = fileContent.Split(' ');
                foreach (var word in words)
                {
                    double[] counts = new double[] { 0, 1 };
                    if (hashtableLexemeCounts.Contains(word))
                    {
                        counts = (double[])hashtableLexemeCounts[word];
                        counts[1]++;
                        hashtableLexemeCounts[word] = counts;
                    }
                    else
                    {
                        hashtableLexemeCounts.Add(word, counts);
                        spamLexemeCount++;
                    }

                }
            }

            txtOutput.AppendText("Total lexeme Count : " + (hamLexemeCount + spamLexemeCount).ToString() + "\n");
            txtOutput.AppendText("Ham lexeme Count : " + hamLexemeCount.ToString() + "\n");
            txtOutput.AppendText("Spam lexeme Count : " + spamLexemeCount.ToString() + " \n");
            txtOutput.AppendText("\n");
            btnCalculateSpamacities.Enabled = true;
            btnTestNewFiles.Enabled = false;
        }

        private void CalculateSpamicities(string lexeme)
        {
            double hamProbability;
            double spamProbability;
            double spamicity;

            double[] counts = (double[])hashtableLexemeCounts[lexeme];

            double hamCount = counts[0];
            double spamCount = counts[1];

            hamProbability = hamCount / hamLexemeCount;
            spamProbability = spamCount / spamLexemeCount;
            if (hamProbability == 0)
            {
                spamicity = 0.99;
            }
            else if (spamProbability == 0)
            {
                spamicity = 0.01;
            }
            else
            {
                spamicity = spamProbability / (spamProbability + hamProbability);//BAYES
            }

            hashtableSpamicities.Add(lexeme, spamicity);

        }

        private void btnCalculateSpamacities_Click(object sender, EventArgs e)
        {
            hashtableSpamicities.Clear();
            foreach (var key in hashtableLexemeCounts.Keys)
            {
                CalculateSpamicities((string)key);
            }
            txtOutput.AppendText("Sepematy values for all lexemes has calculated  \n");
            txtOutput.AppendText("\n");
            btnTestNewFiles.Enabled = true;
        }

        private void btnTestNewFiles_Click(object sender, EventArgs e)
        {
            int[] hamTestResults = TestHams();
            int[] spamTestResults = TestSpams();

            txtOutput.AppendText("\n");
            txtOutput.AppendText("Number of SPAMS :" + spamTestResults[0] + "\n");
            txtOutput.AppendText("Number of SPAMS Classified as HAM (false positive) :" + spamTestResults[2] + "\n");
            txtOutput.AppendText("Number of HAMS :" + hamTestResults[0] + "\n");
            txtOutput.AppendText("Number of HAMS Classified as SPAM  (true negative):" + hamTestResults[2] + "\n");
            txtOutput.AppendText("\n");

            double sumOfFiles = (spamTestResults[0] + hamTestResults[0]);
            double sumOfCorrectFiles = (spamTestResults[1] + hamTestResults[1]);
            double ratio = (sumOfCorrectFiles / sumOfFiles)*100;
            txtOutput.AppendText("Overall number of files : " + sumOfFiles + "\n");
            txtOutput.AppendText("Correctly classified number of files : " + sumOfCorrectFiles + "\n");
            txtOutput.AppendText("Ratio (%) between correctly classified files and overall number of files : " + ratio + "\n");
        }

        private double ClassifyNewFile()
        {
            double multiply = 1;
            double minusOneMultiply = 1;

            foreach (var key in hashtableNewFileSpamicities.Keys)
            {
                multiply *= (double)hashtableNewFileSpamicities[key];
                minusOneMultiply *= (1 - (double)hashtableNewFileSpamicities[key]);
            }

            double result = multiply / (multiply + minusOneMultiply);

            return result;
        }

        private int[] TestSpams()
        {
            int counter = 0;
            int rightCounter = 0;
            int wrongCounter = 0;
            hashtableNewFileSpamicities.Clear();
            int lexemesCounter = 0;

            newFiles = Directory.GetFiles(Environment.CurrentDirectory + "//New Spam//", "*.txt");

            foreach (var file in newFiles)
            {
                lexemesCounter = 0;
                string fileContent = File.ReadAllText(file);
                string[] words = fileContent.Split(' ');
                foreach (var word in words)
                {
                    if (lexemesCounter >= lexemesToBeAnalyzed)
                    {
                        break;
                    }
                    if (!hashtableNewFileSpamicities.Contains(word))
                    {
                        if (hashtableSpamicities.Contains(word))
                        {
                            hashtableNewFileSpamicities.Add(word, hashtableSpamicities[word]);
                            lexemesCounter++;
                        }
                        else
                        {
                            hashtableNewFileSpamicities.Add(word, newLexemeSpamicity);
                            lexemesCounter++;
                        }
                    }
                }
                double result = ClassifyNewFile();
                if (result > 0.5)
                {
                    txtOutput.AppendText(file.Split('/').Last() + " file is A SPAM with the value of : " + result + " \n");
                    rightCounter++;
                }
                else
                {
                    txtOutput.AppendText(file.Split('/').Last() + " file is NOT A SPAM with the value of : " + result + " \n");
                    wrongCounter++;
                }
                counter++;
                hashtableNewFileSpamicities.Clear();

            }
            return new int[] { counter, rightCounter, wrongCounter };
        }
        private int[] TestHams()
        {
            int counter = 0;
            int rightCounter = 0;
            int wrongCounter = 0;
            hashtableNewFileSpamicities.Clear();
            int lexemesCounter = 0;

            newFiles = Directory.GetFiles(Environment.CurrentDirectory + "//New Ham//", "*.txt");

            foreach (var file in newFiles)
            {
                lexemesCounter = 0;
                string fileContent = File.ReadAllText(file);
                string[] words = fileContent.Split(' ');
                foreach (var word in words)
                {
                    if (lexemesCounter >= lexemesToBeAnalyzed)
                    {
                        break;
                    }
                    if (!hashtableNewFileSpamicities.Contains(word))
                    {
                        if (hashtableSpamicities.Contains(word))
                        {
                            hashtableNewFileSpamicities.Add(word, hashtableSpamicities[word]);
                            lexemesCounter++;
                        }
                        else
                        {
                            hashtableNewFileSpamicities.Add(word, newLexemeSpamicity);
                            lexemesCounter++;
                        }
                    }
                }
                double result = ClassifyNewFile();
                if (result > 0.5)
                {
                    txtOutput.AppendText(file.Split('/').Last() + " file is A SPAM with the value of : " + result + " \n");
                    wrongCounter++;
                }
                else
                {
                    txtOutput.AppendText(file.Split('/').Last() + " file is NOT A SPAM with the value of : " + result + " \n");
                    rightCounter++;
                }
                counter++;
                hashtableNewFileSpamicities.Clear();

            }
            return new int[] { counter, rightCounter, wrongCounter };
        }
    }
}
