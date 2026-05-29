using System;
using Microsoft.EntityFrameworkCore;
using RealDougAPI.Models;

namespace RealDougAPI;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
}
