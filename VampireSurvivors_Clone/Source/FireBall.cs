using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic;

public class FireBall
{
    public bool IsDestroyed = false;
    public float lifeTime; // Tempo de vida em segundos
    private Input _input;
    public Vector2 position;
    public Vector2 direction;

    public FireBall(Vector2 initialPosition, Vector2 direction, Input input)
    {
        this.position = initialPosition; // Start a bit ahead of the player
        this.direction = Vector2.Normalize(direction);
        _input = input;
    }

    public void Update(float deltaTime)
    {
        position += direction * 400f * deltaTime;
        lifeTime += deltaTime;
        if (lifeTime >= 2.5f)
        {
            IsDestroyed = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, position, new Rectangle(0, 0, 500, 500), Color.White, 0f, new Vector2(250, 250), 0.2f, SpriteEffects.None, 0f);
    }

    //obtém os limites do fireball para detecção de colisão
    public Rectangle GetBounds()
    {
        return new Rectangle((int)(position.X - 10), (int)(position.Y - 10), 20, 20);
    }


}