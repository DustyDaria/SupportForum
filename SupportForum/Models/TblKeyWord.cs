using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models;

[Table("TBL_KEY_WORD")]
[Index("KeyWord", Name = "KW_NAME_IX")]
public partial class TblKeyWord
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("keyWord")]
    [StringLength(100)]
    [Unicode(false)]
    public string KeyWord { get; set; } = null!;

    [ForeignKey("IdKeyWord")]
    [InverseProperty("IdKeyWords")]
    public virtual ICollection<TblTopic> IdTopics { get; set; } = new List<TblTopic>();
}
