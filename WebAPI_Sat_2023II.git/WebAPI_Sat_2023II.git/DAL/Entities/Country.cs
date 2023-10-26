using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebAPI_Sat_2023II.git.DAL.Entities
{
    public class Country : AuditBase
    {
        [Display(Name = "País")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }
    }
}
