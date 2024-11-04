using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMBASEDEDATOS.Models;

public partial class Tratamiento
{
    public int IdTratamiento { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "La descripción es obligatorio.")]
    [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
    public string Descripcion { get; set; } = null!;

    [Required(ErrorMessage = "La duración estimada es obligatorio.")]
    [Range(1, 15, ErrorMessage = "La duración estimada debe ser un número positivo y no exceder las 15 horas.")]
    public int DuracionEstimada { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Promocione> Promociones { get; set; } = new List<Promocione>();
}
