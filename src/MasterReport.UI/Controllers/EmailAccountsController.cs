﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterReport.UI.Controllers
{
    public class EmailAccountsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
