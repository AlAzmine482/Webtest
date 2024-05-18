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
    [Column("manufacturer_id")]
    public int ManufacturerId { get; set; }

   
    [StringLength(50)]
    [Unicode(false)]
    [Column("manufacturer_name")]
    public string ManufacturerName { get; set; } = null!;

    
    [StringLength(50)]
    [Unicode(false)]
    [Column("manufacturer_country")]
    public string ManufacturerCountry { get; set; } = null!;

    [InverseProperty("Manufacturer")]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
