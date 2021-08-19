using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu
{
    public static class EnumCaster
    {
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static T Previous<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) - 1;
            return (j < 0) ? Arr[Arr.Length - 1] : Arr[j];
        }
    }

    public class MenuConfigCaster
    {

        private Dictionary<int, ushort> KeyDictionary = new Dictionary<int, ushort>();

        public MenuConfigCaster()
        {
            int index = 0;
            foreach (var item in Enum.GetValues(typeof(VirtualKeys)))
            {
                KeyDictionary.Add(index, CastObject<ushort>(item));
                index++;
            }
        }

        public ushort GetKeyByIndex(int index)
        {
            return KeyDictionary[index];
        }

        public T CastObject<T>(object input)
        {
            return (T)input;
        }

        public T ConvertObject<T>(object input)
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }
        public string CorrectValue(ConfigValueEntry _entry)
        {
            return GetString(_entry.Value.GetType(), _entry.Value);
        }

        private string GetString(Type type, object value)
        {
            if (type == typeof(float))
            {
                return ((float)value).ToString("0.00");
            }
            else if (type == typeof(int))
            {
                return value.ToString();
            }
            else if (type == typeof(bool))
            {
                return (bool)value ? "On" : "Off";
            }
            else if (type == typeof(TargetType) || type == typeof(SmoothType))
            {
                return value.ToString();
            }
            else if(type == typeof(AimPriority))
            {
                switch ((AimPriority)value)
                {
                    case AimPriority.ClosestToCrosshair:
                        return "Closest to Crosshair";
                    case AimPriority.DistanceToSelf:
                        return "Distance to Player";
                    default:
                        return "Why so serious?";
                }
            }
            else if(type == typeof(SharpDX.Color))
            {
                return g_Globals.ColorManager.GetNameByColor((SharpDX.Color)value);
            }
            else if(type == typeof(GlowRenderStyle_t))
            {
                switch ((GlowRenderStyle_t)value)
                {

                    case GlowRenderStyle_t.GLOWRENDERSTYLE_RIMGLOW3D:
                        return "Rim Glow";
                    case GlowRenderStyle_t.GLOWRENDERSTYLE_EDGE_HIGHLIGHT:
                        return "Edge Highlight";
                    case GlowRenderStyle_t.GLOWRENDERSTYLE_EDGE_HIGHLIGHT_PULSE:
                        return "Edge Highlight Pulse";

                    case GlowRenderStyle_t.GLOWRENDERSTYLE_DEFAULT:
                        return "Glow";
                    default:

                        return "Off";
                }
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
