using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMBASEDEDATOS.Models;

public partial class Cita
{
    public int IdCita { get; set; }

    [Required(ErrorMessage = "El campo cliente es obligatorio.")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El campo tratamiento es obligatorio.")]
    public int IdTratamiento { get; set; }

    public int? IdPromocion { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    [DataType(DataType.Currency)]
    public decimal Precio { get; set; }

    [StringLength(50, ErrorMessage = "El estatus no puede tener más de 50 caracteres.")]
    public string Estatus { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Promocione? IdPromocionNavigation { get; set; }

    public virtual Tratamiento IdTratamientoNavigation { get; set; } = null!;
}
