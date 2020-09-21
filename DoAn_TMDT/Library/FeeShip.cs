namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeeShip")]
    public partial class FeeShip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeShip()
        {
            AddressOrders = new HashSet<AddressOrder>();
        }

        [StringLength(255)]
        public string ID { get; set; }

        [Column("FeeShip")]
        [StringLength(255)]
        public string FeeShip1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddressOrder> AddressOrders { get; set; }
    }
}
