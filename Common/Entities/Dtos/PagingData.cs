using Common.Entities;

namespace Common
{
    public class PagingData
    {
        // dữ liệu phân trang chính
        public object Data { get; set; }

        // tổng số bản ghi theo filter
        public int TotalRecord { get; set; }

        // tổng số trang
        public int TotalPage { get; set; }

        // trang hiện tại
        public int CurrentPage { get; set; }

        // số bản ghi của trang hiện tại
        public int CurrentPageRecords { get; set; }

        public PagingData(object data, int totalRecord, int totalPage, int currentPage, int currentPageRecords)
        {
            Data = data;
            TotalRecord = totalRecord;
            TotalPage = totalPage;
            CurrentPage = currentPage;
            CurrentPageRecords = currentPageRecords;
        }

    }
}
