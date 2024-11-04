using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMBASEDEDATOS.Models;

public partial class InscripcionCurso
{
    public int IdInscripcion { get; set; }

    [Required(ErrorMessage = "El cliente es obligatorio.")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El curso es obligatorio.")]
    public int IdCurso { get; set; }

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    [DataType(DataType.Date)]
    public DateOnly FechaInicio { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? FechaFin { get; set; }

    [Range(1, 10, ErrorMessage = "La duración debe ser entre 1 y 10 horas.")]
    public int? Duracion { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio total debe ser mayor que cero.")]
    public decimal PrecioTotal { get; set; }

    [Required(ErrorMessage = "El estatus es obligatorio.")]
    public string? Estatus { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Curso IdCursoNavigation { get; set; } = null!;
}
