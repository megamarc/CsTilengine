/******************************************************************************
 * C# port for the Forest.c sample
 * 2022 Simon Vonhoff
 *
 * Tilengine sample
 * 2015 Marc Palacios
 * http://www.tilengine.org
 *
 * This sample shows the usage of resource files. When the resource file is loaded
 * successfully, it loads the original resources of the Forest.c sample.
 *
 ******************************************************************************/

using static Tilengine.TLN;

namespace Forest
{
    public class Program
    {
        private const string ResourceFilename = "assets.dat";
        private const int Width = 424;
        private const int Height = 240;
        private const int ForegroundLayer = 0;
        private static int _position;
        private static int _previousPosition = -1;

        public static void Main()
        {
            TLN_Init(Width, Height, 4, 0, 0);

            // Open resource pack.
            if (!TLN_OpenResourcePack(ResourceFilename, null))
            {
                throw new Exception($"Could not open resource pack: [{ResourceFilename}]", new Exception(TLN_GetError()));
            }

            // Initialize resources.
            TLN_SetLoadPath("forest");
            TLN_LoadWorld("map.tmx", 0);
            var layerWidth = TLN_GetLayerWidth(ForegroundLayer);

            // Create window and enter main loop.
            TLN_CreateWindow(null, 0);
            while (TLN_ProcessWindow())
            {
                TLN_DrawFrame(0);

                // Move 3 pixels right/left on the main layer.
                if (TLN_GetInput(TLN_Input.INPUT_LEFT) && _position > 0)
                {
                    _position -= 3;
                }
                else if (TLN_GetInput(TLN_Input.INPUT_RIGHT) && _position < layerWidth - Width)
                {
                    _position += 3;
                }

                // Only update the world position on change.
                if (_position != _previousPosition)
                {
                    TLN_SetWorldPosition(_position, 0);
                    _previousPosition = _position;
                }
            }

            /* release resources */
            TLN_ReleaseWorld();
            TLN_DeleteWindow();
            TLN_Deinit();
        }
    }
}