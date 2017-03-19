﻿using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        public ProductRepository repoProduct = RepositoryHelper.GetProductRepository();

        //protected override void HandleUnknownAction(string actionName)
        //{
        //    this.RedirectToAction("/").ExecuteResult(this.ControllerContext);
        //}
    }

}