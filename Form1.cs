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
                if (число >= 2013 && число <= 2022)
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
        /// <summary>
        /// Функция создания имен новых элементов (документ, определение, инструменты,транзакции, имена файлов), необходимых для работы с деталями
        /// </summary>
        /// <param name="Name">Имя для ассоциативного обращения к элементам словарей</param>
        private void Имя_нового_документа(string Name)
        {
            // Новый документ детали
            oPartDoc[Name] = (PartDocument)ThisApplication.Documents.Add(
            DocumentTypeEnum.kPartDocumentObject, ThisApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject));
            // Новое определение
            oCompDef[Name] = oPartDoc[Name].ComponentDefinition;
            // Выбор инструментов
            oTransGeom[Name] = ThisApplication.TransientGeometry;
            // Создание транзакции
            oTrans[Name] = ThisApplication.TransactionManager.StartTransaction(
            ThisApplication.ActiveDocument, "Create Sample");
            // Имя файла
            oFileName[Name] = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("Запустите Инвентор!");
                return;
            }

            //Построение детали 1. Опора
            // Объявление локальных переменных
            // Вставка программного кода по пространственному моделированию детали
            //Имя_нового_документа("1. Опора");
            //oPartDoc["1. Опора"].DisplayName = "1. Опора";
            //PlanarSketch oSketch = oCompDef["1. Опора"].Sketches.Add(oCompDef["1. Опора"].WorkPlanes[3]);
            SketchPoint[] point = new SketchPoint[12];
            SketchLine[] lines = new SketchLine[9];
            SketchArc[] arcs = new SketchArc[2];
            SketchCircle[] Окружность = new SketchCircle[1];
            /*
            // Определение координат точек для твердотельного основания
            point[0] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(0, 0), false);
            point[1] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(1.15, 0), false);
            point[2] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(1.15, 0.1), false);
            point[3] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(1.15, 0.2), false);
            point[4] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(0.7, 0.2), false);
            point[5] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(0.7, 0.3), false);
            point[6] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(0.6, 0.3), false);
            point[7] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(0.6, 0.7), false);
            point[8] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(0.5, 0.8), false);
            point[9] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(0.3, 0.8), false);
            point[10] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(0.3, 0.45), false);
            point[11] = oSketch.SketchPoints.Add(oTransGeom["1. Опора"].CreatePoint2d(0, 0.4), false);
            // Построение замкнутого контура твердотельного основания
            lines[0] = oSketch.SketchLines.AddByTwoPoints(point[0], point[1]);
            lines[1] = oSketch.SketchLines.AddByTwoPoints(point[3], point[4]);
            lines[2] = oSketch.SketchLines.AddByTwoPoints(point[6], point[7]);
            lines[3] = oSketch.SketchLines.AddByTwoPoints(point[7], point[8]);
            lines[4] = oSketch.SketchLines.AddByTwoPoints(point[8], point[9]);
            lines[5] = oSketch.SketchLines.AddByTwoPoints(point[9], point[10]);
            lines[6] = oSketch.SketchLines.AddByTwoPoints(point[10], point[11]);
            lines[7] = oSketch.SketchLines.AddByTwoPoints(point[11], point[0]);
            arcs[0] = oSketch.SketchArcs.AddByCenterStartEndPoint(oTransGeom["1. Опора"].CreatePoint2d(
            point[2].Geometry.X, point[2].Geometry.Y), point[1], point[3]);
            arcs[1] = oSketch.SketchArcs.AddByCenterStartEndPoint(oTransGeom["1. Опора"].CreatePoint2d(
            point[5].Geometry.X, point[5].Geometry.Y), point[6], point[4]);
            //Принять эскиз 
            oTrans["1. Опора"].End();
            // Выбор функции твердотельного построения
            Profile oProfile = (Profile)oSketch.Profiles.AddForSolid();
            // Вращение эскиза для получения твердотельной модели
            RevolveFeature revolvefeature = oCompDef["1. Опора"].Features.
            RevolveFeatures.AddFull(oProfile, lines[7],
            PartFeatureOperationEnum.kJoinOperation);
            //MessageBox.Show("Создание детали завершено!", "Сообщение");
            */
            
            ////Построение детали 3. Винт
            //Имя_нового_документа("3. Винт");
            //oPartDoc["3. Винт"].DisplayName = "3. Винт";
            //PlanarSketch oSketch1 = oCompDef["3. Винт"].Sketches.Add(oCompDef["3. Винт"].WorkPlanes[3]);
            //point[0] = oSketch1.SketchPoints.Add(oTransGeom["3. Винт"].CreatePoint2d(0, 0), false);
            //point[1] = oSketch1.SketchPoints.Add(oTransGeom["3. Винт"].CreatePoint2d(0, 0.6), false);
            //point[2] = oSketch1.SketchPoints.Add(oTransGeom["3. Винт"].CreatePoint2d(0.6, 0.6), false);
            //point[3] = oSketch1.SketchPoints.Add(oTransGeom["3. Винт"].CreatePoint2d(0.8, 0.8), false);
            //point[4] = oSketch1.SketchPoints.Add(oTransGeom["3. Винт"].CreatePoint2d(0.8, 4.5), false);
            //point[5] = oSketch1.SketchPoints.Add(oTransGeom["3. Винт"].CreatePoint2d(0.8, 6.2), false);
            //point[6] = oSketch1.SketchPoints.Add(oTransGeom["3. Винт"].CreatePoint2d(0.7, 6.3), false);
            //point[7] = oSketch1.SketchPoints.Add(oTransGeom["3. Винт"].CreatePoint2d(0, 6.3), false);
            //lines[0] = oSketch1.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[1] = oSketch1.SketchLines.AddByTwoPoints(point[3], point[4]);
            //lines[2] = oSketch1.SketchLines.AddByTwoPoints(point[4], point[5]);
            //lines[3] = oSketch1.SketchLines.AddByTwoPoints(point[5], point[6]);
            //lines[4] = oSketch1.SketchLines.AddByTwoPoints(point[6], point[7]);
            //lines[4] = oSketch1.SketchLines.AddByTwoPoints(point[7], point[0]);
            //arcs[0] = oSketch1.SketchArcs.AddByCenterStartEndPoint(oTransGeom["3. Винт"].CreatePoint2d(
            //point[1].Geometry.X, point[1].Geometry.Y), point[0], point[2]);
            //oTrans["3. Винт"].End();
            //Profile oProfile1 = (Profile)oSketch1.Profiles.AddForSolid();
            //// Вращение эскиза для получения твердотельной модели
            //RevolveFeature revolvefeature1 = oCompDef["3. Винт"].Features.
            //RevolveFeatures.AddFull(oProfile1, lines[4],
            //PartFeatureOperationEnum.kJoinOperation);
            //// Создание плоскости построения "oWorkPlane"
            //WorkPlane oWorkPlane = oCompDef["3. Винт"].WorkPlanes.AddByPlaneAndOffset(
            //oCompDef["3. Винт"].WorkPlanes[3], 8 + " мм", false);
            //oWorkPlane.Visible = false;
            ////Выбор рабочей плоскости oWorkPlane и создание эскиза на плоскости "oSketch1"
            //PlanarSketch oSketch2 = oCompDef["3. Винт"].Sketches.Add(oWorkPlane, false);
            //Transaction oTrans2 = ThisApplication.TransactionManager.StartTransaction(ThisApplication.ActiveDocument, "Create Sample2");
            //point[0] = oSketch2.SketchPoints.Add(oTransGeom["3. Винт"].CreatePoint2d(0, 5.5), false);
            //Окружность[0] = oSketch2.SketchCircles.AddByCenterRadius(point[0], 0.4);
            //oTrans2.End();
            //Profile oProfile2 = (Profile)oSketch2.Profiles.AddForSolid();
            //ExtrudeFeature oExtrudeDef = oCompDef["3. Винт"].Features.ExtrudeFeatures.AddByDistanceExtent(
            ///*Эскиз*/oProfile2,/*Длина в см*/1.6,/*Направление вдоль оси*/
            //PartFeatureExtentDirectionEnum.kNegativeExtentDirection,
            ///*Операция*/PartFeatureOperationEnum.kCutOperation,/*Эскиз*/oProfile2);
            ////Резьба
            
            //EdgeCollection EdgeCollection = ThisApplication.TransientObjects.CreateEdgeCollection();
            //Face Face = revolvefeature1.SideFaces[3];
            //Edge StartEdge_1 = Face.Edges[6];
            //EdgeCollection.Add(StartEdge_1);
            //ThreadFeatures ThreadFeatures = oCompDef["3. Винт"].Features.ThreadFeatures;
            //StandardThreadInfo stInfo = ThreadFeatures.CreateStandardThreadInfo(false, true,
            //"ISO Metric profile", "M16x1.25", "6g");
            //ThreadInfo ThreadInfo = (ThreadInfo)stInfo;
            //ThreadFeatures.Add(Face, StartEdge_1, ThreadInfo, false, false, 37 + " мм", 0);
            
            /*
            //Построение детали 4. Штифт
            Имя_нового_документа("4. Штифт");
            oPartDoc["4. Штифт"].DisplayName = "4. Штифт";
            PlanarSketch oSketch3 = oCompDef["4. Штифт"].Sketches.Add(oCompDef["4. Штифт"].WorkPlanes[3]);
            _ = new SketchPoint[6];
            _ = new SketchLine[2];
            _ = new SketchArc[2];
            point[0] = oSketch3.SketchPoints.Add(oTransGeom["4. Штифт"].CreatePoint2d(0, 0), false);
            point[1] = oSketch3.SketchPoints.Add(oTransGeom["4. Штифт"].CreatePoint2d(0, 0.4), false);
            point[2] = oSketch3.SketchPoints.Add(oTransGeom["4. Штифт"].CreatePoint2d(0.4, 0.4), false);
            point[3] = oSketch3.SketchPoints.Add(oTransGeom["4. Штифт"].CreatePoint2d(0.4, 7.1), false);
            point[4] = oSketch3.SketchPoints.Add(oTransGeom["4. Штифт"].CreatePoint2d(0, 7.1), false);
            point[5] = oSketch3.SketchPoints.Add(oTransGeom["4. Штифт"].CreatePoint2d(0, 7.5), false);
            lines[0] = oSketch3.SketchLines.AddByTwoPoints(point[2], point[3]);
            lines[1] = oSketch3.SketchLines.AddByTwoPoints(point[5], point[0]);
            arcs[0] = oSketch3.SketchArcs.AddByCenterStartEndPoint(oTransGeom["4. Штифт"].CreatePoint2d(
            point[1].Geometry.X, point[1].Geometry.Y), point[0], point[2]);
            arcs[1] = oSketch3.SketchArcs.AddByCenterStartEndPoint(oTransGeom["4. Штифт"].CreatePoint2d(
            point[4].Geometry.X, point[4].Geometry.Y), point[3], point[5]);
            oTrans["4. Штифт"].End();
            Profile oProfile3 = (Profile)oSketch3.Profiles.AddForSolid();
            RevolveFeature revolvefeature2 = oCompDef["4. Штифт"].Features.
            RevolveFeatures.AddFull(oProfile3, lines[1],
            PartFeatureOperationEnum.kJoinOperation);
            */

            //Построение детали 5. Упор
            Имя_нового_документа("5. Упор");
            oPartDoc["5. Упор"].DisplayName = "5. Упор";
            PlanarSketch oSketch4 = oCompDef["5. Упор"].Sketches.Add(oCompDef["5. Упор"].WorkPlanes[3]);
            point[0] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(0.7, 0), false);
            point[1] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(2, 0), false);
            point[2] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(2, 0.4), false);
            point[3] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(1.25, 0.4), false);
            point[4] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(1.25, 1.2), false);
            point[5] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(1.05, 1.2), false);
            point[6] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(1.05, 0.7), false);
            point[7] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(0, 0.7), false);
            point[8] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(0, 0.4), false);
            lines[0] = oSketch4.SketchLines.AddByTwoPoints(point[0], point[1]);
            lines[1] = oSketch4.SketchLines.AddByTwoPoints(point[1], point[2]);
            lines[2] = oSketch4.SketchLines.AddByTwoPoints(point[2], point[3]);
            lines[3] = oSketch4.SketchLines.AddByTwoPoints(point[3], point[4]);
            lines[4] = oSketch4.SketchLines.AddByTwoPoints(point[4], point[5]);
            lines[5] = oSketch4.SketchLines.AddByTwoPoints(point[5], point[6]);
            lines[6] = oSketch4.SketchLines.AddByTwoPoints(point[6], point[7]);
            lines[7] = oSketch4.SketchLines.AddByTwoPoints(point[7], point[8]);
            lines[8] = oSketch4.SketchLines.AddByTwoPoints(point[8], point[0]);

            oTrans["5. Упор"].End();
            
            Profile oProfile4 = (Profile)oSketch4.Profiles.AddForSolid();
            RevolveFeature revolvefeature3 = oCompDef["5. Упор"].Features.
            RevolveFeatures.AddFull(oProfile4, lines[7],
            PartFeatureOperationEnum.kJoinOperation);
            //фаски
            EdgeCollection EdgeCollection1 = ThisApplication.TransientObjects.CreateEdgeCollection();
            Face Face1 = revolvefeature3.SideFaces[4];
            Edge StartEdge_2 = Face1.Edges[1];
            EdgeCollection1.Add(StartEdge_2);
            oCompDef["5. Упор"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection1,  1 + "мм",true);

            //вторая фаска(меньше строк)
            EdgeCollection1.Add(revolvefeature3.Faces[7].Edges[1]);
            oCompDef["5. Упор"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection1, 1 + "мм", true);
            FilletFeature oFillet = default(FilletFeature);

            //скругления
            EdgeCollection1.Add(revolvefeature3.Faces[6].Edges[1]);
            FilletDefinition oFilletDef = oCompDef["5. Упор"].Features.FilletFeatures.CreateFilletDefinition();
            oFilletDef.AddConstantRadiusEdgeSet(EdgeCollection1, 0.5 + "мм");
            oFillet = oCompDef["5. Упор"].Features.FilletFeatures.Add(oFilletDef, false);

            EdgeCollection1.Add(revolvefeature3.Faces[8].Edges[2]);
            FilletDefinition oFilletDef1 = oCompDef["5. Упор"].Features.FilletFeatures.CreateFilletDefinition();
            oFilletDef1.AddConstantRadiusEdgeSet(EdgeCollection1, 1 + "мм");
            oFillet = oCompDef["5. Упор"].Features.FilletFeatures.Add(oFilletDef1, false);

            MessageBox.Show("Создание деталей завершено!", "Сообщение");
        }
    }
}
