using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls
{
    public enum StringContainerType
    {
        None,
        Header,
        Entry,
        MultiEntry
    }
    public class StringContainer
    {
        public StringContainerType StringContainerType;
        public ConfigValueEntry ConfigValue;
        public RectangleF Rect;

        public MenuConfigCaster MenuConfigCaster = new MenuConfigCaster();



        public Color ForeColor;
        public Color ValueForeColor = Color.LightSteelBlue;
        //private RectangleF FontRect;


        public bool Active = false;
        public int StringContainerFontSize;
        public virtual Vector2 m_vRenderPosition
        {
            get
            {
                return Vector2.Zero;
            }
        }
        //calc offset on increasing values.
        public virtual Vector2 m_vValueRenderPosition
        {
            get { return Vector2.Zero; }
        }

        public virtual void Increment()
        {
            
        }

        public virtual void Decrement()
        {
            
        }

        public virtual void OnClick()
        {
            
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }
        

        public StringContainer(RectangleF _ContainerStart, ConfigValueEntry configEntry, int row, int column, float height, float width, int fontSize, float containerWidth)
        {
        }



    }
}
