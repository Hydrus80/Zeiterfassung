using System;

namespace Model
{
    public class DMSPartBrandModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public int Marke { get; set; }
    }
}
