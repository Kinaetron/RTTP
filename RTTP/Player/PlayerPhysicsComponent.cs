using Microsoft.Xna.Framework.Input;

using PolyOne;
using PolyOne.Input;
using PolyOne.Components;


namespace RTTP
{
    public class PlayerPhysicsComponent : Component
    {
        public PlayerPhysicsComponent()
            :base(true, false)
        {
        }

        public override void Update()
        {
            base.Update();

            if (PolyInput.Keyboard.Check(Keys.Right)) {
                Entity.Position.X += 5;
            }
            if (PolyInput.Keyboard.Check(Keys.Left)) {
                Entity.Position.X -= 5;
            }

            if (PolyInput.Keyboard.Check(Keys.Up)) {
                Entity.Position.Y -= 5;
            }
            if (PolyInput.Keyboard.Check(Keys.Down)) {
                Entity.Position.Y += 5;
            }

            if (PolyInput.Keyboard.Pressed(Keys.Enter) == true) {
                
            }
        }
    }
}
