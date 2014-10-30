using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace asyncawaitmvc4.Controllers
{
    public class HomeController : Controller
    {
        #region blocking action method
        public ActionResult Index()
        {
            Delay2(2000);
            return View();
        }

        #endregion

        public void Delay2(int milliseconds)        
        {
            Thread.Sleep(milliseconds);
        }


        #region simulate a long running IO operation
        
        public async Task<string> LongRunningIoMethod(int seconds)
        {
            Task t = Task.Delay(seconds);
            await t;
            return "Long Running Method Complete";
        }

        #endregion

        #region asyncronous actionmethod using async and await

        public async Task<ActionResult> Test()
        {
            Task<string> t = Task.Run(() => LongRunningIoMethod(2000));
            string msg = await t;

            ViewBag.Message = msg;
            return View("Index");
        }

        #endregion

    }
}
