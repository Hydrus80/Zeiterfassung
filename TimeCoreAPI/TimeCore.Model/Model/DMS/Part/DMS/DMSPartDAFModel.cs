using System;

namespace Model
{
    public class DMSPartDAFModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public bool DAFMdiUnterdruecken { get; set; }
    }
}
