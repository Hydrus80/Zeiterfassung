using System;

namespace Model
{
    public class DMSPartTaxModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public string MwstKennzeichen { get; set; }
    }
}
