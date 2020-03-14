using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;
using Iris;
using Iris.Graphics;

namespace IrisStein
{
    class Object
    {

        public enum Type
        {
            Lamp = 0,
            Armor = 1,
            Table = 2,
            Plant = 3
        }

        public static string AssetsFolder = "/Sprites/";

        public Type ObjType;
        public Sprite ObjSprite;
        public int PosX, PosY;
        public bool Block;

        public Object(int positionX, int positionY, bool block, Type thisType)
        {
            ObjType = thisType;
            PosY = positionX;
            PosX = positionY;
            Block = block;
        }

        public void InitContent(string contentRoot)
        {
            contentRoot += AssetsFolder;
            switch (ObjType)
            {
                case Type.Lamp:
                    ObjSprite = new Sprite(contentRoot + "lamp.png");
                    break;
                case Type.Armor:
                    ObjSprite = new Sprite(contentRoot + "armor.png");
                    break;
                case Type.Table:
                    ObjSprite = new Sprite(contentRoot + "table-chairs.png");
                    break;
                case Type.Plant:
                    ObjSprite = new Sprite(contentRoot + "plant-green.png");
                    break;
                default:
                    ObjSprite = new Sprite(contentRoot + "lamp.png");
                    break;
            }
        }

        public void Draw(RenderContext context, Vector2 playerPos, Vector2 playerDir, Vector2 screenSize, double viewDist)
        {

            // Viewing space calculation
            float dX = PosX + 0.5f - playerPos.X;
            float dY = PosY + 0.5f - playerPos.Y;

            // Calculate sprite distance from player
            double dist = Sqrt(dX * dX + dY * dY);

            // Sprite angle relative to viewing angle
            double spriteAngle = Atan2(dY, dX) - Atan2(playerDir.Y, playerDir.X);

            // Size of the sprite
            double size = viewDist / (Cos(spriteAngle) * dist);

            double scale = size / ObjSprite.Height;

            // X-position on screen
            float x = -(float)((Tan(spriteAngle) * viewDist) + (size / 2));

            ObjSprite.Position = new Vector2((screenSize.X / 2) + x, (float)((screenSize.Y - size) / 2));
            ObjSprite.Scale = new Vector2((float)scale, (float)scale);

            context.Draw(ObjSprite);
        }

        public void Update(float deltaTime)
        {

        }

    }
}
