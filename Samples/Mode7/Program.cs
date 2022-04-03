/******************************************************************************
 * C# port for the Mode7.c sample
 * 2022 Simon Vonhoff
 *
 * Tilengine sample
 * 2015 Marc Palacios
 * http://www.tilengine.org
 *
 * This example show a classic Mode 7 perspective projection plane like the
 * one seen in SNES games like Super Mario Kart. It uses a single transformed
 * layer with a raster effect setting the scaling factor for each line
 *
 ******************************************************************************/

using System.Numerics;
using static Tilengine.TLN;

namespace Mode7
{
    public class Program
    {
        private const int BackgroundLayer = 1;
        private const int ForegroundLayer = 0;
        private const int Height = 240;
        private const int Width = 400;

        private static float _acceleration;
        private static float _angle;
        private static float _maxSpeed;
        private static Vector2 _position;
        private static IntPtr _road;
        private static float _speed;

        public static void Main(string[] args)
        {
            // Initialize Tilengine
            TLN_Init(Width, Height, 2, 0, 0);
            TLN_SetRasterCallback(RasterCallback);
            TLN_SetBGColor(0, 0, 0);

            // Load resources
            TLN_SetLoadPath("assets");
            _road = TLN_LoadTilemap("track1.tmx", null);
            var horizon = TLN_LoadTilemap("track1_bg.tmx", null);

            // Create window
            TLN_CreateWindow(null, TLN_CreateWindowFlags.CWF_VSYNC);
            TLN_SetWindowTitle("Mode7 demo");

            // Initialize variables
            _position.X = IntegerToFixedBits(-136);
            _position.Y = IntegerToFixedBits(336);
            _acceleration = FloatToFixedBits(0.2f);
            _maxSpeed = FloatToFixedBits(3f);
            _speed = 0;
            _angle = 0;

            // Main loop
            while (TLN_ProcessWindow())
            {
                TLN_Delay(8);
                TLN_SetLayerTilemap(ForegroundLayer, horizon);
                TLN_SetLayerTilemap(BackgroundLayer, horizon);
                TLN_SetLayerPosition(ForegroundLayer, (int)Lerp(_angle * 2, 0, 360, 0, 256), 24);
                TLN_SetLayerPosition(BackgroundLayer, (int)Lerp(_angle, 0, 360, 0, 256), 0);
                TLN_ResetLayerMode(BackgroundLayer);

                // Get input and update variables
                if (TLN_GetInput(TLN_Input.INPUT_LEFT))
                {
                    _angle -= 1.5f;
                }
                else if (TLN_GetInput(TLN_Input.INPUT_RIGHT))
                {
                    _angle += 1.5f;
                }

                // Handle input and update speed
                if (TLN_GetInput(TLN_Input.INPUT_UP))
                {
                    if (_speed < _maxSpeed)
                    {
                        _speed += _acceleration * 0.2f;
                    }
                }
                else if (TLN_GetInput(TLN_Input.INPUT_DOWN))
                {
                    if (_speed > -_maxSpeed)
                    {
                        _speed -= _acceleration * 0.2f;
                    }
                }
                else
                {
                    _speed *= 0.97f;
                }

                if (_speed != 0)
                {
                    // Make sure the angle is between 0 and 360
                    _angle %= 360;
                    if (_angle < 0)
                    {
                        _angle += 360;
                    }

                    // Update position
                    _position.X += MathF.Sin(_angle * MathF.PI / 180f) * _speed;
                    _position.Y -= MathF.Cos(_angle * MathF.PI / 180f) * _speed;
                }

                // Render to window
                TLN_DrawFrame(0);
            }

            // Free resources
            TLN_DeleteTilemap(_road);
            TLN_DeleteTilemap(horizon);
            TLN_DeleteWindow();
            TLN_Deinit();
        }

        /// <summary>
        /// Fixed bits to float.
        /// </summary>
        private static float FixedBitsToFloat(int value)
        {
            return value / 65536f;
        }

        /// <summary>
        /// Fixed bits to int
        /// </summary>
        private static int FixedBitsToInteger(int value)
        {
            return value >> 16;
        }

        /// <summary>
        /// Float to fixed bits.
        /// </summary>
        private static int FloatToFixedBits(float value)
        {
            return (int)(value * 65536f);
        }

        /// <summary>
        /// Integer to fixed bits.
        /// </summary>
        private static int IntegerToFixedBits(int value)
        {
            return value << 16;
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
        /// <param name="line">current scanline</param>
        private static void RasterCallback(int line)
        {
            if (line == 24)
            {
                TLN_SetLayerTilemap(BackgroundLayer, _road);
                TLN_SetLayerPosition(BackgroundLayer,
                    FixedBitsToInteger((int)_position.X),
                    FixedBitsToInteger((int)_position.Y));
                TLN_DisableLayer(ForegroundLayer);
            }

            if (line >= 24)
            {
                var s0 = FloatToFixedBits(0.2f);
                var s1 = FloatToFixedBits(5.0f);
                var s = Lerp(line, 24, Height, s0, s1);
                var scale = FixedBitsToFloat((int)s);
                TLN_SetLayerTransform(BackgroundLayer, _angle, Width / 2f, Height, scale, scale);
            }
        }
    }
}