using Models;
using Models.Interfaces;
using Services.Dto;

namespace Services;
public class TicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly AIService _aIService;

    public TicketService(ITicketRepository ticketRepository, AIService aIService)
    {
        _ticketRepository = ticketRepository;
        _aIService = aIService;
    }

    public async Task CreateTicketAsync(TicketDto dto)
    {
        var ticket = new Ticket
        {
            Id = new Guid(),
            CustomerId = dto.CustomerId,
            Reason = dto.Reason,
            Status = Status.Unassigned,
            Description = dto.Description,
            CreatedDate = DateTime.Now
        };

        await _ticketRepository.AddAsync(ticket);
    }

    public async Task CreateAttachedTicketAsync(TicketDto dto)
    {
        var ticket = new Ticket
        {
            Id = new Guid(),
            CustomerId = dto.CustomerId,
            EmployeeId = dto.EmployeeId,
            Reason = dto.Reason,
            Status = Status.Assigned,
            Description = dto.Description,
            CreatedDate = DateTime.Now
        };

        await _ticketRepository.AddAsync(ticket);

        await _ticketRepository.UpdateRelatedTicketIdAsync(dto.Id, ticket.Id);
    }

    public async Task<Ticket?> GetTicketByIdAsync(Guid id)
    {
        return await _ticketRepository.GetByIdAsync(id);
    }

    public async Task<Ticket?> UpdateDescriptionAsync(TicketDto dto)
    {
        return await _ticketRepository.UpdateDescriptionAsync(dto.Id, dto.Description);
    }

    public async Task<Ticket?> UpdateStatusAsync(TicketDto dto)
    {
        return await _ticketRepository.UpdateStatusAsync(dto.Id, dto.Status);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _ticketRepository.DeleteAsync(id);
    }

    public async Task RevokeTicket(Guid id)
    {
        await _ticketRepository.RevokeTicket(id);
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _ticketRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync(Guid id)
    {
        return await _ticketRepository.GetAllAsync(id);
    }

    public async Task<IEnumerable<Ticket>> GetRelatedTicketTree(Guid ticketId, Guid customerId)
    {
        return await _ticketRepository.GetRelatedTicketTree(ticketId, customerId);
    }
    public async Task<Ticket?> AssignAsync(TicketDto dto)
    {
        return await _ticketRepository.AssignAsync(dto.Id, dto.EmployeeId);
    }

    public async Task<string?> GenerateTicketResponse(string prompt)
    {
        return await _aIService.CallOpenAIAsync(prompt) ?? throw new Exception("API Response cant be null");
    }

    public async Task<IEnumerable<Ticket>> GetFilteredTickets(Status? status, DateTime? start, DateTime? end)
    {
        return await _ticketRepository.GetFilteredTickets(status, start, end);
    }
}

