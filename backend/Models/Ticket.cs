﻿using System.ComponentModel.DataAnnotations;

namespace Models;
public class Ticket
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int EmployeeId {  get; set; }
    public List<TicketRelationship> RelatedTickets { get; set; } = [];
    public DateTime? CreatedDate { get; set; }

    [Required]
    public string Reason { get; set; } = string.Empty;
    public Status Status { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
}
