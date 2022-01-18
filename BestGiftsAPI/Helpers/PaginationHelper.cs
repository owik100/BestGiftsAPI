using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Helpers
{
    public class PaginationHelper: IPaginationHelper
    {
        public int CalculateTotalPages(int count, int pageSze)
        {
            return (int)Math.Ceiling(decimal.Divide(count, pageSze));
        }
    }
}
