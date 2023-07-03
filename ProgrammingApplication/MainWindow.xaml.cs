using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgrammingApplication

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;

        public object RecipeBox { get; private set; }
        public object ScaleFactor { get; private set; }
        public object FilterByIngredient { get; private set; }
        public object FilterByFoodGroup { get; private set; }
        public object MaxCalories{ get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe();
            recipe.EnterDetails();

            recipes.Add(recipe);

            MessageBox.Show("Recipe added successfully!");
        }

        private void DisplayRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("No recipes found");
                return;
            }

            int selectedIndex = RecipeListBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < recipes.Count)
            {
                Recipe selectedRecipe = recipes[selectedIndex];
                selectedRecipe.DisplayRecipe();
            }
            else
            {
                MessageBox.Show("Select a recipe to display");
            }
        }

        private void DisplaySortedRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            List<Recipe> sortedRecipes = recipes.OrderBy(r => r.Name).ToList();

            RecipeListBox.ItemsSource = sortedRecipes;
        }

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = RecipeListBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < recipes.Count)
            {
                Recipe selectedRecipe = recipes[selectedIndex];

                double factor;
                if (!double.TryParse(ScaleFactorTextBox.Text, out factor))
                {
                    MessageBox.Show("Invalid scaling factor");
                }
                else
                {
                    selectedRecipe.ScaleRecipe(factor);
                    MessageBox.Show("Recipe scaled successfully!");
                }
            }
            else
            {
                MessageBox.Show("select a recipe to scale");
            }
        }

        private void ResetQuantitiesButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = RecipeListBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < recipes.Count)
            {
                Recipe selectedRecipe = recipes[selectedIndex];
                selectedRecipe.ResetQuantities();
                MessageBox.Show("Quantities reset to original values");
            }
            else
            {
                MessageBox.Show("Select a recipe to reset quantities");
            }
        }

        private void ClearDataButton_Click(object sender, RoutedEventArgs e)
        {
            recipes.Clear();
            RecipeListBox.ItemsSource = null;
            MessageBox.Show("Data cleared");
        }

        private void FilterByIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            string ingredientName = FilterByIngredientTextBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(ingredientName))
            {
                MessageBox.Show("Enter an ingredient name");
                return;
            }

            List<Recipe> filteredRecipes = recipes.Where(r => r.Ingredients != null && r.Ingredients.Any(i => i.FoodGroup.ToLower() == foodGroup)).ToList();


            RecipeListBox.ItemsSource = filteredRecipes;

            if (filteredRecipes.Count == 0)
            {
                MessageBox.Show($"No recipes found with '{ingredientName}'");
            }
        }

        private void FilterByFoodGroupButton_Click(object sender, RoutedEventArgs e)
        {
            string foodGroup = FilterByFoodGroupTextBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(foodGroup))
            {
                MessageBox.Show("Please enter a food group");
                return;
            }

            List<Recipe> filteredRecipes = recipes.Where(r => r.Ingredients.Any(i => i.Name.ToLower() == ingredientName)).ToList();

            RecipeListBox.ItemsSource = filteredRecipes;

            if (filteredRecipes.Count == 0)
            {
                MessageBox.Show($"No recipes found with '{foodGroup}' food group");
            }
        }

        private void FilterByCaloriesButton_Click(object sender, RoutedEventArgs e)
        {
            string maxCaloriesString = MaxCaloriesTextBox.Text.Trim();

            if (string.IsNullOrEmpty(maxCaloriesString))
            {
                MessageBox.Show("Please enter a maximum calories value");
                return;
            }

            if (!double.TryParse(maxCaloriesString, out double maxCalories))
            {
                MessageBox.Show("Invalid maximum calories value");
                return;
            }

            List<Recipe> filteredRecipes = recipes.Where(r => r.CalculateTotalCalories() <= maxCalories).ToList();

            RecipeListBox.ItemsSource = filteredRecipes;

            if (filteredRecipes.Count == 0)
            {
                MessageBox.Show($"No recipes found with maximum calories <= {maxCalories}");
            }
        }
    }
}




