using System;

namespace Model
{
    public class DMSPartPackingModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public int VerpackungseinheitID  { get; set; }
    }
}
