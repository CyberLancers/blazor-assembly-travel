using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using BlazorAssemblyTravel.Shared.Models;

namespace BlazorAssemblyTravel.Client.Shared;

public partial class CruiseDeals
{
    [Inject] 
    public HttpClient Http { get; set; }
    
    protected List<CruiseDeal> Deals { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Deals = await Http.GetFromJsonAsync<List<CruiseDeal>>("/api/CruiseDeals") ?? new List<CruiseDeal>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}