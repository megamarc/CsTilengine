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
        /// Tile item for Tilemap access functions
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
        /// Frame animation definition.
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
            public TLN_TileFlags flags;

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
            /// Create a fullscreen window.
            /// </summary>
            CWF_FULLSCREEN = (1 << 0),

            /// <summary>
            /// Sync frame updates with vertical retrace.
            /// </summary>
            CWF_VSYNC = (1 << 1),

            /// <summary>
            /// Create a window of the same size as the frame buffer.
            /// </summary>
            CWF_S1 = (1 << 2),

            /// <summary>
            /// Create a window 2x the size of the frame buffer.
            /// </summary>
            CWF_S2 = (2 << 2),

            /// <summary>
            /// Create a window 3x the size of the frame buffer.
            /// </summary>
            CWF_S3 = (3 << 2),

            /// <summary>
            /// Create a window 4x the size of the frame buffer.
            /// </summary>
            CWF_S4 = (4 << 2),

            /// <summary>
            /// Create a window 5x the size of the frame buffer.
            /// </summary>
            CWF_S5 = (5 << 2),

            /// <summary>
            /// Unfiltered scaling
            /// </summary>
            CWF_NEAREST = (1 << 6),
        }

        public enum TLN_Error
        {
            /// <summary>
            /// No error.
            /// </summary>
            TLN_ERR_OK,

            /// <summary>
            /// Not enough memory.
            /// </summary>
            TLN_ERR_OUT_OF_MEMORY,

            /// <summary>
            /// Layer index out of range.
            /// </summary>
            TLN_ERR_IDX_LAYER,

            /// <summary>
            /// Sprite index out of range.
            /// </summary>
            TLN_ERR_IDX_SPRITE,

            /// <summary>
            /// Animation index out of range.
            /// </summary>
            TLN_ERR_IDX_ANIMATION,

            /// <summary>
            /// Picture or tile index out of range.
            /// </summary>
            TLN_ERR_IDX_PICTURE,

            /// <summary>
            /// Invalid IntPtr reference.
            /// </summary>
            TLN_ERR_REF_TILESET,

            /// <summary>
            /// Invalid TLN_Tilemap reference.
            /// </summary>
            TLN_ERR_REF_TILEMAP,

            /// <summary>
            /// Invalid IntPtr reference.
            /// </summary>
            TLN_ERR_REF_SPRITESET,

            /// <summary>
            /// Invalid IntPtr reference.
            /// </summary>
            TLN_ERR_REF_PALETTE,

            /// <summary>
            /// Invalid TLN_Sequence reference.
            /// </summary>
            TLN_ERR_REF_SEQUENCE,

            /// <summary>
            /// Invalid TLN_SequencePack reference.
            /// </summary>
            TLN_ERR_REF_SEQPACK,

            /// <summary>
            /// Invalid TLN_Bitmap reference.
            /// </summary>
            TLN_ERR_REF_BITMAP,

            /// <summary>
            /// Null pointer as argument.
            /// </summary>
            TLN_ERR_NULL_POINTER,

            /// <summary>
            /// Resource file not found.
            /// </summary>
            TLN_ERR_FILE_NOT_FOUND,

            /// <summary>
            /// Resource file has invalid format.
            /// </summary>
            TLN_ERR_WRONG_FORMAT,

            /// <summary>
            /// A width or height parameter is invalid.
            /// </summary>
            TLN_ERR_WRONG_SIZE,

            /// <summary>
            /// Unsupported function.
            /// </summary>
            TLN_ERR_UNSUPPORTED,

            /// <summary>
            /// Invalid TLN_ObjectList reference.
            /// </summary>
            TLN_ERR_REF_LIST
        }

        public enum TLN_LogLevel
        {
            /// <summary>
            /// Don't print anything. (default)
            /// </summary>
            TLN_LOG_NONE,

            /// <summary>
            /// Print only runtime errors.
            /// </summary>
            TLN_LOG_ERRORS,

            /// <summary>
            /// Print everything.
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
        /// Player index for input assignment functions.
        /// </summary>
        public enum TLN_Player
        {
            PLAYER1,
            PLAYER2,
            PLAYER3,
            PLAYER4,
        }

        /// <summary>
        /// Standard inputs query for <see cref="TLN_GetInput"/>.
        /// Up to 32 unique inputs.
        /// </summary>
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
            /// 1st action button. <br/>
            /// Part of the compatibility symbols for pre-1.18 input model.
            /// </summary>
            INPUT_A = INPUT_BUTTON1,

            /// <summary>
            /// 2nd action button. <br/>
            /// Part of the compatibility symbols for pre-1.18 input model.
            /// </summary>
            INPUT_B = INPUT_BUTTON2,

            /// <summary>
            /// 3th action button. <br/>
            /// Part of the compatibility symbols for pre-1.18 input model.
            /// </summary>
            INPUT_C = INPUT_BUTTON3,

            /// <summary>
            /// 4th action button.
            /// Part of the compatibility symbols for pre-1.18 input model.
            /// </summary>
            INPUT_D = INPUT_BUTTON4,

            /// <summary>
            /// 5th action button. <br/>
            /// Part of the compatibility symbols for pre-1.18 input model.
            /// </summary>
            INPUT_E = INPUT_BUTTON5,

            /// <summary>
            /// 6th action button. <br/>
            /// Part of the compatibility symbols for pre-1.18 input model.
            /// </summary>
            INPUT_F = INPUT_BUTTON6,
        }

        #endregion

        #region Setup

        /// <summary>
        /// Initializes the graphic engine.
        /// </summary>
        /// <remarks>
        /// Performs initialization of the main engine, creates the viewport with the specified dimensions
        /// and allocates the number of layers, sprites and animation slots.
        /// </remarks>
        /// <param name="hres">Horizontal resolution in pixels</param>
        /// <param name="vres">Vertical resolution in pixels</param>
        /// <param name="numlayers">Number of layers</param>
        /// <param name="numsprites">Number of sprites</param>
        /// <param name="numanimations">Number of palette animation slots</param>
        /// <returns>TLN_Engine object reference.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_Init(int hres, int vres, int numlayers, int numsprites, int numanimations);

        /// <summary>
        /// Deinitializes the current engine context and frees up used resources.
        /// </summary>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_Deinit();

        /// <summary>
        /// Deletes explicit context.
        /// </summary>
        /// <param name="context">Context reference to delete.</param>
        /// <returns>true if successful or false if an invalid context is supplied.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteContext(IntPtr context);

        /// <summary>
        /// Sets current engine context.
        /// </summary>
        /// <param name="context">TLN_Engine object reference to set as current context, returned by TLN_Init().</param>
        /// <returns>true if successful or false if an invalid context reference is supplied.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetContext(IntPtr context);

        /// <summary>
        /// Returns the current engine context.
        /// </summary>
        /// <returns>TLN_Engine object reference.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetContext();

        /// <summary>
        /// Returns the width in pixels of the framebuffer.
        /// </summary>
        /// <returns>Integer value of width.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetWidth();

        /// <summary>
        /// Returns the height in pixels of the framebuffer.
        /// </summary>
        /// <returns>Integer value of height.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetHeight();

        /// <summary>
        /// Retrieves the number of objects used by Tilengine so far.
        /// </summary>
        /// <remarks>
        /// The objects are the total number of tilesets, tilemaps, spritesets, palettes or sequences combined.
        /// </remarks>
        /// <returns>The total number of objects.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint TLN_GetNumObjects();

        /// <summary>
        /// Retrieves the total amount of memory used by the objects.
        /// </summary>
        /// <remarks>
        /// The objects are the total number of tilesets, tilemaps, spritesets, palettes or sequences combined.
        /// </remarks>
        /// <returns>The total amount of memory used.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint TLN_GetUsedMemory();

        /// <summary>
        /// Retrieves the Tilengine DLL version.
        /// </summary>
        /// <returns>
        /// 32-bit integer containing three packed numbers:
        /// <list type="bullet">
        ///     <item>
        ///         <term>Bits 23:16</term>
        ///         <description>Major version.</description>
        ///     </item>
        ///     <item>
        ///         <term>Bits 15:8</term>
        ///         <description>Minor version.</description>
        ///     </item>
        ///     <item>
        ///         <term>Bits 7:0</term>
        ///         <description>Bugfix revision.</description>
        ///     </item>
        /// </list>
        /// </returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint TLN_GetVersion();

        /// <summary>
        /// Retrieves the number of layers specified during initialization.
        /// </summary>
        /// <returns>The number of layers.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetNumLayers();

        /// <summary>
        /// Retrieves the number of sprites specified during initialization.
        /// </summary>
        /// <returns>The number of sprites.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetNumSprites();

        /// <summary>
        /// Sets the background color.
        /// </summary>
        /// <remarks>
        /// The background color is the color of the pixel when there isn't any layer or sprite at that position. <br/>
        /// This function can be called during a raster callback to create gradient backgrounds.
        /// </remarks>
        /// <param name="r">Red component (0-255)</param>
        /// <param name="g">Green component (0-255)</param>
        /// <param name="b">Blue component (0-255)</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetBGColor(byte r, byte g, byte b);

        /// <summary>
        /// Sets the background color from a tilemap defined color.
        /// </summary>
        /// <param name="tilemap">Reference to the tilemap with the background color to set.</param>
        /// <returns>true if successful or false if an invalid tilemap reference was supplied.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetBGColorFromTilemap(IntPtr tilemap);

        /// <summary>
        /// Disables background color rendering.
        /// </summary>
        /// <remarks>
        /// If you know that the last background layer will always
        /// cover the entire screen, you can disable it to gain some performance.
        /// </remarks>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_DisableBGColor();

        /// <summary>
        /// Sets a static bitmap as background.
        /// Unlike tilemaps or sprites, this bitmap cannot be moved and has no transparency.
        /// </summary>
        /// <param name="bitmap">Reference to bitmap for the background. Set null to disable.</param>
        /// <returns>true if successful or false if an invalid bitmap reference was supplied.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetBGBitmap(IntPtr? bitmap);

        /// <summary>
        /// Changes the palette for the background bitmap.
        /// </summary>
        /// <param name="palette">Reference to palette.</param>
        /// <returns>true if successful or false if an invalid palette reference was supplied.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetBGPalette(IntPtr palette);

        /// <summary>
        /// Specifies the address of the function to call for each drawn scanline.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Tilengine renders its output line by line, just as the 2D graphics chips did. <br/>
        ///         The raster callback is a way to simulate the "horizontal blanking interrupt" of those systems,
        ///         where many parameters of the rendering can be modified per line.
        ///     </para>
        ///     <para>
        ///         Setting a raster callback is optional, but much of the fun of using Tilengine comes from
        ///         the use of raster effects.
        ///     </para>
        /// </remarks>
        /// <param name="videoCallback">Address of the function to call.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetRasterCallback(TLN_VideoCallback videoCallback);

        /// <summary>
        /// Specifies the address of the function to call for each drawn frame.
        /// </summary>
        /// <param name="videoCallback">Address of the function to call.</param>

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetFrameCallback(TLN_VideoCallback videoCallback);

        /// <summary>
        /// Sets the output surface for rendering.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Tilengine does not provide windowing or hardware video access. <br/>
        ///         The application is responsible for assigning and maintaining the surface where
        ///         Tilengine performs the rendering.
        ///     </para>
        ///     <para>
        ///         It can be an SDL surface, a locked DirectX surface,
        ///         an OpenGL texture, or whatever else the application has access to.
        ///     </para>
        /// </remarks>
        /// <param name="data">Pointer to the start of the target framebuffer.</param>
        /// <param name="pitch">Number of bytes for each scanline of the framebuffer.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetRenderTarget(IntPtr data, int pitch);

        /// <summary>
        /// Draws the frame to the previously specified render target.
        /// </summary>
        /// <param name="frame">Optional frame number. Set to 0 to autoincrement from previous value.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_UpdateFrame(int frame);

        /// <summary>
        /// Sets base path for TLN_Load functions.
        /// </summary>
        /// <param name="path">Base path. Files will load at path/filename. Can be NULL.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void TLN_SetLoadPath(string? path);

        /// <summary>
        /// Sets custom blend function to use when BLEND_CUSTOM mode is selected.
        /// </summary>
        /// <param name="blendFunction">
        ///     <para>
        ///         A delegate to a user-provided function that takes two parameters: <br/>
        ///         The source component intensity. <br/>
        ///         The destination component intensity.
        ///     </para>
        ///     <para>
        ///         The delegate should return the desired intensity. This function is
        ///         called for each RGB component when blending is enabled.
        ///     </para>
        /// </param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetCustomBlendFunction(TLN_BlendFunction blendFunction);

        /// <summary>
        /// Sets the logging level for the current Tilengine instance.
        /// </summary>
        /// <param name="logLevel">Member of the <see cref="TLN_LogLevel"/> enumeration.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetLogLevel(TLN_LogLevel logLevel);

        /// <summary>
        /// Open the resource package with optional AES-128 key and binds it.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When the package is opened, it's globally bind to all TLN_LoadXXX functions. <br/>
        ///         The assets inside the package are indexed with their original path/file as
        ///         when they were plain files.
        ///     </para>
        ///     <para>
        ///         As long as the structure used to build the package matches
        ///         the original structure of the assets, <br/>
        ///         the TLN_SetLoadPath() and the TLN_LoadXXX functions will work transparently,
        ///         easing the migration with minimal changes.
        ///     </para>
        /// </remarks>
        /// <param name="filename">File with the resource package (.dat extension)</param>
        /// <param name="key">Optional null-terminated ASCII string with aes decryption key.</param>
        /// <returns>true if the package was opened and made current, or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_OpenResourcePack(string filename, string key);

        /// <summary>
        /// Closes current resource package and unbinds it.
        /// </summary>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_CloseResourcePack();

        #endregion

        #region Errors

        /// <summary>
        /// Sets the global error code of Tilengine.
        /// Useful for custom loaders that need to set the error state.
        /// </summary>
        /// <param name="error">Error code to set.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetLastError(TLN_Error error);

        /// <summary>
        /// Returns the last error after an invalid operation.
        /// </summary>
        /// <returns>The last error code.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern TLN_Error TLN_GetLastError();

        /// <summary>
        /// Returns the string description of the specified error code.
        /// </summary>
        /// <param name="error">Error code to get the description from.</param>
        /// <returns>The description of the specified error code.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern string TLN_GetErrorString(TLN_Error error);

        #endregion

        #region Window

        /// <summary>
        /// Creates a window for rendering.
        /// </summary>
        /// <remarks>
        ///     Creates a host window with basic user input for Tilengine. <br/>
        ///     If fullscreen, it uses the desktop resolution and stretches the output resolution
        ///     with aspect correction, letterboxing or pillarboxing as needed. <br/>
        ///     If windowed, it creates a centered window that is the maximum
        ///     possible integer multiple of the resolution configured in TLN_Init()
        /// </remarks>
        /// <param name="overlay">
        ///     Optional path of a bmp file to overlay (for emulating RGB mask, scanlines, etc.)
        /// </param>
        /// <param name="flags">
        ///     Mask of the possible creation flags: <br/>
        ///     CWF_FULLSCREEN, CWF_VSYNC, CWF_S1 - CWF_S5 (scaling factor, none = auto max)
        /// </param>
        /// <returns>true if the window was created or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_CreateWindow(string? overlay, TLN_CreateWindowFlags flags);

        /// <summary>
        /// Creates a multi-threaded window for rendering.
        /// </summary>
        /// <remarks>
        ///     Creates a host window with basic user input for Tilengine. <br/>
        ///     If fullscreen, it uses the desktop resolution and stretches the output resolution
        ///     with aspect correction, letterboxing or pillarboxing as needed. <br/>
        ///     If windowed, it creates a centered window that is the maximum
        ///     possible integer multiple of the resolution configured in TLN_Init()
        /// </remarks>
        /// <param name="overlay">
        ///     Optional path of a bmp file to overlay (for emulating RGB mask, scanlines, etc.)
        /// </param>
        /// <param name="flags">
        ///     Mask of the possible creation flags: <br/>
        ///     CWF_FULLSCREEN, CWF_VSYNC, CWF_S1 - CWF_S5 (scaling factor, none = auto max)
        /// </param>
        /// <returns>true if the window was created or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_CreateWindowThread(string overlay, TLN_CreateWindowFlags flags);

        /// <summary>
        /// Sets window title.
        /// </summary>
        /// <param name="title">Text with the title to set.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetWindowTitle(string title);

        /// <summary>
        /// Does basic window housekeeping in single-threaded window.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If a window has been created with TLN_CreateWindow,
        ///         this function must be called periodically <br/>
        ///         (call it inside the main loop so it gets called regularly).
        ///     </para>
        ///     <para>
        ///         <b>If the window was created with TLN_CreateWindowThread, do not use this.</b>
        ///     </para>
        /// </remarks>
        /// <returns>
        /// true if window is active or false if the user has requested to end the application. <br/>
        /// (by pressing Esc key or clicking the close button)
        /// </returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ProcessWindow();

        /// <summary>
        /// Gets window state.
        /// </summary>
        /// <returns>
        /// true if window is active or false if the user has requested to end the application. <br/>
        /// (by pressing Esc key or clicking the close button)
        /// </returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_IsWindowActive();

        /// <summary>
        /// Returns the state of a given input
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If a window was created with TLN_CreateWindow, this function provides basic user input. <br/>
        ///         It simulates a classic arcade setup, with 4 directional buttons (INPUT_UP to INPUT_RIGHT),
        ///         <br/> 6 action buttons (INPUT_BUTTON1 to INPUT_BUTTON6) and a start button (INPUT_START).
        ///     </para>
        ///     <para>
        ///         By default directional buttons are mapped to keyboard cursors and joystick 1 D-PAD. <br/>
        ///         The first four action buttons are the keys Z,X,C,V and joystick buttons 1 to 4.
        ///     </para>
        /// </remarks>
        /// <param name="id">
        /// Input to check state. It should be an attribute from <see cref="TLN_Input"/><br/>
        /// Combine this with INPUT_P1 to INPUT_P4 to request input from a specific player.
        /// </param>
        /// <returns>true if the provided input is pressed, otherwise false.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetInput(TLN_Input id);

        /// <summary>
        /// Enables or disables input for a specific player.
        /// </summary>
        /// <param name="player">Player number to toggle (PLAYER1 - PLAYER4)</param>
        /// <param name="enable">Set true to enable, false to disable.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_EnableInput(TLN_Player player, bool enable);

        /// <summary>
        /// Assigns a joystick index to the specified player.
        /// </summary>
        /// <param name="player">Player number to configure (PLAYER1 - PLAYER4)</param>
        /// <param name="index">Joystick index to assign, 0-based index. -1 = disable</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_AssignInputJoystick(TLN_Player player, int index);

        /// <summary>
        /// Assigns a keyboard input to a player.
        /// </summary>
        /// <param name="player">Player number to configure (PLAYER1 - PLAYER4)</param>
        /// <param name="input">Input to associate to the given key.</param>
        /// <param name="keycode">ASCII key value or scancode as defined in SDL.h</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_DefineInputKey(TLN_Player player, TLN_Input input, uint keycode);

        /// <summary>
        /// Assigns a button joystick input to a player.
        /// </summary>
        /// <param name="player">Player number to configure (PLAYER1 - PLAYER4)</param>
        /// <param name="input">Input to associate to the given button</param>
        /// <param name="joyButton">Button index</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_DefineInputButton(TLN_Player player, TLN_Input input, byte joyButton);

        /// <summary>
        /// Draws a frame to the window
        /// </summary>
        /// <remarks>
        /// If a window has been created with TLN_CreateWindow(), it renders the frame to it. <br/>
        /// This function is a wrapper to TLN_UpdateFrame, which also automatically sets the render target
        /// for the window, so when calling this function it is not needed to call TLN_UpdateFrame() too.
        /// </remarks>
        /// <param name="frame">Optional frame number. Set to 0 to autoincrement from previous value.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_DrawFrame(int frame);

        /// <summary>
        /// Thread synchronization for multi-threaded window. Waits until the current
        /// frame has ended rendering.
        /// </summary>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_WaitRedraw();

        /// <summary>
        /// Deletes the window previously created with TLN_CreateWindow() or TLN_CreateWindowThread()
        /// </summary>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_DeleteWindow();

        /// <summary>
        /// <b>Removed in release 1.12, use TLN_EnableCRTEffect() instead.</b>
        /// </summary>
        /// <param name="mode">Enable or disable effect</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_EnableBlur(bool mode);

        /// <summary>
        /// Enables CRT post-processing effect to give a true retro appearance.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This function combines various effects to simulate the output of
        ///         a CRT monitor with low CPU/GPU usage. <br/>
        ///         A small horizontal blur is added to the frame, simulating the continuous output of a
        ///         RF modulator where adjacent pixels got mixed.
        ///     </para>
        ///     <para>
        ///         Many graphic designers use this feature where alternating vertical lines are used to
        ///         create the illusion of more colors or blending. <br/>
        ///         A secondary image is created with overly bright pixels. In a real CRT, brighter colors bleed
        ///         into the surrounding area. The pixel size depends somewhat on its brightness.
        ///     </para>
        ///     <para>
        ///         The threshold and v0 to v3 parameters define a two-segment linear mapping between
        ///         source and destination brightness for the overlay. Optionally the overlay can be softened
        ///         more using a slight gaussian blur filter to create a kind of "bloom"
        ///         effect. This effect is added on top of the frame with the glow_factor value.
        ///     </para>
        /// </remarks>
        /// <param name="overlay">
        /// One of the enumerated <see cref="TLN_Overlay"/> types. <br/>
        /// Choosing TLN_OVERLAY_CUSTOM selects the image passed when calling TLN_CreateWindow.
        /// </param>
        /// <param name="overlayFactor">
        /// Blend factor for the overlay image. <br/>
        /// 0 is full transparent (no effect), 255 is full blending.
        /// </param>
        /// <param name="threshold">
        /// Middle point of the brightness mapping function.
        /// </param>
        /// <param name="v0">Output brightness for input brightness = 0</param>
        /// <param name="v1">Output brightness for input brightness = threshold</param>
        /// <param name="v2">Output brightness for input brightness = threshold</param>
        /// <param name="v3">Output brightness for input brightness = 255</param>
        /// <param name="blur">Adds gaussian blur to brightness overlay, softens image.</param>
        /// <param name="glowFactor">
        /// blend addition factor for brightness overlay.
        /// 0 is not addition, 255 is full addition
        /// </param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_EnableCRTEffect(TLN_Overlay overlay, byte overlayFactor, byte threshold, byte v0, byte v1, byte v2, byte v3, bool blur, byte glowFactor);

        /// <summary>
        /// Disables the CRT post-processing effect.
        /// </summary>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_DisableCRTEffect();

        /// <summary>
        /// Registers a user-defined callback to capture internal SDL2 events.
        /// </summary>
        /// <param name="callback">Callback pointer to a TLN_SDLCallback delegate.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetSDLCallback(TLN_SDLCallback callback);

        /// <summary>
        /// Suspends execution for a certain time.
        /// </summary>
        /// <param name="ms">Number of milliseconds to wait.</param>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_Delay(uint ms);

        /// <summary>
        /// Returns the number of milliseconds since the start of the application.
        /// </summary>
        /// <returns></returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint TLN_GetTicks();

        /// <summary>
        /// Returns horizontal dimension of the window after scaling.
        /// </summary>
        /// <returns>Integer value of the window width.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetWindowWidth();

        /// <summary>
        /// Returns vertical dimension of the window after scaling.
        /// </summary>
        /// <returns>Integer value of the window height.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetWindowHeight();

        #endregion

        #region Spriteset

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateSpriteset(IntPtr bitmap, TLN_SpriteData data, int num_entries);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_LoadSpriteset(string name);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneSpriteset(IntPtr src);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSpriteInfo(IntPtr spriteset, int entry, TLN_SpriteInfo info);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetSpritesetPalette(IntPtr spriteset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TLN_FindSpritesetSprite(IntPtr spriteset, string name);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritesetData(IntPtr spriteset, int entry, TLN_SpriteData[] data, IntPtr pixels, int pitch);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteSpriteset(IntPtr Spriteset);

        #endregion

        #region Tileset

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateTileset(int numtiles, int width, int height, IntPtr palette, IntPtr sp, TLN_TileAttribute[] attributes);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateImageTileset(int numtiles, TLN_TileImage[] images);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadTileset(string filename);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneTileset(IntPtr src);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetTilesetPixels(IntPtr tileset, int entry, byte[] srcdata, int srcpitch);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTileWidth(IntPtr tileset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTileHeight(IntPtr tileset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTilesetNumTiles(IntPtr tileset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetTilesetPalette(IntPtr tileset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetTilesetSequencePack(IntPtr tileset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteTileset(IntPtr tileset);

        #endregion

        #region Tilemap

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateTilemap(int rows, int cols, TLN_Tile[] tiles, uint bgcolor, IntPtr tileset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadTilemap(string filename, string? layername);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneTilemap(IntPtr src);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTilemapRows(IntPtr tilemap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTilemapCols(IntPtr tilemap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetTilemapTileset(IntPtr tilemap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetTilemapTile(IntPtr tilemap, int row, int col, TLN_Tile tile);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetTilemapTile(IntPtr tilemap, int row, int col, TLN_Tile tile);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_CopyTiles(IntPtr src, int srcrow, int srccol, int rows, int cols, IntPtr dst, int dstrow, int dstcol);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteTilemap(IntPtr tilemap);

        #endregion

        #region Palette

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreatePalette(int entries);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadPalette(string filename);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_ClonePalette(IntPtr src);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetPaletteColor(IntPtr palette, int color, byte r, byte g, byte b);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_MixPalettes(IntPtr src1, IntPtr src2, IntPtr dst, byte factor);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_AddPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SubPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ModPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetPaletteData(IntPtr palette, int index);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeletePalette(IntPtr palette);

        #endregion

        #region Bitmap

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateBitmap(int width, int height, int bpp);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadBitmap(string filename);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneBitmap(IntPtr src);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetBitmapPtr(IntPtr bitmap, int x, int y);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetBitmapWidth(IntPtr bitmap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetBitmapHeight(IntPtr bitmap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetBitmapDepth(IntPtr bitmap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetBitmapPitch(IntPtr bitmap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetBitmapPalette(IntPtr bitmap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetBitmapPalette(IntPtr bitmap, IntPtr palette);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteBitmap(IntPtr bitmap);

        #endregion

        #region Objects

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateObjectList();

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_AddTileObjectToList(IntPtr list, ushort id, ushort gid, ushort flags, int x, int y);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadObjectList(string filename, string layername);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneObjectList(IntPtr src);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetListNumObjects(IntPtr list);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetListObject(IntPtr list, TLN_ObjectInfo info);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteObjectList(IntPtr list);

        #endregion

        #region Layers

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayer(int nlayer, IntPtr tileset, IntPtr tilemap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerTilemap(int nlayer, IntPtr tilemap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerBitmap(int nlayer, IntPtr bitmap);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPalette(int nlayer, IntPtr palette);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPosition(int nlayer, int hstart, int vstart);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerScaling(int nlayer, float xfactor, float yfactor);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerAffineTransform(int nlayer, in TLN_Affine affine);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerTransform(int layer, float angle, float dx, float dy, float sx, float sy);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPixelMapping(int nlayer, in TLN_PixelMap table);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerBlendMode(int nlayer, TLN_Blend mode, byte factor);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerColumnOffset(int nlayer, int[] offset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerClip(int nlayer, int x1, int y1, int x2, int y2);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayerClip(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerMosaic(int nlayer, int width, int height);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayerMosaic(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ResetLayerMode(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerObjects(int nlayer, IntPtr objects, IntPtr tileset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPriority(int nlayer, bool enable);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerParent(int nlayer, int parent);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayerParent(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayer(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_EnableLayer(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern TLN_LayerType TLN_GetLayerType(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerPalette(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerTileset(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerTilemap(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerBitmap(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerObjects(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetLayerTile(int nlayer, int x, int y, in TLN_TileInfo info);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetLayerWidth(int nlayer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetLayerHeight(int nlayer);

        #endregion

        #region Sprites

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ConfigSprite(int nsprite, IntPtr spriteset, TLN_TileFlags flags);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteSet(int nsprite, IntPtr spriteset);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteFlags(int nsprite, TLN_TileFlags flags);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_EnableSpriteFlag(int nsprite, TLN_TileFlags flag, bool enable);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritePivot(int nsprite, float px, float py);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritePosition(int nsprite, int x, int y);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritePicture(int nsprite, int entry);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritePalette(int nsprite, IntPtr palette);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteBlendMode(int nsprite, TLN_Blend mode, byte factor);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteScaling(int nsprite, float sx, float sy);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ResetSpriteScaling(int nsprite);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetSpritePicture(int nsprite);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetAvailableSprite();

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_EnableSpriteCollision(int nsprite, bool enable);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSpriteCollision(int nsprite);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSpriteState(int nsprite, TLN_SpriteState state);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetFirstSprite(int nsprite);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetNextSprite(int nsprite, int next);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_EnableSpriteMasking(int nsprite, bool enable);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetSpritesMaskRegion(int top_line, int bottom_line);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteAnimation(int nsprite, IntPtr sequence, int loop);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableSpriteAnimation(int nsprite);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableSprite(int nsprite);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetSpritePalette(int nsprite);

        #endregion

        #region Sequence

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_CreateSequence(string name, int target, int num_frames, TLN_SequenceFrame frames);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_CreateCycle(string name, int numStrips, TLN_ColorStrip strips);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_CreateSpriteSequence(string name, IntPtr spriteSet, string basename, int delay);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneSequence(IntPtr src);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSequenceInfo(IntPtr sequence, TLN_SequenceInfo info);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteSequence(IntPtr sequence);

        #endregion

        #region Sequence Pack

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateSequencePack();

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadSequencePack(string filename);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetSequence(IntPtr sp, int index);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_FindSequence(IntPtr sp, string name);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetSequencePackCount(IntPtr sp);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_AddSequenceToPack(IntPtr sp, IntPtr sequence);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteSequencePack(IntPtr sp);

        #endregion

        #region Colorcycle animation

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetPaletteAnimation(int index, IntPtr palette, IntPtr sequence, bool blend);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetPaletteAnimationSource(int index, IntPtr palette);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetAnimationState(int index);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetAnimationDelay(int index, int frame, int delay);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetAvailableAnimation();

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisablePaletteAnimation(int index);

        #endregion

        #region World

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_LoadWorld(string tmxfile, int first_layer);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_SetWorldPosition(int x, int y);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerParallaxFactor(int nlayer, float x, float y);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteWorldPosition(int nsprite, int x, int y);

        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TLN_ReleaseWorld();

        #endregion
    }
}