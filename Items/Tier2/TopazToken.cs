using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier2
{
    class TopazToken : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;

            Item.value = 10000;
            Item.rare = 3;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.UseSound = SoundID.Item4;
            Item.useStyle = 3;
            Item.useAnimation = 30;
            Item.useTime = 30;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Topaz Token");
      // Tooltip.SetDefault("What will you get?");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            //tier 2 loot
            int x = Main.rand.Next(0, 10);
            switch (x)
            {
                case 0:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("TopazTicket").Type, Main.rand.Next(1, 3));
                    goto default;
                case 1:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("AcidRainRing").Type, 1, false, -1);
                    goto default;
                case 2:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("Gunblade").Type, 1, false, -1);
                    goto default;
                case 3:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("RazorwindRing").Type, 1, false, -1);
                    goto default;
                case 4:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("Gunblade").Type, 1, false, -1);
                    goto default;
                case 5:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("EmbellishedHood").Type);
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("EmbellishedShoes").Type);
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("EmbellishedRobe").Type);
                    goto default;
                case 6:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("BlazingEstoc").Type, 1, false, -1);
                    goto default;
                case 7:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("StingingEpee").Type, 1, false, -1);
                    goto default;
                case 8:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("HellfireGreatbow").Type, 1, false, -1);
                    goto default;
                case 9:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("AzureGreatbow").Type, 1, false, -1);
                    goto default;
                default:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("TopazTicket").Type, Main.rand.Next(1, 3));
                    break;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("SapphireToken").Type, 1);
            recipe.Register();
        }
    }
}
