using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class VoidBurn : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Burn");
            // Description.SetDefault("Fight back to resist the Void");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer pinf = ((MPlayer)player.GetModPlayer(Mod, "MPlayer"));
            if (Main.rand.Next(2) == 0)
            {
                int d = Dust.NewDust(player.position, player.width, player.height, Mod.Find<ModDust>("VoidDust").Type, 0, 0);
            }
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            return true;
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
