using ResurrectedEternal.Draw;
using RRFull;
using RRWAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ResurrectedEternal.Skills.Factory
{


    public class RenderObject : Form
    {
        public event Action OnLoad;
        public Drawing Drawing;

        private Thread _designatedThread;

        public RRWAPI.RECT _windowRect;

        public bool _loaded = false;

        private IntPtr _formHandle = IntPtr.Zero;

        public IntPtr m_dwHandle
        {
            get
            {
                if (_formHandle == IntPtr.Zero)
                    this.Invoke((MethodInvoker)delegate { _formHandle = this.Handle; });

                return _formHandle;
            }
        }

        public void Run()
        {
            _designatedThread = new Thread(() => Application.Run(this));
            _designatedThread.Name = Generators.GetRandomString(16);
            _designatedThread.SetApartmentState(ApartmentState.STA);
            _designatedThread.Priority = ThreadPriority.Normal;
            _designatedThread.Start();

        }

        public RenderObject(System.Drawing.Rectangle _rect)
        {

            _windowRect = _rect;
            this.Text = "";
            this.Name = "";
            this.AccessibleName = "";
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.Black;
            this.TopMost = true;
            this.Icon = OverlayFactory.CreateIcon();
            this.ShowInTaskbar = false;
            this.DoubleBuffered = true;
            this.TopMost = true;
            this.TopLevel = true;
            this.Shown += RenderObject_Shown;


        }
        private Margins margin;
        private void RenderObject_Shown(object sender, EventArgs e)
        {
            Drawing = new Drawing(m_dwHandle, _windowRect);
            margin = new Margins
            {
                Left = _windowRect.Location.X,
                Top = _windowRect.Location.Y,
                Bottom = _windowRect.Size.Height,
                Right = _windowRect.Size.Width
            };
            DefaultState = WAPI.GetWindowLong(m_dwHandle, -20);
            EnableClickThrough();
            //m_bIsDisabled = false;
            WAPI.SetLayeredWindowAttributes(m_dwHandle, 0, 255, WAPI.LWA_ALPHA);
            WAPI.SetWindowPos(m_dwHandle, new IntPtr(-1), margin.Left, margin.Top, margin.Right, margin.Bottom, WAPI.TOPMOST_FLAGS);
            WAPI.DwmExtendFrameIntoClientArea(m_dwHandle, ref margin);
            this.Size = _windowRect.Size;
            this.Location = _windowRect.Location;

            this.TopMost = true;
            this.TopLevel = true;
            User32.AllowSetForegroundWindow((uint)System.Diagnostics.Process.GetCurrentProcess().Id);
            User32.SetForegroundWindow(m_dwHandle);
            User32.ShowWindow(m_dwHandle, User32.SW_SHOWNORMAL);
            OnLoad?.Invoke();
            _loaded = true;


            //Console.WriteLine("Some menu loaded");
        }

        private uint DefaultState = 0;

        private bool m_bIsDisabled = true;

        //private Cursor _Cursor = Generators.LoadCursorFromResource(ResurrectedEternal.Properties.Resources.fuck);

        public void DisableClickThrough()
        {
            if (m_bIsDisabled)
                return;
            m_bIsDisabled = true;
            WAPI.SetWindowLong(
              m_dwHandle,
             WAPI.GWL_EXSTYLE,
             (IntPtr)DefaultState
             );
            WAPI.SetForegroundWindow(m_dwHandle);
        }

        public void EnableClickThrough()
        {
            if (!m_bIsDisabled)
                return;
            m_bIsDisabled = false;

            WAPI.SetWindowLong(
              m_dwHandle,
             WAPI.GWL_EXSTYLE,
             (IntPtr)(WAPI.GetWindowLong(m_dwHandle, RRWAPI.WAPI.GWL_EXSTYLE) | RRWAPI.WAPI.WS_EX_LAYERED | RRWAPI.WAPI.WS_EX_TRANSPARENT)
             );
            //if(Henker.Singleton.Memory.f)
            WAPI.SetForegroundWindow(Henker.Singleton.Memory.GetProcessWindowHandle());

        }

    }

    public static class OverlayFactory
    {
        public static System.Drawing.Icon CreateIcon()
        {
            System.Drawing.Icon ico = null;
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(32, 32))
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
                {
                    Random ran = new Random((int)Environment.TickCount);
                    g.CopyFromScreen(ran.Next(0, 512), ran.Next(0, 512), 0, 0, new System.Drawing.Size(128, 128), System.Drawing.CopyPixelOperation.SourceCopy);
                }
                ico = System.Drawing.Icon.FromHandle(bmp.GetHicon());
            }
            return ico;
        }
        public static RenderObject CreateRenderForm(System.Drawing.Rectangle _rect)
        {
            return new RenderObject(_rect);
        }

    }
}
