![Tilengine logo](Tilengine.png)
# CsTilengine
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
## About CsTilengine
CsTilengine is the C#/NET/Mono binding for Tilengine. It is not a direct 1:1 API translation of the original C library, but it uses familiar C# constructions as classes, arrays, etc.

## Contents
* */src* directory contains the single `tilengine.cs` module with the binding itself
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
using Tilengine;

class test{
	static int Main(string[] args){
		int frame = 0;

		Engine engine = Engine.Init(400, 240, 1, 0, 20);
		engine.LoadPath = "assets/sonic";
		Tilemap foreground = Tilemap.FromFile("Sonic_md_fg1.tmx", null);
		engine.Layers[0].SetMap(foreground);

		Window window = Window.Create(null, WindowFlags.Vsync);
		while (window.Process ()){
			window.DrawFrame(frame);
			frame += 1;
		}
		return 0;
	}
}
```

Resulting output:

![Test](test.png)

## Running the samples (Windows)
There's a `samples.sln` Visual Studio solution file. Open it, build solution and you'll get two executables ready to run.

## Running the samples (Linux/OSX)
Open a terminal window inside the "samples" directory and the following commands:
```
make all
./test
./platformer
```

## License
CsTilengine is released under the permissive MIT license
