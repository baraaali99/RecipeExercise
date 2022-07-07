using System;
using Spectre.Console;
using System.Text.Json;
using RecipeExercise;
using System.Text;
using Newtonsoft.Json;

var recipesList = new List<Recipe>();
var categoriesList = new List<string>();
var JsonPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
string JsonFile = Path.Combine(JsonPath, "D:\\computer science\\Silverkey\\RecipeExercise\\RecipeExercise\\Recipes.json");

	using (StreamReader r = new StreamReader(JsonFile))
	{
		var Data = r.ReadToEnd();
		var Json = JsonConvert.DeserializeObject<List<Recipe>>(Data);
		if (Json != null)
		{
			recipesList = Json;
		}
	}

void UserInterface()
{
	AnsiConsole.Write(
			   new FigletText("Recipes app")
				   .Centered()
				   .Color(Color.DarkOliveGreen1));

}

UserInterface();
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
			   "Edit a Recipe"
		   })
		   .AddChoiceGroup("Categories", new[]
		   {
			   "Add a Category",

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
			UserInterface();
			ListRecipes();
			break;
		case "Add a Recipe":
			UserInterface();
			AddRecipe();
			break;
		case "Edit a Recipe":
			UserInterface();
			EditRecipe();
			break;
		case "Add a Category":
			UserInterface();
			AddCategory();
			break;
		case "Edit a Category":
			UserInterface();
			EditCategory();
			break;
		default:
			UserInterface();
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
	var ingredient = AnsiConsole.Ask<string>("Enter recipe ingredient: ");
	while (ingredient != "")
	{
		ingredients.Add(ingredient);
		ingredient = AnsiConsole.Prompt(new TextPrompt<string>("Enter recipe ingredients: ").AllowEmpty());
	};

	AnsiConsole.MarkupLine("Enter all the [green]instructions[/]. [red] after you're done of writing ingredients press space to move to next step [/]");
	var instruction = AnsiConsole.Ask<string>("Enter [green]recipe[/] instructions: ");
	while (instruction != "")
	{
		instructions.Add(instruction);
		instruction = AnsiConsole.Prompt(new TextPrompt<string>("Enter [green]recipe[/] instructions: ").AllowEmpty());

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
		AnsiConsole.MarkupLine("[red]Threr are no categories so please add Categories your recipes can belong to[/]");
		recipe.Categories.Add("[red]not assigned to specific Category yet[/]");// here the recipe the user added won't belong to any category
	}
	else
	{
		var selectedcategories = AnsiConsole.Prompt(
		new MultiSelectionPrompt<String>()
		.PageSize(10)
		.Title(" Which [white]categories[/] does this recipe belong to?")
		.MoreChoicesText("[grey](Move up and down to reveal more categories)[/]")
		.InstructionsText("[grey](Press [blue]Space[/] to toggle a category, [green]Enter[/] to accept)[/]")
		.AddChoices(categoriesList));

		recipe.Categories = selectedcategories;
	}
	recipesList.Add(recipe);
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
	AnsiConsole.MarkupLine("Enter all the [green]categories[/]. [red] after you're done of writing categories press space to move to next step [/]");
	string category = AnsiConsole.Ask<string>("What is the [green]category[/] called?");
	while (category != "")
	{
		categoriesList.Add(category);
		category = AnsiConsole.Prompt(new TextPrompt<string>("Enter more [green]categories[/]: ").AllowEmpty());
	};

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
	int i = 0;
	foreach (var r in recipesList)
	{
		if (i < recipesList.Count && i < recipesList[i].Categories.Count && r.Categories[i] == recipesList[i].Categories[i])
		{
			r.Categories[i] = newCategoryName;
		}
		i++;
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
			ingredients.Append(ingCnt + "-" + "[gray]" + ingredient + "[/]" + "\n");
		}

		int insCnt = 0;
		var instructions = new StringBuilder();
		foreach (String instruction in recipe.Instructions)
		{
			insCnt++;
			instructions.Append(insCnt + "-" +"[gray]"+ instruction +"[/]"+"\n");
		}

		var categories = new StringBuilder();
		foreach (String category in recipe.Categories)
		{
			categories.Append("-" + "[gray]" + category + "[/]" + "\n");
		}

		table.AddRow(recipe.Title, ingredients.ToString(), instructions.ToString(), categories.ToString());
	}

	AnsiConsole.Write(table);

}


void Save()
{
	File.WriteAllText(JsonFile, JsonConvert.SerializeObject(recipesList));
}
