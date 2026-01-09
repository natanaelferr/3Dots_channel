using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic;

public class Player
{

    private Input _input;

    //Variáveis do player
    public Vector2 _characterAxis;
    public Vector2 Position => _characterAxis;
    public Vector2 Center
    {
        get
        {
            float width = 100f * 1.8f;
            float height = 130f * 1.8f;
            return _characterAxis + new Vector2(width / 2f, height / 2f);
        }
    }

    public float moveSpeed = 200f;
    public int health = 500;

    public Player(Input input)
    {
        _input = input;
    }

    public void Update(float deltaTime)
    {
        if (health > 0)
        {
            if (_input.keyboardCurrent.IsKeyDown(Keys.A))
            {
                _characterAxis.X -= 1 * moveSpeed * deltaTime;
            }

            if (_input.keyboardCurrent.IsKeyDown(Keys.D))
            {
                _characterAxis.X += 1 * moveSpeed * deltaTime;
            }

            if (_input.keyboardCurrent.IsKeyDown(Keys.W))
            {
                _characterAxis.Y -= 1 * moveSpeed * deltaTime;
            }

            if (_input.keyboardCurrent.IsKeyDown(Keys.S))
            {
                _characterAxis.Y += 1 * moveSpeed * deltaTime;
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, new Vector2(_characterAxis.X, _characterAxis.Y), new Rectangle(0, 0, 100, 130), Color.White, 0f, Vector2.Zero, 1.8f, SpriteEffects.None, 0f);
    }

    public Rectangle GetBounds()
    {
        return new Rectangle((int)_characterAxis.X+75, (int)_characterAxis.Y+102, (int)(90), (int)(117));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}