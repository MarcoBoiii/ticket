﻿using Models.Interfaces;

namespace API.Handler;

public class TicketHandler
{
    private readonly ITicketRepository _ticketRepository;
    public TicketHandler(ITicketRepository ticketRepository) =>
         _ticketRepository = ticketRepository;


}
