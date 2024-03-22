using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Data;

[Table("TBL_REACTION")]
public partial class TblReaction
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("rLike")]
    public int? RLike { get; set; }

    [Column("rDislike")]
    public int? RDislike { get; set; }

    [Column("idMsg", TypeName = "decimal(18, 0)")]
    public decimal IdMsg { get; set; }

    [ForeignKey("IdMsg")]
    [InverseProperty("TblReactions")]
    public virtual TblCommunication IdMsgNavigation { get; set; } = null!;
}
