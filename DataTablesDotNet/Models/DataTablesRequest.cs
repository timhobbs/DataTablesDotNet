using System.Collections.Generic;

namespace DataTablesDotNet.Models {

    public class DataTablesRequest {

        public int sEcho { get; set; }

        public int iColumns { get; set; }

        public string sColumns { get; set; }

        public int iDisplayStart { get; set; }

        public int iDisplayLength { get; set; }

        public IList<string> mDataProp { get; set; }

        public string sSearch { get; set; }

        public bool bRegex { get; set; }

        public IList<string> sSearchList { get; set; }

        public IList<bool> bRegexList { get; set; }

        public IList<bool> bSearchable { get; set; }

        public IList<string> iSortCol { get; set; }

        public IList<string> sSortDir { get; set; }

        public int iSortingCols { get; set; }

        public IList<bool> bSortable { get; set; }

        public DataTablesRequest() {
            mDataProp = new List<string>();
            sSearchList = new List<string>();
            bRegexList = new List<bool>();
            bSearchable = new List<bool>();
            iSortCol = new List<string>();
            sSortDir = new List<string>(); 
            bSortable = new List<bool>();
        }
    }
}