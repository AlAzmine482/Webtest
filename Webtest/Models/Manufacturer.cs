using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Webtest.Models;

[Table("manufacturer")]
public partial class Manufacturer
{
    [Key]
    [Column("ManufacturerId")]
    public int ManufacturerId { get; set; }

   
    [StringLength(50)]
    [Unicode(false)]
    [Column("ManufacturerName")]
    public string ManufacturerName { get; set; } = null!;

    
    [StringLength(50)]
    [Unicode(false)]
    [Column("ManufacturerCountry")]
    public string ManufacturerCountry { get; set; } = null!;

    [InverseProperty("Manufacturer")]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
