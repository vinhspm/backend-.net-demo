using MISA.Web08.AMIS.Common.Entities;

namespace MISA.Web08.AMIS.Common.Entities
{
    public class PagingData
    {
        public List<Employee> Data { get; set; }
        public int totalCount { get; set; }
    }
}
