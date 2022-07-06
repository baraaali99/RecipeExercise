using System;
using Spectre.Console;
using System.Text.Json;
using RecipeExercise;
using System.Text;

var recipesList = new List<Recipe>();
var categoriesList = new List<string>();
var _Path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
string _file = Path.Combine(_Path, "D:\\computer science\\Silverkey\\RecipeExercise\\RecipeExercise\\Recipes.json");
if (File.Exists(_file) == false)
{
	File.WriteAllText(_file, JsonSerializer.Serialize(recipesList));
}
else
{
	using (StreamReader r = new StreamReader(_file))
	{
		var Data = r.ReadToEnd();
		var Json = JsonSerializer.Deserialize<List<Recipe>>(Data);
		if (Json != null)
		{
			recipesList = Json;
		}
	}
}
while (true)
{
	var command = AnsiConsole.Prompt(
	   new SelectionPrompt<string>()
		   .Title("What would you like to do?")
		   .AddChoices(new[]
		   {
			   "List all Recipes",
		   })
           .AddChoiceGroup("Recipes", new[]
		   {
			   "Add a Recipe",
			   "Delete a Recipe",
			   "Edit a Recipe"
		   })
		   .AddChoiceGroup("Categories", new[]
		   {
			   "Add a Category",
			   "Delete a Category",
			   "Edit a Category"
		   })
		   .AddChoices(new[]
		   {
			   "Save & Exit"
		   }));
	AnsiConsole.Clear();
	switch (command)
	{
		case "List all Recipes":
			ListRecipes();
			break;
		case "Add a Recipe":
			AddRecipe();
			break;
		case "Edit a Recipe":
			EditRecipe();
			break;
		case "Add a Category":
			AddCategory();
			break;
		case "Edit a Category":
			EditCategory();
			break;
		default:
			Save();
			return;
	}
}

void AddRecipe()
{
	Recipe r = new Recipe();
	var title = AnsiConsole.Ask<string>("What is the [green]recipe[/] called?");
	var ingredients = new List<string>();
	var instructions = new List<string>();
	var categories = new List<string>();

	AnsiConsole.MarkupLine("Enter all the [green]ingredients[/]. [red] after you're done of writing instructions press space to move to next step [/]");
	var ingredient = AnsiConsole.Ask<string>("Enter ingredient: ");
	while (ingredient != "")
	{
		ingredients.Add(ingredient);
		ingredient = AnsiConsole.Prompt(new TextPrompt<string>("Enter recipe ingredients: ").AllowEmpty());
	};

	AnsiConsole.MarkupLine("Enter all the [green]instructions[/]. [red] after you're done of writing instructions press space to move to next step [/]");
	var instruction = AnsiConsole.Ask<string>("[green]Enter recipe instructions please:[/] ");
	while (instruction != "")
	{
		instructions.Add(instruction);
		instruction = AnsiConsole.Prompt(new TextPrompt<string>("[green]Enter recipe instructions please:[/] ").AllowEmpty());
		
	};

	var recipe = new Recipe()
	{
		Title = title,
		Ingredients = ingredients,
		Instructions = instructions,
		Categories = categories
	};

	if (categoriesList.Count == 0)
	{
		AnsiConsole.MarkupLine("[red]please add Categories your recipes can belong to[/]");
		recipe.Categories.Add("[red]not assigned to specific Category yet[/]");
	}
	else
	{
		var selectedcategories = AnsiConsole.Prompt(
		new MultiSelectionPrompt<String>()
		.PageSize(10)
		.Title("[green] Which [white]categories[/] does this recipe belong to?[/]")
		.MoreChoicesText("[grey](Move up and down to reveal more categories)[/]")
		.InstructionsText("[grey](Press [blue]Space[/] to toggle a category, [green]Enter[/] to accept)[/]")
		.AddChoices(categoriesList));

		recipe.Categories = selectedcategories;
	}
	recipesList.Add(recipe);
}

void RemoveRecipe()
{
	if (recipesList.Count == 0)
	{
		AnsiConsole.MarkupLine("There are no Recipes");
		return;
	}
	var selectedRecipes = AnsiConsole.Prompt(
	new MultiSelectionPrompt<Recipe>()
	.PageSize(10)
	.Title("Which [green]recipes[/] does this recipe belong to?")
	.MoreChoicesText("[grey](Move up and down to reveal more recipes)[/]")
	.InstructionsText("[grey](Press [blue]Space[/] to toggle a recipe, [green]Enter[/] to accept)[/]")
	.AddChoices(recipesList));

	foreach (var recipe in selectedRecipes)
	{
		recipesList.Remove(recipe);
	}
}

// incomplete
void EditRecipe()
{
	if (recipesList.Count == 0)
	{
		AnsiConsole.MarkupLine("[red]There are no recipes to [/]");
		return;
	}

	var chosenRecipe = AnsiConsole.Prompt(
	   new SelectionPrompt<Recipe>()
		   .Title("Which Recipe would you like to edit?")
		   .AddChoices(recipesList));

	var command = AnsiConsole.Prompt(
	   new SelectionPrompt<string>()
		   .Title("What would you like to do?")
		   .AddChoices(new[]
		   {
			   "Edit title",
			   "Edit Ingredients",
			   "Edit Instructions",
			   "Edit Categories"
		   }));

	AnsiConsole.Clear();
	switch (command)
	{
		case "Edit title":
			chosenRecipe.Title = AnsiConsole.Ask<string>("What is the [green]recipe[/] called?");
			break;
		case "Edit Ingredients":
			chosenRecipe.Ingredients.Clear();
			AnsiConsole.MarkupLine("Enter all the [green]ingredients[/]. [red] after you're done of writing instructions press space to move to next step [/]");
			var ingredient = AnsiConsole.Ask<string>("Enter ingredient: ");
			while (ingredient != "")
			{
				chosenRecipe.Ingredients.Add(ingredient);
				ingredient = AnsiConsole.Prompt(new TextPrompt<string>("Enter ingredient: ").AllowEmpty());
			};
			break;
		case "Edit Instructions":
			chosenRecipe.Instructions.Clear();
			AnsiConsole.MarkupLine("Enter all the [green]instructions[/]. [red] after you're done of writing instructions press space to move to next step [/]");
			var instruction = AnsiConsole.Ask<string>("Enter instruction: ");
			while (instruction != "")
			{
				chosenRecipe.Instructions.Add(instruction);
				instruction = AnsiConsole.Prompt(new TextPrompt<string>("Enter instruction: ").AllowEmpty());
			};
			break;
		case "Edit Category":
			var selectedcategories = AnsiConsole.Prompt(
			new MultiSelectionPrompt<String>()
			.PageSize(10)
			.Title("Which [green]category[/] does this recipe belong to?")
			.MoreChoicesText("[grey](Move up and down to reveal more categories)[/]")
			.InstructionsText("[grey](Press [blue]Space[/] to toggle a category, [green]Enter[/] to choose the category you toggeled)[/]")
			.AddChoices(categoriesList));

			chosenRecipe.Categories = selectedcategories;
			break;
	}
}

void AddCategory()
{
	string category = AnsiConsole.Ask<string>("What is the [green]category[/] called?");
	categoriesList.Add(category);
}

void EditCategory()
{
	if (categoriesList.Count == 0)
	{
		AnsiConsole.MarkupLine("[red]There are no Categories to be edited[/]");
		return;
	}
	var chosenCategory = AnsiConsole.Prompt(
	   new SelectionPrompt<string>()
		   .Title("Which Category would you like to edit?")
		   .AddChoices(categoriesList));

	String newCategoryName = AnsiConsole.Prompt(new TextPrompt<string>("What would you like to change the name to?"));

	categoriesList.Remove(chosenCategory);
	categoriesList.Add(newCategoryName);

	foreach (var cat in categoriesList)
	{
		foreach (var recipe in recipesList)
		{
			if (cat == chosenCategory)
			{
				recipe.Categories.Remove(chosenCategory);
				recipe.Categories.Add(newCategoryName);
			}
		}
	}
}

void ListRecipes()
{
	var table = new Table();
	table.AddColumn("Recipe Name");
	table.AddColumn("Ingredients");
	table.AddColumn("Instructions");
	table.AddColumn("Categories");

	foreach (var recipe in recipesList)
	{
		int ingCnt = 0;
		var ingredients = new StringBuilder();
		foreach (String ingredient in recipe.Ingredients)
        {
			ingCnt++;
			ingredients.Append(ingCnt+ "-" + "[green]" + ingredient + "[/]" + "\n");
		}

		int insCnt = 0;
		var instructions = new StringBuilder();
		foreach (String instruction in recipe.Instructions)
        {
			insCnt++;
			instructions.Append(insCnt + "-" + instruction + "\n");
		}
			

		var categories = new StringBuilder();
		foreach (String category in recipe.Categories)
		{
			categories.Append("-" + category + "\n");
		}
		
		table.AddRow(recipe.Title, ingredients.ToString(), instructions.ToString(), categories.ToString());
	}

	AnsiConsole.Write(table);
	/*foreach (var recipe in recipesList)
    {
		Console.WriteLine(recipe.Title);
		foreach (var ingredient in recipe.Ingredients)
        {
			AnsiConsole.WriteLine(ingredient);
        }
		foreach (var instructions in recipe.Instructions)
        {
			AnsiConsole.WriteLine(instructions);
        }
		foreach(var categories in recipe.Categories)
        {
			AnsiConsole.WriteLine(categories);
        }
    }*/

}


void Save()
{
	File.WriteAllText(_file, JsonSerializer.Serialize(recipesList));
	//File.WriteAllText(categoriesFile, JsonSerializer.Serialize(categoriesList));
}

