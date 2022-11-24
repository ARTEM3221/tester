using System;
using System.Windows.Forms;
using mark;

namespace Tests
{
    public partial class FinalForm : Form
    {
        double Mark; 

        public FinalForm(string PersonName, string Theme, int NumbersOfQwest, int RightAnswers)
        {
            InitializeComponent();

            NameLabel.Text += PersonName;
            ThemeLabel.Text = Theme;
            NumbersLabel.Text += NumbersOfQwest.ToString();
            RightLabel.Text += RightAnswers.ToString();

            Mark = mark.MarkClass.Mark(NumbersOfQwest, RightAnswers);
      
            MarkLabel.Text += Mark.ToString();            
        }

        #region Exit
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
