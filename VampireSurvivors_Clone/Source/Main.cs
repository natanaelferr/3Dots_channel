using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic;

public class Main
{
    private readonly ContentManager _content;
    private readonly Game _game;
    private readonly GraphicsDeviceManager _graphics;
    private readonly GameWindow _window;


    private Input _inputObj;
    private Player _playerObj;
    private FireBall _fireballObj;
    private Enemy _enemyObj;

    private KeyboardState _previousKeyboardState;
    private MouseState _previousMouseState;


    //Sprites
    private Texture2D _Attacking;
    private Texture2D _Dead;
    private Texture2D _enemy;
    private Texture2D _grass;
    private Texture2D _idle;
    private Texture2D _run;
    private Texture2D _fireball;

    //Listas de objetos
    private List<FireBall> _fireBalls;
    private List<Enemy> _enemies;


    //Variáveis de inimigo
    private float enemySpawnTimer;

    //Variáveis de jogo
    int score = 0;
    

    public Main(ContentManager content, Game game, GraphicsDeviceManager graphics, GameWindow window)
    {
        _content = content;
        _game = game;
        _graphics = graphics;
        _window = window;
    }

    public void Initialize()
    {
        _Attacking = _content.Load<Texture2D>("Attack_1");
        _Dead = _content.Load<Texture2D>("Dead");
        _enemy = _content.Load<Texture2D>("enemy");
        _grass = _content.Load<Texture2D>("grass");
        _idle = _content.Load<Texture2D>("Idle");
        _run = _content.Load<Texture2D>("Run");
        _fireball = _content.Load<Texture2D>("fireball");


        _enemies = new List<Enemy>();
        _fireBalls = new List<FireBall>();

        _inputObj = new Input();
        _playerObj = new Player(_inputObj);
        

    }

    public void LoadContent()
    {
        

    }

    public void Update(float gameTime)
    {
        Random rng = new Random();
        _inputObj.Update(gameTime);
        _playerObj.Update(gameTime);

        //spawna enemies a cada 5 segundos
        enemySpawnTimer += gameTime;
        if (enemySpawnTimer >= 0.1f && (_enemies.Count < 400) && score < 200)
        {
            float x = rng.Next(300, 900);
            float y = rng.Next(100, 800);

            _enemyObj = new Enemy(new Vector2(x, y), _inputObj);
            _enemies.Add(_enemyObj);

            enemySpawnTimer = 0f;
        }

        if (_enemies != null)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime, _playerObj._characterAxis);
                if (enemy.GetBounds().Intersects(_playerObj.GetBounds()))
                {
                    _playerObj.TakeDamage(2);
                    if (_playerObj.IsDead())
                    {
                        // Lógica para quando o jogador morrer
                        _idle = _content.Load<Texture2D>("Dead");
                    }
                }
            }
        }

        //atualiza os fireballs
        if (_inputObj.mouseCurrent.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
        {
            // Remova o worldScale - use a posição direta do mouse
            Vector2 mouseWorld = new Vector2(
                _inputObj.mouseCurrent.X,
                _inputObj.mouseCurrent.Y
            );

            Vector2 fireOrigin = _playerObj.Center;
            Vector2 direction = mouseWorld - fireOrigin;
            direction.Normalize();

            if (!_playerObj.IsDead())
            {
                _fireballObj = new FireBall(fireOrigin, direction, _inputObj);
                _fireBalls.Add(_fireballObj);
            }
        }

        //Remove todos inimigos do mapa quando score chegar a 200
        if (score >= 200)
        {
            _enemies.Clear();
        }

        if (_fireBalls != null)
            {
                foreach (var fireball in _fireBalls)
                {
                    fireball.Update(gameTime);
                    //Se passar 5 segundos ou tocar algum inimigo, destruir o fireball
                    if (fireball.IsDestroyed)
                    {
                        _fireBalls.Remove(fireball);
                        break;
                    }
                    if (_enemies != null)
                    {
                        foreach (var enemy in _enemies)
                        {
                            if (fireball.GetBounds().Intersects(enemy.GetBounds()))
                            {
                                enemy.TakeDamage(100);
                                fireball.IsDestroyed = true;
                                if (enemy.IsDead())
                                {
                                    _enemies.Remove(enemy);
                                    score += 1;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        _previousMouseState = _inputObj.mouseCurrent;
        _previousKeyboardState = _inputObj.keyboardCurrent;

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(transformMatrix: Matrix.CreateScale(1.6f));

        spriteBatch.Draw(_grass, new Vector2(100, 0), new Rectangle(0, 0, 600, 600), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);

        spriteBatch.End();


        spriteBatch.Begin();

        _playerObj.Draw(spriteBatch, _idle);

        //renderização dos inimigos
        if (_enemies != null)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch, _enemy);
            }
        }

        //renderização dos fireballs
        if (_fireBalls != null)
        {
            foreach (var fireball in _fireBalls)
            {
                Console.WriteLine("Fireball Foreach!");
                fireball.Draw(spriteBatch, _fireball);
            }
        }

        spriteBatch.DrawString(_content.Load<SpriteFont>("File"), "Score: " + score + " / 200", new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(_content.Load<SpriteFont>("File"), "Player Life: " + _playerObj.health + " / 500", new Vector2(10, 30), Color.White);

        spriteBatch.End();
        
    }
}