using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Data;

[Table("TBL_TOPIC")]
[Index("Title", Name = "TOPIC_TITLE_IX")]
public partial class TblTopic
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("title")]
    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("question")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Question { get; set; }

    [Column("isAnonymous")]
    public bool? IsAnonymous { get; set; }

    [Column("idForum", TypeName = "decimal(18, 0)")]
    public decimal IdForum { get; set; }

    [Column("idInitiator", TypeName = "decimal(18, 0)")]
    public decimal IdInitiator { get; set; }

    [ForeignKey("IdForum")]
    [InverseProperty("TblTopics")]
    public virtual TblForum IdForumNavigation { get; set; } = null!;

    [ForeignKey("IdInitiator")]
    [InverseProperty("TblTopics")]
    public virtual TblUser IdInitiatorNavigation { get; set; } = null!;

    [InverseProperty("IdTopicNavigation")]
    public virtual ICollection<TblCommunication> TblCommunications { get; set; } = new List<TblCommunication>();

    [ForeignKey("IdTopic")]
    [InverseProperty("IdTopics")]
    public virtual ICollection<TblKeyWord> IdKeyWords { get; set; } = new List<TblKeyWord>();
}
