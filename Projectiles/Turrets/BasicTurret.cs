using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public abstract class BasicTurret : ModProjectile
    {
        protected bool hasTarget;
        protected NPC target;
        protected int range;
        protected int fireRate;
        protected float shootSpeed;
        protected int ammoType;
        protected int secondaryType = 0;
        protected int secondaryRate;
        protected float secondarySpeed;
        protected int secondaryDam;
        protected bool hasStand = false;
        protected int buffID;

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Main.projPet[Projectile.type] = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        protected void basicAI()
        {
            //update time left, required for minion cap
            update();
            if (target==null)
            {
                target = getTarget(range);
            }
            //gets a new target if the old one is dead or out of sight
            else if (!target.active || !Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height) || Projectile.Distance(target.position)>range)
            {
                target = getTarget(range);
            }
            //has target
            if (target != null)
            {
                Vector2 toTarget = target.Center - Projectile.position;
                toTarget += new Vector2((toTarget.X / shootSpeed) * target.velocity.X, (toTarget.Y / shootSpeed) * target.velocity.Y);//attempt to lead target
                Projectile.rotation = toTarget.ToRotation();
                Projectile.ai[0]++;//shot timer
                if(Projectile.ai[0] >= fireRate)
                {
                    shoot(toTarget);
                    Projectile.ai[0] = 0;
                }
                if(secondaryType != 0)
                {
                    Projectile.localAI[1]++;
                    if (Projectile.localAI[1] >= secondaryRate)
                    {
                        shootSecondary(toTarget);
                        Projectile.localAI[1] = 0;
                    }
                }
            }
            //does not have target, idle rotation. ai[1] stores the angle of turret to contact point, set on creation
            else
            {
                if (Projectile.rotation >= Projectile.ai[1] + (6.28f-.785f) - 3.14f)
                {
                    Projectile.localAI[0] = -1;
                }
                else if (Projectile.rotation <= Projectile.ai[1] + .785f - 3.14f)
                {
                    Projectile.localAI[0] = 1;
                }
                Projectile.rotation += .012f * Projectile.localAI[0];
            }
            //flip sprite based on rotation, not currently working
            float adjustedRot = (Projectile.rotation - Projectile.ai[1]) % (2 * (float)Math.PI);
            if (adjustedRot > Math.PI)
            {
                Projectile.spriteDirection = -1;
            }
            else
            {
                Projectile.spriteDirection = 1;
            }
        }
        protected void createStand()
        {
            Vector2 adjustment = new Vector2(-(float)Math.Cos(Projectile.ai[1]), -(float)Math.Sin(Projectile.ai[1])) * 20;
            Projectile.position -= adjustment;
            Projectile.rotation = -Projectile.ai[1];
            int p = Projectile.NewProjectile(Projectile.Center.X + 2*adjustment.X, Projectile.Center.Y + 2*adjustment.Y, 0, 0, Mod.Find<ModProjectile>("TurretStand").Type, 0, 0, Projectile.owner, Projectile.whoAmI);
            Main.projectile[p].timeLeft = Projectile.timeLeft;
            Main.projectile[p].rotation = Projectile.ai[1] + 1.57f;
        }
        protected NPC getTarget(int range)
        {
            float lowestD = range;
            NPC closest = null;
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                float distance = (float)Math.Sqrt((npc.Center.X - Projectile.Center.X) * (npc.Center.X - Projectile.Center.X) + (npc.Center.Y - Projectile.Center.Y) * (npc.Center.Y - Projectile.Center.Y));
                if (lowestD > distance && npc.CanBeChasedBy() && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                {
                    closest = npc;
                    lowestD = distance;
                }
            }
            return closest;
        }
        protected abstract void shoot(Vector2 toTarget);//to be overridden in subclasses
        protected abstract void shootSecondary(Vector2 toTarget);
        protected abstract void update();
        protected int consumeAmmo(out int type)
        {
            Item item = new Item();
            bool flag = false;
            bool canShoot = false;
            int damage = Projectile.damage;
            Player player = Main.player[Projectile.owner];
            for (int i = 57; i >= 54; i--)//bottom up for ammo
            {
                if (player.inventory[i].ammo == ammoType && player.inventory[i].stack > 0)
                {
                    item = player.inventory[i];
                    canShoot = true;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                for (int j = 0; j < 54; j++)
                {
                    if (player.inventory[j].ammo == ammoType && player.inventory[j].stack > 0)
                    {
                        item = player.inventory[j];
                        canShoot = true;
                        break;
                    }
                }
            }
            if (!canShoot)
            {
                type = -1;
                return -1;
            }
            type = item.shoot;
            damage = (int)((damage + item.damage) * player.GetDamage(DamageClass.Ranged));
            if (item.consumable)
            {
                item.stack--;
                if (item.stack <= 0)
                {
                    item.active = false;
                    item.type = 0;
                }
            }
            return damage;
        }
    }
}
