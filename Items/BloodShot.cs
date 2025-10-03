using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class BloodShot : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 35;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 30;
            Item.useAnimation = 30;
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
      // DisplayName.SetDefault("Blood: Shot");
      // Tooltip.SetDefault("Fires a spread of blood clots from your Well");
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
            if (player.buffTime[b] <= 901)
            {
                return false;
            }
            player.buffTime[b] -= 900;
            Vector2 distance = Main.MouseWorld - p.position;
            Vector2 vel = distance;
            vel.Normalize();
            vel *= 18;
            for(int i=0; i<6; i++)
            {
                int q = Projectile.NewProjectile(p.Center.X, p.Center.Y, vel.X + Main.rand.Next(-30, 31)/15, vel.Y + Main.rand.Next(-30, 31) / 15, Mod.Find<ModProjectile>("BloodBullet").Type, (int)(Item.damage * player.GetDamage(DamageClass.Magic)), Item.knockBack, player.whoAmI);
            }
            return true;
        }
    }
}
