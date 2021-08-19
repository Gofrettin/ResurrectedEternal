using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.ClientObjects;
using ResurrectedEternal.GenericObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills
{

    public class xSkin
    {


        public uint fallBackPaint;
        public int m_iItemIdHigh = -1;
        public int m_iEntityQuality = 99;
        public float m_flFallBackWear = 0.0001f;
        public string m_szCustomName = Meme.GenerateName();
        public int m_iFallBackSeed = Generators.random.Next(0, int.MaxValue);
    }
    class SkillModSkin : SkillMod
    {
        private short itemId = 500;
        private uint _KnifeId = 0;
        private const int itemIdHigh = -1;
        private const int entityQuality = 99;
        private const float fallbackWear = 0.0001f;

        private Dictionary<KNIFEINDEX, ItemDefinitionIndex> ReverseDictionary = new Dictionary<KNIFEINDEX, ItemDefinitionIndex>();

        private Dictionary<ItemDefinitionIndex, xSkin> SkinDictionary = new Dictionary<ItemDefinitionIndex, xSkin>();

        public SkillModSkin(Engine engine, Client client) : base(engine, client)
        {

            for (int i = 0; i < Enum.GetValues(typeof(KNIFEINDEX)).Length; i++)
            {
                ReverseDictionary.Add((KNIFEINDEX)i, (ItemDefinitionIndex)Enum.Parse(typeof(ItemDefinitionIndex), ((KNIFEINDEX)i).ToString()));
            }
            SkinDictionary.Add(ItemDefinitionIndex.AWP, new xSkin() { fallBackPaint = 344 });
            SkinDictionary.Add(ItemDefinitionIndex.USPS, new xSkin() { fallBackPaint = 653 });
            SkinDictionary.Add(ItemDefinitionIndex.M4A4, new xSkin() { fallBackPaint = 695 });
            SkinDictionary.Add(ItemDefinitionIndex.M4A1S, new xSkin() { fallBackPaint = 548 }); //527
            SkinDictionary.Add(ItemDefinitionIndex.DEAGLE, new xSkin() { fallBackPaint = 527 });
            SkinDictionary.Add(ItemDefinitionIndex.AK47, new xSkin() { fallBackPaint = 675 });
        }

        public override bool Update()
        {

            return base.Update();
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
        }

        private int KnifeOffset;
        //private IntPtr _weaponHandle;
        //private int _currentWeaponId;
        private Random r = new Random();

        private string _name = Meme.GenerateName();

        private bool CanProcess()
        {
            if (ClientModus == Events.Modus.leaguemode
                || ClientModus == Events.Modus.streammode
                || ClientModus == Events.Modus.streammodefull)
                return false;
            return true;
        }
        public override void Before()
        {
            base.Before();
            if (!(bool)Config.ExtraConfig.enableSkins.Value)
                return;
            if (!CanProcess())
                return;

            if (!Client.UpdateModules
                || !Engine.IsInGame
                || Client.LocalPlayer == null
                || !Client.LocalPlayer.IsValid
                || !Client.LocalPlayer.m_bIsAlive)
            {
                return;
            }

            //if (_vmdraw == null)
            //    _vmdraw = new ConVar("vm_draw_addon", typeof(int));

            _KnifeId = Client.NetworkStringTable.GetModelByIndex(Generators.ModelStringByItemDefinitionIndex((KNIFEINDEX)g_Globals.Config.ExtraConfig.Knifemodel.Value));
            //itemId = Convert.ToInt16(ReverseDictionary[(KNIFEINDEX)g_Globals.Config.ExtraConfig.Knifemodel.Value]);
            KnifeOffset = _KnifeId < 10 ? 0 : 1;
            //if (Engine.GlobalVars.m_flAbsoluteFrameTime < 0.001f)

            foreach (var weapon in Client.LocalPlayer.m_hMyWeapons)
            {
                if (weapon == null || !weapon.IsValid || weapon.m_bDormant || !Generators.IsWeapon(weapon.ClientClass)) continue;
                var m_iAcountId = weapon.m_iAccountId;
                var _originalOwner = weapon.m_OriginalOwnerXuidLow;
                var _wpidx = weapon.m_iItemDefinitionIndex;
                switch (_wpidx)
                {
                    case ItemDefinitionIndex.KNIFE:
                    case ItemDefinitionIndex.KNIFE_T:
                    case ItemDefinitionIndex.KNIFE_BAYONET:
                    case ItemDefinitionIndex.KNIFE_FLIP:
                    case ItemDefinitionIndex.KNIFE_GUT:
                    case ItemDefinitionIndex.KNIFE_KARAMBIT:
                    case ItemDefinitionIndex.KNIFE_M9_BAYONET:
                    case ItemDefinitionIndex.KNIFE_TACTICAL:
                    case ItemDefinitionIndex.KNIFE_FALCHION:
                    case ItemDefinitionIndex.KNIFE_SURVIVAL_BOWIE:
                    case ItemDefinitionIndex.KNIFE_BUTTERFLY:
                    case ItemDefinitionIndex.KNIFE_PUSH:
                        if (_wpidx == (ItemDefinitionIndex)itemId)
                            break;
                        //int idx = _wpidx == ItemDefinitionIndex.KNIFE ? 90 : 65;
                        //modelIndex = Client.LocalPlayer.m_hActiveWeapon.m_nViewModelIndex + idx + 3 * _KnifeId + KnifeOffset;
                        weapon.m_iItemDefinitionIndex = ReverseDictionary[(KNIFEINDEX)g_Globals.Config.ExtraConfig.Knifemodel.Value];
                        weapon.m_nModelIndex = _KnifeId;
                        weapon.m_nViewModelIndex = _KnifeId;//Client.NetworkStringTable.GetModelByIndex(Generators.ModelStringByItemDefinitionIndex((KNIFEINDEX)g_Globals.Config.ExtraConfig.Knifemodel.Value));
                        weapon.m_iEntityQuality = entityQuality;
                        weapon.m_nFallbackSeed = r.Next(9, int.MaxValue);
                        //weapon.m_iItemIdHigh = -1;
                        //weapon.m_iItemIdLow = -1;
                        weapon.m_iAccountId = _originalOwner;
                        //weapon.m_nFallbackPaintKit = 98;
                        weapon.m_flFallbackWear = fallbackWear;
                        weapon.m_szCustomString = _name;
                        //_forceUpdate = true;
                        break;
                    default:
                        if (!SkinDictionary.ContainsKey(_wpidx))
                            continue;
                        var _skin = SkinDictionary[_wpidx];

                        if (weapon.m_nFallbackPaintKit == _skin.fallBackPaint) continue;

                        weapon.m_iItemIdHigh = -1;
                        weapon.m_nFallbackPaintKit = _skin.fallBackPaint;
                        weapon.m_flFallbackWear = _skin.m_flFallBackWear;
                        //weapon.m_OriginalOwnerXuidHigh = 0;
                        //weapon.m_OriginalOwnerXuidLow = 0;
                        weapon.m_iEntityQuality = _skin.m_iEntityQuality;
                        weapon.m_nFallbackSeed = _skin.m_iFallBackSeed;
                        weapon.m_nFallbackStatTrak = 1337;
                        weapon.m_iAccountId = _originalOwner;
                        weapon.m_szCustomString = _skin.m_szCustomName;
                        break;
                }

            }


            var _currentWeapon = Client.LocalPlayer.m_hActiveWeapon;
            if (_currentWeapon == null || !_currentWeapon.IsValid)
                return;
            if (Generators.GetWeaponType(_currentWeapon.m_iItemDefinitionIndex) == WeaponClass.KNIFE)
            {
                var _active = Client.LocalPlayer.m_hViewModelWeapon;
                if (_active == null || !_active.IsValid || _active.m_bDormant)
                    return;

                var _index = _active.m_nModelIndex;
                if (_index != _KnifeId)
                {
                    //_vmdraw.SetValue(0);
                    _active.m_nModelIndex = _KnifeId;
                    //_vmdraw.SetValue(1);
                }



            }
            //if (_forceUpdate)
            //{
            //    Engine.ForceUpdate();
            //    _forceUpdate = false;
            //}
                

        }


    }
}
