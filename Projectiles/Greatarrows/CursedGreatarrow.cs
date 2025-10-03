using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class CursedGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 7;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 2400;
            Projectile.maxPenetrate = 3;
            Projectile.penetrate = 3;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Greatarrow");
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.localAI[0] == 1f)
            {
                target.AddBuff(BuffID.CursedInferno, 600);
            }
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 1f && Projectile.damage != 0)
            {
                Projectile.localAI[1]++;
                if(Projectile.localAI[1] % 3 == 0)
                {
                    int p = Projectile.NewProjectile(Projectile.Center.X - Projectile.velocity.X * 2, Projectile.Center.Y - Projectile.velocity.Y * 2, 0, 0, 101, (int)(Projectile.damage * 0.33), 0f, Projectile.owner, 0f, 0f);
                    Main.projectile[p].maxPenetrate -= 1;
                    Main.projectile[p].penetrate -= 1;
                    Main.projectile[p].extraUpdates = 0;
                    Main.projectile[p].friendly = true;
                    Main.projectile[p].hostile = false;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 68, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !Projectile.noDropItem)
            {
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("CursedGreatarrow").Type, 1, false, 0, false, false);
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