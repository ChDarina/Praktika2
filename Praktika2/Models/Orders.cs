namespace Praktika2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public int IllustratorID { get; set; }

        public int CustomerID { get; set; }

        public int? IllustrationID { get; set; }

        public string Commentary { get; set; }

        public string Feedback { get; set; }

        [StringLength(255)]
        public string OrderStatus { get; set; }

        public int? Price { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Illustrations Illustrations { get; set; }

        public virtual Illustrators Illustrators { get; set; }
    }
}
