using la_mia_pizzeria_crud_mvc.Context;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud_mvc.Controllers;

public class PizzaController : Controller
{
    public IActionResult Index()
    {
        List<Pizza> pizzas = new();
        using (ApplicationDbContext db = new ApplicationDbContext())
        {
            pizzas = db.Pizzas.ToList();
        }
        return View(pizzas);
    }

    public IActionResult ShowById(int id)
    {
        Pizza? pizza = new();
        using (var db = new ApplicationDbContext())
        {
            pizza = db.Pizzas.FirstOrDefault(x => x.Id == id);
        }

        return View(pizza);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Pizza pizza)
    {
        if (!ModelState.IsValid)
        {
            return View("Create",pizza);
        }

        using (var db = new ApplicationDbContext())
        {
            db.Pizzas.Add(pizza);
            
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        using (var db = new ApplicationDbContext())
        {
            Pizza? pizza = db.Pizzas.FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Pizza pizza)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit",pizza);
        }

        using (var db = new ApplicationDbContext())
        {
            Pizza? savedPizza = db.Pizzas.FirstOrDefault(x => x.Id == id);
            savedPizza.Name = pizza.Name;
            savedPizza.Description = pizza.Description;
            savedPizza.Image = pizza.Image;
            savedPizza.Price = pizza.Price;

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