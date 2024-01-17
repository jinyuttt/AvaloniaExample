using System;

namespace AvaDB.ViewModels
{
    public class GridRow
    {
        public int Id { get; set; }
        public string? Name { get; set; }
     
        public string? CellValue {  get; set; }

        public string? CellType { get; set; }
    }
}