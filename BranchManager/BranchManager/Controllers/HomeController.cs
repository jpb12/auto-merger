using System.Web.Mvc;

namespace BranchManager.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}