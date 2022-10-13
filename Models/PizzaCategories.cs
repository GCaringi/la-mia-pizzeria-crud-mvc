namespace la_mia_pizzeria_crud_mvc.Models;

public class PizzaCategories
{
    public Pizza Pizza { get; set; }
    public List<Category> Categories { get; set; }
    //Meglio IEnumerable perchè non è necessario che sia una lista
    public List<Ingredient> Ingredients { get; set; }
    public List<int> SelectedIngredients { get; set; }
    
    public PizzaCategories()
    {
        Pizza = new Pizza();
        Categories = new List<Category>();
        Ingredients = new List<Ingredient>();
        SelectedIngredients = new List<int>();
    }
}