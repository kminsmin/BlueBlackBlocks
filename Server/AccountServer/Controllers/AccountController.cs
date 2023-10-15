using AccountServer.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;

namespace AccountServer.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController (AppDbContext context)
        {
            _context = context;
        }


        [Route("")]
        [Route("/")]
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(AccountDb model)
        {
            AccountDb account = _context.Accounts
                                            .AsNoTracking()
                                            .Where(a => a.AccountName == model.AccountName)
                                            .FirstOrDefault();

            if (account == null)
            {
                _context.Accounts.Add(model);
            }

            _context.SaveChanges();

            return View(model);
        }
    }
}
