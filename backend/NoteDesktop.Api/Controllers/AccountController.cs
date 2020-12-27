using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NoteDesktop.Api.Controllers
{
    public class AccountController : Controller
    {

        // todo register
        // todo login
        // todo provider login/register
        // todo logout
        public IActionResult Index()
        {
            return Ok();
        }
    }
}