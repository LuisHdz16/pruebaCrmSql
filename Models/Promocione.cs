using System;
using System.Collections.Generic;

namespace CRMBASEDEDATOS.Models;

public partial class Promocione
{
    public int IdPromocion { get; set; }

    public int IdTratamiento { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Estatus { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Tratamiento IdTratamientoNavigation { get; set; } = null!;
}
