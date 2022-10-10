using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_crud_mvc.Models;

public class Pizza
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Il campo è obbligatorio")]
    public string Name { get; set; }
    [Column(TypeName = "text")]
    [Required(ErrorMessage = "Il campo è obbligatorio")]
    [RegularExpression(@"(\w+\W){4,}\w+\W*", ErrorMessage = "Inserisci almeno 5 parole")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Il campo è obbligatorio")]
    public string Image { get; set; }
    [Required(ErrorMessage = "Il campo è obbligatorio")]
    [Column(TypeName = "decimal(8,2)")]
    [Range(1,100, ErrorMessage = "Il prezzo deve essere compreso tra 1 e 100")]
    public decimal Price { get; set; }
    
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public List<Ingredient>? Ingredients { get; set; }
    
    public Pizza()
    {
        
    }
}