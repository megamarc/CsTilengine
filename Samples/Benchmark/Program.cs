using System.Runtime.InteropServices;
using static Tilengine.TLN;

namespace Benchmark
{
    public class Program
    {
        private const int Width = 400;
        private const int Height = 240;
        private const int AmountOfSprites = 250;
        private const int AmountOfFrames = 4000;
        private static IntPtr _framebuffer;
        private static int _pixels;

        public static void Main(string[] args)
        {
            var version = TLN_GetVersion();
            Console.WriteLine("Tilengine benchmark tool");
            Console.WriteLine("Written by Simon Vonhoff based on Megamarc's C version.");
            Console.WriteLine($"Library version: {(version >> 16) & 0xFF}.{(version >> 8) & 0xFF}.{version & 0xFF}");
            Console.WriteLine("\nPress any key to start benchmark . . .");
            Console.ReadLine();

            TLN_Init(Width, Height, 1, AmountOfSprites, 0);
            _framebuffer = Marshal.AllocHGlobal(Width * Height * 4);
            TLN_SetRenderTarget(_framebuffer, Height * 4);
            TLN_SetLoadPath("assets");
            TLN_DisableBGColor();

            var tilemap = TLN_LoadTilemap("TF4_bg1.tmx", null);
            TLN_SetLayerTilemap(0, tilemap);
            _pixels = Width * Height;

            Console.Write("Normal layer..........");
            Profile();

            Console.Write("Scaling layer.........");
            TLN_SetLayerScaling(0, 2.0f, 2.0f);
            Profile();

            Console.Write("Affine layer..........");
            TLN_SetLayerTransform(0, 45.0f, 0.0f, 0.0f, 1.0f, 1.0f);
            Profile();

            Console.Write("Blend layer...........");
            TLN_ResetLayerMode(0);
            TLN_SetLayerBlendMode(0, TLN_Blend.BLEND_MIX50, 128);
            Profile();

            Console.Write("Scaling blend layer...");
            TLN_SetLayerScaling(0, 2.0f, 2.0f);
            Profile();

            Console.Write("Affine blend layer....");
            TLN_SetLayerTransform(0, 45.0f, 0.0f, 0.0f, 1.0f, 1.0f);
            Profile();

            TLN_DisableLayer(0);

            var spriteset = TLN_LoadSpriteset("FireLeo");
            for (var c = 0; c < AmountOfSprites; c++)
            {
                int y = c / 25;
                int x = c % 25;
                TLN_ConfigSprite(c, spriteset, TLN_TileFlags.FLAG_NONE);
                TLN_SetSpritePicture(c, 0);
                TLN_SetSpritePosition(c, x * 15, y * 21);
            }
            TLN_GetSpriteInfo(spriteset, 0, out var sprite_info);
            _pixels = AmountOfSprites * sprite_info.w * sprite_info.h;

            Console.Write("Normal sprites........");
            Profile();

            Console.Write("Colliding sprites.....");
            for (var c = 0; c < AmountOfSprites; c++)
            {
                TLN_EnableSpriteCollision(c, true);
            }

            Profile();

            Marshal.FreeHGlobal(_framebuffer);
            TLN_DeleteTilemap(tilemap);
            TLN_Deinit();
            Console.WriteLine("\nBenchmark finished\n");
            Console.WriteLine("Press any key to exit . . .");
            Console.ReadLine();
        }

        private static void Profile()
        {
            var frame = 0;
            var t0 = TLN_GetTicks();

            do
            {
                TLN_UpdateFrame(frame++);
            }
            while (frame < AmountOfFrames);

            var elapsed = TLN_GetTicks() - t0;
            var result = frame * _pixels / elapsed;
            Console.WriteLine($"{result / 1000}.{result % 1000} Mpixels/s");
        }
    }
}