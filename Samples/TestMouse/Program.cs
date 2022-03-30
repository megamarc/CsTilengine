using static Tilengine.TLN;
using static SDL2.SDL;

namespace TestMouse
{
    public partial class Program
    {
        private const int Width = 400;
        private const int Height = 240;
        private const int MaxEntities = 20;
        private static Entity[] _entities = new Entity[MaxEntities];
        private static Entity _selectedEntity;
        private static IntPtr _paletteSelect;
        private static IntPtr _paletteDefault;

        public static void Main(string[] args)
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
                _entities[c] = new Entity
                {
                    Id = c,
                    Enabled = true,
                    X = random.Next(Width),
                    Y = random.Next(Height),
                    W = spriteInfo.w,
                    H = spriteInfo.h,
                    SpriteIndex = c
                };

                TLN_ConfigSprite(_entities[c].SpriteIndex, spriteSet, 0);
                TLN_SetSpritePosition(_entities[c].SpriteIndex, _entities[c].X, _entities[c].Y);
                TLN_SetSpritePicture(_entities[c].SpriteIndex, 0);
            }

            TLN_CreateWindow(null, 0);
            TLN_SetSDLCallback(SDLCallback);

            var frame = 0;
            while (TLN_ProcessWindow())
            {
                TLN_DrawFrame(frame++);
            }
        }

        public static void SDLCallback(in SDL_Event sdlEvent)
        {
            if (sdlEvent.type == SDL_EventType.SDL_MOUSEBUTTONDOWN)
            {
                var mouse = sdlEvent.button;

                // Scale from window space to framebuffer space.
                mouse.x = mouse.x * Width / TLN_GetWindowWidth();
                mouse.y = mouse.y * Height / TLN_GetWindowHeight();

                // Check if the mouse is over an entity.
                for (var c = 0; c < MaxEntities; c++)
                {
                    var entity = _entities[c];
                    if (entity.Enabled && mouse.x >= entity.X && mouse.y >= entity.Y &&
                        mouse.x < entity.X + entity.W && mouse.y < entity.Y + entity.H)
                    {
                        _selectedEntity = entity;
                        _selectedEntity.OnClick(_paletteSelect);
                    }
                }
            }
            else if (sdlEvent.type == SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                if (_selectedEntity != null)
                {
                    _selectedEntity.OnRelease(_paletteDefault);
                }
            }
        }
    }
}
