using Microsoft.Xna.Framework;

using PolyOne.Engine;
using PolyOne.Utility;

using RTTP.Screens;

namespace RTTP
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RTTP : Engine
    {
        static readonly string[] preloadAssets =
        {
            "MenuAssets/gradient",
        };

        public RTTP()
            : base(1280, 720, "RTTP")
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            TileInformation.TileDiemensions(32, 32);

            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            foreach (string asset in preloadAssets)
            {
                Engine.Instance.Content.Load<object>(asset);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (RTTP game = new RTTP())
            {
                game.Run();
            }
        }
    }
}
