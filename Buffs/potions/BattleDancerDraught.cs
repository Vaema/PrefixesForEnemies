using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Buffs.potions
{
    public class BattleDancerDraught : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Battle Dancer");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            modPlayer.battleDance = true;
        }
    }
}
