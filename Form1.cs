using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Вставка библиотек
using Inventor;
using System.Diagnostics;
using System.Reflection;

namespace Pressure_regulator
{
    public partial class Form1 : Form
    {
        public string Версия_Inventor = "2022";
        /// <summary>
        /// ThisApplication - Объект для определения активного состояния Инвентора
        /// </summary>
        private Inventor.Application ThisApplication = null;
        /// <summary>
        /// Словарь для хранения ссылок на документы деталей
        /// </summary>
        private Dictionary<string, PartDocument> oPartDoc = new Dictionary<string,
        PartDocument>();
        /// <summary>
        /// Словарь для хранения ссылок на определения деталей
        /// </summary>
        private Dictionary<string, PartComponentDefinition> oCompDef = new Dictionary<string,
        PartComponentDefinition>();
        /// <summary>
        /// Словарь для хранения ссылок на инструменты создания деталей
        /// </summary>
        private Dictionary<string, TransientGeometry> oTransGeom = new Dictionary<string,
        TransientGeometry>();
        /// <summary>
        /// Словарь для хранения ссылок на транзакции редактирования
        /// </summary>
        private Dictionary<string, Transaction> oTrans = new Dictionary<string, Transaction>();
        /// <summary>
        /// Словарь для хранения имен сохраненных документов деталей
        /// </summary>
        private Dictionary<string, string> oFileName = new Dictionary<string, string>();
        private AssemblyDocument oAssemblyDocName;
        private AssemblyComponentDefinition oAssCompDef;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Версия_Inventor;
            try
            {
                //Проверка наличия активного состояния Инвентора.
                ThisApplication = (Inventor.Application)System.Runtime.InteropServices.
                Marshal.GetActiveObject("Inventor.Application");
                if (ThisApplication != null) label2.Text = "Инвентор открыт!";
                
            }
            catch
            {
                // Если Инвентор не открыт, то возвращаемся в основную программу.
                label2.Text = "Откройте Инвентор!";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Запуск Инвентора может занять несколько минут!");
            System.Diagnostics.Process.Start("C:/Program Files/Autodesk/Inventor " + Версия_Inventor +
            "/Bin/Inventor.exe");
            label2.Text = "Инвентор открыт!";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int число = 0;
            Версия_Inventor = textBox1.Text;

            число = Convert.ToInt16(textBox1.Text);
            if (число >= 2009)
                if (число >= 2015 && число <= 2022)
                {
                    MessageBox.Show("Принята версия Инвентора " + textBox1.Text);
                }
                else
                {
                    if (число < 2015) MessageBox.Show("Используется устаревшая версия Инвентора! " +
                    textBox1.Text);
                    if (число > 2022)
                    {
                        MessageBox.Show("Данная версия Инвентора " + textBox1.Text +
                        " не существует!\nПо умолчанию будет принята версия Инвентора 2022 !");
                        Версия_Inventor = "2022";
                        textBox1.Text = Версия_Inventor;
                    }
                }
            else if (число >= 2000)
            {
                MessageBox.Show("Используется устаревшая версия Инвентора " + textBox1.Text +
                " !\nПо умолчанию будет принята версия Инвентора 2022 !");
                Версия_Inventor = "2022";
                textBox1.Text = Версия_Inventor;
            }
        }
    }
}
