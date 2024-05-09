using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models;

[Table("TBL_REACTION")]
public partial class TblReaction
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("isLike")]
    public bool? IsLike { get; set; }

    [Column("isDislike")]
    public bool? IsDislike { get; set; }

    [Column("idMsg", TypeName = "decimal(18, 0)")]
    public decimal? IdMsg { get; set; }

    [ForeignKey("IdMsg")]
    [InverseProperty("TblReactions")]
    public virtual TblCommunication? IdMsgNavigation { get; set; }
}
