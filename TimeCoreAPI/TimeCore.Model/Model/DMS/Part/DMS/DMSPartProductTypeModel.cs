using System;

namespace Model
{
    public class DMSPartProductTypeModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public string Produkttypengruppe { get; set; }
    }
}
