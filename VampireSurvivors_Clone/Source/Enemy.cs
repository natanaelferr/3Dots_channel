using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic;

public class Enemy
{
    private Input _input;
    public Vector2 Position;
    public float moveSpeed = 100f;
    private int health = 100;


    public Enemy(Vector2 initialPosition, Input input)
    {
        Position = initialPosition;
        _input = input;
    }

    public void Update(float deltaTime, Vector2 playerPosition)
    {
        Vector2 direction = Vector2.Normalize(playerPosition - Position + new Vector2(70, 130));
        Position += direction * moveSpeed * deltaTime;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, Position, new Rectangle(0, 0, 90, 80), Color.White);
    }

    public Rectangle GetBounds()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, 70, 60);
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