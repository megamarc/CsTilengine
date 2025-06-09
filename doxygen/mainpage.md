# Tilengine-CSharp
Reference for the C# binding for Tilengine 2D retro graphic engine: https://www.tilengine.org. It's a lightweigh object-oriented wrapper over the original C API. Most methods and properties translate one-to-one, but using classes, properties, exceptions, etc. 

## Basic classes
Used to initialize the engine and/or create the optional rendering window

|Class                   |Usage
|------------------------|---------------------------------------------------------------
|Tilengine.Engine        |Creation and management of the engine itself
|Tilengine.Window        |Creation and management of the optional built-in window system

## Graphic entities
These entities are statically created when the engine is initialized and their number remains constant. They handle the basic entities the engine draws

|Class                   |Usage
|------------------------|---------------------------------------------------------------
|Tilengine.Layer         |Manages background layers, composed of tilemaps, bitmaps or objects
|Tilengine.Sprite        |Manages moving objects
|Tilengine.Animation     |Manages palette color-cycle animations

## Resources
These entities are loaded, allocated and released at runtime. They're the dynamic data used by the engine to draw layers, sprites and animations

|Class                   |Usage
|------------------------|---------------------------------------------------------------
|Tilengine.Tilemap       |Rectangular arrangement of tiles for Tile layers
|Tilengine.Tileset       |Collection of tiles for Tile layers used to populate tilemaps
|Tilengine.Palette       |Color table used by all drawable elements. Up to 256 entries each
|Tilengine.Spriteset     |Sheet of sprite graphics used to draw and animate sprites
|Tilengine.Bitmap        |Basic bitmap used by Bitmap layers, and internally by tilesets and spritesets
|Tilengine.ObjectList    |List of objects for Object layers. Kind of background layer composed of sprites
|Tilengine.Sequence      |Describes animations for sprites, tiles and strips of colors
|Tilengine.SequencePack  |Collection of sequences

### Basic usage

```csharp
using Tilengine;

class test {
	static int Main(string[] args) 	{
        // setup engine, features like Sega Megadrive (320x240, 2 layers, 80 sprites)
		var engine = Engine.Init(320, 240, 2, 80, 16);
        engine.SetLoadPath("assets/sonic");

		// Setup tiled layers with tilemaps in .tmx files
		engine.Layers[0].Tilemap = Tilemap.FromFile("Sonic_md_fg1.tmx");
        engine.Layers[1].Tilemap = Tilemap.FromFile("Sonic_md_bg1.tmx");

        // setup player sprite
        var playerSprite = engine.Sprites[0]
        playerSprite.SetSpriteset(Spriteset.FromFile("SonicSprite"));
        playerSprite.SetPosition(10, 50);
        playerSprite.Picture = 2

        // setup window and render loop
        var window = Window.Create(WindowFlags.Vsync);
		while (window.Process())
			window.DrawFrame();
		
        return 0;
	}
}
```