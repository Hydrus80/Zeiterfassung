using System;

namespace Model
{
    public class DMSPartMANModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public bool MANFremdteil { get; set; }
    }
}
