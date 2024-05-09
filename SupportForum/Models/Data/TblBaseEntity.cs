using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models.Data;

[Table("TBL_BASE_ENTITY")]
public partial class TblBaseEntity
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("nameAttribute")]
    [StringLength(100)]
    [Unicode(false)]
    public string NameAttribute { get; set; } = null!;

    [Column("dataType")]
    [StringLength(30)]
    [Unicode(false)]
    public string DataType { get; set; } = null!;

    [Column("isSearch")]
    public bool? IsSearch { get; set; }

    [Column("fieldValue")]
    [StringLength(100)]
    [Unicode(false)]
    public string? FieldValue { get; set; }

    [Column("fieldSort")]
    [StringLength(100)]
    [Unicode(false)]
    public string? FieldSort { get; set; }

    [Column("entityAlias")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EntityAlias { get; set; }

    [Column("entity")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Entity { get; set; }
}
