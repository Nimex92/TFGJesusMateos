using iText.Layout.Borders;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cell = iText.Layout.Element.Cell;
using Color = iText.Kernel.Colors.Color;

namespace HolaMundoMAUI
{
    public class BetterTable
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public int CurrentColumn { get; set; } = 0;
        public int CurrentRow { get; set; } = 0;

        public Table Table { get; private set; }
        private List<List<Cell>> Matrix { get; set; } = new List<List<Cell>>();

        public BetterTable() : this(1, 1)
        {
        }

        public BetterTable(int columns, int rows)
        {
            Table = new Table(columns, false);
            Columns = columns;
            Rows = rows;

            var columnWidth = 10000/ columns;


            for (int i = 0; i < Rows; i++)
            {
                Matrix.Add(new List<Cell>());
                for (int j = 0; j < Columns; j++)
                {
                    var newCell = new Cell()
                    {
                        
                    };

                    newCell.SetWidth(columnWidth);
                    newCell.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    
                    Matrix[i].Add(newCell);
                    Table.AddCell(newCell);
                }
            }
        }

        public void SetColorHeader(Color color)
        {
            if (Matrix[0] is null) { return; }

            foreach (var column in Matrix[0])
            {
                column.SetBackgroundColor(color);
            }
        }
        public void SetNextText(string newText)
        {
            Matrix[CurrentRow][CurrentColumn].Add(new Paragraph(newText));

            CurrentColumn++;
            if (CurrentColumn >= Columns)
            {
                CurrentColumn = 0;
                CurrentRow++;
            }

            if (CurrentRow >= Rows)
            {
                CurrentRow = 0;
            }
        }
        public void ChangeTableFontSize(int size)
        {
            if (Matrix[0] is null) { return; }
            for (int i=0; i < Matrix.Count; i++)
            {
                foreach (var column in Matrix[i])
                {
                    column.SetFontSize(size);
                }
            }
        }  
        public void RemoveBorder(int option)
        {
            if (Matrix[0] is null) { return; }
            switch (option)
            {
                case 0:
                    for (int i = 0; i < Matrix.Count; i++)
                    {
                        foreach (var column in Matrix[i])
                        {
                            if (i != 0)
                                column.WithoutBorder(option);
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < Matrix.Count; i++)
                    {
                        foreach (var column in Matrix[i])
                        {
                                column.WithoutBorder(option);
                        }
                    }
                    break;
            };
        }
        public void AddTableBorder(int size)
        {
            Table.SetBorder(new SolidBorder(size));
        }
        public void TableConjunctionUp()
        {
            Table.SetBorderTop(new SolidBorder(0));
        }
        public void TableConjunctionDown()
        {
            Table.SetBorderBottom(new SolidBorder(0));
        }
        public Cell this[int x, int y]
        {
            get { return Matrix[x][y]; }
        }
    }

    public static class CellExtensions
    {
        public static void SetText(this Cell cell, string newText)
        {
            cell.Add(new Paragraph(newText));
        }

        public static void SetFontSize(this Cell cell, int size)
        {
            cell.SetFontSize(size);
        }
        public static void SetTextAlign(this Cell cell,string option)
        {
            switch (option)
            {
                case "START": cell.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT); break;
                case "CENTER": cell.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER); break;
                case "END": cell.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT); break;
            }
        }
        public static void WithoutBorder(this Cell cell,int option)
        {
            switch (option)
            {
                case 0:
                    cell.SetBorderTop(iText.Layout.Borders.Border.NO_BORDER);
                    break;
                case 1:
                    cell.SetBorderTop(iText.Layout.Borders.Border.NO_BORDER);
                    cell.SetBorderBottom(iText.Layout.Borders.Border.NO_BORDER);
                    cell.SetBorderLeft(iText.Layout.Borders.Border.NO_BORDER);
                    cell.SetBorderRight(iText.Layout.Borders.Border.NO_BORDER);
                    break;
                case 2:
                    cell.SetBorderBottom(iText.Layout.Borders.Border.NO_BORDER);
                    break;
            }
        }
        public static void AddAllBorders(this Cell cell)
        {
            cell.SetBorderTop(new SolidBorder(1));
            cell.SetBorderBottom(new SolidBorder(1));
            cell.SetBorderLeft(new SolidBorder(1));
            cell.SetBorderRight(new SolidBorder(1));
        }
        public static void AddBorder(this Cell cell,int option)
        {
            switch (option)
            {
                case 0: cell.SetBorderRight(new SolidBorder(1)); break;
                case 1: cell.SetBorderTop(new SolidBorder(1)); break;
                case 2: cell.SetBorderLeft(new SolidBorder(1)); break;
                case 3: cell.SetBorderBottom(new SolidBorder(1)); break;
            }
        }
        
    }
}
