using System;
using System.Collections.Generic;

namespace WebApplicationFER.DataBase;

public partial class EquipmentType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
