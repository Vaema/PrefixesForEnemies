using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyMods.Projectiles
{
    public class OrionHunter : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 630;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            //ai 0 and 1 are stored velocity X and Y
            if(Projectile.timeLeft%60==0 && Projectile.owner == Main.myPlayer)
            {
                int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, Projectile.ai[0], Projectile.ai[1], 640, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
    }
}
