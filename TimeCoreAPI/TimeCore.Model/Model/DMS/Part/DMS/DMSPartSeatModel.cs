using System;

namespace Model
{
    public class DMSPartSeatModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public bool SEATAutopartDabeiIn310 { get; set; }
        public bool SEATAutopartUnterdruecken { get; set; }

    }
}
