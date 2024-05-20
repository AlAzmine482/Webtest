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
    [Column("CarId")]
    public int CarId { get; set; }

    
    [StringLength(50)]
    [Unicode(false)]
    [Column("CarName")]
    public string CarName { get; set; } 

    [Column("car_drivetrain", TypeName = "text")]
    public string cardrivetrain { get; set; } 

    [Column("CarYear")]
    public int CarYear { get; set; }

    [Column("ManufacturerId")]
    public int ManufacturerId { get; set; }

    [ForeignKey("ManufacturerId")]
    [InverseProperty("Cars")]
    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
