using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items
{
    public class Move : ModItem
    {
        public override void SetDefaults()
        {

            //item.damage = 11;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Blood: Move");
      // Tooltip.SetDefault("Commands your blood well to a location");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Projectile p = null;
            for (int i = 999; i >= 0; i--)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == Mod.Find<ModProjectile>("BloodWell").Type)
                {
                    p = Main.projectile[i];
                    break;
                }
            }
            if (p == null)
            {
                return false;
            }
            Vector2 distance = Main.MouseWorld - p.position;
            Vector2 vel = distance;
            vel.Normalize();
            vel *= 3;
            int time = (int)(distance.Length() / vel.Length());
            p.velocity = vel;
            p.ai[0] = 1f;
            p.ai[1] = time;
            return true;
        }
    }
}
