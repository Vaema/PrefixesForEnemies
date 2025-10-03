using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier4
{
    public class LightSpearRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 55;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 50000;
            Item.rare = 6;
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Light Spear Ring");
      // Tooltip.SetDefault("Summons a ring of holy spears. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[8] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X + 300, Main.MouseWorld.Y, -.001f, 0, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int q = Projectile.NewProjectile(Main.MouseWorld.X - 300, Main.MouseWorld.Y, .001f, 0, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int w = Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y + 300, 0, -.001f, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int e = Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y - 300, 0, .001f, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int r = Projectile.NewProjectile(Main.MouseWorld.X + 212, Main.MouseWorld.Y + 212, -.000707f, -.000707f, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int t = Projectile.NewProjectile(Main.MouseWorld.X - 212, Main.MouseWorld.Y + 212, .000707f, -.000707f, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int y = Projectile.NewProjectile(Main.MouseWorld.X + 212, Main.MouseWorld.Y - 212, -.000707f, .000707f, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            int u = Projectile.NewProjectile(Main.MouseWorld.X - 212, Main.MouseWorld.Y - 212, .000707f, .000707f, Mod.Find<ModProjectile>("HolyLance").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);

            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[8]--;
            if (play.cooldowns[8] == -1)
            {
                play.cooldowns[8] = play.maxCooldowns[8];
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("EmeraldTicket").Type, 3);
            recipe.Register();
        }
    }
}
