using System;
using System.Windows.Forms;
using System.Xml;

namespace TestsEditor
{
    public partial class QwestForm : Form
    {
        XmlTextWriter testWriter; 
        int count; 

        public QwestForm(int k, XmlTextWriter Writer)
        {
            testWriter = Writer;
            count = k;
            InitializeComponent();
            this.Text = "Редагування питання №" + count;
        }

        private void QwestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (QwestBox.Text != "" && answ1.Text != "" && answ2.Text != "" && answ3.Text != "" && answ4.Text != "" && RightAnswerBox.Text != "")
            {
                if (RightAnswerBox.Text == answ1.Text || RightAnswerBox.Text == answ2.Text || RightAnswerBox.Text == answ3.Text || RightAnswerBox.Text == answ4.Text)
                {
                 
                    testWriter.WriteStartElement("q" + count); 

                    
                    testWriter.WriteStartAttribute("text"); 
                    testWriter.WriteString(QwestBox.Text); 
                    testWriter.WriteEndAttribute(); 

                    
                    testWriter.WriteStartAttribute("right");
                    testWriter.WriteString(RightAnswerBox.Text); 
                    testWriter.WriteEndAttribute(); 

                    
                    testWriter.WriteStartElement("answers");
                    testWriter.WriteString(answ1.Text + "|" + answ2.Text + "|" + answ3.Text + "|" + answ4.Text); 
                    testWriter.WriteEndElement(); 

                    testWriter.WriteEndElement(); 

                    this.Dispose(); 
                }
                else
                {
                    MessageBox.Show("Правильна відповідь не збігається до жодних варіантів відповіді!\n\nПідказка: Скопіюйте правильний варіант у полі правильної відповіді!", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заповніть всі поля!", "Помилка!");
            }
        }
    }
}
