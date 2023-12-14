using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Diagnostics;
using System.Threading.Tasks.Sources;
using System.Configuration;
using Microsoft.Data.SqlClient;
using SharpDX.XInput;

namespace PongPong
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private Texture2D Racket;
        private Texture2D BlackBackground;
        private Texture2D Ball;
        public MouseState mouse;
        public Vector2 position;
        public Vector2 velocity;
        public float Y;
        public float gravity;
        public float dx, dy;
        public int xspeed;
        public int yspeed;
        public Rectangle _ball;
        public Rectangle _racket;
        public Rectangle _background;
        public bool is_touching;
        public bool is_falling;
        private SpriteFont font;
        public int score;
        public int Highscore;
        public string start;
        private GameStates _gamestate;

        public  enum GameStates
        {
            Menu,
            Playing,
            Paused

            

        }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

           




        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            

           

            start = "START";
            Highscore = 0;
        



            _gamestate = GameStates.Menu;

            base.Initialize();
        }


        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Racket = Content.Load<Texture2D>("Racket");
            BlackBackground = Content.Load<Texture2D>("BlackBackground");
            Ball = Content.Load<Texture2D>("Ball");
            font = Content.Load<SpriteFont>("Score");
            font = Content.Load<SpriteFont>("Start");

            // TODO: use this.Content to load your game content here
        }


         
        

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape) && _gamestate == GameStates.Menu)
            {
              
                
            }
            else if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape) && _gamestate == GameStates.Playing)
            {
                _gamestate = GameStates.Menu;
                
            }

            if(Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Enter) && _gamestate == GameStates.Menu)
            {
                _gamestate = GameStates.Playing;
                dx = _background.Width/2;
                dy = _background.Height/2;
                is_falling = true;
                is_touching = false;
                score = 0;
                xspeed = 6;
                yspeed = 6;
            }

            _background = new Rectangle(0, 0, 800, 480);
            _ball = new Rectangle((int)dx, (int)dy, 20, 20);
            _racket = new Rectangle(mouse.X, 440, 128, 32);





            if (score > Highscore)
            {
                Highscore = score; 
            }
           



            mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();









          
            
                dx += xspeed;
                dy += yspeed;
            


            if (dy >= _racket.Top && _ball.Bottom <= _racket.Bottom && _ball.Left >= _racket.Left && _ball.Right <= _racket.Right)
            {
                
                yspeed *= -1;

                is_touching = true;

            }
            else
            {
                is_touching = false;
            }
            if(is_touching == true)
            {
                xspeed += 1;

                score += 1;
            }
 
            if ((dx > _background.Width) || (dx < 0))
            {
                xspeed *= -1;
            }
            if( (dy < 0))
            {
                yspeed *= -1;    
            }
            if(dy > _background.Height)
            {
                _gamestate = GameStates.Menu;
                score = 0;
            }

            
            






            // TODO: Add your update logic here

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);

            // TODO: Add your drawing code here
            if(_gamestate == GameStates.Menu)
            {
              
                _spriteBatch.Begin();
                _spriteBatch.Draw(BlackBackground, (_background), Color.White);
                _spriteBatch.DrawString(font, start.ToString(), new Vector2(380, 240), Color.White);
                _spriteBatch.DrawString(font, Highscore.ToString(), new Vector2(400, 200), Color.White);
                _spriteBatch.End();
            }
            else if(_gamestate == GameStates.Playing)
            {
                IsMouseVisible = false;
                _spriteBatch.Begin();
                _spriteBatch.Draw(BlackBackground, (_background), Color.White);
                _spriteBatch.Draw(Racket, (_racket), Color.White);
                _spriteBatch.Draw(Ball, (_ball), Color.White);
                _spriteBatch.DrawString(font, score.ToString(), new Vector2(400, 50), Color.White);
                


                _spriteBatch.End();

            }
            

            base.Draw(gameTime);
        }

    }
}