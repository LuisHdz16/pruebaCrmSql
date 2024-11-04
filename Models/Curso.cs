using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMBASEDEDATOS.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "La descripción es obligatorio.")]
    [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
    public string Descripcion { get; set; } = null!;

    public string? Estatus { get; set; }

    public virtual ICollection<InscripcionCurso> InscripcionCursos { get; set; } = new List<InscripcionCurso>();
}
