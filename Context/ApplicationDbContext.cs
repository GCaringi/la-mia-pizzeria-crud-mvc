using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud_mvc.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Pizza> Pizzas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizzeria;Integrated Security=True");
    }
}