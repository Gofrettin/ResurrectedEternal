using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.BaseObjects
{
    class MaterialModifyControl : BaseEntity
    {
        //|__m_szMaterialName__________________________________ -> 0x09D8 (char[255] )
        //|__m_szMaterialVar___________________________________ -> 0x0AD7 (char[255] )
        //|__m_szMaterialVarValue______________________________ -> 0x0BD6 (char[255] )
        //|__m_iFrameStart_____________________________________ -> 0x0CE0 (int )
        //|__m_iFrameEnd_______________________________________ -> 0x0CE4 (int )
        //|__m_bWrap___________________________________________ -> 0x0CE8 (int )
        //|__m_flFramerate_____________________________________ -> 0x0CEC (float )
        //|__m_bNewAnimCommandsSemaphore_______________________ -> 0x0CF0 (int )
        //|__m_flFloatLerpStartValue___________________________ -> 0x0CF4 (float )
        //|__m_flFloatLerpEndValue_____________________________ -> 0x0CF8 (float )
        //|__m_flFloatLerpTransitionTime_______________________ -> 0x0CFC (float )
        //|__m_bFloatLerpWrap__________________________________ -> 0x0D00 (int )
        //|__m_nModifyMode_____________________________________ -> 0x0D08 (int )
        public MaterialModifyControl(IntPtr addr, ClientClass _classid) : base(addr, _classid)
        {
        }
    }
}
