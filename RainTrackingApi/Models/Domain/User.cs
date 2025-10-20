using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RainTrackingApi.Models.Domain
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(150)]
        public string UserIdentifier { get; set; } // From x-userId header

        [StringLength(150)]
        public string? FirstName { get; set; }

        [StringLength(150)]
        public string? LastName { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
