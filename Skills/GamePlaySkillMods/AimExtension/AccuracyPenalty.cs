using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills.GamePlaySkillMods.AimExtension
{
    //this is complete garbage btw, dont ask me why
    //needs better ratios for when it should be used but you should get the general idea
    public class WeaponAccuracy
    {
        public ItemDefinitionIndex WeaponIndex;
        public float m_fHighest;
        public float m_fLowest = 1f;
        private float m_fHitChance = 101f;
        public WeaponAccuracy(ItemDefinitionIndex _index)
        {
            WeaponIndex = _index;
        }

        public bool CanShoot(float _cur, bool inScope)
        {
            if (_cur < m_fLowest)
                m_fLowest = _cur;
            if (m_fHighest == 0)
                m_fHighest = _cur * 3f;
            else if (_cur > m_fHighest || m_fHighest == 0)
                m_fHighest = _cur;
            //Console.WriteLine(_cur + "  " + m_fLowest + " - " + m_fHighest + " " + (m_fHighest - m_fLowest) * 1.25f);
            switch (Generators.GetWeaponType(WeaponIndex))
            {

                case WeaponClass.HEAVY:
                    return _cur <= m_fHighest * .888f;
                case WeaponClass.SMG:
                    return _cur <= m_fHighest * .666f;
                case WeaponClass.RIFLE:
                    return _cur <= m_fHighest * .555f;
                case WeaponClass.SNIPER:
                    if (inScope)
                        return _cur <= m_fHighest * .222f;
                    else
                        return _cur <= m_fHighest * .777f;
                case WeaponClass.PISTOL:
                    return _cur <= m_fHighest * .3111f;
                case WeaponClass.KNIFE:
                case WeaponClass.OTHER:
                default:
                    return false;
            }
        }
    }
}
