using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier2
{
    [AutoloadEquip(EquipType.Legs)]
    public class EmbellishedShoes : ModItem
    {

        public override void SetDefaults()
        {

            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = 3;
            Item.defense = 5;

        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Embellished Shoes");
      // Tooltip.SetDefault("+4% magic damage and crit");
    }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += .04f;
            player.GetCritChance(DamageClass.Magic) += 4;
        }

        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }*/
    }
}
