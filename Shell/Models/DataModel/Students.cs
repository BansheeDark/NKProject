namespace Shell.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Students
    {
        [Key]
        public int CodeStudents { get; set; }

        [StringLength(10)]
        public string CodeGroups { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string OtherName { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirthday { get; set; }

        public string PlaceofBirth { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? StudentsRecordBook { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? Age { get; set; }
    }
}
