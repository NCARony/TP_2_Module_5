using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using BO;

namespace TP2_Module_5.Utils
{
    public class FakeDb
    {
        private static int index = 1;

        public static List<Pizza> Pizzas { get; set; } = new List<Pizza>();

        public static Pizza AddPizza(Pizza pizza)
        {
            pizza.Id = index;
            index++;
            Pizzas.Add(pizza);
            return pizza;
        }

        public static List<Pate> PatesDisponibles => Pizza.PatesDisponibles;

        public static List<Ingredient> IngredientsDisponibles => Pizza.IngredientsDisponibles;
    }
}