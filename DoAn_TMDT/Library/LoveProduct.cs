namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoveProduct")]
    public partial class LoveProduct
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string IDSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(195)]
        public string UID { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
