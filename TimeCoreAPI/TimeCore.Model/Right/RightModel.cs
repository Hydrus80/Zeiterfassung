using System;
using System.ComponentModel;

namespace Model
{
    public enum eRights
    {
        Administrator = 1,
        Buchhaltung = 2,
        Benutzer = 3
    }

    public class RightModel : IRightModel
    {
        public int ID { get; set; }
        public int RightID { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
