namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductOfEvent")]
    public partial class ProductOfEvent
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(195)]
        public string IDE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string IDSP { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        public virtual Event Event { get; set; }

        public virtual Product Product { get; set; }
    }
}
