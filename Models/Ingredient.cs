using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.Models;

public class Ingredient
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Pizza>? Pizzas { get; set; }

    public Ingredient()
    {
        Pizzas = new List<Pizza>();
    }
}