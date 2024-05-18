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
    public int CarId { get; set; }

    
    [StringLength(50)]
    [Unicode(false)]
    [Column("car_name")]
    public string CarName { get; set; } 

    [Column("car_drivetrain", TypeName = "text")]
    public string CarDrivetrain { get; set; } 

    [Column("car_year")]
    public int CarYear { get; set; }

    [Column("manufacturer_id")]
    public int ManufacturerId { get; set; }

    [ForeignKey("ManufacturerId")]
    [InverseProperty("Cars")]
    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
