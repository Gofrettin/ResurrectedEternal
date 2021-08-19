using ResurrectedEternal.BaseObjects;
using ResurrectedEternal.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal
{
    public static class Cache
    {
        public static Dictionary<string, Dictionary<int, mstudiobbox_t>> m_ModelHitboxes = new Dictionary<string, Dictionary<int, mstudiobbox_t>>();

        public static mstudiobbox_t GetHitBox(BasePlayer pEntity, string szModelName, int iIndex)
        {
            if (m_ModelHitboxes.ContainsKey(szModelName))
                if (m_ModelHitboxes[szModelName].ContainsKey(iIndex))
                    return m_ModelHitboxes[szModelName][iIndex];
                else
                    return default(mstudiobbox_t);

            IntPtr pStudioHdr = MemoryLoader.instance.Reader.Read<IntPtr>(pEntity.m_nStudioHdr);

            if (pStudioHdr == IntPtr.Zero)
                return default(mstudiobbox_t);

            int hitbox_set_index = MemoryLoader.instance.Reader.Read<int>(pStudioHdr + 0xB0);

            int studio_hitbox_set = (int)pStudioHdr + hitbox_set_index;

            int num_hitboxes = MemoryLoader.instance.Reader.Read<int>(new IntPtr(studio_hitbox_set + 0x4));

            int hitbox_index = MemoryLoader.instance.Reader.Read<int>(new IntPtr(studio_hitbox_set + 0x8));

            m_ModelHitboxes.Add(szModelName, new Dictionary<int, mstudiobbox_t>());

            for (int i = 0; i < num_hitboxes; ++i)
            {
                mstudiobbox_t model_hitbox = MemoryLoader.instance.Reader.Read<mstudiobbox_t>(new IntPtr((0x44 * i) + hitbox_index + studio_hitbox_set));
                float radius = model_hitbox.m_flRadius;
                if (radius != -1f)
                {
                    model_hitbox.bbmin.x -= radius;
                    model_hitbox.bbmin.y -= radius;
                    model_hitbox.bbmin.z -= radius;

                    model_hitbox.bbmin.x += radius;
                    model_hitbox.bbmin.y += radius;
                    model_hitbox.bbmin.z += radius;
                }
                m_ModelHitboxes[szModelName].Add(i, model_hitbox);
            }

            if (m_ModelHitboxes[szModelName].ContainsKey(iIndex))
                return m_ModelHitboxes[szModelName][iIndex];
            else
                return default(mstudiobbox_t);
        }

    }
}
