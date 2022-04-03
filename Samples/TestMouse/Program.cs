using static SDL2.SDL;
using static Tilengine.TLN;

namespace TestMouse
{
    public class Program
    {
        private const int Width = 400;
        private const int Height = 240;
        private const int MaxEntities = 20;
        private static readonly Entity[] Entities = new Entity[MaxEntities];
        private static IntPtr _paletteDefault;
        private static IntPtr _paletteSelect;
        private static Entity? _selectedEntity;

        public static void Main()
        {
            // Initialize Tilengine
            TLN_Init(Width, Height, 0, MaxEntities, 0);

            var spriteSet = TLN_LoadSpriteset("assets/smw_sprite.png");
            _paletteDefault = TLN_GetSpritesetPalette(spriteSet);
            _paletteSelect = TLN_ClonePalette(_paletteDefault);
            TLN_AddPaletteColor(_paletteSelect, 64, 64, 64, 1, 32);

            if (!TLN_GetSpriteInfo(spriteSet, 0, out var spriteInfo))
            {
                throw new Exception("Failed to get sprite info.");
            }

            var random = new Random();
            for (var c = 0; c < MaxEntities; c++)
            {
                Entities[c] = new Entity
                {
                    Id = c,
                    Enabled = true,
                    X = random.Next(Width),
                    Y = random.Next(Height),
                    W = spriteInfo.w,
                    H = spriteInfo.h,
                    SpriteIndex = c
                };

                TLN_ConfigSprite(Entities[c].SpriteIndex, spriteSet, 0);
                TLN_SetSpritePosition(Entities[c].SpriteIndex, Entities[c].X, Entities[c].Y);
                TLN_SetSpritePicture(Entities[c].SpriteIndex, 0);
            }

            TLN_CreateWindow(null, 0);
            TLN_SetSDLCallback(SDLCallback);

            var frame = 0;
            while (TLN_ProcessWindow())
            {
                TLN_DrawFrame(frame++);
            }
        }

        public static void SDLCallback(in SDL_Event sdl_event)
        {
            if (sdl_event.type == SDL_EventType.SDL_MOUSEBUTTONDOWN)
            {
                var mouse = sdl_event.button;

                // Scale from window space to framebuffer space.
                mouse.x = mouse.x * Width / TLN_GetWindowWidth();
                mouse.y = mouse.y * Height / TLN_GetWindowHeight();

                // Check if the mouse is over an entity.
                for (var c = 0; c < MaxEntities; c++)
                {
                    var entity = Entities[c];
                    if (entity.Enabled && mouse.x >= entity.X && mouse.y >= entity.Y &&
                        mouse.x < entity.X + entity.W && mouse.y < entity.Y + entity.H)
                    {
                        _selectedEntity = entity;
                        _selectedEntity.OnClick(_paletteSelect);
                    }
                }
            }
            else if (sdl_event.type == SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                _selectedEntity?.OnRelease(_paletteDefault);
            }
        }
    }
}