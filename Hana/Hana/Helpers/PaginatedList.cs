//using Microsoft.EntityFrameworkCore;

//namespace Hana.Helpers
//{
//    public class PaginatedList<T> : List<T>
//    {
//        public int CurrentItems { get; set; } = 0;
//        public int TotalItems { get; private set; }
//        public int PageIndex { get; private set; }
//        public int TotalPages { get; private set; }

//        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
//        {
//            PageIndex = pageIndex;
//            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
//            TotalItems = count;
//            CurrentItems = (pageIndex - 1) * pageSize + items.Count;
//            this.AddRange(items);
//        }

//        public bool HasPreviousPage
//        {
//            get
//            {
//                return (PageIndex > 1);
//            }
//        }

//        public bool HasNextPage
//        {
//            get
//            {
//                return (PageIndex < TotalPages);
//            }
//        }


//        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
//        {
//            var count = await source.CountAsync();
//            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
//            return new PaginatedList<T>(items, count, pageIndex, pageSize);
//        }
//    }


//}
