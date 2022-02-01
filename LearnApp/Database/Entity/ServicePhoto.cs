namespace LearnApp.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServicePhoto")]
    public partial class ServicePhoto
    {
        public int ID { get; set; }

        public int ServiceID { get; set; }

        public byte[] Photo { get; set; }
        [Required]
        public virtual Service Service { get; set; }
    }
}
