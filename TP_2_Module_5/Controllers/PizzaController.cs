using TP2_Module_5.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BO;
using TP_2_Module_5.Models;

namespace TP2_Module_5.Controllers
{
    public class PizzaController : Controller
    {

        // GET: Pizza
        public ActionResult Index()
        {
            List<Pizza> pizzas = FakeDb.Pizzas;
            return View(pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
    {
        Pizza pizza = FakeDb.Pizzas.SingleOrDefault(p => p.Id == id);
        if (pizza == null)
        {
            return RedirectToAction("Index");
        }
        return View(pizza);
    }

    // GET: Pizza/Create
    public ActionResult Create()
    {
        var pizzaVM = new PizzaVM();
        return View(pizzaVM);
    }

    // POST: Pizza/Create
    [HttpPost]
    public ActionResult Create(PizzaVM pizzaVM)
    {
        try
        {
            Pizza newPizza = new Pizza
            {
                Nom = pizzaVM.Pizza.Nom,
                Ingredients = FakeDb.IngredientsDisponibles.Where(i => pizzaVM.SelectedIngredients.Contains(i.Id)).ToList(),
                Pate = FakeDb.PatesDisponibles.SingleOrDefault(p => p.Id == pizzaVM.SelectedPate)
            };
            Pizza addedPizza = FakeDb.AddPizza(newPizza);
            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }

    // GET: Pizza/Edit/5
    public ActionResult Edit(int id)
    {
        Pizza selectedPizza = FakeDb.Pizzas.SingleOrDefault(p => p.Id == id);
        if (selectedPizza == null)
        {
            return RedirectToAction("Index");
        }
        PizzaVM pizzaVM = new PizzaVM
        {
            Pizza = selectedPizza,
            SelectedIngredients = selectedPizza.Ingredients.Select(i => i.Id).ToList(),
            SelectedPate = selectedPizza.Pate.Id
        };
        return View(pizzaVM);
    }

    // POST: Pizza/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, PizzaVM pizzaVM)
    {
        try
        {
            Pizza pizza = FakeDb.Pizzas.SingleOrDefault(p => p.Id == id);
            if (pizza == null)
            {
                return RedirectToAction("Index");
            }

            pizza.Nom = pizzaVM.Pizza.Nom;
            pizza.Ingredients = FakeDb.IngredientsDisponibles.Where(i => pizzaVM.SelectedIngredients.Contains(i.Id)).ToList();
            pizza.Pate = FakeDb.PatesDisponibles.SingleOrDefault(p => p.Id == pizzaVM.SelectedPate);

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }

    // GET: Pizza/Delete/5
    public ActionResult Delete(int id)
    {
        Pizza pizza = FakeDb.Pizzas.SingleOrDefault(p => p.Id == id);
        if (pizza == null)
        {
            return RedirectToAction("Index");
        }
        return View(pizza);
    }

    // POST: Pizza/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection)
    {
        try
        {
            Pizza pizza = FakeDb.Pizzas.SingleOrDefault(p => p.Id == id);
            if (pizza == null)
            {
                return RedirectToAction("Index");
            }
            FakeDb.Pizzas.Remove(pizza);
            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }
}
}
