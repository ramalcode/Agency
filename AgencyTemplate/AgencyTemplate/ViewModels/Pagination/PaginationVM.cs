namespace AgencyTemplate.ViewModels
{
    public class PaginationVM<T>
    {
        public int MaxPageCount { get; set; }
        public int CurrentPage { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
