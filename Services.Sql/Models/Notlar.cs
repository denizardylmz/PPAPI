using System;
using System.Collections.Generic;

namespace Services.Sql.Models;

public partial class Notlar
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public DateTime? CreateDate { get; set; }
}
