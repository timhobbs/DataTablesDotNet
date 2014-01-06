using System.Reflection;

namespace DataTablesDotNet.Models {

    public class DataTablesColumn {

        public string Name { get; set; }

        public int ColumnIndex { get; set; }

        public bool IsSearchable { get; set; }

        public bool IsSortable { get; set; }

        public PropertyInfo Property { get; set; }

        public int SortOrder { get; set; }

        public bool IsCurrentlySorted { get; set; }

        public string SortDirection { get; set; }

        public DataTablesColumn(string name, 
                                int columnIndex, 
                                bool isSearchable,
                                bool isSortable) {
            Name = name;
            ColumnIndex = columnIndex;
            IsSearchable = isSearchable;
            IsSortable = IsSortable;
        }

        public DataTablesColumn()
            : this(string.Empty, 0, true, true) {
        }
    }
}