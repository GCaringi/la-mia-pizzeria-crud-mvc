namespace la_mia_pizzeria_crud_mvc.Models;

public class PizzaCategories
{
    public Pizza Pizza { get; set; }
    public List<Category> Categories { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<Ingredient> SelectedIngredients { get; set; }
    
    public PizzaCategories()
    {
        Pizza = new Pizza();
        Categories = new List<Category>();
    }
}