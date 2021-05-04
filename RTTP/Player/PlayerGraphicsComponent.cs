using Microsoft.Xna.Framework.Graphics;

using PolyOne.Engine;
using PolyOne.Components;

using Microsoft.Xna.Framework;

namespace RTTP
{
    public class PlayerGraphicsComponent : Component
    {
        Texture2D PlayerImage;

        public PlayerGraphicsComponent() :
            base(false, true)
        {
            PlayerImage = Engine.Instance.Content.Load<Texture2D>("Player");
        }

        public override void Draw()
        {
            base.Draw();
            Engine.SpriteBatch.Draw(PlayerImage, this.Entity.Position, Color.White);
        }
    }
}
