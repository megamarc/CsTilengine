using static Tilengine.TLN;

namespace TestMouse
{
    public class Entity
    {
        public bool Enabled { get; set; }
        public int Id { get; set; }
        public int SpriteIndex { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }

        public void OnClick(IntPtr palette)
        {
            TLN_SetSpritePalette(SpriteIndex, palette);
            Console.WriteLine("Clicked entity {0}", Id);
        }

        public void OnRelease(IntPtr palette)
        {
            TLN_SetSpritePalette(SpriteIndex, palette);
        }
    }
}
