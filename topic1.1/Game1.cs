using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace topic1._1
{

   

	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		Rectangle window;

		Texture2D background, background2;

		Texture2D asteroidTexture;

		Rectangle asteroidRect;

		Rectangle rocketRect;

		Rectangle rocketRect2;

		Rectangle rocketRect3;

		Texture2D rocketTexture;

		KeyboardState currentKey;

		float rotationAngle = 0f;

		float rotationSpeed = 2f;

		Random random = new Random();

		int randomX;

		int randomY;

		Color randomColor;

		int randomAlpha;

		float brightness;

		SpriteFont font;

		float time;



        public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;


			
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			window = new Rectangle(0, 0, 800, 500);
			_graphics.PreferredBackBufferWidth = 800;
			_graphics.PreferredBackBufferHeight = 500;
			_graphics.ApplyChanges();
			random = new Random();
			brightness = 1f;
			randomColor = new Color();
			for (int i = 0; i < 3; i++)
			{
				randomX = random.Next(0, window.Width - 50);
				randomY = random.Next(0, window.Height - 50);
				
			}
			
			asteroidRect = new Rectangle(100, 100, 50, 50);
			rocketRect = new Rectangle(-80, randomX, 50, 50);
			rocketRect2 = new Rectangle(850, randomX, 50, 50);
			rocketRect3 = new Rectangle(randomY, -50, 50, 50);

            randomAlpha = random.Next(0, 10);
            base.Initialize();

			
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			background = Content.Load<Texture2D>("image/spaceBack");

			asteroidTexture = Content.Load<Texture2D>("image/asteroid");

			rocketTexture = Content.Load<Texture2D>("image/rocket");

			background2 = Content.Load<Texture2D>("image/space_background");

            font = Content.Load<SpriteFont>("text/maintext");
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			time = (float)gameTime.TotalGameTime.TotalSeconds;
			rotationAngle += rotationSpeed * elapsed;
			KeyboardState currentKey = Keyboard.GetState();
            

            if (currentKey.IsKeyDown(Keys.Right))
			{
				asteroidRect.X += 2;
			}
			else if (currentKey.IsKeyDown(Keys.Left))
			{
				asteroidRect.X -= 2;
			}
			else if (currentKey.IsKeyDown(Keys.Up))
			{
				asteroidRect.Y -= 2;
			}
			else if (currentKey.IsKeyDown(Keys.Down))
			{
				asteroidRect.Y += 2;
			}

			if (currentKey.IsKeyDown(Keys.W))
			{
				rotationSpeed += 0.1f;
			}
			else if (currentKey.IsKeyDown(Keys.S))
			{
				rotationSpeed -= 0.1f;
			}

			// Stops asteroid from leaving the window

			if (asteroidRect.X < 0)
			{
				asteroidRect.X = 0;
			}
			else if (asteroidRect.X + asteroidRect.Width > window.Width)
			{
				asteroidRect.X = window.Width - asteroidRect.Width;
			}
			if (asteroidRect.Y < 0)
			{
				asteroidRect.Y = 0;
			}
			else if (asteroidRect.Y + asteroidRect.Height > window.Height)
			{
				asteroidRect.Y = window.Height - asteroidRect.Height;
			}
			// TODO: Add your update logic here
			if (asteroidRect.Intersects(rocketRect) || asteroidRect.Intersects(rocketRect2) || asteroidRect.Intersects(rocketRect3))
			{
				
				asteroidRect.X = 100; 
				asteroidRect.Y = 100;
				Exit();
				
			}

			if (time > 1f)
			{
				rocketRect.X += 3; 
				
			    
			    
            }

			if (time > 2f)
			{
				rocketRect2.X -= 3; 
			}
			if (time > 3f)
			{
				rocketRect3.Y += 3; 
			}

			if (rocketRect.X > window.Width)
			{
				rocketRect = new Rectangle(-80, random.Next(50, window.Height - 50), 50, 50);

			}
			else if (rocketRect2.X + rocketRect2.Width < 0)
			{
				rocketRect2 = new Rectangle(800, random.Next(50, window.Height - 50), 50, 50);

			}
			else if (rocketRect3.Y > window.Height)
			{
                
                rocketRect3 = new Rectangle(random.Next(50, window.Width - 50), 0,60,60);
				
			}

			if (randomAlpha == 0)
			{
				randomColor = Color.White;
			}
			else if (randomAlpha == 1)
			{
				randomColor = Color.Yellow;
			}
            else if (randomAlpha == 2)
            {
				randomColor = Color.SteelBlue;
            }
            else if (randomAlpha == 3)
            {
                randomColor = Color.Orange;
            }
			else if (randomAlpha == 4)
			{
				randomColor = Color.GhostWhite;
				brightness = 0.5f;
			}
            else if (randomAlpha == 5)
            {
				randomColor = Color.LightGreen;
                brightness = 0.3f;
            }
            else if (randomAlpha == 6)
            {
				randomColor = Color.Lime;
            }
			else if (randomAlpha == 7)
			{
				randomColor = Color.Gold;
			}
			else
			{
				randomColor = Color.White;
			}



            base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_spriteBatch.Begin();
			if (randomAlpha >= 4)
			{
                _spriteBatch.Draw(background, window, Color.White);
            }
            else
            {
                _spriteBatch.Draw(background2, window, Color.White);
            }
           
			_spriteBatch.Draw(
				asteroidTexture,	
				new Vector2(asteroidRect.X + asteroidRect.Width / 2f, asteroidRect.Y + asteroidRect.Height / 2f),
				null,
				Color.White,
				rotationAngle,
				new Vector2(asteroidTexture.Width / 2f, asteroidTexture.Height / 2f),
				new Vector2((float)asteroidRect.Width / asteroidTexture.Width, (float)asteroidRect.Height / asteroidTexture.Height),
				SpriteEffects.None,
				0f);

			_spriteBatch.Draw(rocketTexture, rocketRect,null, randomColor * brightness, 1.5f, new Vector2(rocketTexture.Width / 2f, rocketTexture.Height / 2f), SpriteEffects.None,1f);
			_spriteBatch.Draw(rocketTexture, rocketRect2, null, randomColor * brightness, -1.5f, new Vector2(rocketTexture.Width / 2f, rocketTexture.Height / 2f), SpriteEffects.None,1f);
			_spriteBatch.Draw(rocketTexture, rocketRect3, null, randomColor * brightness, 3.2f, new Vector2(rocketTexture.Width / 2f, rocketTexture.Height / 2f), SpriteEffects.None,1f);
			_spriteBatch.DrawString(font, "_PROGRAM.DRIVEABLE_ASTEROID_ (Don't get hit!)", new Vector2(0, 0), randomColor);
			if (time <= 2)
			{
				_spriteBatch.DrawString(font, "Use arrow keys to move!", new Vector2(600,470), randomColor);
			}
			
			_spriteBatch.End();
			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
