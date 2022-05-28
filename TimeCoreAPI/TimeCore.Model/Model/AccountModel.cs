using System;

namespace Model
{
    public class AccountModel : BasicModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual WorkshopModel Workshop { get; set; }
    }
}
