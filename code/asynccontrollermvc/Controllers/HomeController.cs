using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication7.Controllers
{
    public class HomeController : AsyncController
    {
        public ActionResult Index()
        {
            Thread.Sleep(2000);
            ViewBag.Message = "Welcome to *SYNCHRONOUS* ASP.NET MVC!";
            return View();
        }


       

        public void LongIoDelay(int milliseconds)        
        {
            Thread.Sleep(milliseconds);
            AsyncManager.OutstandingOperations.Decrement();
        }


        #region asyncronous actionmethods using EAP, _Async and _Completed methods

        public void TestAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            new Thread(() => LongIoDelay(2000)).Start();
        }

        public ActionResult TestCompleted(AsyncCompletedEventArgs arg)
        {
            ViewBag.Message = "EAP Completed Event";
            return View("Index");
        }

        #endregion

    }
}
