namespace FitnessPortalAPI.Models
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }
        public int TotalItemsCount { get; set; }
        public PageResult(List<T> items, int totalCount, int pageSize, int pageNumber) 
        { 
            Items = items;
            TotalItemsCount = totalCount;
            ItemFrom = totalCount == 0 ? 0 : pageSize *(pageNumber-1) + 1;
            ItemTo = (ItemFrom + pageSize - 1) <= totalCount ? (ItemFrom + pageSize - 1) : (totalCount);
            TotalPages = (int)Math.Ceiling(totalCount / (double) pageSize);
        }
    }
}
