using System;

namespace Outbox.API.Dto;

public record CreateOrderDto(
    string TrackingNumber,
    string ProductName,
    int Quantity,
    decimal TotalPrice,
    string CustomerEmail);



