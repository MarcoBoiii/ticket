﻿using Data.Repositories;
using Models;

namespace Services;
public class TicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository) =>
        _ticketRepository = ticketRepository;

    public async Task CreateTicketAsync(int customerId, string reason)
    {
        var ticket = new Ticket
        {
            Id = 1,
            CustomerId = customerId,
            Reason = reason,
            Status = Status.None,
            CreatedDate = DateTime.Now
        };

        await _ticketRepository.AddAsync(ticket);
    }

    public async Task<Ticket> GetTicketByIdAsync(string id)
    {
        return await _ticketRepository.GetByIdAsync(id);
    }
}
