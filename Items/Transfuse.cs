using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items
{
    public class Transfuse : ModItem
    {
        public override void SetDefaults()
        {

            //item.damage = 11;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 3;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Blood: Transfuse");
      // Tooltip.SetDefault("Absorb life from your Well");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Projectile p = null;
            int b = player.FindBuffIndex(Mod.Find<ModBuff>("BloodWell").Type);
            for (int i = 999; i >= 0; i--)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == Mod.Find<ModProjectile>("BloodWell").Type)
                {
                    p = Main.projectile[i];
                    break;
                }
            }
            if (p == null || b == -1)
            {
                return false;
            }
            if (player.buffTime[b] <= 241)
            {
                return false;
            }
            player.buffTime[b] /= 2;
            int q = Projectile.NewProjectile(p.position.X, p.position.Y, 0, 0, ProjectileID.VampireHeal, 0, 0, player.whoAmI, player.whoAmI, (player.buffTime[b] / 120));
            return true;
        }
    }
}
