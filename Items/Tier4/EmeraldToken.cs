using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier4
{
    class EmeraldToken : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;

            Item.value = 10000;
            Item.rare = 6;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.UseSound = SoundID.Item4;
            Item.useStyle = 3;
            Item.useAnimation = 30;
            Item.useTime = 30;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Emerald Token");
      // Tooltip.SetDefault("What will you get?");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            //tier 4 loot
            int x = Main.rand.Next(0, 8);
            switch (x)
            {
                case 0:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("EmeraldTicket").Type, Main.rand.Next(1, 3));
                    goto default;
                case 1:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("ChargeBangle").Type, 1, false, -1);
                    goto default;
                case 2:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("LightSpearRing").Type, 1, false, -1);
                    goto default;
                case 3:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("ShadowflameRing").Type, 1, false, -1);
                    goto default;
                case 4:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("GunbladeBurst").Type, 1, false, -1);
                    goto default;
                case 5:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("FrostbiteEpee").Type, 1, false, -1);
                    goto default;
                case 6:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("ShadowflameEstoc").Type, 1, false, -1);
                    goto default;
                case 7:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("LightcrystalGreatbow").Type, 1, false, -1);
                    goto default;
                default:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("EmeraldTicket").Type, Main.rand.Next(1, 3));
                    break;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("RubyToken").Type, 1);
            recipe.Register();
        }
    }
}
