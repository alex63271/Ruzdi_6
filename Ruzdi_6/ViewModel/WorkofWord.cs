using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Ruzdi_6.Commands;
using Ruzdi_6.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ruzdi_6.ViewModel
{
    public class WorkofWord : ViewModel
    {
        private readonly IWindowService service;

        public OpenXmlElementList Values { get; set; }

        public WorkofWord(IWindowService service)
        {

            using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(App.Temp + "/555.docx", true))
            {
                Values = wordprocessingDocument.MainDocumentPart.Document.Body.ChildElements;

                foreach (var value in Values)
                {
                    if (value is Table table)
                    {
                        Tables.Add(table);
                    }
                }
            }

            this.service = service;
        }


        public List<Table> Tables { get; set; } = new();

        private Table selectedTable;
        public Table SelectedTable { get => selectedTable; set => Set(ref selectedTable, value); }


        private OpenXmlElementList childElementsTable;
        public OpenXmlElementList ChildElementsTable  => SelectedTable?.ChildElements;  



        private OpenXmlElementList selectedChildTable;
        public OpenXmlElementList SelectedChildTable
        {
            get => selectedChildTable;


            set => Set(ref selectedChildTable, value);
        }

        





        public void OpenWin(object p) => service.ShowWindowDialog("Word");



        public void InsertTableInDoc(object p)
        {

            // Open a WordprocessingDocument for editing using the filepath.
            using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(App.Temp + "/555.docx", true))
            {

                Values = wordprocessingDocument.MainDocumentPart.Document.ChildElements;


                // Assign a reference to the existing document body.
                Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

                #region создание контейнера и внесение в документ
                // Создание таблицы
                Table tbl = new Table();

                // Установка стиля и ширины таблицы.
                TableProperties tableProp = new TableProperties();
                TableStyle tableStyle = new TableStyle
                {
                    Val = "TableGrid"
                };

                // Сделайте ширину таблицы равной 100% ширины страницы.
                TableWidth tableWidth = new TableWidth()
                {
                    Width = "5000",
                    Type = TableWidthUnitValues.Pct
                };

                // Применить
                tableProp.Append(tableStyle, tableWidth);
                tbl.AppendChild(tableProp);

                // Добавляем 3 столбца(поля) в таблицу
                TableGrid tg = new TableGrid(new GridColumn(), new GridColumn(), new GridColumn());
                tbl.AppendChild(tg);

                // Создаем 1 запись(строку) в таблице
                TableRow tr1 = new TableRow();

                // Добавление ячейки в каждый столбец строки.
                TableCell tc1 = new TableCell(new Paragraph(new Run(new Text("1"))));
                TableCell tc2 = new TableCell(new Paragraph(new Run(new Text("2"))));
                TableCell tc3 = new TableCell(new Paragraph(new Run(new Text("3"))));
                tr1.Append(tc1, tc2, tc3);

                // Внесение сторки(записи) в таблицу
                tbl.AppendChild(tr1);

                // Добавление таьлицы в документ
                body.AppendChild(tbl);

                #endregion

            }
        }

    }
}
