using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models.Data;

[Table("TBL_ATTACHMENT")]
[Index("Entity", Name = "ATTACH_ENTITY_IX")]
[Index("TypeFile", Name = "ATTACH_TYPE_IX")]
public partial class TblAttachment
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("origFileName")]
    [StringLength(100)]
    [Unicode(false)]
    public string OrigFileName { get; set; } = null!;

    [Column("timeCreate", TypeName = "datetime")]
    public DateTime TimeCreate { get; set; }

    [Column("size")]
    public int Size { get; set; }

    [Column("typeFile")]
    [StringLength(50)]
    [Unicode(false)]
    public string TypeFile { get; set; } = null!;

    [Column("entity")]
    [StringLength(50)]
    [Unicode(false)]
    public string Entity { get; set; } = null!;

    [Column("entityId", TypeName = "decimal(18, 0)")]
    public decimal EntityId { get; set; }

    [Column("idInitiator", TypeName = "decimal(18, 0)")]
    public decimal? IdInitiator { get; set; }

    [ForeignKey("IdInitiator")]
    [InverseProperty("TblAttachments")]
    public virtual TblUser? IdInitiatorNavigation { get; set; }
}
