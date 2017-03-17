using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template10Test.Models.Fridge
{
    class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double Amount { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
