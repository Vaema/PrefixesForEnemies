using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class EarthenDraught : ModItem
    {
        public override void SetDefaults()
        {


            Item.maxStack = 30;
            Item.value = 6000;
            Item.rare = 6;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.consumable = true;
            Item.useTurn = true;
            Item.width = 14;
            Item.height = 24;
            Item.UseSound = SoundID.Item3;
            Item.buffType = Mod.Find<ModBuff>("EarthenDraught").Type;
            Item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Earthen Draught");
      // Tooltip.SetDefault("Take 15% less damage while on the ground");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("EmeraldTicket").Type);
            recipe.Register();
        }
    }
}
