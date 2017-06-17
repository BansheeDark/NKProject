namespace Shell.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Faculty")]
    public partial class Faculty
    {
        [Key]
        public int Code { get; set; }

        [StringLength(50)]
        public string University { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
