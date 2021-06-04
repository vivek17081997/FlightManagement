using FlightManagementSystem.BAL.IServices;
using FlightManagementSystem.DAL;
using FlightManagementSystem.DAL.Entities;
using FlightMangementSystem.Models.RequestModel.FlightModels;
using FlightMangementSystem.Models.ResponseModel.FlightModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.BAL.Services
{
	public class FlightServices : IFlightServices, IDisposable
	{
		private readonly ILogger<FlightServices> _logger;
		private readonly ApplicationDbContext _context;

		DbContextTransaction transaction;

		public FlightServices(ILogger<FlightServices> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}


		/// <summary>
		/// Add Flight Detail Method
		/// </summary>
		/// <param name="flightDetail"></param>
		/// <returns></returns>
		public List<FlightAddResponseModel> AddFlightDetailMethod(FlightDetailRequest flightDetail, out bool isAdded)
		{
			//todo add the transactions for the operation
			try
			{
				var flight = _context.FlightDetails.AddAsync(
					new FlightDetail()
					{
						FlightCompany = flightDetail.FlightCompany,
						FlightNumber = flightDetail.FlightNumber,
						TotalSeats = flightDetail.TotalSeats,
						TicketPrice = flightDetail.TicketPrice,
						CreatedBy = 5,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now,
						ModifyBy = 5,
						IsActive = true
					});

				int result = _context.SaveChanges();

				if (result > 0)
				{
					isAdded = true;
				}
				else
				{
					isAdded = false;
				}

				return _context.FlightDetails.Where(x => x.IsActive == true).Select(y =>
					new FlightAddResponseModel()
					{
						FlightId = y.FlightId,
						FlightCompany = y.FlightCompany,
						FlightNumber = y.FlightNumber,
						TicketPrice = y.TicketPrice,
						TotalSeats = y.TotalSeats,
					}
					).ToList();
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Exception : {ex.Message}",ex);
				throw;
			}

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="flightDetail"></param>
		/// <returns></returns>
		public List<FlightAddResponseModel> GetAllFlights()
		{
			try
			{
				return _context.FlightDetails.Where(x => x.IsActive == true).Select(data =>
					new FlightAddResponseModel()
					{
						FlightId = data.FlightId,
						FlightCompany = data.FlightCompany,
						FlightNumber = data.FlightNumber,
						TicketPrice = data.TicketPrice,
						TotalSeats = data.TotalSeats
					}
					).ToList();
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Exception : {ex.Message}",ex);
				throw;
			}

		}


		/// <summary>
		/// Dispose the objects
		/// </summary>
		public void Dispose()
		{

		}

		/// <summary>
		/// Search Flight
		/// </summary>
		/// <param name="requestModel"></param>
		/// <returns></returns>
		public List<SearchedFlightDetailsResponseModel> SearchFlight(FlightSearchRequestModel requestModel)
		{
			try
			{
				var collection = (from Flight in _context.FlightDetails
								  join Departure in _context.DepartureDetails on Flight.FlightId equals Departure.FlightId
								  join Airport in _context.Airports on Departure.FlightId equals Airport.AirportId
								  select new SearchedFlightDetailsResponseModel
								  {
									 FlightId     = Flight.FlightId,
									 FlightNumber= Flight.FlightNumber,
									 FlightCompany= Flight.FlightCompany,
									 TotalSeats = Flight.TotalSeats,
									 TicketPrice = Flight.TicketPrice,
									  DepatureDate = Departure.DepatureDate.Date,
									  DepatureTime = Departure.DepatureDate,
									  From= Departure.AirPortIdFrom,
									  To = Departure.AirPortIdTo,
									  AirportName = Airport.AirportName,
									  AirportId=Airport.AirportId
								  }).OrderBy(x=> x.DepatureDate.TimeOfDay).ToList();

				//todo modify the searching
				var searchResult = collection.Where(flight => flight.FlightNumber == requestModel.FlightNumber || flight.FlightCompany == requestModel.FlightCompany || flight.DepatureDate.Date == requestModel.DepartureDatetime.Date || flight.DepatureDate.TimeOfDay == requestModel.DepartureDatetime.TimeOfDay).ToList();
				
				int pageNo = requestModel.PageNo;

				int recordsPerPage = 10;

				dynamic result = searchResult.Skip(pageNo * recordsPerPage).Take(10).ToList();

				return result;
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Exception : {ex.Message}",ex);
				throw ;
			}
		}
	}
}
