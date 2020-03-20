using TP_2_Module_5.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TP_2_Module_5.ViewsModels
{
    public class PizzaVM
    {
        public Pizza Pizza { get; set; }
        [Required]
        public int SelectedPate { get; set; }
        [Required]
        public List<int> SelectedIngredients { get; set; } = new List<int>();
        [Required]
        public List<SelectListItem> PatesListItems { get; set; } = FakeDb.PatesDisponibles.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() }).ToList();
        [Required]
        public List<SelectListItem> IngredientsListItems { get; set; } = FakeDb.IngredientsDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();

    }
}