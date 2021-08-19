using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.ModuleLoader
{
    public enum Operandi
    {
        AIMBOT,
        VISUAL,
        CONVAR,
        TRIGGER,
        SKIN,
        VISIBLE,
        NEON,
        CHAM,
        BUNNY,
        CASCADELIGHT,
        FOG,
        SUN,
        VIEW,
        WIND,
        POSTPROCESS,
    }
    public enum OperandiAction
    {
        Add,
        Remove
    }
    public class ModuleOperand
    {
        public Operandi Operand;
        public OperandiAction Action;
        public ModuleOperand(Operandi _operandi, OperandiAction _action)
        {
            Operand = _operandi;
            Action = _action;
        }
    }
    public static class ModuleLoader
    {
        private static Queue<ModuleOperand> moduleOperands = new Queue<ModuleOperand>();

        public static void Add(Operandi _operand, OperandiAction _action)
        {

        }

        public static void Add(ModuleOperand _operand)
        {

        }

        public static void Add(ModuleOperand[] _operands)
        {

        }

    }
}
