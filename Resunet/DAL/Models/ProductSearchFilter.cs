﻿namespace Estore.DAL.Models
{
    public class ProductSearchFilter
    {
        public enum SortByColumn { ReleaseDate }
        public enum SortDirection { Asc, Desc }
        public int PageSize { get; set; } = 24;
        public int Offset { get; set; } = 0;
        public int? AuthorId { get; set; }
        public SortByColumn SortBy { get; set; } = SortByColumn.ReleaseDate;
        public SortDirection Direction { get; set; } = SortDirection.Asc;
        public int? CategoryId { get; set; }
        public string? SerieName { get; set; }
    }
}