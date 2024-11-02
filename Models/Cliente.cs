using System;
using System.Collections.Generic;

namespace CRMBASEDEDATOS.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<InscripcionCurso> InscripcionCursos { get; set; } = new List<InscripcionCurso>();
}
