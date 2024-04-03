using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models;

[Table("TBL_FORUM")]
[Index("Title", Name = "FORUM_TITLE_IX")]
public partial class TblForum
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
    public string? Descriptions { get; set; }

    [Column("timeCreate", TypeName = "datetime")]
    public DateTime TimeCreate { get; set; }

    [Column("idParent", TypeName = "decimal(18, 0)")]
    public decimal? IdParent { get; set; }

    [Column("idInitiator", TypeName = "decimal(18, 0)")]
    public decimal IdInitiator { get; set; }

    [Column("idCategory", TypeName = "decimal(18, 0)")]
    public decimal? IdCategory { get; set; }

    [ForeignKey("IdCategory")]
    [InverseProperty("TblForums")]
    public virtual TblCategory? IdCategoryNavigation { get; set; }

    [ForeignKey("IdInitiator")]
    [InverseProperty("TblForums")]
    public virtual TblUser? IdInitiatorNavigation { get; set; } = null!;

    [ForeignKey("IdParent")]
    [InverseProperty("InverseIdParentNavigation")]
    public virtual TblForum? IdParentNavigation { get; set; }

    [InverseProperty("IdParentNavigation")]
    public virtual ICollection<TblForum> InverseIdParentNavigation { get; set; } = new List<TblForum>();

    [InverseProperty("IdForumNavigation")]
    public virtual ICollection<TblTopic> TblTopics { get; set; } = new List<TblTopic>();
}
