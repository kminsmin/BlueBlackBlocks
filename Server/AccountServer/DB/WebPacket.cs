using System.ComponentModel.DataAnnotations;

namespace AccountServer.DB
{
    public class AccountSignupReq
    {

        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public string AccountPasswordConfirm { get; set; }
    }

    public class AccountLoginReq
    {
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
    }

    public class AccountLoginRes
    {
        public int LoginOk { get; set; }
    }
}
