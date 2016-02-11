﻿using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using System.Linq;


namespace KKayle
{
    public class ModeManager
    {


        public static void Combo()
        {
            var Q = Program.Q;
            var W = Program.W;
            var E = Program.E;
            var R = Program.R;
            var alvo = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            if (!alvo.IsValid()) return;

            if (Q.IsReady() && Q.IsInRange(alvo))
            {
                Q.Cast(alvo);
            }
            if (W.IsReady() && W.IsInRange(alvo))
            {
                W.Cast(Player.Instance);
            }
            if (E.IsReady() && Program._Player.Distance(alvo) <= Program._Player.GetAutoAttackRange() + 400)
            {
                E.Cast();
            }
        }

        public static void Harass()
        {
            var Q = Program.Q;
            var W = Program.W;
            var E = Program.E;
            var R = Program.R;
            var alvo = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            if (!alvo.IsValid()) return;
            if (!(Player.Instance.ManaPercent > Program.HarassMenu["ManaH"].Cast<Slider>().CurrentValue))
            {
                return;
            }
            if (Q.IsReady() && Q.IsInRange(alvo) && Program.HarassMenu["HarassQ"].Cast<CheckBox>().CurrentValue)
            {
                Q.Cast(alvo);
            }
            if (W.IsReady() && W.IsInRange(alvo) && Program.HarassMenu["HarassW"].Cast<CheckBox>().CurrentValue)
            {
                W.Cast(Player.Instance);
            }
            if (E.IsReady() && (Program._Player.Distance(alvo) <= Program._Player.GetAutoAttackRange() + 400) && Program.HarassMenu["HarassE"].Cast<CheckBox>().CurrentValue)
            {
                E.Cast();
            }
        }

        public static void LaneClear()
        {
            var Q = Program.Q;
            var W = Program.W;
            var E = Program.E;
            var R = Program.R;
            var minion = EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(m => m.IsValidTarget(E.Range));
            var Cminion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsInRange(Player.Instance.Position, Q.Range) && !t.IsDead && t.IsValid && !t.IsInvulnerable).Count();
            if (minion == null) return;
            if (!(Player.Instance.ManaPercent > Program.FarmMenu["ManaF"].Cast<Slider>().CurrentValue))
            {
                return;
            }
            if (Q.IsReady() && Program.FarmMenu["FarmQ"].Cast<CheckBox>().CurrentValue && Q.IsInRange(minion) && minion.IsValidTarget(Q.Range))
            {
                    Q.Cast(minion);
                
                if (E.IsReady() && Program.FarmMenu["FarmE"].Cast<CheckBox>().CurrentValue && minion.IsValidTarget(Q.Range) && (Cminion >= Program.FarmMenu["MinionE"].Cast<Slider>().CurrentValue))
                {
                    E.Cast();
                }

            }

        }


        public static void JungleClear()
        {
            var Q = Program.Q;
            var W = Program.W;
            var E = Program.E;
            var R = Program.R;
            var jungleMonsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(j => j.Health).FirstOrDefault(j => j.IsValidTarget(Program.Q.Range));
            var Cminion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsInRange(Player.Instance.Position, Q.Range) && !t.IsDead && t.IsValid && !t.IsInvulnerable).Count();
            if (jungleMonsters == null) return;
            if (!(Player.Instance.ManaPercent > Program.FarmMenu["ManaF"].Cast<Slider>().CurrentValue))
            {
                return;
            }
            if (Q.IsReady() && Program.FarmMenu["FarmQ"].Cast<CheckBox>().CurrentValue && Q.IsInRange(jungleMonsters) && jungleMonsters.IsValidTarget(Q.Range))
            {
                Q.Cast(jungleMonsters);

                if (E.IsReady() && Program.FarmMenu["FarmE"].Cast<CheckBox>().CurrentValue && jungleMonsters.IsValidTarget(Q.Range))
                {
                    E.Cast();
                }

            }
        }

             public static void LastHit()
             {

                 var Q = Program.Q;
                 var qminions = EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(m => m.IsValidTarget(Program.Q.Range) && (DamageLib.QCalc(m) > m.Health));
                 if (qminions == null) return;

                 if (Q.IsReady() && Program.Q.IsInRange(qminions) && Program.FarmMenu["LastQ"].Cast<CheckBox>().CurrentValue && qminions.Health < DamageLib.QCalc(qminions))

                     Q.Cast(qminions);
             }

             public static void AutoHeal()
             {


            var Q = Program.Q;
            var W = Program.W;
            var E = Program.E;
            var R = Program.R;
            
            if (!W.IsReady())
            {
                return;
            }

            var lowestHealthAlly = EntityManager.Heroes.Allies.Where(a => W.IsInRange(a) && !a.IsMe).OrderBy(a => a.Health).FirstOrDefault();

            if (Program.HealthPercent() <= Program.HealMenu["HealSelf"].Cast<Slider>().CurrentValue)
            {
                W.Cast(Program.PlayerInstance);
            }

            else if (lowestHealthAlly != null)
            {
                if (!(lowestHealthAlly.Health <= Program.HealMenu["HealAlly"].Cast<Slider>().CurrentValue))
                {
                    return;
                }
                if (Program.HealMenu["autoHeal_" + lowestHealthAlly.BaseSkinName].Cast<CheckBox>().CurrentValue)
                {
                    W.Cast(lowestHealthAlly);
                }
            }
          }
            public static void AutoUlt()
             {
                 var Q = Program.Q;
                 var W = Program.W;
                 var E = Program.E;
                 var R = Program.R;
                 if (!R.IsReady() || Player.Instance.IsRecalling())
                 {
                     return;
                 }

                 var lowestHealthAllies = EntityManager.Heroes.Allies.Where(a => R.IsInRange(a) && !a.IsMe).OrderBy(a => a.Health).FirstOrDefault();

                 if (Player.Instance.HealthPercent <= Program.UltMenu["UltSelf"].Cast<Slider>().CurrentValue)
                 {
                     R.Cast(Player.Instance);
                 }

                 if (lowestHealthAllies == null)
                 {
                     return;
                 }

                 if (!(lowestHealthAllies.Health <= Program.UltMenu["UltAlly"].Cast<Slider>().CurrentValue))
                 {
                     return;
                 }
                 if (Program.UltMenu["autoUlt_" + lowestHealthAllies.BaseSkinName].Cast<CheckBox>().CurrentValue)
                 {
                     R.Cast(lowestHealthAllies);
                 }
             }



     }
}





        
