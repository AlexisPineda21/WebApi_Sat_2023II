using System.ComponentModel.DataAnnotations;

namespace WebAPI_Sat_2023II.git.DAL.Entities
{
    public class AuditBase
    {
        [Key] //DataAnnotation me sirve para definir que está propiedad ID es un PK
        [Required] //No permite nullos
        public virtual Guid Id { get; set; } //Será la PK de todas las tablas de mi BD
        public virtual DateTime? CreatedDate { get; set; } //campos nulleables, notación elvis (?)
        public virtual DateTime? ModifiedDate { get; set; }
    }
}
