using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class GunbladeMelee : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gunblade Melee Bonus");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            bool flag = false;
            int stacks = 1 + player.buffTime[buffIndex] / 120;
            if (stacks >= 5)
            {
                flag = true;
                stacks = 5;
                player.buffTime[buffIndex] = 600;
            }
            player.GetDamage(DamageClass.Melee) += .02f * stacks;
            if (flag)
            {
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
                modPlayer.gunbladeMeleeDebuff = 1;
            }
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            return true;
        }
    }
}
