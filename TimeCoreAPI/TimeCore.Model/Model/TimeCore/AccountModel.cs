using System;

namespace Model
{
    public class AccountModel : BasicModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string GUID { get; set; }
        public int WorkshopID { get; set; }
        public virtual WorkshopModel Workshop { get; set; }

        public void WithoutPassword()
        {
            this.Password = null;
        }

        public void CreateGUID()
        {
            this.GUID = Guid.NewGuid().ToString();
        }

        public void DeaktivateGUID()
        {
            this.GUID = string.Empty;
        }
    }

}
