using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalCoreProje.Models;
using EntityLayer.Concrete; // Doğru Destination sınıfını kullandığımızdan emin oluyoruz

namespace TraversalCoreProje.Areas.Admin.Controllers
{
	[Area("Admin")]
	
	public class CityController : Controller
	{
		private readonly IDestinationService _destinationService;

		public CityController(IDestinationService destinationService)
		{
			_destinationService = destinationService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult CityList()
		{
			var jsoncity = JsonConvert.SerializeObject(_destinationService.TGetList());
			return Json(jsoncity);
		}

		[HttpPost]
		public IActionResult AddCityDestination(EntityLayer.Concrete.Destination destination) // Burada açıkça namespace belirtiyoruz
		{
			destination.Status = true;
			_destinationService.TAdd(destination);
			var values = JsonConvert.SerializeObject(destination);
			return Json(values);
		}
		public IActionResult GetById(int DestinationID)
		{
			var values=_destinationService.TGetByID(DestinationID);
			var jsonvalues = JsonConvert.SerializeObject(values);	
			return Json(jsonvalues);
		}
		public IActionResult DeleteCity(int id)
		{
			var values= _destinationService.TGetByID(id);
			_destinationService.TDelete(values);
			return NoContent();
		}
		public IActionResult UpdateCity(EntityLayer.Concrete.Destination destination)
		{
			
			_destinationService.TUpdate(destination);
			var v= JsonConvert.SerializeObject(destination);
			return Json(v);
		}
	}
}
