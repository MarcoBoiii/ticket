using System.ComponentModel.DataAnnotations;

namespace Models;
public class Ticket
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public Guid EmployeeId {  get; set; }
    public Guid RelatedTicketId { get; set; }
    public DateTime CreatedDate { get; set; }

    [Required]
    public string Reason { get; set; } = string.Empty;
    public Status Status { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
}
