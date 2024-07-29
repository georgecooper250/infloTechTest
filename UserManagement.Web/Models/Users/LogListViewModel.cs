using System;

namespace UserManagement.Web.Models.Users;

public class LogListViewModel
{
    public List<LogListItemViewModel> Items { get; set; } = new();
}

public class LogListItemViewModel
{
    public long LogId { get; set; }
    public long UserId { get; set; } = default!;
    public string Info { get; set; } = default!;
    public DateTime TimeStamp { get; set; }
}