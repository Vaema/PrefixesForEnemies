﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Buffs.potions
{
    public class ElixirOfSteel : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elixir Of Steel");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            modPlayer.steelElixir = true;
        }
    }
}
