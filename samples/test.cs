using Tilengine;

class test {
	static int Main(string[] args) {
		// setup engine
		Engine engine = Engine.Init(400, 240, 1, 0, 20);
		engine.SetLoadPath("assets/sonic");
		engine.Layers[0].Tilemap = Tilemap.FromFile("Sonic_md_fg1.tmx");

        // create window & main loop
        Window window = Window.Create(WindowFlags.Vsync);
		while (window.Process())
			window.DrawFrame();

		return 0;
	}
}
