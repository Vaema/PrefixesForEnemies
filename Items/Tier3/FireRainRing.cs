using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier3
{
    public class FireRainRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 25;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 30000;
            Item.rare = 4;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
            Item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Fire Rain Ring");
      // Tooltip.SetDefault("Rains fire. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[6] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X - 100, player.Center.Y - 900, 0, 0, Mod.Find<ModProjectile>("FireRainSpawner").Type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[6]--;
            if (play.cooldowns[6] == -1)
            {
                play.cooldowns[6] = play.maxCooldowns[6];
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("SapphireTicket").Type, 3);
            recipe.Register();
        }
    }
}
