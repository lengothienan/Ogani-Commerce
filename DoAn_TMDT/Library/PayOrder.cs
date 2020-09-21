namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PayOrder")]
    public partial class PayOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PayOrder()
        {
            InforOrders = new HashSet<InforOrder>();
            Reports = new HashSet<Report>();
        }

        [Key]
        [StringLength(195)]
        public string IDP { get; set; }

        [StringLength(195)]
        public string UID { get; set; }

        [StringLength(255)]
        public string Receiver { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Total { get; set; }

        [StringLength(255)]
        public string DateOrder { get; set; }

        [StringLength(255)]
        public string DateSend { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(255)]
        public string StatusPay { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InforOrder> InforOrders { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
