using static Tilengine.TLN;

namespace Shadow
{
    public class Program
    {
        private const int BackgroundLayer = 1;
        private const int ForegroundLayer = 0;
        private const int MaxHorizontalPosition = 4720;
        private const int Width = 400;
        private const int Height = 240;
        private const int Speed = 2;

        private static readonly RGB[] SkyColors =
        {
            new (0x1D, 0x44, 0x7B),
            new (0x7F, 0xA4, 0xD9),
            new (0x0B, 0x00, 0x4E),
            new (0xEB, 0x99, 0x9D),
        };

        private static RGB _skyHigh;
        private static RGB _skyLow;
        private static int _xpos;

        public static void Main(string[] args)
        {
            // Initialize Tilengine
            TLN_Init(Width, Height, 2, 1, 0);
            TLN_SetRasterCallback(RasterCallback);
            TLN_SetBGColor(0, 128, 128);

            // Load resources
            TLN_SetLoadPath("assets");
            var foreground = TLN_LoadTilemap("SOTB_fg.tmx", null);
            var background = TLN_LoadTilemap("SOTB_bg.tmx", null);
            TLN_SetLayerTilemap(ForegroundLayer, foreground);
            TLN_SetLayerTilemap(BackgroundLayer, background);

            var spriteSet = TLN_LoadSpriteset("SOTB");
            var walkSequence = TLN_CreateSpriteSequence(null, spriteSet, "walk", 6);

            TLN_SetSpriteSet(0, spriteSet);
            TLN_SetSpritePosition(0, 200, 160);
            TLN_SetSpriteAnimation(0, walkSequence, 0);

            // Set sky colors
            _xpos = 2000;
            _skyHigh.R = SkyColors[0].R;
            _skyHigh.G = SkyColors[0].G;
            _skyHigh.B = SkyColors[0].B;
            _skyLow.R = SkyColors[1].R;
            _skyLow.G = SkyColors[1].G;
            _skyLow.B = SkyColors[1].B;

            var frame = 0;
            TLN_CreateWindow(null, 0);
            TLN_SetWindowTitle("SOTB demo");

            // Main loop
            while (TLN_ProcessWindow())
            {
                if (_xpos < MaxHorizontalPosition)
                {
                    _xpos += Speed;
                    if (_xpos >= MaxHorizontalPosition)
                    {
                        TLN_DisableSpriteAnimation(0);
                        TLN_SetSpritePicture(0, 0);
                    }
                }

                // Update sky colors
                if (frame >= 300 && frame <= 900)
                {
                    // Interpolate upper color
                    _skyHigh.R = Lerp(frame, 300, 900, SkyColors[0].R, SkyColors[2].R);
                    _skyHigh.G = Lerp(frame, 300, 900, SkyColors[0].G, SkyColors[2].G);
                    _skyHigh.B = Lerp(frame, 300, 900, SkyColors[0].B, SkyColors[2].B);

                    // Interpolate lower color
                    _skyHigh.R = Lerp(frame, 300, 900, SkyColors[1].R, SkyColors[3].R);
                    _skyHigh.G = Lerp(frame, 300, 900, SkyColors[1].G, SkyColors[3].G);
                    _skyHigh.B = Lerp(frame, 300, 900, SkyColors[1].B, SkyColors[3].B);
                }

                TLN_SetLayerPosition(ForegroundLayer, _xpos, 0);

                // Update frame
                TLN_DrawFrame(frame);
                frame++;
            }

            TLN_DeleteSequence(walkSequence);
            TLN_DeleteTilemap(foreground);
            TLN_DeleteTilemap(background);
            TLN_Deinit();
        }

        /// <summary>
        /// Float linear interpolation
        /// </summary>
        private static byte Lerp(int x, int x0, int x1, int fx0, int fx1)
        {
            return (byte)(fx0 + (fx1 - fx0) * (x - x0) / (x1 - x0));
        }

        private static void RasterCallback(int line)
        {
            // Sky gradient
            if (line < 192)
            {
                // Interpolate between two colors
                RGB color = new()
                {
                    R = Lerp(line, 0, 191, _skyHigh.R, _skyLow.R),
                    G = Lerp(line, 0, 191, _skyHigh.G, _skyLow.G),
                    B = Lerp(line, 0, 191, _skyHigh.B, _skyLow.B)
                };

                TLN_SetBGColor(color.R, color.G, color.B);
            }

            // Background layer
            var pos = -1;
            if (line == 0 || line == 24 || line == 64 || line == 88 || line == 96)
            {
                pos = Lerp(line, 0, 96, (int)(_xpos * 0.7f), (int)(_xpos * 0.2f));
            }
            else if (line == 120)
            {
                pos = _xpos / 2;
            }
            else if (line == 208 || line == 216 || line == 224 || line == 232)
            {
                pos = Lerp(line, 208, 232, (int)(_xpos * 1.0f), (int)(_xpos * 2.0f));
            }

            // Set layer position of background layer
            if (pos != -1)
            {
                TLN_SetLayerPosition(BackgroundLayer, pos, 0);
            }

            pos = -1;
            if (line == 0)
            {
                pos = _xpos;
            }
            else if (line == 216)
            {
                pos = _xpos * 3;
            }

            // Set layer position of foreground layer
            if (pos != -1)
            {
                TLN_SetLayerPosition(ForegroundLayer, pos, 0);
            }
        }

        private struct RGB
        {
            public byte R, G, B;

            public RGB(byte r, byte g, byte b)
            {
                R = r;
                G = g;
                B = b;
            }
        }
    }
}