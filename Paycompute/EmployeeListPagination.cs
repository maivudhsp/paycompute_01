using Microsoft.AspNetCore.Http.Features;
using System.Security.Cryptography.X509Certificates;

namespace Paycompute
{
    public class EmployeeListPagination<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public EmployeeListPagination(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
            //enable or disable our paging button
        public bool IsPreviousPageAvailable => PageIndex > 1;
        public bool IsNextPageAvailable => PageIndex < TotalPages;
        public static EmployeeListPagination<T> Create(IList<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new EmployeeListPagination<T>(items, count, pageIndex, pageSize);
        }
        
    }
}
