﻿namespace Models.Interfaces;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(Guid id);
    Task AddAsync(Ticket ticket);
    Task UpdateAsync(Ticket ticket);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task AssignAsync( Guid ticketId, Guid userId );
}
