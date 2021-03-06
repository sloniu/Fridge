﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template10Test.Models.Fridge
{
    class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string RecipeText { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }


}
