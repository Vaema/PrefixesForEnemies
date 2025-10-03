﻿using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs.potions
{
    public class DuelistDraught : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Duelist Draught");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            modPlayer.duelistDraught = true;
        }
    }
}
