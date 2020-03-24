using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;
using Iris;
using Iris.Graphics;
using Iris.Input;
using System.Diagnostics;
using System.IO;

namespace IrisStein
{
    class WolfGame : Game
    {

        #region Debug WorldMap
        
        public int[,] WorldMap = new int[,]
        {
            {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,7,7,7,7,7,7,7,7},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,7},
            {4,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7},
            {4,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7},
            {4,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,7},
            {4,0,4,0,0,0,0,5,5,5,5,5,5,5,5,5,7,7,0,7,7,7,7,7},
            {4,0,5,0,0,0,0,5,0,5,0,5,0,5,0,5,7,0,0,0,7,7,7,1},
            {4,0,6,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,8},
            {4,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,1},
            {4,0,8,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,8},
            {4,0,0,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,7,7,7,1},
            {4,0,0,0,0,0,0,5,5,5,5,0,5,5,5,5,7,7,7,7,7,7,7,1},
            {6,6,6,6,6,6,6,6,6,6,6,0,6,6,6,6,6,6,6,6,6,6,6,6},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {6,6,6,6,6,6,0,6,6,6,6,0,6,6,6,6,6,6,6,6,6,6,6,6},
            {4,4,4,4,4,4,0,4,4,4,6,0,6,2,2,2,2,2,2,2,3,3,3,3},
            {4,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,0,0,0,2},
            {4,0,0,0,0,0,0,0,0,0,0,0,6,2,0,0,5,0,0,2,0,0,0,2},
            {4,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,2,0,2,2},
            {4,0,6,0,6,0,0,0,0,4,6,0,0,0,0,0,5,0,0,0,0,0,0,2},
            {4,0,0,5,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,2,0,2,2},
            {4,0,6,0,6,0,0,0,0,4,6,0,6,2,0,0,5,0,0,2,0,0,0,2},
            {4,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,0,0,0,2},
            {4,4,4,4,4,4,4,4,4,4,1,1,1,2,2,2,2,2,2,3,3,3,3,3}
        };
        
        /*
        public int[,] WorldMap = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };
        */
        public Object[] SpriteList = new Object[]
        {
            new Object(14, 6, true, Object.Type.Armor),
            new Object(12, 6, true, Object.Type.Armor),
            new Object(10, 6, true, Object.Type.Armor),
            new Object(8, 6, true, Object.Type.Armor),
            new Object(11, 20, false, Object.Type.Lamp),
            new Object(11, 17, false, Object.Type.Lamp),
            new Object(11, 14, false, Object.Type.Lamp),
            new Object(11, 11, false, Object.Type.Lamp),
            new Object(14, 22, true, Object.Type.Plant),
            new Object(8, 22, true, Object.Type.Plant),
            new Object(1, 22, true, Object.Type.Table),
        };
        #endregion

        #region Meta Variables
        public int MapWidth;
        public int MapHeight;
        public int ScreenWidth;
        public int ScreenHeight;
        public uint TextureWidth = 64;
        public uint TextureHeight = 64;

        public static int ScreenWidthDivision = 1;
        public static int ScreenHeightDivision = 1;

        public Object[,] SpriteMap;

        private Font DebugFont;
        private bool FlatRender = false;
        #endregion

        private double PosX = 22.5, PosY = 11.5;
        private double DirX = -1, DirY = 0;
        private double PlaneX = 0, PlaneY = 0.66;
        private double fov = 2 * Atan(0.66 / 1.0);
        private double PlayerMoveSpeed = 5.0;
        private double PlayerRotSpeed = 3.0;
        private double PlayerCollisionWideness = 0.2;
        private double viewDist;

        private Color[,] WallPixels;
        private Texture[] WallTextures;

        private string FPSText = "";

        protected override void Initialize()
        {
            GraphicsSettings.BackBufferWidth = 1280;
            GraphicsSettings.BackBufferHeight = 720;
            ScreenWidth = (int)GraphicsSettings.BackBufferWidth;
            ScreenHeight = (int)GraphicsSettings.BackBufferHeight;
            GraphicsSettings.ColorDepth = 16;
            GraphicsSettings.UpdateImmediately = false;
            GraphicsSettings.FramerateLimit = 0;
            GraphicsSettings.EnableVerticalSync = false;
            GraphicsSettings.AntialiasingLevel = 1;

            GraphicsSettings.CommitChanges();

            MapWidth = WorldMap.GetLength(0);
            MapHeight = WorldMap.GetLength(1);
            SpriteMap = new Object[MapWidth, MapHeight];
            foreach (Object obj in SpriteList)
            {
                SpriteMap[obj.PosX, obj.PosY] = obj;
            }

            viewDist = (ScreenWidth / 2) / Tan(fov / 2);
        }

        protected override void LoadContent()
        {
            DebugFont = Content.Load<Font>("DooM.ttf");
            DebugFont.CharacterSize = 18;

            // Load wall spritesheet into memory
            Sprite AllWallsSprite = Content.Load<Sprite>("walls.jpeg");
            Texture AllWalls = AllWallsSprite.Texture;
            int CellColumns = (int)AllWalls.Width / (int)TextureWidth;
            int CellRows = (int)AllWalls.Height / (int)TextureHeight;
            WallPixels = new Color[CellColumns * CellRows, TextureWidth * TextureHeight];
            WallTextures = new Texture[CellColumns * CellRows];
            for (int cellY = 0; cellY < CellRows; cellY++)
            {
                for (int cellX = 0; cellX < CellColumns; cellX++)
                {
                    WallTextures[(CellColumns * cellY) + cellX] = new Texture(TextureWidth, TextureHeight);
                    for (int texX = 0; texX < TextureWidth; texX++)
                    {
                        for (int texY = 0; texY < TextureHeight; texY++)
                        {
                            WallPixels[(CellColumns * cellY) + cellX, TextureWidth * texX + texY] =
                                AllWalls.GetPixel((uint)((cellX * TextureWidth) + texX),
                                (uint)((cellY * TextureHeight) + texY));
                            WallTextures[(CellColumns * cellY) + cellX].SetPixel(
                                (uint)texX, (uint)texY,
                                AllWalls.GetPixel((uint)((cellX * TextureWidth) + texX),
                                (uint)((cellY * TextureHeight) + texY))
                                );
                        }
                    }
                }
            }

            // Load sprites
            foreach (Object sprite in SpriteList)
            {
                sprite.InitContent(Content.ContentRoot);
            }
        }

        protected override void Draw(RenderContext context)
        {
            context.Clear(Color.Gray);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            List<Object> spritesToDraw = new List<Object>();
            List<Object> spritesDrawn = new List<Object>();

            for (int x = 0; x <= ScreenWidth; x += ScreenWidthDivision)
            {
                // Calculate the direction needed for the ray, and the cam space
                double cameraX = 2 * x / (double)ScreenWidth - 1;
                double rayDirX = DirX + PlaneX * cameraX;
                double rayDirY = DirY + PlaneY * cameraX;

                // Where is the ray located in the map?
                int mapX = (int)PosX;
                int mapY = (int)PosY;

                // These variables tell us how far until the next X side of the matrix 
                // Or the next Y side, relative to the current position
                double sideDistX;
                double sideDistY;

                // Same principle as the sideDistX/Y except its from the *last* X or Y side
                // Also prevent deviding by 0 (does C# even care about this? Should look that up)
                double deltaDistX = (rayDirY == 0) ? 0 : ((rayDirX == 0) ? 1 : Abs(1 / rayDirX));
                double deltaDistY = (rayDirX == 0) ? 0 : ((rayDirY == 0) ? 1 : Abs(1 / rayDirY));
                double perpWallDist;

                // Either -1 or 1 depending on what direction we need to step in next
                int stepX;
                int stepY;

                int hit = 0; // Did we hit a wall?
                int side = 0; // Which side we hit the wall on (North/South or East/West)

                // Actually calculate side and directions
                if (rayDirX < 0)
                {
                    stepX = -1;
                    sideDistX = (PosX - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - PosX) * deltaDistX;
                }
                if (rayDirY < 0)
                {
                    stepY = -1;
                    sideDistY = (PosY - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - PosY) * deltaDistY;
                }

                // Finally, Actually, do the raycasting using the DDA algorithm
                while (hit == 0)
                {
                    // Jump to the next map square/X direction/Y direction
                    if (sideDistX < sideDistY)
                    {
                        sideDistX += deltaDistX;
                        mapX += stepX;
                        side = 0;
                    }
                    else
                    {
                        sideDistY += deltaDistY;
                        mapY += stepY;
                        side = 1;
                    }

                    if (mapX >= WorldMap.GetLength(0) || mapY >= WorldMap.GetLength(1)) break;
                    if (mapX < 0 || mapY < 0) break;

                    // Check if ray has hit a wall
                    if (WorldMap[mapX, mapY] > 0) hit = 1;
                    // Check if a sprite is in view
                    if (SpriteMap[mapX, mapY] != null)
                    {
                        if (!spritesToDraw.Contains(SpriteMap[mapX, mapY])) spritesToDraw.Add(SpriteMap[mapX, mapY]);
                    }
                }

                if (hit == 0) continue;

                // Calculate distance projected on camera direction (Euclidean distance will give fisheye effect!)
                if (side == 0)
                {
                    perpWallDist = (mapX - PosX + (1 - stepX) / 2) / rayDirX;
                }
                else
                {
                    perpWallDist = (mapY - PosY + (1 - stepY) / 2) / rayDirY;
                }

                // Calculate height of the strip that we draw to the screen
                int lineHeight = (int)(ScreenHeight / perpWallDist);

                // Calculate lowest and highest pixel to fill in current stripe
                int drawStart = -lineHeight / 2 + ScreenHeight / 2;
                if (drawStart < 0) drawStart = 0;
                int drawEnd = lineHeight / 2 + ScreenHeight / 2;
                if (drawEnd >= ScreenHeight) drawEnd = ScreenHeight - 1;

                // Calculate texture rendering
                int texNum = WorldMap[mapX, mapY] - 1; // Subtract 1 so we are 0 indexed

                // Calculate where the wall was hit
                double wallX;
                if (side == 0) wallX = PosY + perpWallDist * rayDirY;
                else wallX = PosX + perpWallDist * rayDirX;
                wallX -= Floor(wallX);

                // Calculate the X coordinate of the txture
                int texX = (int)(wallX * (double)TextureWidth);
                if (side == 0 && rayDirX > 0) texX = (int)TextureWidth - texX - 1;
                if (side == 1 && rayDirY < 0) texX = (int)TextureWidth - texX - 1;

                Color color;

                if (!FlatRender)
                {
                    // Draw textures using strips instead of pixels
                    // I don't know why i didn't at least try this to begin with
                    int texY = Min(drawStart, -(lineHeight - ScreenHeight) / 2);
                    Texture stripTex = WallTextures[texNum];
                    Sprite stripSprite = new Sprite(stripTex);
                    stripSprite.SourceRectangle = new Rectangle(texX, 0, ScreenWidthDivision, TextureHeight);
                    stripSprite.Position = new Vector2(x, texY);
                    stripSprite.Scale = new Vector2(1, (float)lineHeight / TextureHeight);
                    stripSprite.UpdateTexture();
                    context.Draw(stripSprite);
                    if(side == 1)
                    {
                        // Shitty solution but fuck it, sprite.Color doesn't work.
                        DrawColorStrip(context, x, drawStart, drawEnd, new Color(0,0,0,50));
                    }
                }
                else
                {
                    // Debugging wall colors for flat renderer
                    switch (WorldMap[mapX, mapY])
                    {
                        case 1: color = Color.Red; break;
                        case 2: color = Color.Green; break;
                        case 3: color = Color.Blue; break;
                        case 4: color = Color.Beige; break;
                        case 5: color = Color.Aquamarine; break;
                        case 6: color = Color.HotPink; break;
                        case 7: color = Color.Purple; break;
                        case 8: color = Color.Brown; break;
                        default: color = Color.Yellow; break;
                    }

                    // Darken the color to create perspective
                    if (side == 1)
                    {
                        color.R /= 2;
                        color.G /= 2;
                        color.B /= 2;
                    }

                    // Actually draw the pixels of the stripe as a vertical line
                    DrawColorStrip(context, x, drawStart, drawEnd, color);
                }
            }

            // Render sprites
            foreach (Object sprite in spritesToDraw)
            {
                sprite.Draw(context,
                    new Vector2((float)PosX, (float)PosY),
                    new Vector2((float)DirX, (float)DirY),
                    new Vector2(ScreenWidth, ScreenHeight),
                    viewDist
                    );
            };

            stopwatch.Stop();
            FPSText += $"\nRender Stopwatch: {stopwatch.ElapsedMilliseconds}";

            context.DrawString(DebugFont, FPSText, Vector2.Zero, Color.Red);
        }

        private void DrawColorStrip(RenderContext context, int screenX, int drawStart, int drawEnd, Color color)
        {
            if (drawEnd - drawStart <= 0) return;
            context.DrawLine(new Vector2(screenX, drawStart), new Vector2(screenX, drawEnd), 1, color);
        }

        protected override void Update(float deltaTime)
        {

            FPSText = $"FPS: {(int)(1.0f / deltaTime)}";
            // If the game is running at 0.25 FPS then just make it flat render
            if (deltaTime >= 4.0f)
            {
                FlatRender = true;
            }

            // Run sprite updates (walking, shooting, etc)
            foreach (Object sprite in SpriteList)
            {
                sprite.Update(deltaTime);
            }

            // Player input

            // Speed of player's movement and rotation of camera
            double moveSpeed = deltaTime * PlayerMoveSpeed; // The constant value is in squares/second
            double rotSpeed = deltaTime * PlayerRotSpeed; // The constant value is in radians/second

            // Move forward if there is no wall in front of you
            if (Keyboard.IsKeyDown(KeyCode.W))
            {
                Vector2 xDir = new Vector2((float)(PosX + DirX * moveSpeed), (float)PosY);
                Vector2 yDir = new Vector2((float)PosX, (float)(PosY + DirY * moveSpeed));
                if (!CheckWallCollision(xDir) && !CheckSpriteCollision(xDir)) PosX += DirX * moveSpeed;
                if (!CheckWallCollision(yDir) && !CheckSpriteCollision(yDir)) PosY += DirY * moveSpeed;
            }
            // Move backwards and check for collision
            if (Keyboard.IsKeyDown(KeyCode.S))
            {
                Vector2 xDir = new Vector2((float)(PosX - DirX * moveSpeed), (float)PosY);
                Vector2 yDir = new Vector2((float)PosX, (float)(PosY - DirY * moveSpeed));
                if (!CheckWallCollision(xDir) && !CheckSpriteCollision(xDir)) PosX -= DirX * moveSpeed;
                if (!CheckWallCollision(yDir) && !CheckSpriteCollision(yDir)) PosY -= DirY * moveSpeed;
            }
            // Rotate camera right
            if (Keyboard.IsKeyDown(KeyCode.D))
            {
                // Cam direction and cam plane are rotated
                double oldDirX = DirX;
                DirX = DirX * Cos(-rotSpeed) - DirY * Sin(-rotSpeed);
                DirY = oldDirX * Sin(-rotSpeed) + DirY * Cos(-rotSpeed);
                double oldPlaneX = PlaneX;
                PlaneX = PlaneX * Cos(-rotSpeed) - PlaneY * Sin(-rotSpeed);
                PlaneY = oldPlaneX * Sin(-rotSpeed) + PlaneY * Cos(-rotSpeed);
            }
            // Rotate camera left
            if (Keyboard.IsKeyDown(KeyCode.A))
            {
                //both camera direction and camera plane must be rotated
                double oldDirX = DirX;
                DirX = DirX * Cos(rotSpeed) - DirY * Sin(rotSpeed);
                DirY = oldDirX * Sin(rotSpeed) + DirY * Cos(rotSpeed);
                double oldPlaneX = PlaneX;
                PlaneX = PlaneX * Cos(rotSpeed) - PlaneY * Sin(rotSpeed);
                PlaneY = oldPlaneX * Sin(rotSpeed) + PlaneY * Cos(rotSpeed);
            }
        }

        private bool CheckWallCollision(Vector2 mapPos)
        {
            if (WorldMap[(int)mapPos.X, (int)mapPos.Y] > 0) return true;
            return false;
        }

        private bool CheckSpriteCollision(Vector2 mapPos)
        {
            foreach (Object sprite in SpriteList)
            {
                if (sprite.PosX == (int)mapPos.X && sprite.PosY == (int)mapPos.Y && sprite.Block)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void KeyPressed(KeyCode keyCode, KeyModifiers modifiers)
        {
            base.KeyPressed(keyCode, modifiers);

            if (keyCode == KeyCode.F)
            {
                FlatRender = !FlatRender;
            }
        }
    }
}
