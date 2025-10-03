using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class Vortex : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 40;
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
      // DisplayName.SetDefault("Blood: Vortex");
      // Tooltip.SetDefault("Your well pulls enemies toward it, dealing damage");
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
            if (player.buffTime[b] <= 301)
            {
                return false;
            }
            player.buffTime[b] -= 300;
            //todo dust
            ArrayList infect = getNPCsInRange(p, 300);
            foreach (NPC npc in infect)
            {
                npc.StrikeNPC((int)(Item.damage * player.GetDamage(DamageClass.Magic)), 0, 0);
                Vector2 pull = -npc.position + p.position;
                pull.Normalize();
                pull *= 8;
                npc.velocity += pull;
            }
            return true;
        }
        private ArrayList getNPCsInRange(Projectile focus, int distance)
        {
            ArrayList NPCsInRange = new ArrayList();
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.Distance(focus.position) < distance && npc.aiStyle != 7 && !(npc.catchItem > 0) && npc.type != 401 && npc.type != 488 && npc.life > 0 && npc.active)
                {
                    NPCsInRange.Add(npc);
                }
            }
            return NPCsInRange;
        }
    }
}
