![Tilengine logo](Tilengine.png)
# CsTilengine
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
## About CsTilengine
CsTilengine is the C#/NET/Mono binding for Tilengine. This fork of [CsTilengine](https://github.com/megamarc/CsTilengine) is a 1:1 API translation of the original C library. 

## Contents
* */CsTilengine/src* directory contains the single `Tilengine.cs` module with the binding itself
* */samples* directory contains various examples ready to run and test

## Prerequisites
Tilengine native shared library must be installed separately. Please refer to https://github.com/megamarc/Tilengine about how to do it.

### Windows
.NET Framework 2.0 or later must be installed

### Linux/OSX
Mono tools and runtime must be installed. In Debian-based distros please execute the following command:
```
sudo apt-get install mono-mcs
```

## Installation
No install step is required. Just make sure that the Tilengine library and the `tilengine.cs` modules are accessible from within your own project

## Basic program
The following program does these actions:
1. Import required classes from tilengine binding
2. Initialize the engine with a resolution of 400x240, one layer, no sprites and 20 animation slots
3. Set the loading path to the assets folder
4. Load a *tilemap*, the asset that contains background layer data
5. Attach the loaded tilemap to the allocated background layer
6. Create a display window with default parameters: windowed, auto scale and CRT effect enabled
7. Run the window loop, updating the display at each iteration until the window is closed

Source code:
```csharp
using static Tilengine.TLN;

pubilc class Test
{
	public static void Main(string[] args)
	{
		TLN_Init(400, 240, 2, 1, 1);
		TLN_SetLoadPath("assets/sonic");
		var foreground = TLN_LoadTilemap("Sonic_md_fg1.tmx", null);
		TLN_SetLayerTilemap(0, foreground);

		TLN_CreateWindow(null, TLN_CreateWindowFlags.CWF_VSYNC);
		while (TLN_ProcessWindow())
		{
			TLN_DrawFrame(0);
		}
	}
}
```

Resulting output:

![Test](test.png)

## Running the samples (Windows)
There's a `CsTilengine.sln` Visual Studio solution file. Open it, navigate to the *Samples* folder.
Build a project and you'll get one executable ready to run.

## License
CsTilengine is released under the permissive MIT license
