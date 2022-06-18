using System;

namespace Model
{
    public class DMSPartQuantityModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public int MengeneinheitID { get; set; }
        public decimal MengeProVerpackungseinheit { get; set; }
    }
}
