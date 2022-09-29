using MISA.Web08.AMIS.Common.Entities;

namespace MISA.Web08.AMIS.Common.Entities
{
    public class PagingData
    {
        public object Data { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageRecords { get; set; }

        public PagingData(object data, int totalRecord, int totalPage, int currentPage, int currentPageRecords)
        {
            Data = data;
            TotalRecord = totalRecord;
            TotalPage = totalPage;
            CurrentPage = currentPage;
            CurrentPageRecords = currentPageRecords;
        }

        public PagingData()
        {
        }
    }
}
