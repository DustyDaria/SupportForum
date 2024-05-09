using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models.Data;

[Table("TBL_CATEGORY")]
[Index("Title", Name = "CAT_TITLE_IX")]
public partial class TblCategory
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("title")]
    [StringLength(128)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("descriptions")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Descriptions { get; set; }

    [Column("isModeration")]
    public bool? IsModeration { get; set; }

    [Column("timeCreate", TypeName = "datetime")]
    public DateTime TimeCreate { get; set; }

    [Column("idInitiator", TypeName = "decimal(18, 0)")]
    public decimal? IdInitiator { get; set; }

    [ForeignKey("IdInitiator")]
    [InverseProperty("TblCategories")]
    public virtual TblUser? IdInitiatorNavigation { get; set; }

    [InverseProperty("IdCategoryNavigation")]
    public virtual ICollection<TblForum> TblForums { get; set; } = new List<TblForum>();
}
