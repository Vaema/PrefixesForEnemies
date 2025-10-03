using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles.Needles
{
    public class HellfireNeedle : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 1;
            Projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hellfire Needle");
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(8) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default(Color), 0.6f);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(Projectile.width >= 80)
            {
                return;
            }
            target.AddBuff(BuffID.OnFire, 300);
            gNPC info = target.GetGlobalNPC<gNPC>();
            info.numNeedlesHellfire++;
            if(info.numNeedlesHellfire >= 6)
            {
                info.numNeedlesHellfire = 0;
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                for (int num507 = 0; num507 < 10; num507++)
                {
                    Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                }
                for (int num508 = 0; num508 < 5; num508++)
                {
                    int num509 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
                    Main.dust[num509].noGravity = true;
                    Main.dust[num509].velocity *= 3f;
                    num509 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num509].velocity *= 2f;
                }
                int num510 = Gore.NewGore(new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num510].velocity *= 0.4f;
                Gore expr_10989_cp_0 = Main.gore[num510];
                expr_10989_cp_0.velocity.X = expr_10989_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
                Gore expr_109B9_cp_0 = Main.gore[num510];
                expr_109B9_cp_0.velocity.Y = expr_109B9_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
                num510 = Gore.NewGore(new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num510].velocity *= 0.4f;
                Gore expr_10A4D_cp_0 = Main.gore[num510];
                expr_10A4D_cp_0.velocity.X = expr_10A4D_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
                Gore expr_10A7D_cp_0 = Main.gore[num510];
                expr_10A7D_cp_0.velocity.Y = expr_10A7D_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
                if (Projectile.owner == Main.myPlayer)
                {
                    Projectile.penetrate = -1;
                    Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
                    Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
                    Projectile.width = 80;
                    Projectile.height = 80;
                    Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
                    Projectile.damage = (int)((60 * Main.player[Projectile.owner].GetDamage(DamageClass.Throwing)) + (Math.Min(80, target.lifeMax * .04)));
                    Projectile.Damage();
                    Projectile.Kill();
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default(Color), 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
