using System;
using Microsoft.Xna.Framework.Media;

using PolyOne.LevelProcessor;
using PolyOne.Engine;
using PolyOne.Scenes;
using PolyOne.Utility;

using RTTP.Platforms;

namespace RTTP
{
    public enum GameTags
    {
        None = 0,
        Player = 1,
        Solid   = 2
    }

    public class Level : Scene, IDisposable
    {

        LevelTiles tiles;
        LevelData levelData = new LevelData();
        LevelTiler tile = new LevelTiler();

        Player player;

        Song backgroundSong;

        public Level ()
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            levelData = Engine.Instance.Content.Load<LevelData>("TestMap");
            tile.LoadContent(levelData);

            bool[,] collisionInfo = LevelTiler.TileConverison(tile.CollisionLayer, 513);
            tiles = new LevelTiles(collisionInfo);
            this.Add(tiles);

            player = new Player(tile.PlayerPosition[0]);
            this.Add(player);
            player.Added(this);

            initalizeMusic();
        }

        private void initalizeMusic()
        {
            if (tile.BackgroundSong != null)
            {
                backgroundSong = Engine.Instance.Content.Load<Song>(tile.BackgroundSong);
                if (MediaPlayer.State == MediaState.Paused) {
                    MediaPlayer.Stop();
                }
                MediaPlayer.Play(backgroundSong);

                MediaPlayer.Volume = 0.5f;
            }
        }

        public override void UnloadContent()
        {
        }

        public override void Update()
        {
            base.Update();
            player.Update();
        }

        public override void Draw()
        {
            Engine.Begin(Resolution.GetScaleMatrix);

            tile.DrawBackground();
            player.Draw();
            tile.DrawForeground();

            Engine.End();
        }

        public void Dispose()
        {
        }
    }
}
