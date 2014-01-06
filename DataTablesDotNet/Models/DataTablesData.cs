using System.Collections.Generic;

namespace DataTablesDotNet.Models {

    public class DataTablesData {

        public int sEcho { get; set; }

        public int iTotalRecords { get; set; }

        public int iTotalDisplayRecords { get; set; }

        public List<List<string>> aaData { get; set; }

        public string sColumns { get; set; }
    }
}