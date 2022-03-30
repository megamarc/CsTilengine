using static SDL2.SDL;
using static Tilengine.TLN;

namespace Barrel
{
    public class Program
    {
        private const int BackgroundLayer = 1;
        private const int ForegroundLayer = 0;
        private const int MaxPalette = 8;
        private const int Width = 400;
        private const int Height = 240;
        private static readonly IntPtr[] Palettes = new IntPtr[MaxPalette];
        private static TLN_Affine _transform;
        private static int _xpos;
        private static int _ypos;

        public static void Main()
        {
            // Setup engine
            TLN_Init(Width, Height, 2, 1, 1);
            TLN_SetRasterCallback(RasterCallback);
            TLN_SetBGColor(115, 48, 57);

            // Load resources
            TLN_SetLoadPath("assets");
            var foreground = TLN_LoadTilemap("castle_fg.tmx", null);
            var background = TLN_LoadTilemap("castle_bg.tmx", null);
            TLN_SetLayerTilemap(ForegroundLayer, foreground);
            TLN_SetLayerTilemap(BackgroundLayer, background);

            // Tweak palettes
            var palette = TLN_GetLayerPalette(BackgroundLayer);
            for (var c = 0; c < MaxPalette; c++)
            {
                var inc = (byte)(c * 7);
                Palettes[c] = TLN_ClonePalette(palette);
                TLN_SubPaletteColor(Palettes[c], inc, inc, inc, 1, 255);
            }

            // Setup affine transform
            _transform = new TLN_Affine
            {
                dx = Width / 2f,
                dy = 1,
                sy = 1
            };

            var simon = new Simon();
            var lastTime = SDL_GetPerformanceCounter();
            TLN_CreateWindow(null, TLN_CreateWindowFlags.CWF_VSYNC);
            TLN_SetWindowTitle("Barrel demo");

            // Main loop
            while (TLN_ProcessWindow())
            {
                // Update time
                SDL_Delay(8);
                var now = SDL_GetPerformanceCounter();
                var deltaTime = (float)((now - lastTime) / (float)SDL_GetPerformanceFrequency());
                lastTime = now;

                // Input
                simon.Tasks(deltaTime);

                // Scroll
                _xpos = simon.GetPositionX();
                TLN_SetLayerPosition(BackgroundLayer, _xpos / 2, -(_ypos++ >> 1));
                TLN_SetLayerPosition(ForegroundLayer, _xpos, 0);

                // Render to window
                TLN_DrawFrame(0);
            }

            // Deinit
            simon.Deinit();
            TLN_DeleteTilemap(foreground);
            TLN_DeleteTilemap(background);
            TLN_Deinit();
        }

        /// <summary>
        /// Float linear interpolation
        /// </summary>
        private static float Lerp(float x, float x0, float x1, float fx0, float fx1)
        {
            return fx0 + (fx1 - fx0) * (x - x0) / (x1 - x0);
        }

        /// <summary>
        /// Raster callback
        /// </summary>
        /// <param name="line">Current scanline</param>
        private static void RasterCallback(int line)
        {
            var angle = Lerp(line, 0, Height - 1, 0, MathF.PI);
            var factor = (1 - MathF.Sin(angle)) * 0.4f + 1;
            _transform.sx = factor;

            TLN_SetLayerAffineTransform(BackgroundLayer, _transform);

            if (line < 70)
            {
                var index = (int)Lerp(line, 0, 70, 0, 7);
                TLN_SetLayerPalette(BackgroundLayer, Palettes[index]);
            }
            else if (line > 170)
            {
                var index = (int)Lerp(line, 170, Height, 7, 0);
                TLN_SetLayerPalette(BackgroundLayer, Palettes[index]);
            }
            else
            {
                TLN_SetLayerPalette(BackgroundLayer, Palettes[7]);
            }
        }
    }
}