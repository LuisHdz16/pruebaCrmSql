using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMBASEDEDATOS.Models;

public partial class Promocione
{

    public int IdPromocion { get; set; }
    [Required(ErrorMessage = "El tratamiento es obligatorio.")]
    public int IdTratamiento { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "\"La descripción es obligatorio.")]
    [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
    public string? Descripcion { get; set; }

    public string Estatus { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Tratamiento IdTratamientoNavigation { get; set; } = null!;
}
