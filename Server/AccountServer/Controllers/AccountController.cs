using AccountServer.DB;
using AccountServer.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountServer.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordEncryptor _passwordEncryptor;

        public AccountController(AppDbContext context, PasswordEncryptor passwordEncryptor)
        {
            _context = context;
            _passwordEncryptor = passwordEncryptor;
        }


        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        [Route("signup")]
        public IActionResult Signup(AccountSignupReq req)
        {
            if (req.AccountName == null || req.AccountPassword == null || req.AccountPasswordConfirm == null)
                return Redirect("~/account/signup");

            AccountDb account = _context.Accounts
                                            .AsNoTracking()
                                            .Where(a => a.AccountName == req.AccountName)
                                            .FirstOrDefault();

            if (account == null)
            {
                string encryptPassword = _passwordEncryptor.Encrypt(req.AccountPassword);

                AccountDb newAccount = new AccountDb()
                {
                    AccountName = req.AccountName,
                    AccountPassword = encryptPassword
                };
                _context.Accounts.Add(newAccount);

                _context.SaveChanges();
            }
            else
            {
                ViewData["Message"] = "이미 존재하는 아이디입니다.";
                return View(req);
            }

            ViewData["Message"] = "회원 가입이 완료되었습니다.";
            return View(req);
        }


        [HttpPost]
        [Route("login")]
        public AccountLoginRes Login([FromBody] AccountLoginReq req)
        {
            AccountDb account = _context.Accounts
                                .AsNoTracking()
                                .Where(a => a.AccountName == req.AccountName)
                                .FirstOrDefault();

            AccountLoginRes res = new AccountLoginRes();

            if (account != null)
            {
                string reqPassword =req.AccountPassword;
                string accountPassword = account.AccountPassword;
                if (_passwordEncryptor.IsmatchPassword(reqPassword, accountPassword))
                {
                    res.LoginOk = (int)Define.LoginResult.Success;
                    return res;
                }
            }

            res.LoginOk = (int)Define.LoginResult.Faile;
            return res;
        }
    }
}
