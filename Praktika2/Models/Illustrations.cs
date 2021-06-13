namespace Praktika2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Illustrations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Illustrations()
        {
            Orders = new HashSet<Orders>();
            Technics = new HashSet<Technics>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IllustrationID { get; set; }

        public int IllustratorID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime DLDate { get; set; }

        public bool Privacy { get; set; }

        public bool Hiding { get; set; }

        public virtual Illustrators Illustrators { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Technics> Technics { get; set; }
    }
}
