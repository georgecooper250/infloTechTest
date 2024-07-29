using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models;

public class Log
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long LogId { get; set; }
    public long UserId { get; set; } = default!;
    public string Info { get; set; } = default!;
    public DateTime TimeStamp { get; set; }

    public Log(long userId, string info, DateTime timeStamp) {
        UserId = userId;
        Info = info;
        TimeStamp = timeStamp;
    }
}