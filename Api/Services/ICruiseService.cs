using BlazorAssemblyTravel.Shared.Models;

namespace BlazorAssemblyTravel.Api.Services;

public interface ICruiseService
{
    Task<List<CruiseDeal>> GetCruiseDeals();
}