using FlightMangementSystem.Models.RequestModel.FlightModels;
using FlightMangementSystem.Models.ResponseModel.FlightModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.BAL.IServices
{
	public interface IFlightServices
	{
		List<FlightAddResponseModel> AddFlightDetailMethod(FlightDetailRequest flightDetail,out bool isAdded);
		List<FlightAddResponseModel> GetAllFlights();
		List<SearchedFlightDetailsResponseModel> SearchFlight(FlightSearchRequestModel requestModel);
		
	}
}
