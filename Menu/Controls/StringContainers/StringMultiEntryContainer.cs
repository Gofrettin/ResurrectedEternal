using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Menu.Controls.StringContainers
{
    class StringMultiEntryContainer : StringContainer
    {
        public StringMultiEntryContainer(RectangleF _ContainerStart, ConfigValueEntry configEntry, int row, int column, float height, float width, int fontSize, float containerWidth) : base(_ContainerStart, configEntry, row, column, height, width, fontSize, containerWidth)
        {
        }

        public override void Decrement()
        {
            base.Decrement();
        }

        public override void Increment()
        {
            base.Increment();
        }

        public override void OnClick()
        {
            base.OnClick();
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
