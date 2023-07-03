using System;

namespace ProgrammingApplication
{
    internal class Recipe
    {
        private string recipeName;

        public Recipe()
        {
        }

        public Recipe(string recipeName)
        {
            this.recipeName = recipeName;
        }

        public object Name { get; internal set; }
        public object Ingredients { get; internal set; }

        internal double CalculateTotalCalories()
        {
            throw new NotImplementedException();
        }

        internal void DisplayRecipe()
        {
            throw new NotImplementedException();
        }

        internal void EnterDetails()
        {
            throw new NotImplementedException();
        }

        internal void ResetQuantities()
        {
            throw new NotImplementedException();
        }

        internal void ScaleRecipe(double factor)
        {
            throw new NotImplementedException();
        }
    }
}