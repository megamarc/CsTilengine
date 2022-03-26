#region License

/* CsTilengine - 1:1 Api C# Wrapper for Tilengine v2.9.4
 * Copyright (C) 2022 Simon Vonhoff <mailto:simon.vonhoff@outlook.com>
 *
 * Tilengine - The 2D retro graphics engine with raster effects
 * Copyright (c) 2018 Marc Palacios Domènech <mailto:megamarc@hotmail.com>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 */

/*
 *****************************************************************************
 * C# Tilengine wrapper - Up to date to library version 2.9.4
 * http://www.tilengine.org
 *****************************************************************************
 */

#endregion

#region Using Statements

using SDL2;
using System.Runtime.InteropServices;

#endregion

namespace Tilengine
{
    public static class TLN
    {
        #region CsTilengine Variables

        private const string NativeLibName = "Tilengine";

        #endregion

        #region Structures

        /// <summary>
        /// Affine transformation parameters
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TLN_Affine
        {
            /// <summary>
            /// Rotation in degrees
            /// </summary>
            public float angle;

            /// <summary>
            /// Horizontal translation
            /// </summary>
            public float dx;

            /// <summary>
            /// Vertical translation
            /// </summary>
            public float dy;

            /// <summary>
            /// Horizontal scaling
            /// </summary>
            public float sx;

            /// <summary>
            /// Vertical scaling
            /// </summary>
            public float sy;
        }

        /// <summary>
        /// Tile item for Tilemap access methods
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TLN_Tile
        {
            public uint value;

            /// <summary>
            /// Tile index
            /// </summary>
            public uint index;

            /// <summary>
            /// <see cref="TLN_TileFlags"/> Attributes
            /// </summary>
            public TLN_TileFlags flags;
        }

        /// <summary>
        /// Frame animation definition
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TLN_SequenceFrame
        {
            /// <summary>
            /// Tile/sprite index
            /// </summary>
            public int index;

            /// <summary>
            /// Time delay for next frame
            /// </summary>
            public int delay;
        }

        /// <summary>
        /// Color strip definition
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TLN_ColorStrip
        {
            /// <summary>
            /// Time delay between frames
            /// </summary>
            public int delay;

            /// <summary>
            /// Index of first color to cycle
            /// </summary>
            public byte first;

            /// <summary>
            /// Number of colors in the cycle
            /// </summary>
            public byte count;

            /// <summary>
            /// Direction: 0 = descending, 1 = ascending
            /// </summary>
            public byte dir;
        }

        /// <summary>
        /// Sequence info returned by <see cref="TLN_GetSequenceInfo"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TLN_SequenceInfo
        {
            /// <summary>
            /// Sequence name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string name;

            /// <summary>
            /// Number of frames
            /// </summary>
            public int num_frames;
        }

        /// <summary>
        /// Sprite creation info for <see cref="TLN_CreateSpriteset"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TLN_SpriteData
        {
            /// <summary>
            /// Entry name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string name;

            /// <summary>
            /// Horizontal position
            /// </summary>
            public int x;

            /// <summary>
            /// Vertical position
            /// </summary>
            public int y;

            /// <summary>
            /// Width
            /// </summary>
            public int w;

            /// <summary>
            /// Height
            /// </summary>
            public int h;
        }

        /// <summary>
        /// Sprite information
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TLN_SpriteInfo
        {
            /// <summary>
            /// Width of sprite
            /// </summary>
            public int w;

            /// <summary>
            /// Height of sprite
            /// </summary>
            public int h;
        }

        /// <summary>
        /// Tile information returned by <see cref="TLN_GetLayerTile"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TLN_TileInfo
        {
            /// <summary>
            /// Tile index
            /// </summary>
            public ushort index;

            /// <summary>
            /// <see cref="TLN_TileFlags"/> Attributes
            /// </summary>
            public ushort flags;

            /// <summary>
            /// Row number in the tilemap
            /// </summary>
            public int row;

            /// <summary>
            /// Col number in the tilemap
            /// </summary>
            public int col;

            /// <summary>
            /// Horizontal position inside the tile
            /// </summary>
            public int xoffset;

            /// <summary>
            /// Vertical position inside the tile
            /// </summary>
            public int yoffset;

            /// <summary>
            /// Color index at collision point
            /// </summary>
            public byte color;

            /// <summary>
            /// Tile type
            /// </summary>
            public byte type;

            /// <summary>
            /// Cell is empty
            /// </summary>
            public bool empty;
        }

        /// <summary>
        /// Object item info returned by <see cref="TLN_GetObjectInfo"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TLN_ObjectInfo
        {
            /// <summary>
            /// Unique ID
            /// </summary>
            public ushort id;

            /// <summary>
            /// Graphic ID (tile index)
            /// </summary>
            public ushort gid;

            /// <summary>
            /// <see cref="TLN_TileFlags"/> Attributes
            /// </summary>
            public ushort flags;

            /// <summary>
            /// Horizontal position
            /// </summary>
            public int x;

            /// <summary>
            /// Vertical position
            /// </summary>
            public int y;

            /// <summary>
            /// Horizontal size
            /// </summary>
            public int width;

            /// <summary>
            /// Vertical size
            /// </summary>
            public int height;

            /// <summary>
            /// Object type
            /// </summary>
            public uint type;

            /// <summary>
            /// Object is visible
            /// </summary>
            public bool visible;

            /// <summary>
            /// Object name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string name;
        }

        /// <summary>
        /// Tileset attributes for <see cref="TLN_CreateTileset"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TLN_TileAttribute
        {
            /// <summary>
            /// Tile type
            /// </summary>
            public byte type;

            /// <summary>
            /// Priority flag set
            /// </summary>
            public bool priority;
        }

        /// <summary>
        /// Pixel mapping for <see cref="TLN_SetLayerPixelMapping"/>
        /// </summary>
        public struct TLN_PixelMap
        {
            /// <summary>
            /// Horizontal pixel displacement
            /// </summary>
            public ushort dx;

            /// <summary>
            /// Vertical pixel displacement
            /// </summary>
            public ushort dy;
        }

        /// <summary>
        /// Image tile for <see cref="TLN_CreateImageTileset"/>
        /// </summary>
        public struct TLN_TileImage
        {
            /// <summary>
            /// Bitmap for tile image
            /// </summary>
            public IntPtr bitmap;

            /// <summary>
            /// Unique ID
            /// </summary>
            public ushort id;

            /// <summary>
            /// Tile type
            /// </summary>
            public byte type;
        }

        /// <summary>
        /// Sprite state
        /// </summary>
        public struct TLN_SpriteState
        {
            /// <summary>
            /// Horizontal screen position
            /// </summary>
            public int x;

            /// <summary>
            /// Vertical screen position
            /// </summary>
            public int y;

            /// <summary>
            /// Actual width on screen (after scaling)
            /// </summary>
            public int w;

            /// <summary>
            /// Actual height on screen (after scaling)
            /// </summary>
            public int h;

            /// <summary>
            /// Sprite flags
            /// </summary>
            public uint flags;

            /// <summary>
            /// Assigned palette
            /// </summary>
            public IntPtr palette;

            /// <summary>
            /// Assigned spriteset
            /// </summary>
            public IntPtr spriteset;

            /// <summary>
            /// Graphic index inside spriteset
            /// </summary>
            public int index;

            /// <summary>
            /// Sprite is enabled
            /// </summary>
            public bool enabled;

            /// <summary>
            /// Pixel collision detection is enabled
            /// </summary>
            public bool collision;
        }

        #endregion

        #region Enumerations

        /// <summary>
        /// Tile/sprite flags
        /// </summary>
        [Flags]
        public enum TLN_TileFlags
        {
            /// <summary>
            /// No flags
            /// </summary>
            FLAG_NONE = 0,

            /// <summary>
            /// Horizontal flip
            /// </summary>
            FLAG_FLIPX = 1 << 15,

            /// <summary>
            /// Vertical flip
            /// </summary>
            FLAG_FLIPY = 1 << 14,

            /// <summary>
            /// Row/column flip (unsupported, Tiled compatibility)
            /// </summary>
            FLAG_ROTATE = 1 << 13,

            /// <summary>
            /// Tile goes in front of sprite layer
            /// </summary>
            FLAG_PRIORITY = 1 << 12,

            /// <summary>
            /// Sprite won't be drawn inside masked region
            /// </summary>
            FLAG_MASKED = 1 << 11
        }

        /// <summary>
        /// Layer blend modes. Must be one of these and are mutually exclusive.
        /// </summary>
        public enum TLN_Blend
        {
            /// <summary>
            /// Blending disabled
            /// </summary>
            BLEND_NONE,

            /// <summary>
            /// Color averaging 1
            /// </summary>
            BLEND_MIX25,

            /// <summary>
            /// Color averaging 2
            /// </summary>
            BLEND_MIX50,

            /// <summary>
            /// Color averaging 3
            /// </summary>
            BLEND_MIX75,

            /// <summary>
            /// Color is always brighter (simulate light effects)
            /// </summary>
            BLEND_ADD,

            /// <summary>
            /// Color is always darker (simulate shadow effects)
            /// </summary>
            BLEND_SUB,

            /// <summary>
            /// Color is always darker (simulate shadow effects)
            /// </summary>
            BLEND_MOD,

            /// <summary>
            /// User provided blend function with <see cref="TLN_SetCustomBlendFunction"/>
            /// </summary>
            BLEND_CUSTOM
        }

        /// <summary>
        /// Layer type retrieved by TLN_GetLayerType
        /// </summary>
        public enum TLN_LayerType
        {
            /// <summary>
            /// Undefined
            /// </summary>
            LAYER_NONE,

            /// <summary>
            /// Tilemap-based layer
            /// </summary>
            LAYER_TILE,

            /// <summary>
            /// Objects layer
            /// </summary>
            LAYER_OBJECT,

            /// <summary>
            /// Bitmapped layer
            /// </summary>
            LAYER_BITMAP
        }

        /// <summary>
        /// Overlays for CRT effect
        /// </summary>
        public enum TLN_Overlay
        {
            /// <summary>
            /// No overlay
            /// </summary>
            TLN_OVERLAY_NONE,

            /// <summary>
            /// Shadow mask pattern
            /// </summary>
            TLN_OVERLAY_SHADOWMASK,

            /// <summary>
            /// Aperture grille pattern
            /// </summary>
            TLN_OVERLAY_APERTURE,

            /// <summary>
            /// Scanlines pattern
            /// </summary>
            TLN_OVERLAY_SCANLINES,

            /// <summary>
            /// User-provided when calling <see cref="TLN_CreateWindow"/>
            /// </summary>
            TLN_OVERLAY_CUSTOM,
        }

        [Flags]
        public enum TLN_CreateWindowFlags
        {
            /// <summary>
            /// Create a fullscreen window
            /// </summary>
            CWF_FULLSCREEN = (1 << 0),

            /// <summary>
            /// Sync frame updates with vertical retrace
            /// </summary>
            CWF_VSYNC = (1 << 1),

            /// <summary>
            /// Create a window the same size as the framebuffer
            /// </summary>
            CWF_S1 = (1 << 2),

            /// <summary>
            /// Create a window 2x the size the framebuffer
            /// </summary>
            CWF_S2 = (2 << 2),

            /// <summary>
            /// Create a window 3x the size the framebuffer
            /// </summary>
            CWF_S3 = (3 << 2),

            /// <summary>
            /// Create a window 4x the size the framebuffer
            /// </summary>
            CWF_S4 = (4 << 2),

            /// <summary>
            /// Create a window 5x the size the framebuffer
            /// </summary>
            CWF_S5 = (5 << 2),

            /// <summary>
            /// Unfiltered upscaling
            /// </summary>
            CWF_NEAREST = (1 << 6),
        }

        public enum TLN_Error
        {
            /// <summary>
            /// No error
            /// </summary>
            TLN_ERR_OK,

            /// <summary>
            /// Not enough memory
            /// </summary>
            TLN_ERR_OUT_OF_MEMORY,

            /// <summary>
            /// Layer index out of range
            /// </summary>
            TLN_ERR_IDX_LAYER,

            /// <summary>
            /// Sprite index out of range
            /// </summary>
            TLN_ERR_IDX_SPRITE,

            /// <summary>
            /// Animation index out of range
            /// </summary>
            TLN_ERR_IDX_ANIMATION,

            /// <summary>
            /// Picture or tile index out of range
            /// </summary>
            TLN_ERR_IDX_PICTURE,

            /// <summary>
            /// Invalid IntPtr reference
            /// </summary>
            TLN_ERR_REF_TILESET,

            /// <summary>
            /// Invalid TLN_Tilemap reference
            /// </summary>
            TLN_ERR_REF_TILEMAP,

            /// <summary>
            /// Invalid IntPtr reference
            /// </summary>
            TLN_ERR_REF_SPRITESET,

            /// <summary>
            /// Invalid IntPtr reference
            /// </summary>
            TLN_ERR_REF_PALETTE,

            /// <summary>
            /// Invalid TLN_Sequence reference
            /// </summary>
            TLN_ERR_REF_SEQUENCE,

            /// <summary>
            /// Invalid IntPtr reference
            /// </summary>
            TLN_ERR_REF_SEQPACK,

            /// <summary>
            /// Invalid TLN_Bitmap reference
            /// </summary>
            TLN_ERR_REF_BITMAP,

            /// <summary>
            /// Null pointer as argument
            /// </summary>
            TLN_ERR_NULL_POINTER,

            /// <summary>
            /// Resource file not found
            /// </summary>
            TLN_ERR_FILE_NOT_FOUND,

            /// <summary>
            /// Resource file has invalid format
            /// </summary>
            TLN_ERR_WRONG_FORMAT,

            /// <summary>
            /// A width or height parameter is invalid
            /// </summary>
            TLN_ERR_WRONG_SIZE,

            /// <summary>
            /// Unsupported function
            /// </summary>
            TLN_ERR_UNSUPPORTED,

            /// <summary>
            /// Invalid TLN_ObjectList reference
            /// </summary>
            TLN_ERR_REF_LIST
        }

        public enum TLN_LogLevel
        {
            /// <summary>
            /// Don't print anything (default)
            /// </summary>
            TLN_LOG_NONE,

            /// <summary>
            /// Print only runtime errors
            /// </summary>
            TLN_LOG_ERRORS,

            /// <summary>
            /// Print everything
            /// </summary>
            TLN_LOG_VERBOSE,
        }

        #endregion

        #region Callbacks

        public delegate void TLN_VideoCallback(int line);

        public delegate byte TLN_BlendFunction(byte src, byte dst);

        public delegate void TLN_SDLCallback(in SDL.SDL_Event sdlEvent);

        #endregion

        #region Input

        /// <summary>
        /// Player index for input assignment functions
        /// </summary>
        public enum TLN_Player
        {
            PLAYER1,
            PLAYER2,
            PLAYER3,
            PLAYER4,
        }

        public enum TLN_Input
        {
            /// <summary>
            /// No input
            /// </summary>
            INPUT_NONE,

            /// <summary>
            /// Up direction
            /// </summary>
            INPUT_UP,

            /// <summary>
            /// Down direction
            /// </summary>
            INPUT_DOWN,

            /// <summary>
            /// Left direction
            /// </summary>
            INPUT_LEFT,

            /// <summary>
            /// Right direction
            /// </summary>
            INPUT_RIGHT,

            /// <summary>
            /// 1st action button
            /// </summary>
            INPUT_BUTTON1,

            /// <summary>
            /// 2nd action button
            /// </summary>
            INPUT_BUTTON2,

            /// <summary>
            /// 3th action button
            /// </summary>
            INPUT_BUTTON3,

            /// <summary>
            /// 4th action button
            /// </summary>
            INPUT_BUTTON4,

            /// <summary>
            /// 5th action button
            /// </summary>
            INPUT_BUTTON5,

            /// <summary>
            /// 6th action button
            /// </summary>
            INPUT_BUTTON6,

            /// <summary>
            /// Start button
            /// </summary>
            INPUT_START,

            /// <summary>
            /// Window close (only Player 1 keyboard)
            /// </summary>
            INPUT_QUIT,

            /// <summary>
            /// CRT toggle (only Player 1 keyboard)
            /// </summary>
            INPUT_CRT,

            // Up to 32 unique inputs

            /// <summary>
            /// Request player 1 input (default)
            /// </summary>
            INPUT_P1 = TLN_Player.PLAYER1,

            /// <summary>
            /// Request player 2 input
            /// </summary>
            INPUT_P2 = TLN_Player.PLAYER2 << 5,

            /// <summary>
            /// Request player 3 input
            /// </summary>
            INPUT_P3 = TLN_Player.PLAYER3 << 5,

            /// <summary>
            /// Request player 4 input
            /// </summary>
            INPUT_P4 = TLN_Player.PLAYER4 << 5,

            /// <summary>
            /// 1 action button.
            /// Part of the compatibility symbols for pre-1.18 input model
            /// </summary>
            INPUT_A = INPUT_BUTTON1,

            /// <summary>
            /// 2nd action button.
            /// Part of the compatibility symbols for pre-1.18 input model
            /// </summary>
            INPUT_B = INPUT_BUTTON2,

            /// <summary>
            /// 3th action button.
            /// Part of the compatibility symbols for pre-1.18 input model
            /// </summary>
            INPUT_C = INPUT_BUTTON3,

            /// <summary>
            /// 4th action button.
            /// Part of the compatibility symbols for pre-1.18 input model
            /// </summary>
            INPUT_D = INPUT_BUTTON4,

            /// <summary>
            /// 5th action button.
            /// Part of the compatibility symbols for pre-1.18 input model
            /// </summary>
            INPUT_E = INPUT_BUTTON5,

            /// <summary>
            /// 6th action button.
            /// Part of the compatibility symbols for pre-1.18 input model
            /// </summary>
            INPUT_F = INPUT_BUTTON6,
        }

        #endregion

        #region Setup

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_Init(int hres, int vres, int numlayers, int numsprites, int numanimations);

        [DllImport(NativeLibName)]
        public static extern void TLN_Deinit();

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteContext(IntPtr context);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetContext(IntPtr context);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetContext();

        [DllImport(NativeLibName)]
        public static extern int TLN_GetWidth();

        [DllImport(NativeLibName)]
        public static extern int TLN_GetHeight();

        [DllImport(NativeLibName)]
        public static extern uint TLN_GetNumObjects();

        [DllImport(NativeLibName)]
        public static extern uint TLN_GetUsedMemory();

        [DllImport(NativeLibName)]
        public static extern uint TLN_GetVersion();

        [DllImport(NativeLibName)]
        public static extern int TLN_GetNumLayers();

        [DllImport(NativeLibName)]
        public static extern int TLN_GetNumSprites();

        [DllImport(NativeLibName)]
        public static extern void TLN_SetBGColor(byte r, byte g, byte b);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetBGColorFromTilemap(IntPtr tilemap);

        [DllImport(NativeLibName)]
        public static extern void TLN_DisableBGColor();

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetBGBitmap(IntPtr bitmap);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetBGPalette(IntPtr palette);

        [DllImport(NativeLibName)]
        public static extern void TLN_SetRasterCallback(TLN_VideoCallback videoCallback);

        [DllImport(NativeLibName)]
        public static extern void TLN_SetFrameCallback(TLN_VideoCallback videoCallback);

        [DllImport(NativeLibName)]
        public static extern void TLN_SetRenderTarget(byte[] data, int pitch);

        [DllImport(NativeLibName)]
        public static extern void TLN_UpdateFrame(int frame);

        [DllImport(NativeLibName)]
        public static extern void TLN_SetLoadPath(string path);

        [DllImport(NativeLibName)]
        public static extern void TLN_SetCustomBlendFunction(TLN_BlendFunction blendFunction);

        [DllImport(NativeLibName)]
        public static extern void TLN_SetLogLevel(TLN_LogLevel logLevel);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_OpenResourcePack(string filename, string key);

        [DllImport(NativeLibName)]
        public static extern void TLN_CloseResourcePack();

        #endregion

        #region Errors

        [DllImport(NativeLibName)]
        public static extern void TLN_SetLastError(TLN_Error error);

        [DllImport(NativeLibName)]
        public static extern TLN_Error TLN_GetLastError();

        [DllImport(NativeLibName)]
        public static extern string TLN_GetErrorString(TLN_Error error);

        #endregion

        #region Window

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_CreateWindow(string? overlay, int flags);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_CreateWindowThread(string overlay, int flags);

        [DllImport(NativeLibName)]
        public static extern void TLN_SetWindowTitle(string title);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ProcessWindow();

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_IsWindowActive();

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetInput(TLN_Input id);

        [DllImport(NativeLibName)]
        public static extern void TLN_EnableInput(TLN_Player player, bool enable);

        [DllImport(NativeLibName)]
        public static extern void TLN_AssignInputJoystick(TLN_Player player, int index);

        [DllImport(NativeLibName)]
        public static extern void TLN_DefineInputKey(TLN_Player player, TLN_Input input, uint keycode);

        [DllImport(NativeLibName)]
        public static extern void TLN_DefineInputButton(TLN_Player player, TLN_Input input, byte joyButton);

        [DllImport(NativeLibName)]
        public static extern void TLN_DrawFrame(int frame);

        [DllImport(NativeLibName)]
        public static extern void TLN_WaitRedraw();

        [DllImport(NativeLibName)]
        public static extern void TLN_DeleteWindow();

        [DllImport(NativeLibName)]
        public static extern void TLN_EnableBlur(bool mode);

        [DllImport(NativeLibName)]
        public static extern void TLN_EnableCRTEffect(TLN_Overlay overlay, byte overlayFactor, byte threshold, byte v0, byte v1, byte v2, byte v3, bool blur, byte glowFactor);

        [DllImport(NativeLibName)]
        public static extern void TLN_DisableCRTEffect();

        [DllImport(NativeLibName)]
        public static extern void TLN_SetSDLCallback(TLN_SDLCallback callback);

        [DllImport(NativeLibName)]
        public static extern void TLN_Delay(uint ms);

        [DllImport(NativeLibName)]
        public static extern uint TLN_GetTicks();

        [DllImport(NativeLibName)]
        public static extern int TLN_GetWindowWidth();

        [DllImport(NativeLibName)]
        public static extern int TLN_GetWindowHeight();

        #endregion

        #region Spriteset

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateSpriteset(IntPtr bitmap, TLN_SpriteData data, int num_entries);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_LoadSpriteset(string name);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CloneSpriteset(IntPtr src);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSpriteInfo(IntPtr spriteset, int entry, TLN_SpriteInfo info);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetSpritesetPalette(IntPtr spriteset);

        [DllImport(NativeLibName)]
        public static extern int TLN_FindSpritesetSprite(IntPtr spriteset, string name);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritesetData(IntPtr spriteset, int entry, TLN_SpriteData[] data, IntPtr pixels, int pitch);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteSpriteset(IntPtr Spriteset);

        #endregion

        #region Tileset

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateTileset(int numtiles, int width, int height, IntPtr palette, IntPtr sp, TLN_TileAttribute[] attributes);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateImageTileset(int numtiles, TLN_TileImage[] images);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_LoadTileset(string filename);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CloneTileset(IntPtr src);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetTilesetPixels(IntPtr tileset, int entry, byte[] srcdata, int srcpitch);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetTileWidth(IntPtr tileset);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetTileHeight(IntPtr tileset);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetTilesetNumTiles(IntPtr tileset);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetTilesetPalette(IntPtr tileset);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetTilesetSequencePack(IntPtr tileset);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteTileset(IntPtr tileset);

        #endregion

        #region Tilemap

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateTilemap(int rows, int cols, TLN_Tile[] tiles, uint bgcolor, IntPtr tileset);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_LoadTilemap(string filename, string? layername);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CloneTilemap(IntPtr src);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetTilemapRows(IntPtr tilemap);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetTilemapCols(IntPtr tilemap);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetTilemapTileset(IntPtr tilemap);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetTilemapTile(IntPtr tilemap, int row, int col, TLN_Tile tile);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetTilemapTile(IntPtr tilemap, int row, int col, TLN_Tile tile);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_CopyTiles(IntPtr src, int srcrow, int srccol, int rows, int cols, IntPtr dst, int dstrow, int dstcol);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteTilemap(IntPtr tilemap);

        #endregion

        #region Palette

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreatePalette(int entries);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_LoadPalette(string filename);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_ClonePalette(IntPtr src);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetPaletteColor(IntPtr palette, int color, byte r, byte g, byte b);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_MixPalettes(IntPtr src1, IntPtr src2, IntPtr dst, byte factor);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_AddPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SubPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ModPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetPaletteData(IntPtr palette, int index);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeletePalette(IntPtr palette);

        #endregion

        #region Bitmap

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateBitmap(int width, int height, int bpp);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_LoadBitmap(string filename);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CloneBitmap(IntPtr src);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetBitmapPtr(IntPtr bitmap, int x, int y);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetBitmapWidth(IntPtr bitmap);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetBitmapHeight(IntPtr bitmap);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetBitmapDepth(IntPtr bitmap);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetBitmapPitch(IntPtr bitmap);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetBitmapPalette(IntPtr bitmap);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetBitmapPalette(IntPtr bitmap, IntPtr palette);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteBitmap(IntPtr bitmap);

        #endregion

        #region Objects

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateObjectList();

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_AddTileObjectToList(IntPtr list, ushort id, ushort gid, ushort flags, int x, int y);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_LoadObjectList(string filename, string layername);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CloneObjectList(IntPtr src);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetListNumObjects(IntPtr list);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetListObject(IntPtr list, TLN_ObjectInfo info);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteObjectList(IntPtr list);

        #endregion

        #region Layers

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayer(int nlayer, IntPtr tileset, IntPtr tilemap);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerTilemap(int nlayer, IntPtr tilemap);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerBitmap(int nlayer, IntPtr bitmap);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPalette(int nlayer, IntPtr palette);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPosition(int nlayer, int hstart, int vstart);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerScaling(int nlayer, float xfactor, float yfactor);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerAffineTransform(int nlayer, in TLN_Affine affine);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerTransform(int layer, float angle, float dx, float dy, float sx, float sy);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPixelMapping(int nlayer, in TLN_PixelMap table);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerBlendMode(int nlayer, TLN_Blend mode, byte factor);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerColumnOffset(int nlayer, int[] offset);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerClip(int nlayer, int x1, int y1, int x2, int y2);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayerClip(int nlayer);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerMosaic(int nlayer, int width, int height);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayerMosaic(int nlayer);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ResetLayerMode(int nlayer);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerObjects(int nlayer, IntPtr objects, IntPtr tileset);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPriority(int nlayer, bool enable);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerParent(int nlayer, int parent);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayerParent(int nlayer);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayer(int nlayer);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_EnableLayer(int nlayer);

        [DllImport(NativeLibName)]
        public static extern TLN_LayerType TLN_GetLayerType(int nlayer);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetLayerPalette(int nlayer);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetLayerTileset(int nlayer);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetLayerTilemap(int nlayer);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetLayerBitmap(int nlayer);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetLayerObjects(int nlayer);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetLayerTile(int nlayer, int x, int y, in TLN_TileInfo info);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetLayerWidth(int nlayer);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetLayerHeight(int nlayer);

        #endregion

        #region Sprites

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ConfigSprite(int nsprite, IntPtr spriteset, TLN_TileFlags flags);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteSet(int nsprite, IntPtr spriteset);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteFlags(int nsprite, TLN_TileFlags flags);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_EnableSpriteFlag(int nsprite, TLN_TileFlags flag, bool enable);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritePivot(int nsprite, float px, float py);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritePosition(int nsprite, int x, int y);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritePicture(int nsprite, int entry);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritePalette(int nsprite, IntPtr palette);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteBlendMode(int nsprite, TLN_Blend mode, byte factor);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteScaling(int nsprite, float sx, float sy);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ResetSpriteScaling(int nsprite);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetSpritePicture(int nsprite);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetAvailableSprite();

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_EnableSpriteCollision(int nsprite, bool enable);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSpriteCollision(int nsprite);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSpriteState(int nsprite, TLN_SpriteState state);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetFirstSprite(int nsprite);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetNextSprite(int nsprite, int next);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_EnableSpriteMasking(int nsprite, bool enable);

        [DllImport(NativeLibName)]
        public static extern void TLN_SetSpritesMaskRegion(int top_line, int bottom_line);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteAnimation(int nsprite, IntPtr sequence, int loop);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableSpriteAnimation(int nsprite);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableSprite(int nsprite);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetSpritePalette(int nsprite);

        #endregion

        #region Sequence

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateSequence(string name, int target, int num_frames, TLN_SequenceFrame frames);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateCycle(string name, int numStrips, TLN_ColorStrip strips);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateSpriteSequence(string name, IntPtr spriteSet, string basename, int delay);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CloneSequence(IntPtr src);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSequenceInfo(IntPtr sequence, TLN_SequenceInfo info);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteSequence(IntPtr sequence);

        #endregion

        #region Sequencepack

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_CreateSequencePack();

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_LoadSequencePack(string filename);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_GetSequence(IntPtr sp, int index);

        [DllImport(NativeLibName)]
        public static extern IntPtr TLN_FindSequence(IntPtr sp, string name);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetSequencePackCount(IntPtr sp);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_AddSequenceToPack(IntPtr sp, IntPtr sequence);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteSequencePack(IntPtr sp);

        #endregion

        #region Colorcycle animation

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetPaletteAnimation(int index, IntPtr palette, IntPtr sequence, bool blend);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetPaletteAnimationSource(int index, IntPtr palette);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetAnimationState(int index);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetAnimationDelay(int index, int frame, int delay);

        [DllImport(NativeLibName)]
        public static extern int TLN_GetAvailableAnimation();

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisablePaletteAnimation(int index);

        #endregion

        #region World

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_LoadWorld(string tmxfile, int first_layer);

        [DllImport(NativeLibName)]
        public static extern void TLN_SetWorldPosition(int x, int y);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerParallaxFactor(int nlayer, float x, float y);

        [DllImport(NativeLibName)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteWorldPosition(int nsprite, int x, int y);

        [DllImport(NativeLibName)]
        public static extern void TLN_ReleaseWorld();

        #endregion
    }
}