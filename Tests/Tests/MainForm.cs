using System;
using System.Xml;
using System.Windows.Forms;

namespace Tests
{
    public partial class MainForm : Form
    {
        XmlReader xmlReader;
        string PersonName; 
        string Theme; 
        int nv; 
        int RightAnsw; 
        int position = 0;   

        string qw; 
        string[] answ = new string[4]; 
        string right;
        bool righting; 

        public MainForm(string TestPath, string personName, string theme)
        {
            InitializeComponent();
            PersonName = personName; 
            Theme = theme; 

            MessageBox.Show("Для початку тестування натисніть \"ОК\"", "Тестування");

            xmlReader = new XmlTextReader(TestPath);
            xmlReader.Read();

            ReadNombers(); 
            LoadQwest();
            ShowQwest();
        }

        #region Чтение количества вопросов
        public void ReadNombers()
        {
           
            do xmlReader.Read();
            while (xmlReader.Name != "qw");

            nv = Convert.ToInt32(xmlReader.GetAttribute("numbers")); 

            xmlReader.Read(); 
        }
        #endregion

        #region Загрузка вопроса
        public void LoadQwest()
        {
            position++;

            if (position > nv)
                Itog();
            else
            {
               
                do xmlReader.Read();
                while (xmlReader.Name != "q" + position);

                if (xmlReader.Name == "q" + position)
                {
                    qw = xmlReader.GetAttribute("text"); 
                    right = xmlReader.GetAttribute("right"); 

                    xmlReader.Read(); 

                   
                    do xmlReader.Read();
                    while (xmlReader.Name != "answers");

                    xmlReader.Read(); 

                    answ = xmlReader.Value.Split('|'); 
                }
            }
        }
        #endregion

        #region Вывод вопроса
        public void ShowQwest()
        {
            QwLabel.Text = qw; 

            
            radioButton0.Text = answ[0];
            radioButton1.Text = answ[1];
            radioButton2.Text = answ[2];
            radioButton3.Text = answ[3];

            NextButton.Enabled = false; 
        }
        #endregion

        #region Проверка
        public void Checked()
        {
            if (righting == true)
                RightAnsw++;
        }
        #endregion

        public void Itog()
        {
            MessageBox.Show("Тестування закінчено!", "Тестування");
            FinalForm FF = new FinalForm(PersonName, Theme, nv, RightAnsw);
            this.Dispose();
            FF.ShowDialog();
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

        private void NextButton_Click(object sender, EventArgs e)
        {
            Checked();
            LoadQwest(); 
            ShowQwest(); 
            righting = false; 
        }

        #region Проверка правоты при выделении RadioButton'a
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            righting = false;

            if (radioButton0.Checked)
            {
                if (radioButton0.Text == right)
                    righting = true;
            }
            if (radioButton1.Checked)
            {
                if (radioButton1.Text == right)
                    righting = true;
            }
            if (radioButton2.Checked)
            {
                if (radioButton2.Text == right)
                    righting = true;
            }
            if (radioButton3.Checked)
            {
                if (radioButton3.Text == right)
                    righting = true;
            }

            NextButton.Enabled = true;
        }
        #endregion

        
    }
}
