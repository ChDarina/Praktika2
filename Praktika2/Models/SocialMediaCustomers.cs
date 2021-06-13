namespace Praktika2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SocialMediaCustomers
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SocialMediaID { get; set; }

        [Required]
        [StringLength(255)]
        public string SocialMediaReference { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual SocialMedias SocialMedias { get; set; }
    }
}
