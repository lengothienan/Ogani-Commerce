namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        [StringLength(255)]
        public string IDCM { get; set; }

        [StringLength(195)]
        public string UID { get; set; }

        [Column("Comment")]
        [StringLength(255)]
        public string Comment1 { get; set; }

        [StringLength(255)]
        public string IDSP { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
