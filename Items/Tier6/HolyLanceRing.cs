using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier6
{
    public class HolyLanceRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 150;
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
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Holy Lance Ring");
      // Tooltip.SetDefault("Bring down angelic lances. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[15] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X + 304, Main.MouseWorld.Y, -.001f, 0, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int q = Projectile.NewProjectile(Main.MouseWorld.X - 296, Main.MouseWorld.Y, .001f, 0, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int e = Projectile.NewProjectile(Main.MouseWorld.X + 12, Main.MouseWorld.Y - 300, 0, .001f, Mod.Find<ModProjectile>("BigHolyLance").Type, Item.damage*3, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int y = Projectile.NewProjectile(Main.MouseWorld.X + 216, Main.MouseWorld.Y - 212, -.000707f, .000707f, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int u = Projectile.NewProjectile(Main.MouseWorld.X - 208, Main.MouseWorld.Y - 212, .000707f, .000707f, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);

            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[15]--;
            if (play.cooldowns[15] == -1)
            {
                play.cooldowns[15] = play.maxCooldowns[15];
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("LightSpearRing").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("AmberTicket").Type, 3);
            recipe.Register();
        }
    }
}
