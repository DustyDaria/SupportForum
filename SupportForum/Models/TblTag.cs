using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models;

[Table("TBL_TAG")]
[Index("Tag", Name = "TT_NAME_IX")]
public partial class TblTag
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("tag")]
    [StringLength(100)]
    [Unicode(false)]
    public string Tag { get; set; } = null!;

    [ForeignKey("IdTag")]
    [InverseProperty("IdTags")]
    public virtual ICollection<TblTopic> IdTopics { get; set; } = new List<TblTopic>();
}
