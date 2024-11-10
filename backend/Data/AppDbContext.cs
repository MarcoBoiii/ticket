﻿using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TicketRelationship> TicketRelationships { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TicketRelationship>()
            .HasKey(tr => new { tr.TicketId, tr.RelatedTicketId });

        modelBuilder.Entity<TicketRelationship>()
            .HasOne(tr => tr.Ticket)
            .WithMany(t => t.RelatedTickets)
            .HasForeignKey(tr => tr.TicketId);

        modelBuilder.Entity<TicketRelationship>()
            .HasOne(tr => tr.RelatedTicket)
            .WithMany()
            .HasForeignKey(tr => tr.RelatedTicketId);
    }

}