using AddTextToImage.Mvc.Models;
using AddTextToImage.Mvc.Models.Home;
using Microsoft.AspNetCore.Mvc;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;

namespace AddTextToImage.Mvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
		{
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();
			return View(model);
		}
		[HttpPost]
		public IActionResult Index(IndexViewModel model)
		{
			var downloadsPath = string.Empty;

			using (var img = SixLabors.ImageSharp.Image.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "paper.jpg")))
			{
				FontCollection collection = new();
				var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources"));
				FontFamily family = collection.Add(Path.Combine(path, "Autography.otf"));
				Font font = family.CreateFont(80, FontStyle.Regular);

				// The options are optional
				TextOptions options = new(font)
				{
					Origin = new PointF(150, 100), // Set the rendering origin.
					TabWidth = 8, // A tab renders as 8 spaces wide
					WrappingLength = 1400, // Greater than zero so we will word wrap at 500 pixels wide
					HorizontalAlignment = HorizontalAlignment.Left,
				};

				IBrush brush = Brushes.Solid(Color.Black);
				string text = model.Text;
				downloadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "paper-with-text.jpg");

				img.Mutate(x => x.DrawText(options, text, brush));
				img.Save(Path.Combine(downloadsPath));
			}

			ResultViewModel resultModel = new ResultViewModel();
			resultModel.Path = "/images/paper-with-text.jpg";
			return RedirectToAction("Result", resultModel);
		}

		public IActionResult Result(ResultViewModel model)
		{
			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}