namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Report")]
    public partial class Report
    {
        [StringLength(255)]
        public string ID { get; set; }

        [StringLength(195)]
        public string UID { get; set; }

        [StringLength(255)]
        public string Mess { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(195)]
        public string IDP { get; set; }

        public virtual PayOrder PayOrder { get; set; }

        public virtual User User { get; set; }
    }
}
