using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP2_Module_5.Utils;

namespace TP_2_Module_5.Models
{
    public class PizzaVM
    {
        public Pizza Pizza { get; set; }
        public List<int> SelectedIngredients { get; set; } = new List<int>();
        public int SelectedPate { get; set; }
        public List<SelectListItem> PatesListItems { get; set; } = FakeDb.PatesDisponibles.Select(p => new SelectListItem { Text = p.Nom, Value = p.Id.ToString() }).ToList();
        public List<SelectListItem> IngredientsListItems { get; set; } = FakeDb.IngredientsDisponibles.Select(i => new SelectListItem { Text = i.Nom, Value = i.Id.ToString() }).ToList();

    }
}