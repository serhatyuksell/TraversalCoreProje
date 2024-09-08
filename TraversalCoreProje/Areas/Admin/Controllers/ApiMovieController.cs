using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TraversalCoreProje.Areas.Admin.Models;
namespace TraversalCoreProje.Areas.Admin.Controllers
{
	[Area("Admin")]
	
	public class ApiMovieController : Controller
	{
		List<ApiMovieViewModel> apiMovies=new List<ApiMovieViewModel>();
		public async Task<IActionResult> Index()
		{
			
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
				Headers =
	{
		{ "x-rapidapi-key", "7e4d04afb0msh2121dd1c4e5e7bfp1ab01djsnacd9c8195357" },
		{ "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				apiMovies = JsonConvert.DeserializeObject<List<ApiMovieViewModel>>(body);
				return View(apiMovies);
			}
		}
	}
}
