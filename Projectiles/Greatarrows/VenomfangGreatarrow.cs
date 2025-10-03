using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class VenomfangGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 9;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 2400;
            Projectile.maxPenetrate = 10;
            Projectile.penetrate = 10;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Venomfang Greatarrow");
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 180 * (int)(1 + Projectile.localAI[0]) * 10);
        }
        public override void AI()
        {
        }
        public override void OnKill(int timeLeft)
        {
            if (Projectile.localAI[0] == 1f)
            {
                for (int num385 = 0; num385 < 3; num385++)
                {
                    float num386 = -Projectile.velocity.X * Main.rand.Next(30, 60) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    float num387 = -Projectile.velocity.Y * Main.rand.Next(30, 60) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    int p = Projectile.NewProjectile(Projectile.position.X + num386, Projectile.position.Y + num387, num386, num387, ProjectileID.VenomFang, (int)(Projectile.damage * 0.5), 0f, Projectile.owner);
                    Main.projectile[p].timeLeft = 90;
                }
            }          
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.ToxicBubble, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !Projectile.noDropItem)
            {
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("VenomfangGreatarrow").Type, 1, false, 0, false, false);
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.localAI[0]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = (float)reader.ReadDouble();
        }
    }
}