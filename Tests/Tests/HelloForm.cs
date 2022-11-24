using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Check;

namespace TestsEditor
{
    public partial class HelloForm : Form
    {
        XmlTextWriter testWriter; 

        public HelloForm()
        {
            InitializeComponent();

            if (!Checking.DataChecking())
                MessageBox.Show("Деякі файли, необхідні для стабільної роботи програми, не знайшли!!!\nРекомендуємо перевстановити програму!", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            DirectoryInfo TestsDir = new DirectoryInfo("Tests"); 

            if (!TestsDir.Exists) 
                TestsDir.Create(); 

            comboBox1.Items.AddRange(TestsDir.GetDirectories()); 
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && ThemeBox.Text != "" && NameBox.Text != "")
            {
                try
                {
                    testWriter = new XmlTextWriter("Tests\\" + comboBox1.Text + "\\" + NameBox.Text + ".xml", Encoding.UTF8); 
                }
                catch (DirectoryNotFoundException) 
                {
                    Directory.CreateDirectory("Tests\\" + comboBox1.Text); 
                    testWriter = new XmlTextWriter("Tests\\" + comboBox1.Text + "\\" + NameBox.Text + ".xml", Encoding.UTF8); 
                }

                testWriter.Formatting = Formatting.Indented; 

               
                testWriter.WriteStartDocument(); 
                testWriter.WriteStartElement("test"); 

             
                testWriter.WriteStartElement("Theme"); 
                testWriter.WriteString(ThemeBox.Text); 
                testWriter.WriteEndElement(); 

               
                testWriter.WriteStartElement("qw"); 
                testWriter.WriteStartAttribute("numbers"); 
                testWriter.WriteString(NumQwBox.Value.ToString());
                testWriter.WriteEndAttribute();

                for (int i = 1; i <= NumQwBox.Value; i++)
                {
                    QwestForm QF = new QwestForm(i, testWriter);
                    QF.ShowDialog();
                }

                
                testWriter.WriteEndElement();
                testWriter.WriteEndElement(); 
                testWriter.WriteEndDocument();

                testWriter.Close();

                MessageBox.Show("Усі питання успішно створені!", "Вихід");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Заповніть всі поля!", "Помилка!");
            }
        }
    }
}
