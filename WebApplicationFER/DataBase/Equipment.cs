using System;
using System.Collections.Generic;

namespace WebApplicationFER.DataBase;

public partial class Equipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? TypeId { get; set; }

    public string Code { get; set; } = null!;

    public string? Location { get; set; }

    public virtual EquipmentType? Type { get; set; }
}
