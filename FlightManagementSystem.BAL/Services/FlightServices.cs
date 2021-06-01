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
		public List<FlightAddResponseModel> AddFlightDetailMethod(FlightDetailRequest flightDetail)
		{
			using (transaction = (DbContextTransaction)_context.Database.BeginTransaction())
			{
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
						transaction.Commit();
					else
						transaction.Rollback();

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
					transaction.Rollback();
					_logger.LogInformation($"Exception : {ex.Message}");
					throw;
				}
			}

		}
		/// <summary>
		/// Dispose the objects
		/// </summary>
		public void Dispose()
		{
			throw new NotImplementedException();
		}

	}
}
