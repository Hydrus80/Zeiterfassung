using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
   
    public class BasicModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
