using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier1
{
    class AmethystToken : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;

            Item.value = 10000;
            Item.rare = 2;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.UseSound = SoundID.Item4;
            Item.useStyle = 3;
            Item.useAnimation = 30;
            Item.useTime = 30;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Amethyst Token");
      // Tooltip.SetDefault("What will you get?");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            //tier 1 loot
            int x = Main.rand.Next(0, 7);
            switch (x)
            {
                case 0:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("AmethystTicket").Type, Main.rand.Next(1,3));
                    goto default;
                case 1:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("ManaBattery").Type, 1, false, -1);
                    goto default;
                case 2:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("ScourgeRing").Type, 1, false, -1);
                    goto default;
                case 3:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("IceShardRing").Type, 1, false, -1);
                    goto default;
                case 4:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("SimpleRapier").Type, 1, false, -1);
                    goto default;
                case 5:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("FencerFoil").Type, 1, false, -1);
                    goto default;
                case 6:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("WoodGreatbow").Type, 1, false, -1);
                    goto default;
                default:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("AmethystTicket").Type, Main.rand.Next(1, 3));
                    break;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("TopazToken").Type, 1);
            recipe.Register();
        }
    }
}
