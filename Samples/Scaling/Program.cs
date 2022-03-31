using static Tilengine.TLN;

namespace Scaling
{
    public class Program
    {
        private const int BackgroundLayer = 1;
        private const int ForegroundLayer = 0;
        private const int MaxPalette = 8;
        private const int Width = 400;
        private const int Height = 240;
        private const int MinScale = 50;
        private const int MaxScale = 200;

        private static readonly RGB[] SkyColors =
        {
            new RGB(0x19, 0x54, 0x75),
            new RGB(0x2C, 0xB0, 0xDC)
        };

        private static int _xpos;
        private static int _ypos;
        private static int _scale;

        public static void Main(string[] args)
        {
            // Initialize Tilengine
            TLN_Init(Width, Height, 2, 0, 0);
            TLN_SetBGColor(34, 136, 170);
            TLN_SetRasterCallback(RasterCallback);
            TLN_CreateWindow(null, 0);

            // Load resources
            TLN_SetLoadPath("assets");
            var foreground = TLN_LoadTilemap("psycho.tmx", null);
            var background = TLN_LoadTilemap("rolo.tmx", null);
            TLN_SetLayerTilemap(ForegroundLayer, foreground);
            TLN_SetLayerTilemap(BackgroundLayer, background);

            // Intitial values
            _xpos = 0;
            _ypos = 192;
            _scale = 100;

            // Main loop
            while (TLN_ProcessWindow())
            {
                // Handle input
                if (TLN_GetInput(TLN_Input.INPUT_LEFT))
                {
                    _xpos--;
                }

                if (TLN_GetInput(TLN_Input.INPUT_RIGHT))
                {
                    _xpos++;
                }

                if (TLN_GetInput(TLN_Input.INPUT_UP) && _ypos > 0)
                {
                    _ypos--;
                }

                if (TLN_GetInput(TLN_Input.INPUT_DOWN))
                {
                    _ypos++;
                }

                if (TLN_GetInput(TLN_Input.INPUT_A) && _scale < MaxScale)
                {
                    _scale += 1;
                }

                if (TLN_GetInput(TLN_Input.INPUT_B) && _scale > MinScale)
                {
                    _scale -= 1;
                }

                var foregroundScale = _scale / 100.0f;
                var backgroundScale = Lerp(_scale, MinScale, MaxScale, 0.75f, 1.5f);
                var maxy = 640 - (240 * 100 / _scale);

                if (_ypos > maxy)
                {
                    _ypos = maxy;
                }

                // Update position
                var bgypos = Lerp(_scale, MinScale, MaxScale, 0, 80);
                TLN_SetLayerPosition(ForegroundLayer, _xpos * 2, _ypos);
                TLN_SetLayerPosition(BackgroundLayer, _xpos, (int) bgypos);
                TLN_SetLayerScaling(ForegroundLayer, foregroundScale, foregroundScale);
                TLN_SetLayerScaling(BackgroundLayer, backgroundScale, backgroundScale);

                // Draw to window
                TLN_DrawFrame(0);
            }

        }

        /// <summary>
        /// Float linear interpolation
        /// </summary>
        private static float Lerp(float x, float x0, float x1, float fx0, float fx1)
        {
            return fx0 + (fx1 - fx0) * (x - x0) / (x1 - x0);
        }

        private static void RasterCallback(int line)
        {
            if (line <= 152)
            {
                RGB color = new()
                {
                    R = (byte)Lerp(line, 0, 152, SkyColors[0].R, SkyColors[1].R),
                    G = (byte)Lerp(line, 0, 152, SkyColors[0].G, SkyColors[1].G),
                    B = (byte)Lerp(line, 0, 152, SkyColors[0].B, SkyColors[1].B)
                };

                TLN_SetBGColor(color.R, color.G, color.B);
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
