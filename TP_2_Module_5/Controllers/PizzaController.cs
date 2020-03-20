using TP_2_Module_5.ViewsModels;
using TP_2_Module_5.Utils;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using TP_2_Module_5;

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
                if (ModelState.IsValid)
                {
                    Pizza newPizza = new Pizza
                    {
                        Nom = pizzaVM.Pizza.Nom,
                        Ingredients = FakeDb.IngredientsDisponibles.Where(i => pizzaVM.SelectedIngredients.Contains(i.Id)).ToList(),
                        Pate = FakeDb.PatesDisponibles.SingleOrDefault(p => p.Id == pizzaVM.SelectedPate)
                    };

                    Pizza checkPizza = FakeDb.Pizzas.FirstOrDefault(p => p.Nom == pizzaVM.Pizza.Nom);
                    int DoublePizza = FakeDb.Pizzas.Where(p => p.Ingredients.Count() == newPizza.Ingredients.Count()
                                                      && !p.Ingredients.Except(newPizza.Ingredients).Any()).ToList().Count();

                    if (DoublePizza > 2)
                    {
                        ModelState.AddModelError("", "Une pizza avec ces ingrédients existe déjà");
                    }
                    else if (checkPizza != null)
                    {
                        ModelState.AddModelError("", "Ce nom de pizza existe déjà");
                    }
                    else if (newPizza.Pate == null)
                    {
                        ModelState.AddModelError("", "Le paramètre Pate est obligatoire");
                    }
                    else if (pizzaVM.SelectedIngredients.Count() < 2)
                    {
                        ModelState.AddModelError("", "La pizza doit contenir au moins 2 ingrédients");
                    }
                    else if (newPizza.Ingredients.Count() > 5)
                    {
                        ModelState.AddModelError("", "La pizza doit contenir au maximum 5 ingrédients");
                    }
                    else
                    {
                        newPizza.Id = FakeDb.Pizzas.Count == 0 ? 1 : FakeDb.Pizzas.Max(p => p.Id) + 1;
                        FakeDb.Pizzas.Add(newPizza);
                        return RedirectToAction("index");
                    }
                    return View(pizzaVM);
                }
                else
                {
                    ModelState.AddModelError("", "Une erreur est survenue lors de la soumission du formulaire");
                    return View(pizzaVM);
                }
            }
            catch
            {
                return View(pizzaVM);
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
