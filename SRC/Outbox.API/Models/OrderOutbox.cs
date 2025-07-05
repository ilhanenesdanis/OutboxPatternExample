using System;

namespace Outbox.API.Models;

public sealed class OrderOutbox
{
    public OrderOutbox()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsComplated { get; set; }
    public bool IsFailed { get; set; }
    public string? FailMessage { get; set; }
    public DateTime ComplatedDate { get; set; }
    public DateTime LastAttempt{ get; set; }
}
