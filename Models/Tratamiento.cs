using System;
using System.Collections.Generic;

namespace CRMBASEDEDATOS.Models;

public partial class Tratamiento
{
    public int IdTratamiento { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? DuracionEstimada { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Promocione> Promociones { get; set; } = new List<Promocione>();
}
