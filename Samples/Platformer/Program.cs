/******************************************************************************
 * C# port for the Platformer.c sample
 * 2022 Simon Vonhoff
 *
 * Tilengine sample
 * 2015 Marc Palacios
 * http://www.tilengine.org
 *
 * This example show a classic sidescroller. It mimics the actual Sega Genesis
 * Sonic game. It uses two layers, where the background one has multiple strips
 * and a linescroll effect. It also uses color animation for the water cycle
 *
 ******************************************************************************/

using static Tilengine.TLN;

namespace Platformer
{
    public class Program
    {
        private const int BackgroundLayer = 1;
        private const int ForegroundLayer = 0;
        private const int Height = 240;
        private const int Width = 400;
        private static readonly float[] IncBackground = new float[6];
        private static readonly float[] PosBackground = new float[6];

        private static readonly RGB[] SkyColors = {
            new (0x1B, 0x00, 0x8B),
            new (0x00, 0x74, 0xD7),
            new (0x24, 0x92, 0xDB),
            new (0x1F, 0x7F, 0xBE)
        };

        private static float _posForeground;
        private static float _speed;

        public static void Main()
        {
            // Setup engine.
            TLN_Init(Width, Height, 2, 0, 1);
            TLN_SetRasterCallback(RasterCallback);
            TLN_SetBGColor(0, 128, 238);

            // Load resources.
            TLN_SetLoadPath("assets");
            var foreground = TLN_LoadTilemap("Sonic_md_fg1.tmx", null);
            var background = TLN_LoadTilemap("Sonic_md_bg1.tmx", null);
            TLN_SetLayerTilemap(ForegroundLayer, foreground);
            TLN_SetLayerTilemap(BackgroundLayer, background);

            var sp = TLN_LoadSequencePack("Sonic_md_seq.sqx");
            var sequence = TLN_FindSequence(sp, "seq_water");

            // Assign color sequence to various entries in palette.
            var palette = TLN_GetLayerPalette(BackgroundLayer);
            TLN_SetPaletteAnimation(TLN_GetAvailableAnimation(), palette, sequence, true);

            // Compute increments for variable background scrolling speeds.
            IncBackground[0] = 0.562f;
            IncBackground[1] = 0.437f;
            IncBackground[2] = 0.375f;
            IncBackground[3] = 0.625f;
            IncBackground[4] = 1.0f;
            IncBackground[5] = 2.0f;

            // Startup display
            TLN_CreateWindow(null, 0);

            while (TLN_ProcessWindow())
            {
                if (TLN_GetInput(TLN_Input.INPUT_RIGHT))
                {
                    _speed += 0.02f;
                    if (_speed > 1.0f)
                    {
                        _speed = 1.0f;
                    }
                }
                else if (_speed > 0.0f)
                {
                    _speed -= 0.02f;
                    if (_speed < 0.0f)
                    {
                        _speed = 0.0f;
                    }
                }

                if (TLN_GetInput(TLN_Input.INPUT_LEFT))
                {
                    _speed -= 0.02f;
                    if (_speed < -1.0f)
                    {
                        _speed = -1.0f;
                    }
                }
                else if (_speed < 0.0f)
                {
                    _speed += 0.02f;
                    if (_speed > 0.0f)
                    {
                        _speed = 0.0f;
                    }
                }

                // Scroll
                _posForeground += 3.0f * _speed;
                TLN_SetLayerPosition(ForegroundLayer, (int)_posForeground, 0);
                for (var c = 0; c < 6; c++)
                {
                    PosBackground[c] += IncBackground[c] * _speed;
                }

                // Render to window
                TLN_DrawFrame(0);
            }

            // Deinit
            TLN_DeleteTilemap(foreground);
            TLN_DeleteTilemap(background);
            TLN_DeleteSequencePack(sp);
            TLN_Deinit();
        }

        private static void InterpolateColor(int v, int v1, int v2, RGB color1, RGB color2, out RGB result)
        {
            result = new RGB
            {
                R = (byte)Lerp(v, v1, v2, color1.R, color2.R),
                G = (byte)Lerp(v, v1, v2, color1.G, color2.G),
                B = (byte)Lerp(v, v1, v2, color1.B, color2.B)
            };
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
            var pos = line switch
            {
                0 => PosBackground[0],
                32 => PosBackground[1],
                48 => PosBackground[2],
                64 => PosBackground[3],
                112 => PosBackground[4],
                >= 152 => Lerp(line, 152, 224, PosBackground[4], PosBackground[5]),
                _ => -1f
            };

            if (pos >= 0)
            {
                TLN_SetLayerPosition(BackgroundLayer, (int)pos, 0);
            }

            // Background color gradients
            if (line < 112)
            {
                InterpolateColor(line, 0, 112, SkyColors[0], SkyColors[1], out var color);
                TLN_SetBGColor(color.R, color.G, color.B);
            }
            else if (line >= 144)
            {
                InterpolateColor(line, 144, Height, SkyColors[2], SkyColors[3], out var color);
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