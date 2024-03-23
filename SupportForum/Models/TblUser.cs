using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportForum.Models;

[Table("TBL_USERS")]
public partial class TblUser
{
    [Key]
    [Column("id", TypeName = "decimal(18, 0)")]
    public decimal Id { get; set; }

    [Column("userLogin")]
    [StringLength(100)]
    [Unicode(false)]
    public string UserLogin { get; set; } = null!;

    [Column("fullName")]
    [StringLength(200)]
    [Unicode(false)]
    public string FullName { get; set; } = null!;

    [Column("currRole", TypeName = "decimal(1, 0)")]
    public decimal CurrRole { get; set; }

    [Column("dateLastActivity", TypeName = "datetime")]
    public DateTime? DateLastActivity { get; set; }

    [InverseProperty("IdInitiatorNavigation")]
    public virtual ICollection<TblCommunication> TblCommunications { get; set; } = new List<TblCommunication>();

    [InverseProperty("IdInitiatorNavigation")]
    public virtual ICollection<TblForum> TblForums { get; set; } = new List<TblForum>();

    [InverseProperty("IdInitiatorNavigation")]
    public virtual ICollection<TblTopic> TblTopics { get; set; } = new List<TblTopic>();
}
