using la_mia_pizzeria_crud_mvc.Context;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud_mvc.Controllers;

[Authorize]
public class PizzaController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        List<Pizza> pizzas = new();
        using (ApplicationDbContext db = new ApplicationDbContext())
        {
            pizzas = db.Pizzas.Include("Category").Include("Ingredients").ToList();
        }
        return View(pizzas);
    }

    [HttpGet]
    public IActionResult ShowById(int id)
    {
        Pizza? pizza = new();
        using (var db = new ApplicationDbContext())
        {
            pizza = db.Pizzas.Include("Category").FirstOrDefault(x => x.Id == id);
        }

        return View(pizza);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        PizzaCategories join = new PizzaCategories();

        join.Categories = new ApplicationDbContext().Categories.ToList();
        join.Ingredients = new ApplicationDbContext().Ingredients.ToList();

        return View(join);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PizzaCategories formData)
    {
        if (!ModelState.IsValid)
        {
            formData.Categories = new ApplicationDbContext().Categories.ToList();
            formData.Ingredients = new ApplicationDbContext().Ingredients.ToList();
            return View("Create",formData);
        }

        
        using (var db = new ApplicationDbContext())
        {
            formData.Pizza.Ingredients = db.Ingredients.Where(x => formData.SelectedIngredients.Contains(x.Id)).ToList();
            db.Pizzas.Add(formData.Pizza);
            
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        using (var db = new ApplicationDbContext())
        {
            Pizza? pizza = db.Pizzas.Include("Category").Include("Ingredients").FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            PizzaCategories join = new PizzaCategories();
            join.Pizza = pizza;
            join.Categories = db.Categories.ToList();
            join.Ingredients = db.Ingredients.ToList();

            return View(join);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, PizzaCategories formData)
    {
        if (!ModelState.IsValid)
        {
            formData.Ingredients = new ApplicationDbContext().Ingredients.ToList();
            formData.Categories = new ApplicationDbContext().Categories.ToList();
            return View("Edit",formData);
        }

        using (var db = new ApplicationDbContext())
        {
            Pizza? pizza = db.Pizzas.Where(x=>x.Id == id).Include("Ingredients").FirstOrDefault();
            pizza.Ingredients = db.Ingredients.Where(x => formData.SelectedIngredients.Contains(x.Id)).ToList();
            db.Update(pizza);
            
            db.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        using (var db = new ApplicationDbContext())
        {
            Pizza? pizza = db.Pizzas.FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            db.Pizzas.Remove(pizza);
            db.SaveChanges();
        }

        return RedirectToAction("Index");
    }
    
}