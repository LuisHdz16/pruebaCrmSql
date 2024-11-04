using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMBASEDEDATOS.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "Los apellidos son obligatorios.")]
    [StringLength(50, ErrorMessage = "Los apellidos no deben exceder los 50 caracteres.")]
    public string Apellidos { get; set; } = null!;
    [Required(ErrorMessage = "El teléfono es obligatorio.")]

    [RegularExpression(@"^\d{2}-\d{4}-\d{4}$", ErrorMessage = "El formato del teléfono debe ser 81-23685-8822.")]
    public string Telefono { get; set; } = null!;

    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    [StringLength(100, ErrorMessage = "El correo electrónico no debe exceder los 100 caracteres.")]
    public string Correo { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<InscripcionCurso> InscripcionCursos { get; set; } = new List<InscripcionCurso>();
}
