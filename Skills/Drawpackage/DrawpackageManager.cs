using ResurrectedEternal.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Skills.Drawpackage
{
    class DrawpackageManager
    {
        private Dictionary<IntPtr, DrawPackage> DrawDictionary = new Dictionary<IntPtr, DrawPackage>();
        private Dictionary<IntPtr, DrawPackage> FadeDictionary = new Dictionary<IntPtr, DrawPackage>();



        public DrawPackage Add(BasePlayer bp)
        {
            if (DrawDictionary.ContainsKey(bp.BaseAddress))
                return DrawDictionary[bp.BaseAddress];

            DrawDictionary.Add(bp.BaseAddress, new DrawPackage(bp));
            return DrawDictionary[bp.BaseAddress];
        }

        public DrawPackage[] GetPackages(BasePlayer[] players)
        {
            foreach (var item in players)
            {
                if (!item.IsValid) continue;
                if (DrawDictionary.ContainsKey(item.BaseAddress))
                {
                    DrawDictionary[item.BaseAddress].BasePlayer = item;
                    continue;
                }
                   
                DrawDictionary.Add(item.BaseAddress, new DrawPackage(item));
            }
            return DrawDictionary.Values.ToArray();
        }

        public void AddFade(IntPtr fade)
        {
            DrawDictionary[fade].StartFade();
            if (!FadeDictionary.ContainsKey(fade))
                FadeDictionary.Add(fade, DrawDictionary[fade]);
            if (DrawDictionary.ContainsKey(fade))
                DrawDictionary.Remove(fade);
            //return FadeDictionary[fade];
        }

        public void RemoveFade(IntPtr fade)
        {
            if (FadeDictionary.ContainsKey(fade))
                FadeDictionary.Remove(fade);
        }

        public DrawPackage[] GetFades()
        {
            return FadeDictionary.Values.ToArray();
        }

        public void Clear()
        {
            FadeDictionary.Clear();
            DrawDictionary.Clear();
        }

    }
}
