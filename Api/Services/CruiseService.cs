using BlazorAssemblyTravel.Api.Data;
using BlazorAssemblyTravel.Api.Data.Repositories;
using BlazorAssemblyTravel.Shared.Models;

namespace BlazorAssemblyTravel.Api.Services;

public class CruiseService : ICruiseService
{
    private IUnitOfWork _unitOfWork;
    
    public CruiseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<CruiseDeal>> GetCruiseDeals()
    {
        var startDate = DateTime.Today.AddMonths(1);

        var statistics =
            await _unitOfWork.ItineraryStatisticRepository.Get(i => i.StartDate > startDate && i.StartPrice > 0, 
                i => i.OrderByDescending(t => t.DiscountPercent), 
                10, "Itinerary");

        var cruiseDeals = new List<CruiseDeal>();
        foreach (var statistic in statistics)
        {
            var cruiseDeal = new CruiseDeal
            {
                ItineraryId = statistic.ItineraryId,
                ItineraryTitle = statistic.Itinerary.Title ?? string.Empty,
                DateStart = statistic.Itinerary.DateStart,
                RoomType = statistic.RoomType.Description ?? string.Empty,
                StartPrice = statistic.StartPrice,
                EndPrice = statistic.EndPrice,
                Discount = Convert.ToInt32(statistic.DiscountPercent * 100)
            };
            cruiseDeals.Add(cruiseDeal);
        }

        return cruiseDeals;
    }
}