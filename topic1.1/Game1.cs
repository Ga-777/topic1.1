using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace topic1._1
{

    enum ScreenState
	{
		MainMenu,
		Gameplay,
		GameOver
	}

	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		Rectangle window;

		Texture2D background;

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
			for (int i = 0; i < 3; i++)
			{
				randomX = random.Next(0, window.Width - 50);
				randomY = random.Next(0, window.Height - 50);
				
			}
			
			asteroidRect = new Rectangle(100, 100, 50, 50);
			rocketRect = new Rectangle(-80, randomX, 50, 50);
			rocketRect2 = new Rectangle(850, randomX, 50, 50);
			rocketRect3 = new Rectangle(randomY, -50, 50, 50);


			base.Initialize();

			
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			background = Content.Load<Texture2D>("image/spaceBack");

			asteroidTexture = Content.Load<Texture2D>("image/asteroid");

			rocketTexture = Content.Load<Texture2D>("image/rocket");
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			float time = (float)gameTime.TotalGameTime.TotalSeconds;
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
				// Handle collision with rocket
				// For example, you can reset the asteroid's position or reduce health
				asteroidRect.X = 100; // Reset position
				asteroidRect.Y = 100;
				
			}
			
			if (time > 1f)
			{
				rocketRect.X += 1; // Move rocket to the right
			}

			if (time > 2f)
			{
				rocketRect2.X -= 1; // Move rocket to the left
			}
			if (time > 3f)
			{
				rocketRect3.Y += 1; // Move rocket to the up
			}

			if (rocketRect.X > window.Width)
			{
				rocketRect = new Rectangle(-80, random.Next(0, window.Height - 50), 50, 50);

			}
			else if (rocketRect2.X + rocketRect2.Width < 0)
			{
				rocketRect2 = new Rectangle(-80, random.Next(0, window.Height - 50), 50, 50);

			}
			else if (rocketRect3.Y > window.Height)
			{
				rocketRect3.Y = -rocketRect3.Height; // Reset position to the top
				
			}

			


			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_spriteBatch.Begin();
			_spriteBatch.Draw(background, window, Color.White);
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

			_spriteBatch.Draw(rocketTexture, rocketRect,null, Color.White, 1.5f, new Vector2(rocketTexture.Width / 2f, rocketTexture.Height / 2f), SpriteEffects.None,1f);
			_spriteBatch.Draw(rocketTexture, rocketRect2, null, Color.White, -1.5f, new Vector2(rocketTexture.Width / 2f, rocketTexture.Height / 2f), SpriteEffects.None,1f);
			_spriteBatch.Draw(rocketTexture, rocketRect3, null, Color.White, 3.2f, new Vector2(rocketTexture.Width / 2f, rocketTexture.Height / 2f), SpriteEffects.None,1f);
			_spriteBatch.End();
			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
