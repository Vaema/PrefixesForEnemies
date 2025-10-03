using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier6
{
    public class SolarShowerRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 90;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 10;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Solar Shower Ring");
      // Tooltip.SetDefault("Rains solar matter. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[17] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X, player.Center.Y - 900, 0, 0, Mod.Find<ModProjectile>("SolarShowerSpawner").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[17]--;
            if (play.cooldowns[17] == -1)
            {
                play.cooldowns[17] = play.maxCooldowns[17];
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("FireRainRing").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("AmberTicket").Type, 3);
            recipe.Register();
        }
    }
}
