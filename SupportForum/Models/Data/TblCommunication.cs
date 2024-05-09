using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models.Data;

[Table("TBL_COMMUNICATION")]
public partial class TblCommunication
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("timeCreate", TypeName = "datetime")]
    public DateTime TimeCreate { get; set; }

    [Column("textMsg")]
    [StringLength(1000)]
    [Unicode(false)]
    public string TextMsg { get; set; } = null!;

    [Column("isEdit")]
    public bool? IsEdit { get; set; }

    [Column("isAnswer")]
    public bool? IsAnswer { get; set; }

    [Column("estimate", TypeName = "decimal(1, 0)")]
    public decimal? Estimate { get; set; }

    [Column("cmnPath")]
    [StringLength(3000)]
    [Unicode(false)]
    public string? CmnPath { get; set; }

    [Column("idParent", TypeName = "decimal(18, 0)")]
    public decimal? IdParent { get; set; }

    [Column("idInitiator", TypeName = "decimal(18, 0)")]
    public decimal? IdInitiator { get; set; }

    [Column("idTopic", TypeName = "decimal(18, 0)")]
    public decimal? IdTopic { get; set; }

    [ForeignKey("IdInitiator")]
    [InverseProperty("TblCommunications")]
    public virtual TblUser? IdInitiatorNavigation { get; set; }

    [ForeignKey("IdParent")]
    [InverseProperty("InverseIdParentNavigation")]
    public virtual TblCommunication? IdParentNavigation { get; set; }

    [ForeignKey("IdTopic")]
    [InverseProperty("TblCommunications")]
    public virtual TblTopic? IdTopicNavigation { get; set; }

    [InverseProperty("IdParentNavigation")]
    public virtual ICollection<TblCommunication> InverseIdParentNavigation { get; set; } = new List<TblCommunication>();

    [InverseProperty("IdMsgNavigation")]
    public virtual ICollection<TblReaction> TblReactions { get; set; } = new List<TblReaction>();
}
