namespace Demo.Models
{
    public class PaginatedList<T> : List<T>
    {
        private Pageable Pageable { get; set; }
        public PaginatedList(List<T> items, Pageable pageable) 
        {
            Pageable = pageable;
            AddRange(items);
        }

        public static PaginatedList<T> Search(IQueryable<T> query, Pageable pageable)
        {
            var result = query.Skip(pageable.Page*pageable.Size).Take(pageable.Size).ToList();
            return new PaginatedList<T>(result, pageable);
        }
    }
}
