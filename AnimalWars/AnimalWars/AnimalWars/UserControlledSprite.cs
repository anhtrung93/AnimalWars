using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace AnimalWars
{
    class UserControlledSprite: Sprite
    {
        SpriteManager spriteManager;
        public int evade = -1; // default: not evade
        public float collideRange = 75;
        public Vector2 amzPos = Vector2.Zero;
        public Vector2 currentDirection;

        public UserControlledSprite(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, SpriteManager spriteManager, Texture2D imagiBlood)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend,vision, type, isMine, blood, rateImage, live, level)
        {
            this.spriteManager = spriteManager;
            this.imagiBlood = imagiBlood;
        }
        
        // My variables
        public Vector2 destination;
        //public Vector2 direction{get; set;}
        public bool isRunning = false;
        public override void Update(GameTime gameTime)
        {
            MoveByMouse(destination);
            base.Update(gameTime);
        }
        
        public void MoveByMouse(Vector2 destination)
        {
            Vector2 lastPosition = position;
            Vector2 direction = destination - position;
            direction.Normalize();
            position += direction * velocity;
            Vector2 speed = direction * velocity;


            if (isRunning && evade > -1 && spriteManager.GetLengthPlayerList() > 1)
            {
                Vector2 posit = spriteManager.GetPositionList[evade];
                Vector2 invertDirection = new Vector2(direction.Y, -direction.X);
                invertDirection = invertDirection * collideRange * 2;
                Vector2 amazingPosition = destination + invertDirection;
                Vector2 amazingDirection = amazingPosition - position;
                amazingDirection.Normalize();
                Vector2 amazingSpeed = amazingDirection * velocity;
                amzPos = amazingPosition;

                if (position.X > posit.X)
                {
                    position.X += Math.Abs(amazingSpeed.X);
                    if (!IsSafe)
                        position.X = lastPosition.X;

                }

                else if (position.X < posit.X)
                {
                    position.X -= Math.Abs(amazingSpeed.X);
                    if (!IsSafe)
                        position.X = lastPosition.X;

                }

                // Move away from the player vertically
                if (position.Y > posit.Y)
                {
                    position.Y += Math.Abs(amazingSpeed.Y);
                    if (!IsSafe)
                        position.Y = lastPosition.Y;

                }
                else if (position.Y < posit.Y)
                {
                    position.Y -= Math.Abs(amazingSpeed.Y);
                    if (!IsSafe)
                        position.Y = lastPosition.Y;
                }


            }

            // check xem nó luôn ở trạng thái không intersect vị trí với bất kỳ sprite nào.
            if (!IsSafe)
                position = lastPosition;// undo move before
            if (isRunning && spriteManager.GetLengthPlayerList() > 1) // nếu sprite đang chạy thì mới tránh
            {
                // nếu khoảng cách sprite gần nhất nhỏ hơn khoảng cách tối thiểu cho phép
                Vector2[] positionList = spriteManager.GetPositionList;
                int nearestSprite = GetNearestSprite(positionList);
                if (nearestSprite != -1) // check xem co sprite nao xung quanh khong
                {
                    if (Vector2.Distance(positionList[GetNearestSprite(positionList)], position) < collideRange)
                    {
                        evade = GetNearestSprite(positionList); // set evade is ready
                    }
                    else
                        evade = -1;
                } // khong co sprite nao xung quanh thi set khong co sprite nao can tranh
                else
                {
                    evade = -1; // reset evade
                }
            }
            if ((destination - position).Length() < velocity)
            {
                isRunning = false;
            }
          // lấy giá trị của hướng di chuyển của sprite sau 1 frame bằng cách sử dụng lastPosition đã lưu trước đó.
            currentDirection = position - lastPosition;
            currentDirection.Normalize();
            //angle = Math.Atan2(currentDirection.X, currentDirection.Y) * 360 / (2 * Math.PI);


            
        }

        public int GetNearestSprite(Vector2[] positionList)
        {
            float shortestDistance = 10000;
            int j = -1;
            for (int i = 0; i < positionList.Length; i++)
            {
                if (positionList[i] != new Vector2(-1, -1)) // neu ma sprite chua chet.
                {
                    Vector2 pos = positionList[i];
                    if (position != pos) // kiềm tra xem đó có phải là sprite hiện tại không?
                    {
                        float distance = Vector2.Distance(pos, position);
                        if (distance < shortestDistance)
                        {
                            j = i; // set j
                            shortestDistance = distance; // set shortesDistance
                        }
                    }
                }

            }
            return j;
        }
        public void CheckOuOfScreen(Rectangle clientRectangle)
        {
            Vector2 imageSize = new Vector2(frameSize.X, frameSize.Y);
            float minWidth = imageSize.X / 2;
            float minHeight = imageSize.Y / 2;
            float maxWidth = clientRectangle.Width - imageSize.X / 2;
            float maxHeight = clientRectangle.Height - imageSize.Y / 2;
            if (destination.X < minWidth)
                destination.X = minWidth;
            if (destination.X > maxWidth)
                destination.X = maxWidth;
            if (destination.Y < minHeight)
                destination.Y = minHeight;
            if (destination.Y > maxHeight)
                destination.Y = maxHeight;

        }
        public bool IsSafe
        {
            get
            {
                Rectangle[] rl = this.spriteManager.rectangleList;
                for (int i = 0; i < rl.Length; i++)
                {
                    // nếu sprite còn sống
                    if (rl[i] != Rectangle.Empty)
                    {
                        // check if it is not itself
                        if (this.boundsRectangle != rl[i])
                        {
                            if (this.boundsRectangle.Intersects(rl[i]))
                                return false;
                        }
                    }
                }
                return true;
            }

        }
       
        

    }
}
