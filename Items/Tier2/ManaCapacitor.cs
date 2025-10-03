using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items.Tier2
{
    public class ManaCapacitor : ModItem
    {
        int cooldown = 0;
        public override void SetDefaults()
        {

            Item.accessory = true;
            Item.width = 10;
            Item.height = 10;
            Item.rare = 3;

            Item.value = 20000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Mana Capacitor");
      // Tooltip.SetDefault("Gives a burst of mana when low. 20 second cooldown.");
    }

        public override void UpdateEquip(Player player)
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            if (player.statMana < 20 && cooldown <= 0)
            {
                cooldown = 1200;
                player.statMana = Math.Min(player.statMana + 200, player.statManaMax2);
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 30, player.width, player.height), new Color(0, 30, 200, 255), "" + 200);

            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ManaBattery").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("TopazTicket").Type, 3);
            recipe.Register();
        }
    }
}
