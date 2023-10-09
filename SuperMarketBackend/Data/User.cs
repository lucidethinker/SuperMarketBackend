using System;
using System.Collections.Generic;

namespace SuperMarketBackend.Data;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public bool IsDeleted { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public int? UserType { get; set; }

    public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
}
