using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Linq;
using Check;

namespace Tests
{
    public partial class LoadForm : Form
    {
        XmlReader xmlThemeRead;
        DirectoryInfo testsDirectory = new DirectoryInfo("Tests"); 

        public LoadForm()
        {
            InitializeComponent();
            if (!Checking.DataChecking())
                MessageBox.Show("Деякі файли, необхідні для стабільної роботи програми, не знайшли!!!\nРекомендуємо перевстановити програму!", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            foreach (TextBox textbox in this.Controls.OfType<TextBox>())
            {
                textbox.Dispose();
            }

            if (testsDirectory.Exists == false)
                CreateTestDir();

            comboBox1.Items.AddRange(testsDirectory.GetDirectories()); 
        }

        #region Обновление ListBox'a
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) 
        {
            ThemeLabel.Text = "Тема тесту: "; 
            DirectoryInfo testsDir = new DirectoryInfo("Tests\\" + comboBox1.Text); 
            listBox1.Items.Clear(); 

            foreach (FileInfo file in testsDir.GetFiles())
            {
                listBox1.Items.Add(Path.GetFileNameWithoutExtension(file.FullName));
            }

            LoadButton.Enabled = false;
        }
        #endregion

        #region Exit
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Ви впевнені, що хочете вийти?", "Вихід", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
            else
                this.Visible = true;
        }
        #endregion

        #region Создание папок
        public void CreateTestDir()
        {
            testsDirectory.Create();
            testsDirectory.CreateSubdirectory("Істория");
            testsDirectory.CreateSubdirectory("Алгебра");
            testsDirectory.CreateSubdirectory("Українська мова");
            testsDirectory.CreateSubdirectory("Українська література");
            testsDirectory.CreateSubdirectory("Геометрія");
        }
        #endregion

        #region Выбор теста в listBox'e
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                xmlThemeRead = new XmlTextReader("Tests\\" + comboBox1.Text + "\\" + listBox1.Text + ".xml");

               
                do xmlThemeRead.Read();
                while (xmlThemeRead.Name != "Theme");

             
                xmlThemeRead.Read();

               
                ThemeLabel.Text = "Тема тесту: " + xmlThemeRead.Value;

     
                xmlThemeRead.Read();

                LoadButton.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Такого файлу немає чи немає прав для його відкриття!\n\t\tВиберіть інший файл!", "Помилка!");
            }
        }
        #endregion

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Введіть ваше ім'я!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            else
            {
                string xmlPath = "Tests\\" + comboBox1.Text + "\\" + listBox1.Text + ".xml"; 

                MainForm MF = new MainForm(xmlPath, textBox1.Text, ThemeLabel.Text);
                MF.Show(); 
                this.Visible = false; 
            }
        }

        
    }
}
