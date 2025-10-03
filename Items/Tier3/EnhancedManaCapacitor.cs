using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class EnhancedManaCapacitor : ModItem
    {
        int cooldown = 0;
        public override void SetDefaults()
        {

            Item.accessory = true;
            Item.width = 10;
            Item.height = 10;
            Item.rare = 4;

            Item.value = 30000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Enhanced Mana Capacitor");
      // Tooltip.SetDefault("Gives a burst of mana when low. 10 second cooldown.");
    }

        public override void UpdateEquip(Player player)
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            if (player.statMana < 20 && cooldown <= 0)
            {
                cooldown = 600;
                player.statMana = Math.Min(player.statMana + 200, player.statManaMax2);
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 30, player.width, player.height), new Color(0, 30, 200, 255), "" + 200);

            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ManaCapacitor").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("SapphireTicket").Type, 3);
            recipe.Register();
        }
    }
}
