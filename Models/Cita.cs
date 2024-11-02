using System;
using System.Collections.Generic;

namespace CRMBASEDEDATOS.Models;

public partial class Cita
{
    public int IdCita { get; set; }

    public int IdCliente { get; set; }

    public int IdTratamiento { get; set; }

    public int? IdPromocion { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Precio { get; set; }

    public string? Estatus { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Promocione? IdPromocionNavigation { get; set; }

    public virtual Tratamiento IdTratamientoNavigation { get; set; } = null!;
}
