using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items
{
    public class Seekers : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 30;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 3;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Blood: Seekers");
      // Tooltip.SetDefault("Rapidly fires homing blood clots from your Well");
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
            player.buffTime[b] -= 240;
            Vector2 distance = Main.MouseWorld - p.position;
            Vector2 vel = distance;
            vel.Normalize();
            vel *= 12;
            int q = Projectile.NewProjectile(p.Center.X, p.Center.Y, vel.X, vel.Y, Mod.Find<ModProjectile>("BloodSeeker").Type, (int)(Item.damage * player.GetDamage(DamageClass.Magic)), Item.knockBack, player.whoAmI);
            return true;
        }
    }
}
