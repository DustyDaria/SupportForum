using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models;

[Table("TBL_INSTRUCTION")]
[Index("Title", Name = "INSTR_TITLE_IX")]
public partial class TblInstruction
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("title")]
    [StringLength(128)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("instruction")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Instruction { get; set; } = null!;

    [Column("isShort")]
    public bool? IsShort { get; set; }

    [Column("entity")]
    [StringLength(50)]
    [Unicode(false)]
    public string Entity { get; set; } = null!;
}
