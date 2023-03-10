using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_PROJECT_MPT.Models
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState LoginSort { get; private set; }
        public SortState EmailSort { get; private set; }
        public SortState Current { get; private set; }
        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ?
                SortState.IdDesc : SortState.IdAsc;
            EmailSort = sortOrder == SortState.EmailAsc ?
                SortState.EmailSesc : SortState.EmailAsc;
            LoginSort = sortOrder == SortState.LoginAsc ?
                SortState.LoginDesc : SortState.LoginAsc;
            Current = sortOrder;
        }
    }
}
