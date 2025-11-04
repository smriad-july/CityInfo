using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(255) COLLATE SQL_Latin1_General_CP1_CS_AS")]
        public string userName { get; set; }
        [Required]
        [MaxLength(50)]
        public string password { get; set; }
        [Required]
        [MaxLength(50)]
        public string email { get; set; }

        [Required]
        [MaxLength(50)]
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string token { get; set; }


        public ICollection<City> cities { get; set; }
            = new List<City>();

        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
            = new List<PointOfInterest>();

    }
}
