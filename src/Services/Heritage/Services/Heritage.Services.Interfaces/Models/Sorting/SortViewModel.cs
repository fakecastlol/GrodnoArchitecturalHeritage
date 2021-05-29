namespace Heritage.Services.Interfaces.Models.Sorting
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; }
        public SortState AddressSort { get; private set; }
        public SortState Current { get; private set; } 

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            AddressSort = sortOrder == SortState.AddressAsc ? SortState.AddressDesc : SortState.AddressAsc;
            Current = sortOrder;
        }
    }
}
