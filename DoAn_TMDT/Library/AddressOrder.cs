namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AddressOrder")]
    public partial class AddressOrder
    {
        [StringLength(255)]
        public string ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string FeeShip { get; set; }

        public virtual FeeShip FeeShip1 { get; set; }
    }
}
