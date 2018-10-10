using Tilengine;

class test{
	static int Main(string[] args){
		int frame = 0;

		/* setup engine */
		Engine engine = Engine.Init(400, 240, 1, 0, 20);
		engine.LoadPath = "assets/sonic";
		Tilemap foreground = Tilemap.FromFile("Sonic_md_fg1.tmx", null);
		engine.Layers[0].SetMap(foreground);

		/* main loop */
		Window window = Window.Create(null, WindowFlags.Vsync);
		while (window.Process ()){
			window.DrawFrame(frame);
			frame += 1;
		}
		return 0;
	}
}
