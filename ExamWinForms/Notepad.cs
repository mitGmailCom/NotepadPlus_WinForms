using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamWinForms
{
    public partial class FormNotepadPus : Form
    {
        string path1 = "newTextFile";
        string path2 = ".txt";
        string fullPath = null;
        private string CopyText { get; set; } = null;
        private int N { get; set; } = 1;
        private bool FlagStStrip { get; set; } = false;
        int indText = 0;
        int indLastIndNewString = 0;
        int currentX = 0;
        int currentY = 1;
        bool flagShowCurPos = false;
        List<string> strMas = new List<string>();
        bool flafFirstStep = true;



        public FormNotepadPus()
        {
            InitializeComponent();
        }


        private void FormNotepadPus_Load(object sender, EventArgs e)
        {
            statusStripNotepad.Visible = false;
            
        }



        private void CheckNullX()
        {
            if (flafFirstStep == false)
            {
                if (strMas[currentY - 1].Length == 0)
                {
                    toolStripStatusLabelNotepad.Text = $"строка - {currentY} позиция - {0}";
                    flagShowCurPos = true;
                }
            }
            if (flafFirstStep == true)
            {
                if (strMas[0].Length == 0)
                {
                    toolStripStatusLabelNotepad.Text = $"строка - {currentY} позиция - {0}";
                    flagShowCurPos = true;
                    flafFirstStep = false;
                }
                flafFirstStep = false;
            }
                       
        }


        private void ShowPosition(string val)
        {
            flagShowCurPos = false;

            //Point pntCursorPosition = new Point();
            // pntCursorPosition.X = txbTextEditor.GetLineFromCharIndex(txbTextEditor.SelectionStart);
            // pntCursorPosition.Y = txbTextEditor.SelectionStart - txbTextEditor.GetFirstCharIndexOfCurrentLine();


            if (val == "Right")
            {
                currentX = currentX + 1;
                indText += 1;

                
                CheckNullX();

                if (flagShowCurPos == false)
                {
                    toolStripStatusLabelNotepad.Text = $"строка - {currentY} позиция - {currentX}";
                    
                }
            }

            if (val == "Left")
            {
                flagShowCurPos = false;
                currentX = currentX - 1;//txbTextEditor.SelectionStart;
                indText -= 1;
                CheckNullX();

                if (flagShowCurPos == false)
                {
                    toolStripStatusLabelNotepad.Text = $"строка - {currentY} позиция - {currentX}";
                }
            }

            if (val == "Up")
            {
                flagShowCurPos = false;
                /*pntCursorPosition.X*/
                currentY = currentY - 1;
                CheckNullX();

                if (flagShowCurPos == false)
                {
                    toolStripStatusLabelNotepad.Text = $"строка - {currentY} позиция - {currentX}";
                   
                }
            }

            if (val == "Down")
            {
                flagShowCurPos = false;
                currentY = currentY + 1;
                CheckNullX();

                if (flagShowCurPos == false)
                {
                    toolStripStatusLabelNotepad.Text = $"строка - {currentY} позиция - {currentX}";
                   
                }
            }
        }


        private void Recur()
        {
            if (File.Exists(path1 + N.ToString() + path2))
            {
                N++;
                Recur();
            }
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(path1 + path2))
                            {
                                using (FileStream fstream = new FileStream(path1 + path2, FileMode.CreateNew, FileAccess.Write))
                    N++;
                fullPath = path1 + path2;
                            }
                        else
            {
                Recur();
                                using (FileStream fstream = new FileStream(path1 + N.ToString() + path2, FileMode.CreateNew, FileAccess.Write))
                    fullPath = path1 + N.ToString() + path2;
                            }
            txbTextEditor.Text = "";
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text_files(*.txt)|*.txt|All_files(*.*)|*.*";
            openFile.FilterIndex = 1;
                        if (openFile.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrWhiteSpace(openFile.FileName))
                                    {
                                        using (StreamReader reader = new StreamReader(openFile.FileName, Encoding.Default))
                                            {
                        txbTextEditor.Text = reader.ReadToEnd();
                                            }
                                    }
                            }
            this.Text = openFile.FileName;
            fullPath = openFile.FileName;
            String[] substrings = txbTextEditor.Text.Split('\n');
            foreach (var item in substrings)
            {
                strMas.Add(item);
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (N == 0)
                            {
                                using (FileStream fstream = new FileStream(fullPath, FileMode.Open, FileAccess.Write))
                                    {
                                        using (StreamWriter sw = new StreamWriter(fstream, Encoding.Unicode))
                                            {
                        sw.WriteLine(txbTextEditor.Text);
                                            }
                                    }
                            }
            
                        else
            {
                                using (FileStream fstream = new FileStream(fullPath, FileMode.Open, FileAccess.Write))
                                    {
                                        using (StreamWriter sw = new StreamWriter(fstream, Encoding.Unicode))
                                            {
                        sw.WriteLine(txbTextEditor.Text);
                                            }
                                    }
                            }
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Текстовый файл(*.txt) | *.txt";
                        if (save.ShowDialog() == DialogResult.OK)
                            {
                                using (StreamWriter writer = new StreamWriter(save.FileName))
                                    {
                    writer.Write(this.txbTextEditor.Text);
                    writer.Close();
                                    }
                            }
            this.Text = save.FileName;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyText = txbTextEditor.SelectedText;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txbTextEditor.SelectedText = CopyText;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyText = txbTextEditor.SelectedText;
            txbTextEditor.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txbTextEditor.SelectAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txbTextEditor.SelectedText = "";
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txbTextEditor.Undo();
        }



        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorBackground = new ColorDialog();
                        if (colorBackground.ShowDialog() == DialogResult.OK)
                this.txbTextEditor.BackColor = colorBackground.Color;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
                        if (font.ShowDialog() == DialogResult.OK)
                this.txbTextEditor.Font = font.Font;
        }



        private void stringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FlagStStrip == false)
            {
                FlagStStrip = true;
                statusStripNotepad.Visible = true;
                
            }
            else if (FlagStStrip == true)
            {
                FlagStStrip = false;
                statusStripNotepad.Visible = false;
            }
        }

        private void txbTextEditor_KeyDown(object sender, KeyEventArgs e)
        {
            ShowPosition(e.KeyData.ToString());
        }

        private void txbTextEditor_MouseDown(object sender, MouseEventArgs e)
        {
            int countStrok = 0;
            int X = e.Location.X;
            int Y = e.Location.Y;
            indText = txbTextEditor.GetCharIndexFromPosition(new Point(X, Y));
            string tt = txbTextEditor.Text.Substring(0, indText);
            int indLastIndNewString = tt.LastIndexOf('\n');
            int ttt = indText - indLastIndNewString - 1;
            currentX = ttt;

            for (int i = 0; i < tt.Length; i++)
            {
                if (txbTextEditor.Text[i] == '\n')
                        countStrok++;
            }
            currentY = countStrok;
            toolStripStatusLabelNotepad.Text = $"строка - {currentY} позиция - {currentX}";
        }

    }
}
