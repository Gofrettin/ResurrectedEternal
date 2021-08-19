using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace ResurrectedEternal.Draw
{
    public class Drawing
    {
        public static Drawing instance;
        public Size2 Size;

        private WindowRenderTarget Device;
        private HwndRenderTargetProperties renderProperties;

        private SharpDX.Direct2D1.Factory factory = new SharpDX.Direct2D1.Factory();
        private SharpDX.DirectWrite.Factory fontFactory = new SharpDX.DirectWrite.Factory();
        private CustomColorRenderer customRenderer = new CustomColorRenderer();

        public string Fontname => (string)g_Globals.Config.VisualConfig.Font.Value;
        public FontStyle FontStyle => (FontStyle)g_Globals.Config.VisualConfig.Font.Value;
        public FontWeight FontWeight => (FontWeight)g_Globals.Config.VisualConfig.Font.Value;

        public int FontSize => (int)g_Globals.Config.VisualConfig.FontSize.Value;

        private FontAndColorManager FontAndColorManager;
        private ConcurrentDictionary<string, RectangleF> m_cdFontCache = new ConcurrentDictionary<string, RectangleF>();
        public Drawing(IntPtr DeviceHandle, System.Drawing.Rectangle _rect)
        {

            instance = this;
            Size = new Size2(_rect.Width, _rect.Height);
            renderProperties = new HwndRenderTargetProperties()
            {
                Hwnd = DeviceHandle,
                PixelSize = Size,
                PresentOptions = PresentOptions.Immediately
            };
            Device = new WindowRenderTarget(
                factory,
                new RenderTargetProperties(new PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied)),
                renderProperties);

            Device.TextRenderingParams = new RenderingParams(fontFactory, 2.000f, 100f, 100f, PixelGeometry.Flat, RenderingMode.Outline);
            Device.AntialiasMode = AntialiasMode.PerPrimitive;
            FontAndColorManager = new FontAndColorManager(Device, fontFactory);

            FontAndColorManager.AddFont(
                "Calibri",
                14,
                FontWeight.Bold,
                FontStyle.Normal);

            customRenderer.AssignResources(Device, new MultiBrush(FontAndColorManager.GetBrush(Color.White), FontAndColorManager.GetBrush(Color.Black)));

        }

        private bool _hasDrawn = false;

        public void BeginDraw()
        {
            if (Device == null)
                return;
            _hasDrawn = true;
            Device.BeginDraw();
            Device.Clear(SharpDX.Color.Transparent);
        }

        public void EndDraw()
        {
            if (Device == null || !_hasDrawn)
                return;
            Device.EndDraw();
        }
        public void DrawText(string text, int fontSize, SharpDX.Color color, float posX, float posY)
        {
            var _font = FontAndColorManager.GetFont(Fontname, fontSize);
            Device.DrawText(text, _font, MeasureString(_font, text, posX, posY), FontAndColorManager.GetBrush(color));

        }
        public void DrawText(string text, SharpDX.Color color, Vector2 position)
        {
            if (string.IsNullOrEmpty(text))
                return;
            position.X = Convert.ToInt32(EngineMath.Clamp(position.X, int.MinValue, int.MaxValue));
            position.Y = Convert.ToInt32(EngineMath.Clamp(position.Y, int.MinValue, int.MaxValue));
            DrawGlyphs(text, color, position, FontSize, Fontname);
            return;
            //SharpDX.Mathematics.Interop.RawRectangleF rect = new SharpDX.Mathematics.Interop.RawRectangleF((int)theVecs.X, (int)theVecs.Y, width, height);
            //Device.DrawText(text, FontAndColorManager.GetFont(g_Globals.Config.sFontName), MeasureString(FontAndColorManager.GetFont(g_Globals.Config.sFontName), text, position), FontAndColorManager.GetBrush(color));

        }
        public void DrawText(string text, SharpDX.Color color, Vector2 position, int fontSize)
        {
            if (string.IsNullOrEmpty(text))
                return;
            position.X = Convert.ToInt32(EngineMath.Clamp(position.X, int.MinValue, int.MaxValue));
            position.Y = Convert.ToInt32(EngineMath.Clamp(position.Y, int.MinValue, int.MaxValue));
            DrawGlyphs(text, color, position, fontSize, Fontname);
            return;
            //SharpDX.Mathematics.Interop.RawRectangleF rect = new SharpDX.Mathematics.Interop.RawRectangleF((int)theVecs.X, (int)theVecs.Y, width, height);
            //Device.DrawText(text, FontAndColorManager.GetFont(g_Globals.Config.sFontName), MeasureString(FontAndColorManager.GetFont(g_Globals.Config.sFontName), text, position), FontAndColorManager.GetBrush(color));

        }
        public void DrawText(string text, SharpDX.Color color, Vector2 position, string fontName, int fontSize)
        {
            if (string.IsNullOrEmpty(text))
                return;
            position.X = Convert.ToInt32(EngineMath.Clamp(position.X, int.MinValue, int.MaxValue));
            position.Y = Convert.ToInt32(EngineMath.Clamp(position.Y, int.MinValue, int.MaxValue));
            DrawGlyphs(text, color, position, fontSize, fontName);
            return;
            //SharpDX.Mathematics.Interop.RawRectangleF rect = new SharpDX.Mathematics.Interop.RawRectangleF((int)theVecs.X, (int)theVecs.Y, width, height);
            //Device.DrawText(text, FontAndColorManager.GetFont(g_Globals.Config.sFontName), MeasureString(FontAndColorManager.GetFont(g_Globals.Config.sFontName), text, position), FontAndColorManager.GetBrush(color));

        }
        public void DrawText(string text, SharpDX.Color color, Vector2 position, string fontName)
        {
            if (string.IsNullOrEmpty(text))
                return;
            position.X = Convert.ToInt32(EngineMath.Clamp(position.X, int.MinValue, int.MaxValue));
            position.Y = Convert.ToInt32(EngineMath.Clamp(position.Y, int.MinValue, int.MaxValue));
            DrawGlyphs(text, color, position, FontSize, fontName);
            return;
            //SharpDX.Mathematics.Interop.RawRectangleF rect = new SharpDX.Mathematics.Interop.RawRectangleF((int)theVecs.X, (int)theVecs.Y, width, height);
            //Device.DrawText(text, FontAndColorManager.GetFont(g_Globals.Config.sFontName), MeasureString(FontAndColorManager.GetFont(g_Globals.Config.sFontName), text, position), FontAndColorManager.GetBrush(color));

        }
        public void DrawGlyphs(string text, SharpDX.Color color, Vector2 position, int fontSize, string fontName)
        {
            //Device.DrawText(text, FontAndColorManager.GetFont(g_Globals.Config.FontConfig.sFontName), MeasureString(FontAndColorManager.GetFont(Config.FontConfig.sFontName), text, position), FontAndColorManager.GetBrush(color));
            //return;
            TextLayout tl = FontAndColorManager.GetText(text, fontName, fontSize, color, Color.Black, this);
            tl.Draw(customRenderer, position.X, position.Y);

            //Device.DrawGlyphRun(new RawVector2(position.X, position.Y), new GlyphRun().)
        }

        internal void UpdatePixelSize(Size2 newSize)
        {
            //Device.PixelSize = newSize;
        }

        public void DrawLine(Vector2 start, Vector2 end, float width, SharpDX.Color color)
        {
            Device.DrawLine(start, end, FontAndColorManager.GetBrush(color), width);

        }
        public void DrawLine(float x1, float y1, float x2, float y2, float w, SharpDX.Color color)
        {
            Device.DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), FontAndColorManager.GetBrush(color), w);
        }

        public void DrawCircle(RawVector2 center, float radX, float radY, Color color)
        {

            Device.DrawEllipse(new Ellipse(center, radX, radY), FontAndColorManager.GetBrush(color));

        }

        public void DrawDot(RawVector2 position, float radX, float radY, Color color)
        {
            //Ellipse e = new ;

            Device.FillEllipse(new Ellipse(position, radX, radY), FontAndColorManager.GetBrush(color));

        }

        RectangleF NextBox = new RectangleF();
        RectangleF OutlineNextBox = new RectangleF();

        float thicknessOffset = 1f;
        float espHeight = 0f;
        float espHeightOffset = 10;


        float GetDistanceOffset(float dist)
        {
            //The further away the smaller the value must be, 10 is a good play ratio
            //The closer it gets the bigger the value must be
            if (dist > 10f)
                return 11f * (dist / 100);
            return dist / 10 * 20;
        }

        public void FillRectangle(RectangleF _rect, Color c)
        {
            Device.FillRectangle(_rect, FontAndColorManager.GetBrush(c));
        }

        public void FillRectangleWithOutline(RectangleF _rect, Color c)
        {
            AddPaddingToRect(ref _rect);
            Device.DrawRectangle(_rect, FontAndColorManager.GetBrush(Color.Black), 1);
            RemovePaddingFromRect(ref _rect);
            Device.FillRectangle(_rect, FontAndColorManager.GetBrush(c));

        }

        public void DrawRectagleWithOutline(RectangleF _rect, Color color)
        {
            AddPaddingToRect(ref _rect);
            Device.DrawRectangle(_rect, FontAndColorManager.GetBrush((Color)g_Globals.Config.OtherConfig.cBoundingBoxOutlineColor.Value), 1);
            RemovePaddingFromRect(ref _rect);
            Device.DrawRectangle(_rect, FontAndColorManager.GetBrush(color), (float)g_Globals.Config.VisualConfig.LineThickness.Value);

        }
        public void DrawRoundedRectangle(RectangleF _rect, float radX, float radY, Color color)
        {
            Device.FillRoundedRectangle(new RoundedRectangle()
            {
                RadiusX = radX,
                RadiusY = radY,
                Rect = _rect

            }, FontAndColorManager.GetBrush(color));
            //Device.DrawRectangle(_rect,, (float)g_Globals.Config.VisualConfig.LineThickness.Value);
        }

        public void DrawRectagle(RectangleF _rect, Color color)
        {
            Device.DrawRectangle(_rect, FontAndColorManager.GetBrush(color), (float)g_Globals.Config.VisualConfig.LineThickness.Value);
        }
        private void AddPaddingToRect(ref RectangleF _Rect)
        {
            _Rect.X -= (float)g_Globals.Config.VisualConfig.LineThickness.Value;
            _Rect.Y -= (float)g_Globals.Config.VisualConfig.LineThickness.Value;
            _Rect.Width += (float)g_Globals.Config.VisualConfig.LineThickness.Value * 2;
            _Rect.Height += (float)g_Globals.Config.VisualConfig.LineThickness.Value * 2;
        }
        private void RemovePaddingFromRect(ref RectangleF _Rect)
        {
            _Rect.X += (float)g_Globals.Config.VisualConfig.LineThickness.Value;
            _Rect.Y += (float)g_Globals.Config.VisualConfig.LineThickness.Value;
            _Rect.Width -= (float)g_Globals.Config.VisualConfig.LineThickness.Value * 2;
            _Rect.Height -= (float)g_Globals.Config.VisualConfig.LineThickness.Value * 2;
        }
        public void DrawRectangle(Vector2 PPOS, Vector2 theHead, SharpDX.Color pColor, float distance)
        {
            distance *= 0.01905f;
            thicknessOffset = (float)g_Globals.Config.VisualConfig.LineThickness.Value;
            espHeight = theHead.Y - PPOS.Y;
            espHeightOffset = .333f;

            OutlineNextBox = new RectangleF(
                PPOS.X + (espHeight / 4) - (thicknessOffset),
                (theHead.Y - espHeightOffset) - (thicknessOffset),
                EngineMath.GetDistanceToPoint(new Vector2(PPOS.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(PPOS.X - (espHeight / 4), (theHead.Y - espHeightOffset)) + (1.8f * thicknessOffset)), //Distance
                EngineMath.GetDistanceToPoint(new Vector2(PPOS.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(PPOS.X - (espHeight / 4), PPOS.Y) + (1.3f * thicknessOffset)) //Distance
                );


            Device.DrawRectangle(OutlineNextBox, FontAndColorManager.GetBrush((Color)g_Globals.Config.OtherConfig.cBoundingBoxOutlineColor.Value), (float)g_Globals.Config.VisualConfig.LineThickness.Value / 2f);

            OutlineNextBox = new RectangleF(
                PPOS.X + (espHeight / 4) + (thicknessOffset),
                (theHead.Y - espHeightOffset) + (thicknessOffset),
                EngineMath.GetDistanceToPoint(new Vector2(PPOS.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(PPOS.X - (espHeight / 4), (theHead.Y - espHeightOffset)) - (1.8f * thicknessOffset)), //Distance
                EngineMath.GetDistanceToPoint(new Vector2(PPOS.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(PPOS.X - (espHeight / 4), PPOS.Y) - (1.3f * thicknessOffset)) //Distance
                );
            Device.DrawRectangle(OutlineNextBox, FontAndColorManager.GetBrush((Color)g_Globals.Config.OtherConfig.cBoundingBoxOutlineColor.Value), (float)g_Globals.Config.VisualConfig.LineThickness.Value / 2f);

            NextBox = new RectangleF(
                PPOS.X + (espHeight / 4),
                (theHead.Y - espHeightOffset),
                EngineMath.GetDistanceToPoint(new Vector2(PPOS.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(PPOS.X - (espHeight / 4), (theHead.Y - espHeightOffset))), //Distance
                EngineMath.GetDistanceToPoint(new Vector2(PPOS.X + (espHeight / 4), (theHead.Y - espHeightOffset)), new Vector2(PPOS.X - (espHeight / 4), PPOS.Y)) //Distance
                );
            Device.DrawRectangle(NextBox, FontAndColorManager.GetBrush(pColor), (float)g_Globals.Config.VisualConfig.LineThickness.Value);

        }


        public void DrawRectangle(Vector2 position, float size, SharpDX.Color color)
        {
            RectangleF NextRectangle = new RectangleF(position.X - size, position.Y - size, size * 2, size * 2);
            Device.FillRectangle(new RawRectangleF(NextRectangle.Left, NextRectangle.Top, NextRectangle.Right, NextRectangle.Bottom), FontAndColorManager.GetBrush(color));
        }
        public void DrawRectangle(RawRectangleF pos, SharpDX.Color color)
        {
            Device.FillRectangle(pos, FontAndColorManager.GetBrush(color));
        }
        public void DrawRectangleOutline(RawRectangleF pos, float strokeWidth, SharpDX.Color color)
        {
            Device.DrawRectangle(pos, FontAndColorManager.GetBrush(color), strokeWidth);
        }
        public void DrawRectangleOutline(Vector2 position, float size, SharpDX.Color color)
        {
            RectangleF NextRectangle = new RectangleF((position.X - 1) - 2f, (position.Y - 1) - 2f, (1 * 2) + 2f, (1 * 2) + 2f);
            Device.DrawRectangle(NextRectangle, FontAndColorManager.GetBrush(color), 1f);
        }

        public void DrawBox(Vector2 PPOS, Vector2 theHead, float distance, SharpDX.Color pColor)
        {
            DrawRectangle(PPOS, theHead, pColor, distance);
            return;
            //Top Line
            //DrawLine(PPOS.X - (height / 4), (theHead.Y - 10), PPOS.X + (height / 4), (theHead.Y - 10), Program.Config.fESPLineThickness, pColor);
            //Left Down
            //DrawLine(PPOS.X + (height / 4), (theHead.Y - 10), PPOS.X + (height / 4), PPOS.Y, Program.Config.fESPLineThickness, pColor);
            //Right Down
            //Drawing.DrawDot(new RawVector2(PPOS.X - (height / 4), (theHead.Y - 10)), 2f, 2f, Color.Red);
            //Drawing.DrawDot(new RawVector2(PPOS.X - (height / 4), PPOS.Y), 2f, 2f, Color.Green);
            //DrawLine(PPOS.X - (height / 4), (theHead.Y - 10), PPOS.X - (height / 4), PPOS.Y, Program.Config.fESPLineThickness, pColor);

            //Bottom Line
            //DrawLine(PPOS.X - (height / 4), PPOS.Y, PPOS.X + (height / 4), PPOS.Y, Program.Config.fESPLineThickness, pColor);


        }

        private void DrawCrossHairOutline(RawRectangleF topDown, RawRectangleF leftRight, Color c, float strokeWidth)
        {
            strokeWidth += .1f;
            topDown.Top -= strokeWidth;
            topDown.Bottom += strokeWidth;
            topDown.Left -= strokeWidth;
            topDown.Right += strokeWidth;
            DrawRectangleOutline(topDown, strokeWidth, c);
            leftRight.Top -= strokeWidth;
            leftRight.Bottom += strokeWidth;
            leftRight.Left -= strokeWidth;
            leftRight.Right += strokeWidth;
            DrawRectangleOutline(leftRight, strokeWidth, c);
            leftRight.Top += strokeWidth;
            leftRight.Bottom -= strokeWidth;
            leftRight.Left += strokeWidth;
            leftRight.Right -= strokeWidth;
            topDown.Top += strokeWidth;
            topDown.Bottom -= strokeWidth;
            topDown.Left += strokeWidth;
            topDown.Right -= strokeWidth;
        }
        public void DrawCrosshair(Vector2 ScreenMid, float Length, float Width, SharpDX.Color cColor, Color crossColor)
        {
            //ScreenMid.X -= 1f;
            //ScreenMid.Y -= 1f;
            float hCross = ScreenMid.X - (Width / 2);
            float wCross = ScreenMid.Y - (Length * 2);
            RawRectangleF rTopDown = new RawRectangleF(hCross, wCross, hCross + Width, ScreenMid.Y + (Length * 2));
            float sidewCross = ScreenMid.Y - (Width / 2);
            float sidehCross = ScreenMid.X - (Length * 2);
            RawRectangleF rLeftRight = new RawRectangleF(sidehCross,
                    ScreenMid.Y - Width / 2,
                    sidehCross + (Length * 4),
                    sidewCross + Width);
            DrawCrossHairOutline(rTopDown, rLeftRight, crossColor, .555f);
            //DrawDot(new RawVector2(hCross, wCross), 2, 2, Color.Black);
            //DrawDot(new RawVector2(wCross - Width, ScreenMid.Y + (Length * 2)), 2, 2, Color.Black);
            DrawRectangle(rTopDown, cColor);


            DrawRectangle(rLeftRight, cColor);

            //DrawDot(new RawVector2(ScreenMid.X - (Length * 2), ScreenMid.Y - (Width * 2)), 2, 2, Color.Black);
            //DrawRectangle(new RawRectangleF(ScreenMid.X, ScreenMid.Y + Length + 1, ScreenMid.X + Width, ScreenMid.Y - Length + 1), cColor);
            //DrawRectangleOutline(new RawRectangleF(ScreenMid.X, ScreenMid.Y - Length - 1, ScreenMid.X + Width, ScreenMid.Y - Length + 1), .555f, Color.Black);
            //DrawRectangleOutline(new RawRectangleF(ScreenMid.X - Length - 1, ScreenMid.Y, ScreenMid.X - Length + 1, ScreenMid.Y + Width), .555f, Color.Black);
            //DrawLine(new Vector2(ScreenMid.X + Length, ScreenMid.Y), new Vector2(ScreenMid.X - Length, ScreenMid.Y), Width, cColor);
            //DrawLine(new Vector2(ScreenMid.X, ScreenMid.Y + Length), new Vector2(ScreenMid.X, ScreenMid.Y - Length), Width, cColor);
        }

        public SharpDX.RectangleF MeasureString(TextFormat format, string text, float maxWidth, float maxHeight, float posX, float posY)
        {
            SharpDX.RectangleF rect;
            using (TextLayout layout = new TextLayout(fontFactory, text, format, maxWidth, maxHeight))
            {
                rect = new SharpDX.RectangleF(posX, posY, layout.Metrics.Width + .5f, layout.Metrics.Height + .5f);
            }
            return rect;
        }

        public SharpDX.RectangleF MeasureString(TextFormat format, string text, float posX, float posY)
        {
            return MeasureString(format, text, float.MaxValue, float.MaxValue, posX, posY);
        }
        public SharpDX.RectangleF MeasureString(TextFormat format, string text, Vector2 pos)
        {
            return MeasureString(format, text, float.MaxValue, float.MaxValue, pos.X, pos.Y);
        }
        public SharpDX.RectangleF MeasureString(string text, int fontSize)
        {
            return MeasureString(text, float.MaxValue, float.MaxValue, fontSize);
        }
        

        public SharpDX.RectangleF MeasureString(string text, float maxWidth, float maxHeight, int fontSize)
        {
            if (m_cdFontCache.ContainsKey(text + fontSize))
                return m_cdFontCache[text + fontSize];

            //create a textlayout dictionary to cache already measured strings.
            //Remember to clear the dictioanry if the font size changes
            SharpDX.RectangleF rect;
            using (TextLayout layout = new TextLayout(fontFactory, text, FontAndColorManager.GetFont(Fontname, fontSize), maxWidth, maxHeight))
            {
                rect = new SharpDX.RectangleF(0, 0, layout.Metrics.Width + .5f, layout.Metrics.Height + .5f);
            }
            m_cdFontCache.TryAdd(text + fontSize, rect);
            return rect;
        }

        public void Clear()
        {
            Device.Dispose();
        }
    }

    public class FontAndColorManager
    {
        private Dictionary<SharpDX.Color, SolidColorBrush> BrushDictionary = new Dictionary<SharpDX.Color, SolidColorBrush>();
        private Dictionary<string, TextFormat> TextFormatDictionary = new Dictionary<string, TextFormat>();
        private WindowRenderTarget ReferencedDrawingDevice;
        private SharpDX.DirectWrite.Factory ReferencedFontFactory;
        private Dictionary<string, TextLayout> TextLayoutDictionary = new Dictionary<string, TextLayout>();
        private Dictionary<string, MultiBrush> MultiBrushDictionary = new Dictionary<string, MultiBrush>();



        public FontAndColorManager(WindowRenderTarget d, SharpDX.DirectWrite.Factory f)
        {
            ReferencedDrawingDevice = d;
            ReferencedFontFactory = f;
        }

        public SolidColorBrush GetBrush(SharpDX.Color clr)
        {
            if (!BrushDictionary.ContainsKey(clr))
                AddBrush(clr);
            return BrushDictionary[clr];
        }

        public TextFormat GetFont(string fontName, int fontSize)
        {
            if (!TextFormatDictionary.ContainsKey(fontName + fontSize ))
            {
                AddFont(fontName, fontSize, (FontWeight)g_Globals.Config.VisualConfig.FontWeight.Value, (FontStyle)g_Globals.Config.VisualConfig.FontStyle.Value);
            }
            return TextFormatDictionary[fontName + fontSize];
        }

        public TextLayout GetText(string text, string fontName, int fontSize, SharpDX.Color baseColor, SharpDX.Color outlineColor, Drawing refernce)
        {
            if (!TextLayoutDictionary.ContainsKey(text + fontName + fontSize + baseColor))
                AddTextLayout(text, fontName, fontSize, baseColor, outlineColor, refernce);

            return TextLayoutDictionary[text + fontName + fontSize + baseColor];
        }

        private MultiBrush GetMultiBrush(Color c1, Color c2)
        {
            if (MultiBrushDictionary.ContainsKey(GetCI(c1, c2)))
                return MultiBrushDictionary[GetCI(c1, c2)];

            MultiBrushDictionary.Add(GetCI(c1, c2), new MultiBrush(GetBrush(c1), GetBrush(c2)));
            return MultiBrushDictionary[GetCI(c1, c2)];
        }
        RectangleF calcRect = new RectangleF();
        private void AddTextLayout(string text, string fontName, int fontSize, SharpDX.Color baseColor, SharpDX.Color outlineColor, Drawing refernce)
        {
            calcRect = refernce.MeasureString(text, fontSize);
            TextLayout tl = new TextLayout(ReferencedFontFactory, text, GetFont(fontName, fontSize), calcRect.Width + 1f, calcRect.Height + 1f);
            tl.SetDrawingEffect(GetMultiBrush(baseColor, outlineColor), new TextRange(0, Convert.ToInt32(calcRect.Width)));
            TextLayoutDictionary.Add(text + fontName + fontSize + baseColor, tl);
        }

        private void AddBrush(SharpDX.Color clr)
        {
            BrushDictionary.Add(clr, new SolidColorBrush(ReferencedDrawingDevice, clr));
        }

        public void AddFont(string fontName, int fontSize, FontWeight fontWeight, SharpDX.DirectWrite.FontStyle fontStyle)
        {
            if (ReferencedFontFactory == null)
                return;

            string fontKey = fontName + fontSize;

            if (TextFormatDictionary.ContainsKey(fontKey))
            {
                TextFormat tf = TextFormatDictionary[fontKey];
                if (tf.FontSize == fontSize && tf.FontWeight == fontWeight && tf.FontStyle == fontStyle)
                {
                    return;
                }
                else
                {
                    TextFormat updateFont = new TextFormat(ReferencedFontFactory, fontName, fontWeight, fontStyle, fontSize);
                    updateFont.WordWrapping = WordWrapping.NoWrap;
                    TextFormatDictionary[fontKey] = updateFont;
                }
            }
            else
            {
                TextFormat newFont = new TextFormat(ReferencedFontFactory, fontName, fontWeight, fontStyle, fontSize);
                newFont.WordWrapping = WordWrapping.NoWrap;
                TextFormatDictionary.Add(fontKey, newFont);
            }
            //TextLayoutDictionary.Clear();
            //MultiBrushDictionary.Clear();

        }

        private string GetCI(Color c1, Color c2)
        {
            return c1.ToString() + c2.ToString();
        }
    }
}
