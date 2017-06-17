namespace Shell.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Groups
    {
        [Key]
        public int CodeGroups { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public int? Courses { get; set; }

        [StringLength(10)]
        public string Semester { get; set; }

        [StringLength(50)]
        public string Faculty { get; set; }

        [StringLength(50)]
        public string Training { get; set; }
    }
}
