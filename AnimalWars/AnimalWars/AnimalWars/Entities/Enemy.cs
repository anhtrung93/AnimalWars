using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AnimalWars.Screens;
using AnimalWars.Screens.Maps;

namespace AnimalWars.Entities
{
    class Enemy: Character
    {
        public int tamNhin;
        public Map spriteManager;
        public Vector2 currentDirection;
        public bool isRunning = false;
        // enermy có thêm thuôc tính tầm nhìn
        // ban đầu di chuyển random 
        // nhưng phát hiện quân trong tầm nhìn nó sẽ di chuyển để đánh
        public Enemy(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, Map spriteManager, Texture2D bloddImage, int tamNhin)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend, vision, type, isMine, blood, rateImage, live, level)
        {
            this.imagiBlood = bloddImage;
            this.spriteManager = spriteManager;
            this.tamNhin = tamNhin;
        }
        // Cần vị trí của player gần nhất trong tầm nhìn
        public int GetNearestPlayer(Vector2[] playerPositionList)
        {
            //float shortestDistance = 10000;
            int j = -1;
            for (int i = 0; i < playerPositionList.Length; i++)
            {
                if (playerPositionList[i] != new Vector2(-1, -1)) // neu ma sprite chua chet.
                {
                    Vector2 pos = playerPositionList[i];
                    if (position != pos) // kiềm tra xem đó có phải là sprite hiện tại không?
                    {
                        float distance = Vector2.Distance(pos, position);
                        if (distance < tamNhin) // nếu trong tầm nhìn của enemy
                        {
                            j = i; // set j
                            //shortestDistance = distance; // set shortesDistance
                        }
                    }
                }

            }
            return j;
        }
        public override void Update(GameTime gameTime)
        {
            ChangeImageByMoving();
            Vector2 lastPositon = position; // sử dụng để backup lại vị trí khi di chuyển không hợp lý
            // lấy vị trí của player gần nhất trong tầm nhìn
            int playerIndex = GetNearestPlayer(spriteManager.GetPositionList);
            // nếu phát hiện mục tiêu gần nhất trong tầm nhìn
            if (playerIndex != -1)
            {
                Vector2 playerPosition = spriteManager.GetPositionList[playerIndex];
                Vector2 x = playerPosition - position;
                x.Normalize();
                position += (velocity * x);
                // restore lại vị trí khi nó đè lên hình sprite khác
                if (!IsSafe)
                    position = lastPositon;
            }
            currentDirection = position - lastPositon;
            // xác định xem enemy có dịch chuyển hay không
            if (currentDirection != Vector2.Zero)
            {
                isRunning = true;
                currentState = CharacterState.DICHUYEN;

            }
            else
            {
                isRunning = false;
                currentState = CharacterState.DUNGYEN;
                currentDirection.Normalize();
            }
            // nếu nó di chuyển thì phải cập nhập Update cũ để cập nhật frame mới => tạo ảnh động di chuyển
        //    if (isRunning)
            {

                base.Update(gameTime);
            }
        }
        // kiểm tra xem sprite có giao nhau hay đè hình lên sprite khác
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
        public double movingAngle
        {
            get
            {
                return (Math.Atan2(currentDirection.X, currentDirection.Y) * 360 / (2 * Math.PI));
            }
        }
    }
    
}
