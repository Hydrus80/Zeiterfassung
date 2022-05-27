using System;

namespace Model
{
    public class AccountRightModel : IAccountRightModel
    {
        public int ID { get; set; }
        public int RightID { get; set; }
        public virtual AccountModel Account { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
