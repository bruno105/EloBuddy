﻿using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Utils;
using System.Linq;

namespace KTalon
{
    internal class Itens
    {
       
        public static readonly Item Youmuu = new Item((int)ItemId.Youmuus_Ghostblade);
        public static readonly Item Tiamat = new Item((int)ItemId.Tiamat);
        public static readonly Item Hydra = new Item((int)ItemId.Titanic_Hydra);
        public static readonly Item botrk = new Item((int)ItemId.Blade_of_the_Ruined_King);
        public static readonly Item alfange = new Item((int)ItemId.Bilgewater_Cutlass);
        public static void UseItens()
        {
            var E = Program.E;
            var alvo = TargetSelector.GetTarget((E.Range + 300), DamageType.Physical);
            if (Program._Player.Distance(alvo) <= E.Range + 300 )
            {
                if (Youmuu.IsOwned())
                {
                    Youmuu.Cast();

                }
            }
            if (botrk.IsOwned() && botrk.IsInRange(alvo))
            {
                botrk.Cast(alvo);

            }
            if (Tiamat.IsOwned() && Tiamat.IsInRange(alvo))
            {
                Tiamat.Cast();

            }
            if (Hydra.IsOwned() && Hydra.IsInRange(alvo))
            {
                Hydra.Cast();

            }
            if (alfange.IsOwned() && alfange.IsInRange(alvo))
            {
                alfange.Cast();

            }

        
        }






    }
}