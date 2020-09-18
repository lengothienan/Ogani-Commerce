namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InforOrder")]
    public partial class InforOrder
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(195)]
        public string IDP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string IDSP { get; set; }

        [StringLength(255)]
        public string Amount { get; set; }

        [StringLength(255)]
        public string IntoMoney { get; set; }

        [StringLength(255)]
        public string price { get; set; }

        public virtual PayOrder PayOrder { get; set; }

        public virtual Product Product { get; set; }
    }
}
