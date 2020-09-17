namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductOfType")]
    public partial class ProductOfType
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(195)]
        public string IDLSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string IDSP { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}
