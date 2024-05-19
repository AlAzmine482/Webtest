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
    public int manufacturer_id { get; set; }

   
    [StringLength(50)]
    [Unicode(false)]
    [Column("manufacturer_name")]
    public string manufacturer_name { get; set; } = null!;

    
    [StringLength(50)]
    [Unicode(false)]
    [Column("manufacturer_country")]
    public string manufacturer_country { get; set; } = null!;

    [InverseProperty("Manufacturer")]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
