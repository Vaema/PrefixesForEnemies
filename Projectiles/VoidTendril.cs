using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class VoidTendril : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.timeLeft = 120;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Tendril");
        }
        public override void AI()
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("VoidDust").Type, 0, 0);
            Main.dust[d].noGravity = true;
            Main.dust[d].scale += .5f;
            Projectile.velocity = Projectile.velocity.RotatedByRandom(.2);
            if(Projectile.timeLeft % 10 == 0)
            {
                int p = Projectile.NewProjectile(Projectile.position, Vector2.Zero, Mod.Find<ModProjectile>("VoidHitbox").Type, Projectile.damage, Projectile.knockBack);
                Main.projectile[p].timeLeft = Projectile.timeLeft + 20;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            MPlayer pinf = ((MPlayer)target.GetModPlayer(Mod, "MPlayer"));
            pinf.voidBurn = Math.Max((int)(damage / 25f + target.statLifeMax2 / 200f + 1), pinf.voidBurn);
            target.AddBuff(Mod.Find<ModBuff>("VoidBurn").Type, 480 + 100*damage);
        }
    }
}
