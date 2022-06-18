using System;

namespace Model
{
    public class DMSPartIvecoModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public int LieferantID { get; set; }
        public string LieferantME { get; set; }
        public string LieferantMwSt { get; set; }
        public string LieferantWG { get; set; }
        public int LiRabattgruppeID { get; set; }

    }
}
