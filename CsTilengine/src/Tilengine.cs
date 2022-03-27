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
            /// Sprite is enabled.
            /// </summary>
            public bool enabled;

            /// <summary>
            /// Pixel collision detection is enabled.
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
            /// Tile goes in front of sprite layer.
            /// </summary>
            FLAG_PRIORITY = 1 << 12,

            /// <summary>
            /// Sprite won't be drawn inside masked region.
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
        /// Creates a host window with basic user input for Tilengine. <br/>
        /// If fullscreen, it uses the desktop resolution and stretches the output resolution
        /// with aspect correction, letterboxing or pillarboxing as needed. <br/>
        /// If windowed, it creates a centered window that is the maximum
        /// possible integer multiple of the resolution configured in TLN_Init()
        /// </remarks>
        /// <param name="overlay">
        /// Optional path of a bmp file to overlay (for emulating RGB mask, scanlines, etc.)
        /// </param>
        /// <param name="flags">
        /// Mask of the possible creation flags: <br/>
        /// CWF_FULLSCREEN, CWF_VSYNC, CWF_S1 - CWF_S5 (scaling factor, none = auto max)
        /// </param>
        /// <returns>true if the window was created or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_CreateWindow(string? overlay, TLN_CreateWindowFlags flags);

        /// <summary>
        /// Creates a multi-threaded window for rendering.
        /// </summary>
        /// <remarks>
        /// Creates a host window with basic user input for Tilengine. <br/>
        /// If fullscreen, it uses the desktop resolution and stretches the output resolution
        /// with aspect correction, letterboxing or pillarboxing as needed. <br/>
        /// If windowed, it creates a centered window that is the maximum
        /// possible integer multiple of the resolution configured in TLN_Init()
        /// </remarks>
        /// <param name="overlay">
        /// Optional path of a bmp file to overlay (for emulating RGB mask, scanlines, etc.)
        /// </param>
        /// <param name="flags">
        /// Mask of the possible creation flags: <br/>
        /// CWF_FULLSCREEN, CWF_VSYNC, CWF_S1 - CWF_S5 (scaling factor, none = auto max)
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

        /// <summary>
        /// Creates a new spriteset.
        /// </summary>
        /// <param name="bitmap">Bitmap reference containing the sprite graphics.</param>
        /// <param name="data">Array of TLN_SpriteData structures with sprite descriptions.</param>
        /// <param name="num_entries">Number of entries in the TLN_SpriteData array.</param>
        /// <returns>
        /// Reference to the created spriteset, or <see cref="IntPtr.Zero"/> if an error occurred.
        /// </returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateSpriteset(IntPtr bitmap, TLN_SpriteData[] data, int num_entries);

        /// <summary>
        /// Loads a spriteset from an image png and its associated atlas descriptor.
        /// </summary>
        /// <remarks>
        /// The spriteset comes in a pair of files: an image file (bmp or png)
        /// and a standard atlas descriptor (json, csv or txt)
        /// The supported json format is the array.
        /// </remarks>
        /// <param name="name">
        /// Base name of the files containing the spriteset, with or without .png extension.
        /// </param>
        /// <returns>
        /// Reference to the newly loaded spriteset, or <see cref="IntPtr.Zero"/> if an error occurred.
        /// </returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_LoadSpriteset(string name);

        /// <summary>
        /// Creates a copy of the specified spriteset and its associated palette.
        /// </summary>
        /// <param name="src">Spriteset to clone.</param>
        /// <returns>
        /// Reference to the newly cloned spriteset, or <see cref="IntPtr.Zero"/> if an error occurred.
        /// </returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneSpriteset(IntPtr src);

        /// <summary>
        /// Query the details about the specified sprite inside a spriteset.
        /// </summary>
        /// <param name="spriteset">Reference to the spriteset to get info about.</param>
        /// <param name="entry">The entry index inside the spriteset [0, num_sprites - 1]</param>
        /// <param name="info">
        /// Pointer to application-allocated <see cref="TLN_SpriteInfo"/>
        /// structure that will receive the data.
        /// </param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSpriteInfo(IntPtr spriteset, int entry, out TLN_SpriteInfo info);

        /// <summary>
        /// Returns a reference to the palette associated to the specified spriteset.
        /// </summary>
        /// <remarks>
        /// The palette of a spriteset is created at load time and cannot be modified. When <see cref="TLN_ConfigSprite"/>
        /// function is used to setup a sprite, the palette associated with the specified spriteset is automatically
        /// assigned to that sprite, but it can be later replaced with <see cref="TLN_SetSpritePalette"/>.
        /// </remarks>
        /// <param name="spriteset">Spriteset to obtain the palette.</param>
        /// <returns>
        /// Reference to the palette, or <see cref="IntPtr.Zero"/> if an error occurred.
        /// </returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetSpritesetPalette(IntPtr spriteset);

        /// <summary>
        /// Retrieves a reference to the palette associated to the specified spriteset.
        /// </summary>
        /// <param name="spriteset">Spriteset where to find the sprite.</param>
        /// <param name="name">Name of the sprite to find.</param>
        /// <returns>Sprite index [0, num_sprites - 1] if found, or -1 if not found.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TLN_FindSpritesetSprite(IntPtr spriteset, string name);

        /// <summary>
        /// Sets attributes and pixels of a given sprite inside a spriteset.
        /// </summary>
        /// <param name="spriteset">Spriteset to set the data.</param>
        /// <param name="entry">The entry index inside the spriteset to modify [0, num_sprites - 1].</param>
        /// <param name="data">
        /// Pointer to a user-provided <see cref="TLN_SpriteData"/>
        /// structure with the sprite description.
        /// </param>
        /// <param name="pixels">Pointer to source pixel data.</param>
        /// <param name="pitch">Number of bytes for each scanline of the source pixel data.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpritesetData(IntPtr spriteset, int entry, TLN_SpriteData[] data, IntPtr pixels, int pitch);

        /// <summary>
        /// Deletes the specified spriteset and frees up memory.
        /// </summary>
        /// <remarks>
        /// <b>Don't delete a spriteset which is attached by a sprite.</b>
        /// </remarks>
        /// <param name="Spriteset">Spriteset to delete.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteSpriteset(IntPtr Spriteset);

        #endregion

        #region Tileset

        /// <summary>
        /// Creates a tile-based tileset.
        /// </summary>
        /// <param name="numtiles">Number of tiles that the tileset will hold.</param>
        /// <param name="width">Width of each tile (must be multiple of 8)</param>
        /// <param name="height">Height of each tile (must be multiple of 8)</param>
        /// <param name="palette">Reference to the palette to assign</param>
        /// <param name="sp">
        /// Optional reference to the optional sequence pack with
        /// associated tileset animations. Can be NULL.
        /// </param>
        /// <param name="attributes">
        /// Optional array of attributes, one for each tile. Can be NULL.
        /// </param>
        /// <returns>Reference to the created tileset, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateTileset(int numtiles, int width, int height, IntPtr palette, IntPtr? sp, TLN_TileAttribute[]? attributes);

        /// <summary>
        /// Creates a multiple image-based tileset.
        /// </summary>
        /// <param name="numtiles">Number of tiles that the tileset will hold.</param>
        /// <param name="images">Array of image structures, one for each tile. Can be NULL.</param>
        /// <returns>Reference to the created tileset, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateImageTileset(int numtiles, TLN_TileImage[]? images);

        /// <summary>
        /// Loads a tileset from a Tiled TSX file.
        /// </summary>
        /// <remarks>
        /// An associated palette is also created, it can be obtained by calling <see cref="TLN_GetTilesetPalette"/>
        /// </remarks>
        /// <param name="filename">TSX file to load.</param>
        /// <returns>Reference to the newly loaded tileset or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadTileset(string filename);

        /// <summary>
        /// Creates a copy of the specified tileset and its associated palette.
        /// </summary>
        /// <param name="src">Tileset to clone</param>
        /// <returns>A reference to the newly cloned tileset, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneTileset(IntPtr src);

        /// <summary>
        /// Sets pixel data for a tile in a tile-based tileset.
        /// </summary>
        /// <param name="tileset">Reference to the tileset.</param>
        /// <param name="entry">Number of tile to set [0, num_tiles - 1]</param>
        /// <param name="srcdata">Pointer to pixel data to set</param>
        /// <param name="srcpitch">Bytes per line of source data</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetTilesetPixels(IntPtr tileset, int entry, byte[] srcdata, int srcpitch);

        /// <summary>
        /// Retrieves the width in pixels of each individual tile in the tileset.
        /// </summary>
        /// <param name="tileset">Reference to the tileset to get info from.</param>
        /// <returns>The tile width.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTileWidth(IntPtr tileset);

        /// <summary>
        /// Retrieves the height in pixels of each individual tile in the tileset.
        /// </summary>
        /// <param name="tileset">Reference to the tileset to get info from.</param>
        /// <returns>The tile height.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTileHeight(IntPtr tileset);

        /// <summary>
        /// Retrieves the number of different tiles in a tileset.
        /// </summary>
        /// <param name="tileset">Reference to the tileset to get info from</param>
        /// <returns>The amount of tiles.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTilesetNumTiles(IntPtr tileset);

        /// <summary>
        /// Retrieves a reference to the palette associated to the specified tileset.
        /// </summary>
        /// <remarks>
        /// The palette of a tileset is created at load time and cannot be modified. <br/>
        /// When <see cref="TLN_SetLayer"/> function is used to attach a tileset to a layer,
        /// the palette associated with the specified tileset is automatically assigned to
        /// that layer, but it can be later replaced with <see cref="TLN_SetLayerPalette"/>
        /// </remarks>
        /// <param name="tileset">Reference to the tileset to get the palette.</param>
        /// <returns>Reference to the palette or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetTilesetPalette(IntPtr tileset);

        /// <summary>
        /// Retrieves a reference to the optional sequence pack associated to the specified tileset.
        /// </summary>
        /// <param name="tileset">Reference to the tileset to get the palette</param>
        /// <returns>Reference to the sequencepack or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetTilesetSequencePack(IntPtr tileset);

        /// <summary>
        /// Deletes the specified tileset and frees up memory.
        /// </summary>
        /// <remarks>
        /// <b>Don't delete a tileset which is attached to a layer.</b>
        /// </remarks>
        /// <param name="tileset">Tileset to delete</param>
        /// <returns></returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteTileset(IntPtr tileset);

        #endregion

        #region Tilemap

        /// <summary>
        /// Creates a new tilemap.
        /// </summary>
        /// <param name="rows">Number of rows (vertical dimension)</param>
        /// <param name="cols">Number of cols (horizontal dimension)</param>
        /// <param name="tiles">Array of tiles with data (see struct <see cref="TLN_Tile"/>)</param>
        /// <param name="bgcolor">Background color value (RGB32 packed)</param>
        /// <param name="tileset">Optional reference to associated tileset. Can be NULL.</param>
        /// <returns>Reference to the created tilemap, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateTilemap(int rows, int cols, TLN_Tile[] tiles, uint bgcolor, IntPtr? tileset);

        /// <summary>
        /// Loads a tilemap layer from a Tiled .tmx file.
        /// </summary>
        /// <param name="filename">TMX file with the tilemap.</param>
        /// <param name="layername">Optional name of the layer inside the tmx file to load. Set to NULL to load the first layer.</param>
        /// <returns>Reference to the newly loaded tilemap, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadTilemap(string filename, string? layername);

        /// <summary>
        /// Creates a copy of the specified tilemap.
        /// </summary>
        /// <param name="src">Reference to the tilemap to clone.</param>
        /// <returns>A reference to the newly cloned tilemap, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneTilemap(IntPtr src);

        /// <summary>
        /// Returns the number of vertical tiles in the tilemap.
        /// </summary>
        /// <param name="tilemap">Reference of the tilemap.</param>
        /// <returns>The number of vertical lines.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTilemapRows(IntPtr tilemap);

        /// <summary>
        /// Returns the number of horizontal tiles in the tilemap.
        /// </summary>
        /// <param name="tilemap">Reference of the tilemap.</param>
        /// <returns>The number of horizontal lines.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetTilemapCols(IntPtr tilemap);

        /// <summary>
        /// Returns the optional associated tileset to the specified tilemap.
        /// </summary>
        /// <param name="tilemap">Reference of the tilemap.</param>
        /// <returns>Reference to the associated tileset.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetTilemapTileset(IntPtr tilemap);

        /// <summary>
        /// Gets data of a single tile inside a tilemap.
        /// </summary>
        /// <param name="tilemap">Reference of the tilemap to get the tile.</param>
        /// <param name="row">Vertical location of the tile.</param>
        /// <param name="col">Horizontal location of the tile.</param>
        /// <param name="tile">Reference to an application allocated <see cref="TLN_Tile"/> which will contain the data.</param>
        /// <returns>true if successful or false if an invalid tilemap reference was supplied.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetTilemapTile(IntPtr tilemap, int row, int col, out TLN_Tile tile);

        /// <summary>
        /// Sets a tile of a tilemap.
        /// </summary>
        /// <param name="tilemap">Reference to the tilemap.</param>
        /// <param name="row">Row (vertical position) of the tile [0 - num_rows - 1]</param>
        /// <param name="col">Column (horizontal position) of the tile [0 - num_cols - 1]</param>
        /// <param name="tile">Reference to the tile to set, or NULL to set an empty tile.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetTilemapTile(IntPtr tilemap, int row, int col, TLN_Tile? tile);

        /// <summary>
        /// Copies blocks of tiles between two tilemaps.
        /// </summary>
        /// <remarks>
        /// Use this function to implement tile streaming.
        /// </remarks>
        /// <param name="src">Reference to the source tilemap.</param>
        /// <param name="srcrow">Starting row (vertical position) inside the source tilemap.</param>
        /// <param name="srccol">Starting column (horizontal position) inside the source tilemap.</param>
        /// <param name="rows">Number of rows to copy.</param>
        /// <param name="cols">Number of columns to copy.</param>
        /// <param name="dst">Reference to the target tilemap.</param>
        /// <param name="dstrow">Starting row (vertical position) inside the target tilemap.</param>
        /// <param name="dstcol">Starting column (horizontal position) inside the target tilemap.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_CopyTiles(IntPtr src, int srcrow, int srccol, int rows, int cols, IntPtr dst, int dstrow, int dstcol);

        /// <summary>
        /// Deletes the specified tilemap and frees up memory.
        /// </summary>
        /// <remarks>
        /// <b>Don't delete a tilemap which is attached to a layer.</b>
        /// </remarks>
        /// <param name="tilemap">Reference to the tilemap to delete.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteTilemap(IntPtr tilemap);

        #endregion

        #region Palette

        /// <summary>
        /// Creates a new color table.
        /// </summary>
        /// <param name="entries">Number of color entries (typically 256)</param>
        /// <returns>Reference to the created palette, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreatePalette(int entries);

        /// <summary>
        /// Loads a palette from a standard .act file.
        /// </summary>
        /// <param name="filename">ACT file containing the palette to load.</param>
        /// <returns>A reference to the newly loaded palette, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadPalette(string filename);

        /// <summary>
        /// Creates a copy of the specified palette.
        /// </summary>
        /// <param name="src">Reference to the palette to clone.</param>
        /// <returns>A reference to the newly cloned palette, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_ClonePalette(IntPtr src);

        /// <summary>
        /// Sets the RGB color value of a palette entry.
        /// </summary>
        /// <param name="palette">Reference to the palette to modify.</param>
        /// <param name="color">Index of the palette entry to modify (0-255)</param>
        /// <param name="r">Red component of the color (0-255)</param>
        /// <param name="g">Green component of the color (0-255)</param>
        /// <param name="b">Blue component of the color (0-255)</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetPaletteColor(IntPtr palette, int color, byte r, byte g, byte b);

        /// <summary>
        /// Mixes two palettes to create a third one.
        /// </summary>
        /// <param name="src1">Reference to the first source palette.</param>
        /// <param name="src2">Reference to the second source palette.</param>
        /// <param name="dst">Reference to the target palette.</param>
        /// <param name="factor">
        ///     Mixing factor.
        ///     <list type="bullet">
        ///         <item>
        ///             <term>0</term>
        ///             <description>100% src1</description>
        ///         </item>
        ///         <item>
        ///             <term>128</term>
        ///             <description>50% src1 | 50% src2</description>
        ///         </item>
        ///         <item>
        ///             <term>255</term>
        ///             <description>100% src2</description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_MixPalettes(IntPtr src1, IntPtr src2, IntPtr dst, byte factor);

        /// <summary>
        /// Modifies a range of colors by adding the provided color value to the selected range. <br/>
        /// The result is always a brighter color.
        /// </summary>
        /// <param name="palette">Reference to the palette to modify.</param>
        /// <param name="r">Red component of the color (0-255)</param>
        /// <param name="g">Green component of the color (0-255)</param>
        /// <param name="b">Blue component of the color (0-255)</param>
        /// <param name="start">Index of the first color entry to modify.</param>
        /// <param name="num">Number of colors from start to modify.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_AddPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        /// <summary>
        /// Modifies a range of colors by subtracting the provided color value to the selected range. <br/>
        /// The result is always a darker color.
        /// </summary>
        /// <param name="palette">Reference to the palette to modify.</param>
        /// <param name="r">Red component of the color (0-255)</param>
        /// <param name="g">Green component of the color (0-255)</param>
        /// <param name="b">Blue component of the color (0-255)</param>
        /// <param name="start">Index of the first color entry to modify.</param>
        /// <param name="num">Number of colors from start to modify</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SubPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        /// <summary>
        /// Modifies a range of colors by modulating (normalized product) the provided color value to the selected range. <br/>
        /// The result is always a darker color.
        /// </summary>
        /// <param name="palette">Reference to the palette to modify.</param>
        /// <param name="r">Red component of the color (0-255)</param>
        /// <param name="g">Green component of the color (0-255)</param>
        /// <param name="b">Blue component of the color (0-255)</param>
        /// <param name="start">Index of the first color entry to modify.</param>
        /// <param name="num">Number of colors from start to modify.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ModPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        /// <summary>
        /// Returns the color value of a palette entry.
        /// </summary>
        /// <param name="palette">Reference to the palette to get the color.</param>
        /// <param name="index">Index of the palette entry to obtain (0-255)</param>
        /// <returns>32-bit integer with the packed color in internal pixel format RGBA.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint TLN_GetPaletteData(IntPtr palette, int index);

        /// <summary>
        /// Deletes the specified palette and frees up memory.
        /// </summary>
        /// <remarks>
        /// <b>Don't delete a palette which is attached to a layer or sprite.</b>
        /// </remarks>
        /// <param name="palette">Reference to the palette to delete.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeletePalette(IntPtr palette);

        #endregion

        #region Bitmap

        /// <summary>
        /// Creates a memory bitmap.
        /// </summary>
        /// <param name="width">Width in pixels</param>
        /// <param name="height">Height in pixels</param>
        /// <param name="bpp">Bits per pixel</param>
        /// <returns>Reference to the created bitmap, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateBitmap(int width, int height, int bpp);

        /// <summary>
        /// Load image file (8-bit BMP or PNG)
        /// </summary>
        /// <param name="filename">File name with the image</param>
        /// <returns>Handler to the loaded image, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadBitmap(string filename);

        /// <summary>
        /// Creates a copy of a bitmap
        /// </summary>
        /// <param name="src">Reference to the original bitmap.</param>
        /// <returns>Reference to the created bitmap, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneBitmap(IntPtr src);

        /// <summary>
        /// Gets memory access for direct pixel manipulation.
        /// </summary>
        /// <remarks>
        /// Care must be taken in manipulating memory directly, as it can crash the application.
        /// </remarks>
        /// <param name="bitmap">Reference to the bitmap</param>
        /// <param name="x">Starting x position [0, width - 1]</param>
        /// <param name="y">Starting y position [0, height - 1]</param>
        /// <returns>Pointer to pixel data starting at x,y</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetBitmapPtr(IntPtr bitmap, int x, int y);

        /// <summary>
        /// Gets the width of specified bitmap in pixels.
        /// </summary>
        /// <param name="bitmap">Reference to the bitmap.</param>
        /// <returns>Width in pixels</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetBitmapWidth(IntPtr bitmap);

        /// <summary>
        /// Gets the height of specified bitmap in pixels.
        /// </summary>
        /// <param name="bitmap">Reference to the bitmap.</param>
        /// <returns>Height in pixels</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetBitmapHeight(IntPtr bitmap);

        /// <summary>
        /// Gets the bits per pixel.
        /// </summary>
        /// <param name="bitmap">Reference to the bitmap.</param>
        /// <returns>Bits per pixel</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetBitmapDepth(IntPtr bitmap);

        /// <summary>
        /// Gets the number of bytes per scanline (also known as a stride)
        /// </summary>
        /// <param name="bitmap">Reference to the bitmap.</param>
        /// <returns>Number of bytes per scanline.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetBitmapPitch(IntPtr bitmap);

        /// <summary>
        /// Gets the associated palette of a bitmap.
        /// </summary>
        /// <param name="bitmap">Reference to the bitmap.</param>
        /// <returns>Reference to the bitmap palette, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetBitmapPalette(IntPtr bitmap);

        /// <summary>
        /// Assigns a new palette to the bitmap.
        /// </summary>
        /// <param name="bitmap">Reference to the bitmap.</param>
        /// <param name="palette">Reference to the palette.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetBitmapPalette(IntPtr bitmap, IntPtr palette);

        /// <summary>
        /// Deletes bitmap and frees up resources.
        /// </summary>
        /// <param name="bitmap">Reference to the bitmap.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteBitmap(IntPtr bitmap);

        #endregion

        #region Objects

        /// <summary>
        /// Creates a TLN_ObjectList.
        /// </summary>
        /// <remarks>
        /// The list is created empty, it must be populated with TLN_AddSpriteToList()
        /// and assigned to a layer with TLN_SetLayerObjects()
        /// </remarks>
        /// <returns>Reference to the new object list, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CreateObjectList();

        /// <summary>
        /// Adds an image-based tileset item to given TLN_ObjectList.
        /// </summary>
        /// <param name="list">Reference to TLN_ObjectList</param>
        /// <param name="id">Unique ID of the tileset object.</param>
        /// <param name="gid">Graphic Id (tile index) of the tileset object.</param>
        /// <param name="flags">Member or combination of <see cref="TLN_TileFlags"/></param>
        /// <param name="x">Layer-space horizontal coordinate of the top-left corner.</param>
        /// <param name="y">Layer-space vertical coordinate of the top-left corner.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_AddTileObjectToList(IntPtr list, ushort id, ushort gid, TLN_TileFlags flags, int x, int y);

        /// <summary>
        /// Loads an object list from a Tiled object layer.
        /// </summary>
        /// <param name="filename">Name of the .tmx file containing the list.</param>
        /// <param name="layername">Name of the layer to load.</param>
        /// <returns>Reference to the loaded object, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr TLN_LoadObjectList(string filename, string layername);

        /// <summary>
        /// Creates a copy of a given TLN_ObjectList object.
        /// </summary>
        /// <param name="src">Reference to the source object to clone.</param>
        /// <returns>A reference to the newly cloned object list, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_CloneObjectList(IntPtr src);

        /// <summary>
        /// Gets the number of items in a TLN_ObjectList
        /// </summary>
        /// <param name="list">Pointer to TLN_ObjectList to query.</param>
        /// <returns>The number of items.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetListNumObjects(IntPtr list);

        /// <summary>
        /// Iterates over elements in a TLN_ObjectList
        /// </summary>
        /// <param name="list">Reference to TLN_ObjectList to get items.</param>
        /// <param name="info">Pointer to user-allocated TLN_ObjectInfo struct</param>
        /// <returns>true if an item returned, false if no more items are left.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetListObject(IntPtr list, out TLN_ObjectInfo info);

        /// <summary>
        /// Deletes object list.
        /// </summary>
        /// <param name="list">Reference to list to delete.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DeleteObjectList(IntPtr list);

        #endregion

        #region Layers

        /// <summary>
        /// <b>Deprecated, use <see cref="TLN_SetLayerTilemap"/> instead.</b> <br/>
        /// Configures a background layer with the specified tileset and tilemap.
        /// </summary>
        /// <remarks>
        /// This function doesn't modify the current position nor the blend mode,
        /// but assigns the palette of the specified tileset.
        /// </remarks>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="tileset">
        /// Optional reference to the tileset to assign. If the tilemap has a reference to its own tileset,
        /// passing NULL will assign the default tileset.
        /// </param>
        /// <param name="tilemap">Reference to the tilemap to assign.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayer(int nlayer, IntPtr? tileset, IntPtr tilemap);

        /// <summary>
        /// Configures a tiled background layer with the specified tilemap.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="tilemap">Reference to the tilemap to assign.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerTilemap(int nlayer, IntPtr tilemap);

        /// <summary>
        /// Configures a background layer with the specified full bitmap.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="bitmap"> Reference to the bitmap to assign.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerBitmap(int nlayer, IntPtr bitmap);

        /// <summary>
        /// Sets the color palette to the layer.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When a layer is assigned with a tileset with the function TLN_SetLayer(), it
        ///         automatically sets the palette of the assigned tileset to the layer.
        ///         Use this function to override it and set another palette.
        ///     </para>
        ///     <para>
        ///         Call this function inside a raster callback to change the palette in the middle
        ///         of the frame to get raster effect colors, like and "underwater" palette below the
        ///         water line in a partially submerged background, or a gradient palette in an area at
        ///         the top of the screen to simulate a "depth fog effect" in a pseudo 3d background.
        ///     </para>
        /// </remarks>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="palette">Reference to the palette to assign to the layer.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPalette(int nlayer, IntPtr palette);

        /// <summary>
        /// Sets the position of the tileset that corresponds to the upper left corner.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The tileset usually spans an area much bigger than the viewport. Use this
        ///         function to move the viewport insde the tileset. Change this value progressively
        ///         for each frame to get a scrolling effect.
        ///     </para>
        ///     <para>
        ///         Call this function inside a raster callback to get a raster scrolling effect.
        ///         Use this to create horizontal strips of the same
        ///         layer that move at different speeds to simulate depth. The extreme case of this effect, where
        ///         the position is changed in each scanline, is called "line scroll" and was the technique used by
        ///         games such as Street Fighter II to simualte a pseudo 3d floor, or many racing games to simulate
        ///         a 3D road.
        ///     </para>
        /// </remarks>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="hstart">Horizontal offset in the tileset on the left side.</param>
        /// <param name="vstart">Vertical offset in the tileset on the top side.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPosition(int nlayer, int hstart, int vstart);

        /// <summary>
        /// Sets simple scaling.
        /// </summary>
        /// <remarks>
        /// By default the scaling factor of a given layer is 1.0f, 1.0f, which means
        /// no scaling. Use values below 1.0 to downscale (shrink) and above 1.0 to upscale (enlarge).
        /// Call TLN_ResetLayerMode() to disable scaling.
        /// </remarks>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="xfactor">Horizontal scale factor.</param>
        /// <param name="yfactor">Vertical scale factor.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerScaling(int nlayer, float xfactor, float yfactor);

        /// <summary>
        /// Sets affine transform matrix to enable rotating and scaling of this layer.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Enable the transformation matrix to give the layer the capabilities of the famous
        ///         Super Nintendo / Famicom Mode 7. Beware that the rendering of a transformed layer
        ///         uses more CPU than a regular layer.
        ///     </para>
        ///     <para>
        ///         Unlike the original Mode 7, that could only transform
        ///         the single layer available, Tilengine can transform all the layers at the same time. The only
        ///         limitation is the available CPU power.
        ///     </para>
        ///     <para>
        ///         Call this function inside a raster callback to set the transformation matrix in the middle of
        ///         the frame. Setting it for each scanline is the trick used by many Super Nintendo games to fake
        ///         a 3D perspective projection.
        ///     </para>
        /// </remarks>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="affine">Pointer to an TLN_Affine matrix, or NULL to disable it</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerAffineTransform(int nlayer, in TLN_Affine? affine);

        /// <summary>
        /// Sets affine transform matrix to enable rotating and scaling of this layer.
        /// </summary>
        /// <remarks>
        /// This function is a simple wrapper to TLN_SetLayerAffineTransform() without using the TLN_Affine struct.
        /// </remarks>
        /// <param name="layer">Layer index [0, num_layers - 1]</param>
        /// <param name="angle">Rotation angle in degrees.</param>
        /// <param name="dx">Horizontal displacement.</param>
        /// <param name="dy">Vertical displacement.</param>
        /// <param name="sx">Horizontal scaling.</param>
        /// <param name="sy">Vertical scaling.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerTransform(int layer, float angle, float dx, float dy, float sx, float sy);

        /// <summary>
        /// Sets the table for pixel mapping render mode.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="table">User-provided array of hres*vres sized TLN_PixelMap items.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPixelMapping(int nlayer, in TLN_PixelMap table);

        /// <summary>
        /// Sets the blending mode (transparency effect)
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="mode">Member of the <see cref="TLN_Blend"/> enumeration</param>
        /// <param name="factor">
        /// Deprecated as of 1.12, left for backwards compatibility but doesn't have effect.
        /// </param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerBlendMode(int nlayer, TLN_Blend mode, byte factor);

        /// <summary>
        /// Enables column offset mode for this layer.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Column offset is a value that is added or subtracted (depending on the sign) <br/>
        ///         to the vertical position for that layer (see TLN_SetLayerPosition) for
        ///         each column in the tilemap assigned to that layer.
        ///     </para>
        ///     <para>
        ///         This feature is typically used to simulate vertical strips moving at different
        ///         speeds, or combined with a line scroll effect, to fake rotations where the angle
        ///         is small. The Sega Genesis games Puggsy and Chuck Rock II used this trick to simulate
        ///         partially rotating backgrounds.
        ///     </para>
        /// </remarks>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="offset">Array of offsets to set. Set NULL to disable column offset mode.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerColumnOffset(int nlayer, int[] offset);

        /// <summary>
        /// Enables clipping rectangle on selected layer.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="x1">Left coordinate</param>
        /// <param name="y1">Top coordinate</param>
        /// <param name="x2">Right coordinate</param>
        /// <param name="y2">Bottom coordinate</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerClip(int nlayer, int x1, int y1, int x2, int y2);

        /// <summary>
        /// Disables clipping rectangle on selected layer
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayerClip(int nlayer);

        /// <summary>
        /// Enables mosaic effect (pixelation) for selected layer.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="width">Horizontal pixel size</param>
        /// <param name="height">Vertical pixel size</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerMosaic(int nlayer, int width, int height);

        /// <summary>
        /// Disables mosaic effect for selected layer.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayerMosaic(int nlayer);

        /// <summary>
        /// Disables scaling or affine transform for the layer.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ResetLayerMode(int nlayer);

        /// <summary>
        /// Configures a background layer with a object list and an image-based tileset.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="objects">Reference to the TLN_ObjectList to attach.</param>
        /// <param name="tileset">
        /// Optional reference to the image-based tileset object.
        /// If NULL, object list must have an attached tileset
        /// </param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerObjects(int nlayer, IntPtr objects, IntPtr? tileset);

        /// <summary>
        /// Sets full layer priority, appearing in front of sprites.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <param name="enable">Has full priority.</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerPriority(int nlayer, bool enable);

        /// <summary>
        /// <b>Removed, kept for API compatibility.</b>
        /// </summary>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetLayerParent(int nlayer, int parent);

        /// <summary>
        /// <b>Removed, kept for API compatibility.</b>
        /// </summary>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayerParent(int nlayer);

        /// <summary>
        /// Returns the layer width in pixels.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableLayer(int nlayer);

        /// <summary>
        /// Enables a layer previously disabled with <see cref="TLN_DisableLayer"/>
        /// </summary>
        /// <remarks>
        /// The layer must have been previously configured. A layer without a prior configuration can't be enabled.
        /// </remarks>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_EnableLayer(int nlayer);

        /// <summary>
        /// Gets the type of a layer.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>A member of the <see cref="TLN_LayerType"/> enumeration.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern TLN_LayerType TLN_GetLayerType(int nlayer);

        /// <summary>
        /// Gets the active palette of a layer.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>
        /// Reference of the palette assigned to the layer, or <see cref="IntPtr.Zero"/> if an error occurred.
        /// </returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerPalette(int nlayer);

        /// <summary>
        /// Returns the active tileset on a
        /// <see cref="TLN_LayerType.LAYER_TILE"/> or <see cref="TLN_LayerType.LAYER_OBJECT"/> layer type.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>Reference to the active tileset, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerTileset(int nlayer);

        /// <summary>
        /// Returns the active tilemap on a <see cref="TLN_LayerType.LAYER_TILE"/> layer type.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>Reference to the active tilemap, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerTilemap(int nlayer);

        /// <summary>
        /// Returns the active bitmap on a <see cref="TLN_LayerType.LAYER_BITMAP"/> layer type.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>Reference to the active bitmap, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerBitmap(int nlayer);

        /// <summary>
        /// Returns the active bitmap on a <see cref="TLN_LayerType.LAYER_OBJECT"/> layer type.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>Reference to the active objects list, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TLN_GetLayerObjects(int nlayer);

        /// <summary>
        /// Gets information about the tile located in tilemap space.
        /// </summary>
        /// <param name="nlayer">Id of the layer to query [0, num_layers - 1]</param>
        /// <param name="x">Horizontal position</param>
        /// <param name="y">Vertical position</param>
        /// <param name="info">
        /// Pointer to an application-allocated <see cref="TLN_TileInfo"/>
        /// struct that will receive the data.
        /// </param>
        /// <returns>true if successful or false if an error occurred.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetLayerTile(int nlayer, int x, int y, out TLN_TileInfo info);

        /// <summary>
        /// Returns the layer width in pixels.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>Layer width in pixels.</returns>
        [DllImport(NativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TLN_GetLayerWidth(int nlayer);

        /// <summary>
        /// Returns the layer height in pixels.
        /// </summary>
        /// <param name="nlayer">Layer index [0, num_layers - 1]</param>
        /// <returns>Layer height in pixels.</returns>
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