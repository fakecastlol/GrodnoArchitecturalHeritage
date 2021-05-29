namespace Identity.Services.Interfaces.Models.Sorting
{
    public class SortViewModel
    {
        public SortState EmailSort { get; private set; }
        public SortState LoginSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            EmailSort = sortOrder == SortState.EmailAsc ? SortState.EmailDesc : SortState.EmailAsc;
            LoginSort = sortOrder == SortState.LoginAsc ? SortState.LoginDesc : SortState.LoginAsc;
            Current = sortOrder;
        }
    }
}
