using System;

namespace Model
{
    public class AccountModel : IBasicModel, IAccountModel
    {
        public int ID { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual WorkshopModel Workshop { get; set; }
    }
}
