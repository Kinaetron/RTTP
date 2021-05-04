using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using PolyOne;
using PolyOne.Utility;
using PolyOne.Components;
using PolyOne.Input;
using PolyOne.Collision;
using PolyOne.Scenes;


namespace RTTP
{
    public class Player : Entity
    {
        private const float runAccel = 4.0f;
        private const float turnMul = 1.0f;
        private const float normMaxHorizSpeed = 12.0f;

        private const float gravity = 0.4f;
        private const float fallspeed = 4.0f;

        private Vector2 remainder;
        private Vector2 velocity;

         private StateMachine state;
         CounterSet<string> counters = new CounterSet<string>();
         PlayerGraphicsComponent graphics = new PlayerGraphicsComponent();

        public Player(Vector2 position)
               :base(position)
        {

            this.Tag((int)GameTags.Player);
            this.Collider = new Hitbox((float)24.0f, (float)48.0f, 0.0f, 0.0f);

            state = new StateMachine(2);
            state.SetCallbacks(0, new Func<int>(NormalUpdate), null, new Action(EnterNormal), new Action(LeaveNormal));
            state.SetCallbacks(1, new Func<int>(WhackUpdate), null, new Action(EnterWhack), new Action(LeaveWhack));

            this.Add(counters);
            this.Add(graphics);
            this.Add(state);

            counters["graceTimer"] = 20.0f;
            counters["stopTimer"] = 50.0f;

            //state.LogAllStates();
        }

        public override void Added(Scene scene)
        {
            base.Added(scene);
        }

        private void EnterNormal()
        {
            //PolyDebug.Log("Started Enter Normal");
        }

        private void LeaveNormal()
        {
            //PolyDebug.Log("Started Leave Normal");
        }

        private int NormalUpdate()
        {
            float sign = 0;

            velocity.Y += gravity;
            velocity.Y = MathHelper.Clamp(velocity.Y, -fallspeed, fallspeed);

            if (PolyInput.Keyboard.Pressed(Keys.Enter) == true) {
                return 1;
            }

            if (PolyInput.Keyboard.Check(Keys.Right) ||
                PolyInput.Keyboard.Check(Keys.D) ||
                PolyInput.GamePads[0].LeftStickHorizontal(0.3f) > 0.1f) {
                sign = 1;  
            }
            else if (PolyInput.Keyboard.Check(Keys.Left) || 
                     PolyInput.Keyboard.Check(Keys.A) ||
                     PolyInput.GamePads[0].LeftStickHorizontal(0.3f) < -0.1f) {
                sign = -1;
            }
            else {
                velocity.X = 0;
            }

            velocity.X += runAccel * sign;
            velocity.X = MathHelper.Clamp(velocity.X, -normMaxHorizSpeed, normMaxHorizSpeed);

            float currentSign = Math.Sign(velocity.X);

            if (currentSign != 0 && currentSign != sign) {
                velocity.X *= turnMul;
            }

            GamePad.SetVibration(PlayerIndex.One, 0.5f, 0.5f);

            MovementHorizontal(velocity.X);
            MovementVerical(velocity.Y);
            return 0;
        }

        private void MovementHorizontal(float amount)
        {
            remainder.X += amount;
            int move = (int)Math.Round((double)remainder.X);

            if(move != 0)
            {
                remainder.X -= move;
                int sign = Math.Sign(move);

                while (move != 0)
                {
                    Vector2 newPosition = Position + new Vector2(sign, 0);

                    if (this.CollideFirst((int)GameTags.Solid, newPosition) != null) {
                        remainder.X = 0;
                        break;
                    }
                    Position.X += sign;
                    move -= sign;
                }
            }
        }

        private void MovementVerical(float amount)
        {
            remainder.Y += amount;
            int move = (int)Math.Round((double)remainder.Y);

            if (move < 0)
            {
                remainder.Y -= move;
                while (move != 0)
                {
                    Vector2 newPosition = Position + new Vector2(0, -1.0f);
                    if (this.CollideFirst((int)GameTags.Solid, newPosition) != null) {
                        remainder.Y = 0;
                        break;
                    }
                    Position.Y += -1.0f;
                    move -= -1;
                }
            }
            else if(move > 0)
            {
                while (move != 0)
                {
                    Vector2 newPosition = Position + new Vector2(0, 1.0f);
                    if (this.CollideFirst((int)GameTags.Solid, newPosition) != null) {
                        remainder.Y = 0;
                        break;
                    }
                    Position.Y += 1.0f;
                    move -= 1;
                }
            }
        }

        private void EnterWhack()
        {
            PolyDebug.Log("Started Enter Whack");
        }

        private void LeaveWhack()
        {
            PolyDebug.Log("Started Leave Whack");
        }

        private int WhackUpdate()
        {
           //PolyDebug.Log("Whack Update is updating");
            if (PolyInput.Keyboard.Pressed(Keys.Enter) == true) {
                return 0;
            }
            else{
                return 1;
            }
        }
    }
}