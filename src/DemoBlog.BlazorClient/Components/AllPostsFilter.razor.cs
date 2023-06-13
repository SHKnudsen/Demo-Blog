using DemoBlog.BlazorClient.Services;
using DemoBlog.Domain.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace DemoBlog.BlazorClient.Components
{
    public partial class AllPostsFilter
    {
        private static Func<Cuisine, string> CuisineConverter => cuisine => cuisine.Country;
        private string SearchText { get; set; } = "";

        
        [Parameter]
        public MudColor BackgroundColor { get; set; }

        public IEnumerable<Cuisine> SelectedCuisines { get; set; } = new HashSet<Cuisine>();

        public MudChip[] SelectedFilterChips;


        void Closed(MudChip chip)
        {
            var cuisine = (Cuisine)chip.Value;
            SelectedCuisines = SelectedCuisines.Where(x => !x.Equals(cuisine));
        }
    }
}
