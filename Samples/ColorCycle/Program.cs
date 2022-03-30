using static Tilengine.TLN;

namespace ColorCycle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize engine
            TLN_Init(640, 480, 0, 0, 1);

            // Load resources
            TLN_SetLoadPath("assets");
            var background = TLN_LoadBitmap("beach.png");
            var palette = TLN_GetBitmapPalette(background);
            var sequencePack = TLN_LoadSequencePack("beach.sqx");
            var sequence = TLN_FindSequence(sequencePack, "beach");

            // Setup
            TLN_SetBGBitmap(background);
            TLN_SetPaletteAnimation(0, palette, sequence, true);

            // Create window
            TLN_CreateWindow(null, TLN_CreateWindowFlags.CWF_VSYNC);
            TLN_SetWindowTitle("Color cycle demo");

            // Main loop
            while (TLN_ProcessWindow())
            {
                TLN_DrawFrame(0);
            }

            // Shutdown
            TLN_DeleteBitmap(background);
            TLN_DeleteSequencePack(sequencePack);
            TLN_Deinit();
        }
    }
}