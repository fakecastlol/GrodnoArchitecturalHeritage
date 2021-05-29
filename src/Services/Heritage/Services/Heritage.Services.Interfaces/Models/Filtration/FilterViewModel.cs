namespace Heritage.Services.Interfaces.Models.Filtration
{
    using Heritage.Services.Interfaces.Models.Construction;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;

    namespace FilterSortPagingApp.Models
    {
        public class FilterViewModel
        {
            public FilterViewModel(List<ConstructionResponseCoreModel> constructions, Guid? construction, string name)
            {
                Constructions = new SelectList(constructions, "Id", "Name", construction);
                SelectedConstruction = construction;
                SelectedName = name;
            }
            public SelectList Constructions { get; private set; }
            public Guid? SelectedConstruction { get; private set; }   
            public string SelectedName { get; private set; } 
        }
    }
}
