using System.ComponentModel.DataAnnotations;

namespace ClaimsReserving.Models
{
    public class FileModel
    {
        [Required]
        public int Id;

        [StringLength(255)]
        public string Name;
    }
}
