class Recipe
{
    public string Title { get; set; }
    public string Instructions { get; set; }
    public List<string> Categories { get; set; }
    public string Ingredients { get; set; }
    public int Id;

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
    public void EditRecipe()
    {
        Console.WriteLine("Enter the Id of the Recipe you want to edit");
        Console.WriteLine("What you would like to change ?");
        Console.WriteLine("1- Title");
        Console.WriteLine("2- Recipe Ingredient");
        Console.WriteLine("3- Recipe Instruction");
        Console.WriteLine("4- Recipe Categories");
    }
}
