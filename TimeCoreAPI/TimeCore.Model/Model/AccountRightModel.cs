using System;

namespace Model
{
    public class AccountRightModel : BasicModel
    {
        public int RightID { get; set; }
        public virtual AccountModel Account { get; set; }
    }
}
