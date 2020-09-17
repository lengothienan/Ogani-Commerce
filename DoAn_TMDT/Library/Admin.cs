namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [StringLength(255)]
        public string IDAD { get; set; }

        [StringLength(255)]
        public string AID { get; set; }

        [StringLength(255)]
        public string PW { get; set; }
    }
}
