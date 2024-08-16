using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystemFrontend.Model
{
    public class PageSettingModel
    {
        public PageSettingModel() { }
        public PageSettingModel(int pageNo, int pageSize, int pageCount)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = pageCount;
        }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
