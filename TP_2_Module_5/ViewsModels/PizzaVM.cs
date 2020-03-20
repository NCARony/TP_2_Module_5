using TP_2_Module_5.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace TP_2_Module_5.ViewsModels
{
    public class PizzaVM
    {
        public Pizza Pizza { get; set; }
        public int SelectedPate { get; set; }
        public List<int> SelectedIngredients { get; set; } = new List<int>();
        public List<SelectListItem> PatesListItems { get; set; } = FakeDb.PatesDisponibles.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() }).ToList();
        public List<SelectListItem> IngredientsListItems { get; set; } = FakeDb.IngredientsDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();

    }
}