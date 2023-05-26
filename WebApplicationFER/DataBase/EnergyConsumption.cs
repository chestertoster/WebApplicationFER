using System;
using System.Collections.Generic;

namespace WebApplicationFER.DataBase;

public partial class EnergyConsumption
{
    public string? EquipmentCode { get; set; }

    public string? EquipmentName { get; set; }

    public int? EnergyTypeId { get; set; }

    public int? ActivityDirectionId { get; set; }

    public DateOnly? ReadingDate { get; set; }

    public int? Volume { get; set; }

    public virtual ActivityDirection? ActivityDirection { get; set; }

    public virtual EnergyType? EnergyType { get; set; }

    public virtual Equipment? Equipment { get; set; }

    public virtual Equipment? EquipmentCodeNavigation { get; set; }
}
