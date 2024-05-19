using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Webtest.Models;

[Table("car")]
public partial class Car
{
    [Key]
    [Column("car_id")]
    public int car_id { get; set; }

    
    [StringLength(50)]
    [Unicode(false)]
    [Column("car_name")]
    public string car_name { get; set; } 

    [Column("car_drivetrain", TypeName = "text")]
    public string car_drivetrain { get; set; } 

    [Column("car_year")]
    public int car_year { get; set; }

    [Column("manufacturer_id")]
    public int manufacturer_id { get; set; }

    [ForeignKey("manufacturer_id")]
    [InverseProperty("Cars")]
    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
