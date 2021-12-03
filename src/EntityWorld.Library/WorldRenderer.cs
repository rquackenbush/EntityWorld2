using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;
using System.IO;

namespace EntityWorld.Library
{
    public class WorldRenderer
    {
        private readonly FontFamily _fontFamily;
        private readonly Font _font;

        public WorldRenderer()
        {
            var fontCollection = new FontCollection();

            foreach(var file in Directory.GetFiles(@"c:\windows\fonts", "*.ttf"))
            {
                fontCollection.Install(file);
            }

            _fontFamily = fontCollection.Find("Consolas");
            _font = _fontFamily.CreateFont(10, FontStyle.Regular);
        }

        public void Render(WorldState worldState, string filename)
        {
            using (var image = new Image<Rgba32>(worldState.WorldBounds.Width, worldState.WorldBounds.Height))
            {
                image.Mutate(x => x.Fill(Color.LightGray));

                var food = new Rectangle(worldState.FoodBounds.X, worldState.FoodBounds.Y, worldState.FoodBounds.Width, worldState.FoodBounds.Height);

                image.Mutate(x => x.Fill(Color.Yellow, food));

                foreach(var entity in worldState.Entities)
                {
                    var entityRectangle = new Rectangle(entity.CurrentPosition.X, entity.CurrentPosition.Y, 4, 4);

                    image.Mutate(x => x.Fill(Color.Red, entityRectangle));

                    var options = new DrawingOptions();

                    options.TextOptions.ApplyKerning = true;
                    options.TextOptions.TabWidth = 8; // a tab renders as 8 spaces wide
                    options.TextOptions.WrapTextWidth = 100; // greater than zero so we will word wrap at 100 pixels wide
                    options.TextOptions.HorizontalAlignment = HorizontalAlignment.Left; // right align

                    IBrush brush = Brushes.Horizontal(Color.Red, Color.Blue);
                    IPen pen = Pens.Solid(Color.Green, 1);
                    string text = entity.StomachContentsLevel.ToString();

                    // draws a star with Horizontal red and blue hatching with a dash dot pattern outline.
                    image.Mutate(x => x.DrawText(options, text, _font, brush, pen, new PointF(entity.CurrentPosition.X + 6, entity.CurrentPosition.Y)));
                }

                image.SaveAsPng(filename);
            }
        }
    }
}
