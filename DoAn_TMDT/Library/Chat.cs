namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chat")]
    public partial class Chat
    {
        [Key]
        [StringLength(255)]
        public string IDChat { get; set; }

        [StringLength(255)]
        public string Mess { get; set; }

        [StringLength(195)]
        public string FromWho { get; set; }

        [StringLength(195)]
        public string ToWho { get; set; }

        [StringLength(255)]
        public string Time { get; set; }
    }
}
