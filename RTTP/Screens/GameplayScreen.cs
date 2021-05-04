using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

using PolyOne.ScreenManager;
using PolyOne.Components;
using System.Collections;
using PolyOne.Engine;

namespace RTTP.Screens
{
    public enum GameState
    {
        Normal,
        Whack
    }

    class GameplayScreen : GameScreen
    {
        float pauseAlpha;

        //"Death" by John Donne
        const string poem = "\"Death\" by John Donne\n\n" +
                            "Death be not proud, though some have called thee\n" +
                            "Mighty and dreadfull, for, thou art not so,\n" +
                            "For, those, whom thou think'st, thou dost overthrow,\n" +
                            "Die not, poore death, nor yet canst thou kill me.\n" +
                            "From rest and sleepe, which but thy pictures bee,\n" +
                            "Much pleasure, then from thee, much more must flow,\n" +
                            "And soonest our best men with thee doe goe,\n" +
                            "Rest of their bones, and soules deliverie.\n" +
                            "Thou art slave to Fate, Chance, kings, and desperate men,\n" +
                            "And dost with poyson, warre, and sicknesse dwell,\n" +
                            "And poppie, or charmes can make us sleepe as well,\n" +
                            "And better then thy stroake; why swell'st thou then;\n" +
                            "One short sleepe past, wee wake eternally,\n" +
                            "And death shall be no more; death, thou shalt die.";

        Coroutine courtines = new Coroutine(ReadPoem(poem));

        Level level = new Level();

        Player player = new Player(new Vector2(100, 100));

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            ScreenManager.Game.ResetElapsedTime();
            level.LoadContent();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                    bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive == true)
            {

                if (MediaPlayer.State == MediaState.Paused) {
                    MediaPlayer.Resume();
                }

                //if (courtines.Finished == false)
                //courtines.Update();
                level.Update();
            }
            else {

                if (MediaPlayer.State == MediaState.Playing) {
                    MediaPlayer.Pause();
                }
            }
        }

        public override void HandleInput(InputMenuState input)
        {
            if (input.IsPauseGame(ControllingPlayer)) {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            level.Draw();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        static IEnumerator ReadPoem(string poem)
        {
            //Read the poem letter by letter
            foreach (var letter in poem)
            {
                Console.Write(letter);
                switch (letter)
                {
                    //Pause for punctuation
                    case ',':
                    case ';':
                        yield return 500;
                        break;

                    //Long pause for full-stop
                    case '.':
                        yield return 1000;
                        break;

                    //Short pause for anything else
                    default:
                        yield return 50;
                        break;
                }
            }
        }
    }
}
