using System;
using System.Collections.Generic;

namespace CRMBASEDEDATOS.Models;

public partial class InscripcionCurso
{
    public int IdInscripcion { get; set; }

    public int IdCliente { get; set; }

    public int IdCurso { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public int? Duracion { get; set; }

    public decimal PrecioTotal { get; set; }

    public string? Estatus { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Curso IdCursoNavigation { get; set; } = null!;
}
