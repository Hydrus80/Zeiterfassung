using System;

namespace Model
{
    public class DMSPartSupplierModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public bool DAFMdiUnterdruecken { get; set; }
    }
}
