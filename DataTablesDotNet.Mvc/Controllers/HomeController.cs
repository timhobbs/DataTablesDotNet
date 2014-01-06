using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DataTablesDotNet.Models;
using DataTablesDotNet.Mvc.Data;

namespace DataTablesDotNet.Mvc.Controllers {

    public class HomeController : Controller {

        public ActionResult Index() {
            return View();
        }

        public JsonResult GetUsers(DataTablesRequest model) {
            string dataPath = ConfigurationManager.AppSettings["UsersPath"];
            var repo = new UserRepository(dataPath);
            var data = repo.GetAll().AsQueryable();
            var dataTableParser = new DataTablesParser<User>(model, data);
            var formattedList = dataTableParser.Process();

            return Json(formattedList, JsonRequestBehavior.AllowGet);
        }
    }
}