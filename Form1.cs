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
            SketchPoint[] point = new SketchPoint[20];
            SketchLine[] lines = new SketchLine[20];
            SketchArc[] arcs = new SketchArc[5];
            SketchCircle[] Окружность = new SketchCircle[3];
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
            //Имя_нового_документа("5. Упор");
            //oPartDoc["5. Упор"].DisplayName = "5. Упор";
            //PlanarSketch oSketch4 = oCompDef["5. Упор"].Sketches.Add(oCompDef["5. Упор"].WorkPlanes[3]);
            //point[0] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(0.7, 0), false);
            //point[1] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(2, 0), false);
            //point[2] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(2, 0.4), false);
            //point[3] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(1.25, 0.4), false);
            //point[4] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(1.25, 1.2), false);
            //point[5] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(1.05, 1.2), false);
            //point[6] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(1.05, 0.7), false);
            //point[7] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(0, 0.7), false);
            //point[8] = oSketch4.SketchPoints.Add(oTransGeom["5. Упор"].CreatePoint2d(0, 0.4), false);
            //lines[0] = oSketch4.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch4.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch4.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch4.SketchLines.AddByTwoPoints(point[3], point[4]);
            //lines[4] = oSketch4.SketchLines.AddByTwoPoints(point[4], point[5]);
            //lines[5] = oSketch4.SketchLines.AddByTwoPoints(point[5], point[6]);
            //lines[6] = oSketch4.SketchLines.AddByTwoPoints(point[6], point[7]);
            //lines[7] = oSketch4.SketchLines.AddByTwoPoints(point[7], point[8]);
            //lines[8] = oSketch4.SketchLines.AddByTwoPoints(point[8], point[0]);

            //oTrans["5. Упор"].End();

            //Profile oProfile4 = (Profile)oSketch4.Profiles.AddForSolid();
            //RevolveFeature revolvefeature3 = oCompDef["5. Упор"].Features.
            //RevolveFeatures.AddFull(oProfile4, lines[7],
            //PartFeatureOperationEnum.kJoinOperation);
            ////фаски
            //EdgeCollection EdgeCollection1 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //Face Face1 = revolvefeature3.SideFaces[3];
            //Edge StartEdge_2 = Face1.Edges[1];
            //EdgeCollection1.Add(StartEdge_2);
            //oCompDef["5. Упор"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection1,  1 + "мм",true);

            ////вторая фаска(меньше строк)
            //EdgeCollection1.Add(revolvefeature3.Faces[3].Edges[2]);
            //oCompDef["5. Упор"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection1, 1 + "мм", true);

            //FilletFeature oFillet = default(FilletFeature);

            ////скругления
            //EdgeCollection1.Add(revolvefeature3.Faces[6].Edges[1]);
            //FilletDefinition oFilletDef = oCompDef["5. Упор"].Features.FilletFeatures.CreateFilletDefinition();
            //oFilletDef.AddConstantRadiusEdgeSet(EdgeCollection1, 0.5 + "мм");
            //oFillet = oCompDef["5. Упор"].Features.FilletFeatures.Add(oFilletDef, false);

            //EdgeCollection1.Add(revolvefeature3.Faces[5].Edges[1]);
            //FilletDefinition oFilletDef1 = oCompDef["5. Упор"].Features.FilletFeatures.CreateFilletDefinition();
            //oFilletDef1.AddConstantRadiusEdgeSet(EdgeCollection1, 1 + "мм");
            //oFillet = oCompDef["5. Упор"].Features.FilletFeatures.Add(oFilletDef1, false);

            //Построение детали 10. Тарелка
            //Имя_нового_документа("10. Тарелка");
            //oPartDoc["10. Тарелка"].DisplayName = "10. Тарелка";
            //PlanarSketch oSketch5 = oCompDef["10. Тарелка"].Sketches.Add(oCompDef["10. Тарелка"].WorkPlanes[3]);
            //point[0] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(0, 0), false);
            //point[1] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(2.8, 0), false);
            //point[2] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(2.8, 0.8), false);
            //point[3] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(2.5, 0.8), false);
            //point[4] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(2.5, 0.3), false);
            //point[5] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(1.25, 0.3), false);
            //point[6] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(1.25, 0.8), false);
            //point[7] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(1.05, 0.8), false);
            //point[8] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(1.05, 0.3), false);
            //point[9] = oSketch5.SketchPoints.Add(oTransGeom["10. Тарелка"].CreatePoint2d(0, 0.3), false);
            //lines[0] = oSketch5.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch5.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch5.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch5.SketchLines.AddByTwoPoints(point[3], point[4]);
            //lines[4] = oSketch5.SketchLines.AddByTwoPoints(point[4], point[5]);
            //lines[5] = oSketch5.SketchLines.AddByTwoPoints(point[5], point[6]);
            //lines[6] = oSketch5.SketchLines.AddByTwoPoints(point[6], point[7]);
            //lines[7] = oSketch5.SketchLines.AddByTwoPoints(point[7], point[8]);
            //lines[8] = oSketch5.SketchLines.AddByTwoPoints(point[8], point[9]);
            //lines[9] = oSketch5.SketchLines.AddByTwoPoints(point[9], point[0]);

            //oTrans["10. Тарелка"].End();

            //Profile oProfile5 = (Profile)oSketch5.Profiles.AddForSolid();
            //RevolveFeature revolvefeature4 = oCompDef["10. Тарелка"].Features.
            //RevolveFeatures.AddFull(oProfile5, lines[9],
            //PartFeatureOperationEnum.kJoinOperation);

            ////фаски
            //EdgeCollection EdgeCollection2 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //EdgeCollection2.Add(revolvefeature4.Faces[1].Edges[1]);
            //EdgeCollection2.Add(revolvefeature4.Faces[2].Edges[1]);
            //EdgeCollection2.Add(revolvefeature4.Faces[5].Edges[1]);
            //EdgeCollection2.Add(revolvefeature4.Faces[7].Edges[2]);
            //oCompDef["10. Тарелка"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection2, 0.5 + "мм", true);

            ////Построение детали 11. Диафрагма
            //Имя_нового_документа("11. Диафрагма");
            //oPartDoc["11. Диафрагма"].DisplayName = "11. Диафрагма";
            //PlanarSketch oSketch6 = oCompDef["11. Диафрагма"].Sketches.Add(oCompDef["11. Диафрагма"].WorkPlanes[3]);
            //point[0] = oSketch6.SketchPoints.Add(oTransGeom["11. Диафрагма"].CreatePoint2d(0, 0), false);
            //point[1] = oSketch6.SketchPoints.Add(oTransGeom["11. Диафрагма"].CreatePoint2d(4.5, 0), false);
            //point[2] = oSketch6.SketchPoints.Add(oTransGeom["11. Диафрагма"].CreatePoint2d(4.5, 0.11), false);
            //point[3] = oSketch6.SketchPoints.Add(oTransGeom["11. Диафрагма"].CreatePoint2d(0, 0.11), false);
            //lines[0] = oSketch6.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch6.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch6.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch6.SketchLines.AddByTwoPoints(point[3], point[0]);
            //oTrans["11. Диафрагма"].End();
            //Profile oProfile6 = (Profile)oSketch6.Profiles.AddForSolid();
            //RevolveFeature revolvefeature5 = oCompDef["11. Диафрагма"].Features.
            //RevolveFeatures.AddFull(oProfile6, lines[3],
            //PartFeatureOperationEnum.kJoinOperation);

            ////Построение детали 12. Кольцо 80х70
            //Имя_нового_документа("12. Кольцо 80х70");
            //oPartDoc["12. Кольцо 80х70"].DisplayName = "12. Кольцо 80х70";
            //PlanarSketch oSketch7 = oCompDef["12. Кольцо 80х70"].Sketches.Add(oCompDef["12. Кольцо 80х70"].WorkPlanes[3]);
            //point[0] = oSketch7.SketchPoints.Add(oTransGeom["12. Кольцо 80х70"].CreatePoint2d(3.65, 0), false);
            //point[1] = oSketch7.SketchPoints.Add(oTransGeom["12. Кольцо 80х70"].CreatePoint2d(0, 0), false);
            //point[2] = oSketch7.SketchPoints.Add(oTransGeom["12. Кольцо 80х70"].CreatePoint2d(0, 2), false);
            //lines[0] = oSketch7.SketchLines.AddByTwoPoints(point[1], point[2]);
            //Окружность[0] = oSketch7.SketchCircles.AddByCenterRadius(point[0], 0.29);
            //oTrans["12. Кольцо 80х70"].End();
            //Profile oProfile7 = (Profile)oSketch7.Profiles.AddForSolid();
            //RevolveFeature revolvefeature6 = oCompDef["12. Кольцо 80х70"].Features.
            //RevolveFeatures.AddFull(oProfile7, lines[0],
            //PartFeatureOperationEnum.kJoinOperation);

            ////Построение детали 14. Втулка
            //Имя_нового_документа("14. Втулка");
            //oPartDoc["14. Втулка"].DisplayName = "14. Втулка";
            //PlanarSketch oSketch8 = oCompDef["14. Втулка"].Sketches.Add(oCompDef["14. Втулка"].WorkPlanes[3]);
            //point[0] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0.25, 0), false);
            //point[1] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0.7, 0), false);
            //point[2] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0.7, 0.3), false);
            //point[3] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0.45, 0.3), false);
            //point[4] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0.45, 0.4), false);
            //point[5] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0.5, 0.4), false);
            //point[6] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0.5, 1), false);
            //point[7] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0.25, 1), false);
            //point[8] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0, 0), false);
            //point[9] = oSketch8.SketchPoints.Add(oTransGeom["14. Втулка"].CreatePoint2d(0, 2), false);
            //lines[0] = oSketch8.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch8.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch8.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch8.SketchLines.AddByTwoPoints(point[3], point[4]);
            //lines[4] = oSketch8.SketchLines.AddByTwoPoints(point[4], point[5]);
            //lines[5] = oSketch8.SketchLines.AddByTwoPoints(point[5], point[6]);
            //lines[6] = oSketch8.SketchLines.AddByTwoPoints(point[6], point[7]);
            //lines[7] = oSketch8.SketchLines.AddByTwoPoints(point[7], point[0]);
            //lines[8] = oSketch8.SketchLines.AddByTwoPoints(point[8], point[9]);
            //oTrans["14. Втулка"].End();

            //Profile oProfile8 = (Profile)oSketch8.Profiles.AddForSolid();
            //RevolveFeature revolvefeature7 = oCompDef["14. Втулка"].Features.
            //RevolveFeatures.AddFull(oProfile8, lines[8],
            //PartFeatureOperationEnum.kJoinOperation);

            ////фаски
            //EdgeCollection EdgeCollection3 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //EdgeCollection3.Add(revolvefeature7.Faces[1].Edges[2]);
            //EdgeCollection3.Add(revolvefeature7.Faces[5].Edges[1]);
            //oCompDef["14. Втулка"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection3, 0.2 + "мм", true);
            //EdgeCollection EdgeCollection4 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //EdgeCollection4.Add(revolvefeature7.Faces[8].Edges[2]);
            //oCompDef["14. Втулка"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection4, 0.5 + "мм", true);

            ////Построение детали 15. Шток
            //Имя_нового_документа("15. Шток");
            //oPartDoc["15. Шток"].DisplayName = "15. Шток";
            //PlanarSketch oSketch9 = oCompDef["15. Шток"].Sketches.Add(oCompDef["15. Шток"].WorkPlanes[3]);
            //point[0] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0, 0), false);
            //point[1] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0.25, 0), false);
            //point[2] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0.25, 1.22), false);
            //point[3] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0.35, 1.22), false);
            //point[4] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0.35, 1.5), false);
            //point[5] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0.7, 1.5), false);
            //point[6] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0.7, 1.55), false);
            //point[7] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0.25, 2), false);
            //point[8] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0.25, 6.5), false);
            //point[9] = oSketch9.SketchPoints.Add(oTransGeom["15. Шток"].CreatePoint2d(0, 6.5), false);
            //lines[0] = oSketch9.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch9.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch9.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch9.SketchLines.AddByTwoPoints(point[3], point[4]);
            //lines[4] = oSketch9.SketchLines.AddByTwoPoints(point[4], point[5]);
            //lines[5] = oSketch9.SketchLines.AddByTwoPoints(point[5], point[6]);
            //lines[6] = oSketch9.SketchLines.AddByTwoPoints(point[6], point[7]);
            //lines[7] = oSketch9.SketchLines.AddByTwoPoints(point[7], point[8]);
            //lines[8] = oSketch9.SketchLines.AddByTwoPoints(point[8], point[9]);
            //lines[9] = oSketch9.SketchLines.AddByTwoPoints(point[9], point[0]);
            //oTrans["15. Шток"].End();
            //Profile oProfile9 = (Profile)oSketch9.Profiles.AddForSolid();
            //RevolveFeature revolvefeature8 = oCompDef["15. Шток"].Features.
            //RevolveFeatures.AddFull(oProfile9, lines[9],
            //PartFeatureOperationEnum.kJoinOperation);

            ////скругления
            //EdgeCollection EdgeCollection5 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //FilletFeature oFillet1 = default(FilletFeature);
            //EdgeCollection5.Add(revolvefeature8.Faces[1].Edges[1]);
            //FilletDefinition oFilletDef = oCompDef["15. Шток"].Features.FilletFeatures.CreateFilletDefinition();
            //oFilletDef.AddConstantRadiusEdgeSet(EdgeCollection5, 2.5 + "мм");
            //oFillet1 = oCompDef["15. Шток"].Features.FilletFeatures.Add(oFilletDef, false);

            ////Построение детали 16. Прокладка
            //Имя_нового_документа("16. Прокладка");
            //oPartDoc["16. Прокладка"].DisplayName = "16. Прокладка";
            //PlanarSketch oSketch10 = oCompDef["16. Прокладка"].Sketches.Add(oCompDef["16. Прокладка"].WorkPlanes[3]);
            //point[0] = oSketch10.SketchPoints.Add(oTransGeom["16. Прокладка"].CreatePoint2d(0.375, 0), false);
            //point[1] = oSketch10.SketchPoints.Add(oTransGeom["16. Прокладка"].CreatePoint2d(1.3, 0), false);
            //point[2] = oSketch10.SketchPoints.Add(oTransGeom["16. Прокладка"].CreatePoint2d(1.3, 0.2), false);
            //point[3] = oSketch10.SketchPoints.Add(oTransGeom["16. Прокладка"].CreatePoint2d(0.375, 0.2), false);
            //point[4] = oSketch10.SketchPoints.Add(oTransGeom["16. Прокладка"].CreatePoint2d(0, 0), false);
            //point[5] = oSketch10.SketchPoints.Add(oTransGeom["16. Прокладка"].CreatePoint2d(0, 2), false);
            //lines[0] = oSketch10.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch10.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch10.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch10.SketchLines.AddByTwoPoints(point[3], point[0]);
            //lines[4] = oSketch10.SketchLines.AddByTwoPoints(point[4], point[5]);
            //oTrans["16. Прокладка"].End();
            //Profile oProfile10 = (Profile)oSketch10.Profiles.AddForSolid();
            //RevolveFeature revolvefeature9 = oCompDef["16. Прокладка"].Features.
            //RevolveFeatures.AddFull(oProfile10, lines[4],
            //PartFeatureOperationEnum.kJoinOperation);

            ////Построение детали 17. Кольцо 55х48
            //Имя_нового_документа("17. Кольцо 55х48");
            //oPartDoc["17. Кольцо 55х48"].DisplayName = "17. Кольцо 55х48";
            //PlanarSketch oSketch11 = oCompDef["17. Кольцо 55х48"].Sketches.Add(oCompDef["17. Кольцо 55х48"].WorkPlanes[3]);
            //point[0] = oSketch11.SketchPoints.Add(oTransGeom["17. Кольцо 55х48"].CreatePoint2d(2.5, 0), false);
            //point[1] = oSketch11.SketchPoints.Add(oTransGeom["17. Кольцо 55х48"].CreatePoint2d(0, 0), false);
            //point[2] = oSketch11.SketchPoints.Add(oTransGeom["17. Кольцо 55х48"].CreatePoint2d(0, 2), false);
            //lines[0] = oSketch11.SketchLines.AddByTwoPoints(point[1], point[2]);
            //Окружность[0] = oSketch11.SketchCircles.AddByCenterRadius(point[0], 0.205);
            //oTrans["17. Кольцо 55х48"].End();
            //Profile oProfile11 = (Profile)oSketch11.Profiles.AddForSolid();
            //RevolveFeature revolvefeature10 = oCompDef["17. Кольцо 55х48"].Features.
            //RevolveFeatures.AddFull(oProfile11, lines[0],
            //PartFeatureOperationEnum.kJoinOperation);

            ////Построение детали 19. Клапан
            //Имя_нового_документа("19. Клапан");
            //oPartDoc["19. Клапан"].DisplayName = "19. Клапан";
            //PlanarSketch oSketch12 = oCompDef["19. Клапан"].Sketches.Add(oCompDef["19. Клапан"].WorkPlanes[3]);
            //point[0] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(1.3, 0), false);
            //point[1] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(1.5, 0), false);
            //point[2] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(1.5, 0.5), false);
            //point[3] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(0.6, 0.5), false);
            //point[4] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(0.6, 1.4), false);
            //point[5] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(0.25, 1.4), false);
            //point[6] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(0.25, 0.3), false);
            //point[7] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(1.3, 0.3), false);
            //point[8] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(0, 0), false);
            //point[9] = oSketch12.SketchPoints.Add(oTransGeom["19. Клапан"].CreatePoint2d(0, 2), false);
            //lines[0] = oSketch12.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch12.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch12.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch12.SketchLines.AddByTwoPoints(point[3], point[4]);
            //lines[4] = oSketch12.SketchLines.AddByTwoPoints(point[4], point[5]);
            //lines[5] = oSketch12.SketchLines.AddByTwoPoints(point[5], point[6]);
            //lines[6] = oSketch12.SketchLines.AddByTwoPoints(point[6], point[7]);
            //lines[7] = oSketch12.SketchLines.AddByTwoPoints(point[7], point[0]);
            //lines[8] = oSketch12.SketchLines.AddByTwoPoints(point[8], point[9]);
            //oTrans["19. Клапан"].End();
            //Profile oProfile12 = (Profile)oSketch12.Profiles.AddForSolid();
            //RevolveFeature revolvefeature11 = oCompDef["19. Клапан"].Features.
            //RevolveFeatures.AddFull(oProfile12, lines[8],
            //PartFeatureOperationEnum.kJoinOperation);
            ////фаски
            //EdgeCollection EdgeCollection6 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //EdgeCollection6.Add(revolvefeature11.Faces[1].Edges[1]);
            //EdgeCollection6.Add(revolvefeature11.Faces[1].Edges[2]);
            //EdgeCollection6.Add(revolvefeature11.Faces[7].Edges[2]);
            //oCompDef["19. Клапан"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection6, 0.5 + "мм", true);
            //EdgeCollection EdgeCollection7 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //EdgeCollection7.Add(revolvefeature11.Faces[6].Edges[1]);
            //oCompDef["19. Клапан"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection7, 1 + "мм", true);
            //FilletFeature oFillet2 = default(FilletFeature);
            //EdgeCollection EdgeCollection8 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //EdgeCollection8.Add(revolvefeature11.Faces[1].Edges[1]);
            //FilletDefinition oFilletDef = oCompDef["19. Клапан"].Features.FilletFeatures.CreateFilletDefinition();
            //oFilletDef.AddConstantRadiusEdgeSet(EdgeCollection8, 0.5 + "мм");
            //oFillet2 = oCompDef["19. Клапан"].Features.FilletFeatures.Add(oFilletDef, false);

            ////Построение детали 20. Пробка
            //Имя_нового_документа("20. Пробка");
            //oPartDoc["20. Пробка"].DisplayName = "20. Пробка";
            //PlanarSketch oSketch13 = oCompDef["20. Пробка"].Sketches.Add(oCompDef["20. Пробка"].WorkPlanes[3]);
            //point[0] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(0, 0), false);
            //point[1] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(3.167, 0), false);
            //point[2] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(3.65, 0.129), false);
            //point[3] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(3.65, 0.471), false);
            //point[4] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(3.167, 0.6), false);
            //point[5] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(2.344, 0.6), false);
            //point[6] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(2.344, 0.65), false);
            //point[7] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(2.294, 0.65), false);
            //point[8] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(2.294, 0.953), false);
            ////point[9] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(2.394, 0.853), false);
            ////point[10] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(2.323, 0.923), false);
            //point[11] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(2.4, 1), false);
            //point[12] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(2.4, 1.85), false);
            //point[13] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(2.25, 2), false);
            //point[14] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(1.8, 2), false);
            //point[15] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(1.75, 1.95), false);
            //point[16] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(1.75, 1.2), false);
            //point[17] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(1, 0.767), false);
            //point[18] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(1, 0.2), false);
            //point[19] = oSketch13.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(0, 0.2), false);
            //lines[0] = oSketch13.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch13.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch13.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch13.SketchLines.AddByTwoPoints(point[3], point[4]);
            //lines[4] = oSketch13.SketchLines.AddByTwoPoints(point[4], point[5]);
            //lines[5] = oSketch13.SketchLines.AddByTwoPoints(point[7], point[8]);
            //lines[6] = oSketch13.SketchLines.AddByTwoPoints(point[8], point[11]);
            //lines[7] = oSketch13.SketchLines.AddByTwoPoints(point[11], point[12]);
            //lines[8] = oSketch13.SketchLines.AddByTwoPoints(point[12], point[13]);
            //lines[9] = oSketch13.SketchLines.AddByTwoPoints(point[13], point[14]);
            //lines[10] = oSketch13.SketchLines.AddByTwoPoints(point[14], point[15]);
            //lines[11] = oSketch13.SketchLines.AddByTwoPoints(point[15], point[16]);
            //lines[12] = oSketch13.SketchLines.AddByTwoPoints(point[16], point[17]);
            //lines[13] = oSketch13.SketchLines.AddByTwoPoints(point[17], point[18]);
            //lines[14] = oSketch13.SketchLines.AddByTwoPoints(point[18], point[19]);
            //lines[15] = oSketch13.SketchLines.AddByTwoPoints(point[19], point[0]);
            //arcs[0] = oSketch13.SketchArcs.AddByCenterStartEndPoint(oTransGeom["20. Пробка"].CreatePoint2d(
            //point[6].Geometry.X, point[6].Geometry.Y), point[7], point[5]);
            //oTrans["20. Пробка"].End();
            //Profile oProfile13 = (Profile)oSketch13.Profiles.AddForSolid();
            //RevolveFeature revolvefeature12 = oCompDef["20. Пробка"].Features.
            //RevolveFeatures.AddFull(oProfile13, lines[15],
            //PartFeatureOperationEnum.kJoinOperation);
            //WorkPlane oWorkPlane1 = oCompDef["20. Пробка"].WorkPlanes.AddByPlaneAndOffset(
            //oCompDef["20. Пробка"].WorkPlanes[2], 6 + " мм", false);
            //oWorkPlane1.Visible = false;
            ////Выбор рабочей плоскости oWorkPlane и создание эскиза на плоскости "oSketch14"
            //PlanarSketch oSketch14 = oCompDef["20. Пробка"].Sketches.Add(oWorkPlane1, false);

            //point[0] = oSketch14.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(3.167, 1.829), false);
            //point[1] = oSketch14.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(0, 3.657), false);
            //point[2] = oSketch14.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(-3.167, 1.829), false);
            //point[3] = oSketch14.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(-3.167, -1.829), false);
            //point[4] = oSketch14.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(0, -3.657), false);
            //point[5] = oSketch14.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(3.167, -1.829), false);
            //point[6] = oSketch14.SketchPoints.Add(oTransGeom["20. Пробка"].CreatePoint2d(0, 0), false);
            //lines[0] = oSketch14.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch14.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch14.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch14.SketchLines.AddByTwoPoints(point[3], point[4]);
            //lines[4] = oSketch14.SketchLines.AddByTwoPoints(point[4], point[5]);
            //lines[6] = oSketch14.SketchLines.AddByTwoPoints(point[5], point[0]);
            //Окружность[0] = oSketch14.SketchCircles.AddByCenterRadius(point[6], 7.3);
            //oTrans2.End();
            //Profile oProfile14 = (Profile)oSketch14.Profiles.AddForSolid();
            //ExtrudeFeature oExtrudeDef1 = oCompDef["20. Пробка"].Features.ExtrudeFeatures.AddByDistanceExtent(
            ///*Эскиз*/oProfile14,/*Длина в см*/0.6,/*Направление вдоль оси*/
            //PartFeatureExtentDirectionEnum.kNegativeExtentDirection,
            ///*Операция*/PartFeatureOperationEnum.kCutOperation,/*Эскиз*/oProfile14);
            //EdgeCollection EdgeCollection9 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //Face Face1 = revolvefeature12.SideFaces[6];
            //Edge StartEdge_2 = Face1.Edges[2];
            //EdgeCollection9.Add(StartEdge_2);
            //ThreadFeatures ThreadFeatures = oCompDef["20. Пробка"].Features.ThreadFeatures;
            //StandardThreadInfo stInfo2 = ThreadFeatures.CreateStandardThreadInfo(false, true,
            //"ISO Metric profile", "M48x1.5", "6g");
            //ThreadInfo ThreadInfo = (ThreadInfo)stInfo2;
            //ThreadFeatures.Add(Face1, StartEdge_2, ThreadInfo, false, false, 11 + "мм", 0);

            ////Построение детали 21. Шайба
            //Имя_нового_документа("21. Шайба");
            //oPartDoc["21. Шайба"].DisplayName = "21. Шайба";
            //PlanarSketch oSketch15 = oCompDef["21. Шайба"].Sketches.Add(oCompDef["21. Шайба"].WorkPlanes[3]);
            //point[0] = oSketch15.SketchPoints.Add(oTransGeom["21. Шайба"].CreatePoint2d(0, 0), false);
            //point[1] = oSketch15.SketchPoints.Add(oTransGeom["21. Шайба"].CreatePoint2d(0, 2), false);
            //point[2] = oSketch15.SketchPoints.Add(oTransGeom["21. Шайба"].CreatePoint2d(0.35, 0), false);
            //point[3] = oSketch15.SketchPoints.Add(oTransGeom["21. Шайба"].CreatePoint2d(0.9, 0), false);
            //point[4] = oSketch15.SketchPoints.Add(oTransGeom["21. Шайба"].CreatePoint2d(0.9, 0.03), false);
            //point[5] = oSketch15.SketchPoints.Add(oTransGeom["21. Шайба"].CreatePoint2d(0.35, 0.03), false);
            //lines[0] = oSketch15.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch15.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[2] = oSketch15.SketchLines.AddByTwoPoints(point[3], point[4]);
            //lines[3] = oSketch15.SketchLines.AddByTwoPoints(point[4], point[5]);
            //lines[4] = oSketch15.SketchLines.AddByTwoPoints(point[5], point[2]);
            //oTrans["21. Шайба"].End();
            //Profile oProfile15 = (Profile)oSketch15.Profiles.AddForSolid();
            //RevolveFeature revolvefeature13 = oCompDef["21. Шайба"].Features.
            //RevolveFeatures.AddFull(oProfile15, lines[0],
            //PartFeatureOperationEnum.kJoinOperation);

            ////Построение детали 22. Угольник
            //Имя_нового_документа("22. Угольник");
            //oPartDoc["22. Угольник"].DisplayName = "22. Угольник";
            //PlanarSketch oSketch16 = oCompDef["22. Угольник"].Sketches.Add(oCompDef["22. Угольник"].WorkPlanes[3]);
            //point[0] = oSketch16.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(1.1, 0.9), false);
            //point[1] = oSketch16.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(-1.1, 0.9), false);
            //point[2] = oSketch16.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(-1.1, -0.9), false);
            //point[3] = oSketch16.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(1.1, -0.9), false);
            //lines[0] = oSketch16.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch16.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch16.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch16.SketchLines.AddByTwoPoints(point[3], point[0]);
            //oTrans["22. Угольник"].End();
            //Profile oProfile16 = (Profile)oSketch16.Profiles.AddForSolid();
            //ExtrudeFeature oExtrudeDef2 = oCompDef["22. Угольник"].Features.ExtrudeFeatures.AddByDistanceExtent(
            ///*Эскиз*/oProfile16,/*Длина в см*/0.9,/*Направление вдоль оси*/
            //PartFeatureExtentDirectionEnum.kPositiveExtentDirection,
            ///*Операция*/PartFeatureOperationEnum.kJoinOperation,/*Эскиз*/oProfile16);
            //ExtrudeFeature oExtrudeDef3 = oCompDef["22. Угольник"].Features.ExtrudeFeatures.AddByDistanceExtent(
            ///*Эскиз*/oProfile16,/*Длина в см*/0.9,/*Направление вдоль оси*/
            //PartFeatureExtentDirectionEnum.kNegativeExtentDirection,
            ///*Операция*/PartFeatureOperationEnum.kJoinOperation,/*Эскиз*/oProfile16);

            //PlanarSketch oSketch17 = oCompDef["22. Угольник"].Sketches.Add(oCompDef["22. Угольник"].WorkPlanes[3]); 
            //point[0] = oSketch17.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(-1.1, 0.675), false);
            //point[1] = oSketch17.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(-.919, 0.6 ), false);
            //point[2] = oSketch17.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(0.7, 0.6), false);
            //point[3] = oSketch17.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(1.06, 0), false);
            //point[4] = oSketch17.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(-1.1, 0), false);
            //lines[0] = oSketch17.SketchLines.AddByTwoPoints(point[4], point[0]);
            //lines[1] = oSketch17.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[2] = oSketch17.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[3] = oSketch17.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[4] = oSketch17.SketchLines.AddByTwoPoints(point[3], point[4]);
            //oTrans2.End();
            //Profile oProfile17 = (Profile)oSketch17.Profiles.AddForSolid();
            //RevolveFeature revolvefeature14 = oCompDef["22. Угольник"].Features.
            //RevolveFeatures.AddFull(oProfile17, lines[4],
            //PartFeatureOperationEnum.kCutOperation);

            //EdgeCollection EdgeCollection10 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //Face Face2 = revolvefeature14.SideFaces[2];
            //Edge StartEdge_3 = Face2.Edges[1];
            //EdgeCollection10.Add(StartEdge_3);
            //ThreadFeatures ThreadFeatures1 = oCompDef["22. Угольник"].Features.ThreadFeatures;
            //StandardThreadInfo stInfo2 = ThreadFeatures1.CreateStandardThreadInfo(false, true,
            //"ISO Metric profile", "M12x1.5", "6g");
            //ThreadInfo ThreadInfo1 = (ThreadInfo)stInfo2;
            //ThreadFeatures1.Add(Face2, StartEdge_3, ThreadInfo1, false, false, 10 + "мм", 0);

            ////фаски
            //EdgeCollection EdgeCollection11 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //EdgeCollection11.Add(oExtrudeDef2.Faces[1].Edges[1]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[1].Edges[2]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[1].Edges[3]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[1].Edges[4]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[2].Edges[1]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[2].Edges[2]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[2].Edges[3]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[2].Edges[4]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[3].Edges[1]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[3].Edges[2]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[3].Edges[3]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[3].Edges[4]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[4].Edges[1]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[4].Edges[2]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[4].Edges[3]);
            //EdgeCollection11.Add(oExtrudeDef2.Faces[4].Edges[4]);
            //oCompDef["22. Угольник"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection11, 1 + "мм", true);

            //Transaction oTrans3 = ThisApplication.TransactionManager.StartTransaction(ThisApplication.ActiveDocument, "Create Sample3");
            //WorkPlane oWorkPlane2 = oCompDef["22. Угольник"].WorkPlanes.AddByPlaneAndOffset(
            //oCompDef["22. Угольник"].WorkPlanes[3], 9 + " мм", false);
            //oWorkPlane2.Visible = false;
            ////Выбор рабочей плоскости oWorkPlane и создание эскиза на плоскости "oSketch14"
            //PlanarSketch oSketch18 = oCompDef["22. Угольник"].Sketches.Add(oWorkPlane2, false);

            //point[0] = oSketch18.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(0.5, 0), false);
            //Окружность[0] = oSketch18.SketchCircles.AddByCenterRadius(point[0], 0.525);
            //Окружность[1] = oSketch18.SketchCircles.AddByCenterRadius(point[0], 0.1);
            //oTrans3.End();

            //Profile oProfile18 = (Profile)oSketch18.Profiles.AddForSolid();
            //ExtrudeFeature oExtrudeDef4 = oCompDef["22. Угольник"].Features.ExtrudeFeatures.AddByDistanceExtent(
            ///*Эскиз*/oProfile18,/*Длина в см*/4.4,/*Направление вдоль оси*/
            //PartFeatureExtentDirectionEnum.kPositiveExtentDirection,
            ///*Операция*/PartFeatureOperationEnum.kJoinOperation,/*Эскиз*/oProfile18);

            //Transaction oTrans4 = ThisApplication.TransactionManager.StartTransaction(ThisApplication.ActiveDocument, "Create Sample3");
            //PlanarSketch oSketch19 = oCompDef["22. Угольник"].Sketches.Add(oCompDef["22. Угольник"].WorkPlanes[2]);

            //point[0] = oSketch19.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(0.025, 5.3), false);
            //point[1] = oSketch19.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(-0.5, 5.3), false);
            //point[2] = oSketch19.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(-0.5, 6.1), false);
            //point[3] = oSketch19.SketchPoints.Add(oTransGeom["22. Угольник"].CreatePoint2d(-0.055, 6.1), false);
            //lines[0] = oSketch19.SketchLines.AddByTwoPoints(point[0], point[1]);
            //lines[1] = oSketch19.SketchLines.AddByTwoPoints(point[1], point[2]);
            //lines[2] = oSketch19.SketchLines.AddByTwoPoints(point[2], point[3]);
            //lines[3] = oSketch19.SketchLines.AddByTwoPoints(point[3], point[0]);
            //oTrans4.End();
            //Profile oProfile19 = (Profile)oSketch19.Profiles.AddForSolid();
            //RevolveFeature revolvefeature15 = oCompDef["22. Угольник"].Features.
            //RevolveFeatures.AddFull(oProfile19, lines[1],
            //PartFeatureOperationEnum.kJoinOperation);

            //EdgeCollection EdgeCollection12 = ThisApplication.TransientObjects.CreateEdgeCollection();
            //Face Face3 = revolvefeature15.SideFaces[3];
            //Edge StartEdge_4 = Face3.Edges[1];
            //EdgeCollection12.Add(StartEdge_4);
            //ThreadFeatures ThreadFeatures2 = oCompDef["22. Угольник"].Features.ThreadFeatures;
            //StandardThreadInfo stInfo3 = ThreadFeatures2.CreateStandardThreadInfo(false, true,
            //"ISO Taper External", "R1/8", "");
            //ThreadInfo ThreadInfo2 = (ThreadInfo)stInfo2;
            //ThreadFeatures2.Add(Face2, StartEdge_3, ThreadInfo2, false, true);

            //Построение детали 23. Клапан
            Имя_нового_документа("23. Клапан");
            oPartDoc["23. Клапан"].DisplayName = "23. Клапан";
            PlanarSketch oSketch20 = oCompDef["23. Клапан"].Sketches.Add(oCompDef["23. Клапан"].WorkPlanes[3]);

            point[0] = oSketch20.SketchPoints.Add(oTransGeom["23. Клапан"].CreatePoint2d(0.075, 0), false);
            point[1] = oSketch20.SketchPoints.Add(oTransGeom["23. Клапан"].CreatePoint2d(0.515, 0), false);
            point[2] = oSketch20.SketchPoints.Add(oTransGeom["23. Клапан"].CreatePoint2d(0.425, 0.9), false);
            point[3] = oSketch20.SketchPoints.Add(oTransGeom["23. Клапан"].CreatePoint2d(0, 0.9), false);
            point[4] = oSketch20.SketchPoints.Add(oTransGeom["23. Клапан"].CreatePoint2d(0, 0.3), false);
            point[5] = oSketch20.SketchPoints.Add(oTransGeom["23. Клапан"].CreatePoint2d(0.075, 0.3), false);
            lines[0] = oSketch20.SketchLines.AddByTwoPoints(point[0], point[1]);
            lines[1] = oSketch20.SketchLines.AddByTwoPoints(point[1], point[2]);
            lines[2] = oSketch20.SketchLines.AddByTwoPoints(point[2], point[3]);
            lines[3] = oSketch20.SketchLines.AddByTwoPoints(point[3], point[4]);
            lines[4] = oSketch20.SketchLines.AddByTwoPoints(point[4], point[5]);
            lines[5] = oSketch20.SketchLines.AddByTwoPoints(point[5], point[0]);
            oTrans["23. Клапан"].End();
            Profile oProfile20 = (Profile)oSketch20.Profiles.AddForSolid();
            RevolveFeature revolvefeature16 = oCompDef["23. Клапан"].Features.
            RevolveFeatures.AddFull(oProfile20, lines[3],
            PartFeatureOperationEnum.kJoinOperation);
            
            EdgeCollection EdgeCollection13 = ThisApplication.TransientObjects.CreateEdgeCollection();
            EdgeCollection13.Add(revolvefeature16.Faces[1].Edges[2]);
            EdgeCollection13.Add(revolvefeature16.Faces[1].Edges[1]);
            oCompDef["23. Клапан"].Features.ChamferFeatures.AddUsingDistance(EdgeCollection13, 1 + "мм", true);

            EdgeCollection EdgeCollection14 = ThisApplication.TransientObjects.CreateEdgeCollection();
            Face Face4 = revolvefeature16.SideFaces[1];
            Edge StartEdge_5 = Face4.Edges[1];
            EdgeCollection14.Add(StartEdge_5);// SketchPoint[] point = new SketchPoint[20];
            //ThreadFeatures ThreadFeatures3 = oCompDef["23. Клапан"].Features.ThreadFeatures;
            ThreadTableQuery ThreadFeatures3 = oCompDef["23. Клапан"].Features.Application.GeneralOptions.ThreadTableQuery;
            //GeneralOptions oGeneralOptions = ThisApplication.GeneralOptions;
            ////GeneralOptions oGeneralOptions = oCompDef["23. Клапан"].GeneralOptions;
            //ThreadTableQuery oThreadTable = oGeneralOptions.ThreadTableQuery;
            //String  oTypes;
            ////oTypes = oThreadTable.GetAvailableThreadTypes;
            ////oTypes[0] = oCompDef["23. Клапан"].GetAvailableThreadTypes();
            //String oSize;
            //oSize = oThreadTable.GetAvailableThreadSizes(false, "NPT");
            //String oDestignation = (String)oThreadTable.GetAvailableDesignations(false, "NPT", oSize);
            //oDestignation = 
            TaperedThreadInfo stInfo3 = (TaperedThreadInfo)ThreadFeatures3.CreateThreadInfo(false, true, "NPT", "R1/8");
            TaperedThreadInfo ThreadInfo3 = stInfo3;
            ThreadFeatures3.CreateThreadInfo.ThreadInfo3(Face4, StartEdge_5, ThreadInfo3, false, true);
            
            MessageBox.Show("Создание деталей завершено!", "Сообщение");


        }
    }
}
