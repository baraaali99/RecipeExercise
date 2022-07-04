using System;
using Spectre.Console;

class Recipe
{
    public string Title { get; set; }
    public string Instructions { get; set; }
    public List<string> Categories { get; set; }
    public string Ingredients { get; set; }
    public Guid Id { get; set; }

    public Recipe(string title, string instructions, List<string> categories, string ingredients)
    {
        Title = title;
        Instructions = instructions;
        Categories = categories;
        Ingredients = ingredients;
    }
    public Recipe()
    {
        Title = "";
        Instructions = "";
        Categories = new List<string>();
        Ingredients = "";
    }
}
class Operations
{
    public List<Recipe> Recipes { get; set; }

    public List<Recipe> ListRecipes() { return Recipes; }
    public void AddRecipe(Recipe NewRecipe)
    {
        Recipes.Add(NewRecipe);


    }
    public Recipe getRecipe(Guid id)
    {
        var recipe = Recipes.Find(rec => rec.Id == id);
        if (recipe == null)
        {
            Console.WriteLine("Wrong Id input");
            Environment.Exit(0);
        }
        return recipe;
    }
    public void EditInstruction(Guid id, string Newinstruction)
    {
        var recipe = getRecipe(id);
        recipe.Instructions = Newinstruction;

    }
    public void EditTitle(Guid id , string NewTitle)
    {
        var recipe = getRecipe(id);
        recipe.Title = NewTitle;
    }

    public void EditIngredient(Guid id , string NewIngredient)
    {
        var recipe = getRecipe(id);
        recipe.Ingredients = NewIngredient;
    }
    public void RemoveCategory(Guid id, string category)
    {
        var recipe = getRecipe(id);
        recipe.Categories.RemoveAll(c => c == category);
    }
    public void EditCategory(Guid id, string category, string newCategory)
    {
        RemoveCategory(id, category);
        AddCategory(id, newCategory);
    }
    public void AddCategory(Guid id, string newCategory)
    {
        var recipe = getRecipe(id);
        recipe.Categories.Add(newCategory);
    }
    public void EditRecipe()
    {
        Console.WriteLine("Enter the Id of the Recipe you want to edit");
        Guid Id = Convert.ChangeType(Console.ReadLine(), Guid)
;        Console.WriteLine("What you would like to change ?");
        int Choice = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("1- Title");
        Console.WriteLine("2- Recipe Ingredient");
        Console.WriteLine("3- Recipe Instruction");
        Console.WriteLine("4- Recipe Categories");
        if (Choice == 1) {
            Console.WriteLine("Enter the new Title");
            string NewTitle = Console.ReadLine();
            EditTitle(Id, NewTitle);
        } 
    }
}
