using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class RazorSeekers : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 60;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 12;
            Item.useAnimation = 12;
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
      // DisplayName.SetDefault("Blood: Razor Seekers");
      // Tooltip.SetDefault("Piercing seekers hardened and sharpened by magic");
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
            if (player.buffTime[b] <= 481)
            {
                return false;
            }
            player.buffTime[b] -= 480;
            Vector2 distance = Main.MouseWorld - p.position;
            Vector2 vel = distance;
            vel.Normalize();
            vel *= 12;
            int q = Projectile.NewProjectile(p.Center.X, p.Center.Y, vel.X, vel.Y, Mod.Find<ModProjectile>("RazorSeeker").Type, (int)(Item.damage * player.GetDamage(DamageClass.Magic)), Item.knockBack, player.whoAmI);
            return true;
        }
    }
}
