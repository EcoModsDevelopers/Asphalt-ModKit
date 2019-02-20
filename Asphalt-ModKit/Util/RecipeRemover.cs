using Asphalt.Service;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.TextLinks;
using Eco.ModKit;
using Eco.Mods.TechTree;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asphalt.Utils
{
    public static class RecipeRemover
    {
        [HarmonyPatch(typeof(Recipe), "Initialize", new Type[] { })]
        internal static class RecipeInitalizePatch
        {
            static void Postfix()
            {
                ServiceHelper.CallStaticMethod("OnRecipesInitialized");
            }
        }

        public static void RemoveRecipe(Type pTargetRecipeType)
        {
            // Get all the existing recipe
            Dictionary<Type, Recipe[]> staticRecipes = (Dictionary<Type, Recipe[]>)typeof(CraftingComponent).GetFields(BindingFlags.Static | BindingFlags.NonPublic).First(x => x.Name.Contains("staticRecipes")).GetValue(null);

            // Get all the recipe to table dicationary
            Dictionary<Type, List<Type>> recipeToTable = (Dictionary<Type, List<Type>>)typeof(CraftingComponent).GetFields(BindingFlags.Static | BindingFlags.NonPublic).First(x => x.Name.Contains("recipeToTable")).GetValue(null);

            // Get all item to recipe dictionary
            Dictionary<Type, List<Recipe>> itemToRecipe = (Dictionary<Type, List<Recipe>>)typeof(CraftingComponent).GetFields(BindingFlags.Static | BindingFlags.NonPublic).First(x => x.Name.Contains("itemToRecipe")).GetValue(null);

            lock (staticRecipes)
            {
                Type targetTable = CraftingComponent.TablesForRecipe(pTargetRecipeType).First();

                Recipe targetRecipe = null;
                Recipe[] recipes;

                // Get all the recipe inside the table
                if (staticRecipes.TryGetValue(targetTable, out recipes))
                {
                    // Get the recipe
                    targetRecipe = recipes.First(x => x.GetType() == pTargetRecipeType);

                    // Remove the target recipe from the recipe list
                    recipes = recipes.Where(x => x.GetType() != pTargetRecipeType).ToArray();
                }
                // Set back the recipe inside the static recipe dictionnary
                staticRecipes[targetTable] = recipes;

                // Remove the recipe from the recipe to table dictionary
                recipeToTable[pTargetRecipeType].Remove(targetTable);

                // Remove the recipe from the item to recipe dictionary
                targetRecipe?.Products.ForEach(product => itemToRecipe[product.Item.Type].Remove(targetRecipe));

                // Remove the table from the list of table
                // Only if there is no more recipe inside the table
                if (recipes.Length == 0)
                    CraftingComponent.AllTableWorldObjects.Remove(targetTable);

                // After removing the recipe we need to update the skill unlock

                // Get all the skillunlock tooltips
                Dictionary<Type, Dictionary<int, List<LocString>>> skillUnlocksTooltips = (Dictionary<Type, Dictionary<int, List<LocString>>>)typeof(Skill).GetFields(BindingFlags.Static | BindingFlags.NonPublic).First(x => x.Name.Contains("skillUnlocksTooltips")).GetValue(null);

                // Get the skill that unlock the recipe
                Type skillType = RequiresSkillAttribute.Cache.Get(pTargetRecipeType).FirstOrDefault()?.SkillItem.Type;

                // Get the level that unlock the recipe
                int? recipeUnlockLevel = RequiresSkillAttribute.Cache.Get(pTargetRecipeType).FirstOrDefault()?.Level;

                // If there is a require skill
                if (skillType != null && skillUnlocksTooltips.ContainsKey(skillType))
                {
                    // Get the list of unlock for the skill for the unlock level
                    List<LocString> unlocks = skillUnlocksTooltips[skillType][recipeUnlockLevel.Value];

                    // Search the correct unlock
                    for (int i = 0; i < unlocks.Count; i++)
                    {
                        if (unlocks[i] == new LocString(Text.Indent(targetRecipe.UILink())))
                        {
                            // remove the loc string from the list
                            unlocks.RemoveAt(i);
                            break;
                        }
                    }
                    // Set the new unlock list
                    skillUnlocksTooltips[skillType][recipeUnlockLevel.Value] = unlocks;
                }

                FieldInfo fi = typeof(SkillModifiedValueManager).GetField("skillBenefits", BindingFlags.Static | BindingFlags.NonPublic);
                Dictionary<Type, Dictionary<LocString, List<SkillModifiedValue>>> benefits = (Dictionary<Type, Dictionary<LocString, List<SkillModifiedValue>>>)fi.GetValue(null);

                foreach (var benefitSkillEntry in benefits)
                {
                    foreach (var entry in benefitSkillEntry.Value.ToArray())
                    {
                        if (entry.Key.ToString().Contains("Recipe:" + pTargetRecipeType.FullName))
                        {
                            benefitSkillEntry.Value.Remove(entry.Key);
                        }
                    }
                }
            }

            var allRecipes = Recipe.AllRecipes.ToList();
            allRecipes.RemoveAll(r => r.GetType() == pTargetRecipeType);
            typeof(Recipe).GetProperty("AllRecipes", BindingFlags.Static | BindingFlags.Public).SetValue(null, allRecipes.ToArray());

            RemoveFromDictionary("itemToRecipesWithProduct", pTargetRecipeType);
            RemoveFromDictionary("itemToRecipesWithIngredient", pTargetRecipeType);
            RemoveFromDictionary("skillToRecipes", pTargetRecipeType);

            RemoveFromDictionaryRecipeName(pTargetRecipeType);
        }

        private static void RemoveFromDictionary(string pPropertyName, Type pTargetRecipeType)
        {
            Dictionary<Type, IEnumerable<Recipe>> dictionary = (Dictionary<Type, IEnumerable<Recipe>>)typeof(Recipe).GetField(pPropertyName, BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            foreach (var entry in dictionary.ToDictionary(k => k.Key, v => v.Value))
            {
                if (entry.Value.Any(r => r.GetType() == pTargetRecipeType))
                {
                    List<Recipe> recipes = entry.Value.ToList();

                    recipes.RemoveAll(r => r.GetType() == pTargetRecipeType);

                    dictionary[entry.Key] = recipes;
                }
            }
        }

        private static void RemoveFromDictionaryRecipeName(Type pTargetRecipeType)
        {
            Dictionary<string, Type> dictionary = (Dictionary<string, Type>)typeof(Recipe).GetField("recipeNameDictionary", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            foreach (var entry in dictionary.ToDictionary(k => k.Key, v => v.Value))
            {
                if (entry.Value == pTargetRecipeType)
                {
                    dictionary.Remove(entry.Key);
                }
            }
        }
    }
}