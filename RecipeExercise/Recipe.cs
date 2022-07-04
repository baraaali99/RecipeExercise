using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeExercise
{
    internal class Recipe
    {
        public string Title { get; set; }
        public List<string> Ingredients = new List<string>();
        public List<string> Instructions = new List<string>();
        public List <string> Categories = new List<string>();
        public Guid Id { get; set; }
        public Recipe(string title, List<string> ingredients, List<string> instructions, List<string> categories , Guid id)
        {
            Title = title;
            Ingredients = ingredients;
            Instructions = instructions;
            Id = id;
            Categories = categories;
        }

        public Recipe()
        {
            Title = "";
            Instructions = new List<string>();
            Categories = new List<string>();
            Ingredients = new List<string>();

        }

    }
}
