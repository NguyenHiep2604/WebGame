using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGame.Controllers
{
    [Filters.Permission]
    public class BaseController : Controller
    {
        // GET: Base
        public BaseController()
        {

        }
    }
}