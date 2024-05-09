using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models.Data;

[Table("TBL_NEWS")]
public partial class TblNews
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("title")]
    [StringLength(128)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("descriptions")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Descriptions { get; set; } = null!;
}
