using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using AnimalWars.Screens.Maps;

namespace AnimalWars.Entities
{
    abstract class AutoCharacter : Character
    {
        public int visionRange;
        public Map spriteManager;
        public Vector2 currentDirection;
        public bool isRunning = false;

        // enermy có thêm thuôc tính tầm nhìn
        // ban đầu di chuyển random 
        // nhưng phát hiện quân trong tầm nhìn nó sẽ di chuyển để đánh
        public AutoCharacter(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, Map playinScreen, Texture2D bloodImage, int visionRange)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend, vision, type, isMine, blood, rateImage, live, level)
        {
            this.imagiBlood = bloodImage;
            this.spriteManager = playinScreen;
            this.visionRange = visionRange;
        }

        // Cần vị trí của player gần nhất trong tầm nhìn

        public override void Update(GameTime gameTime)
        {
            ChangeImageByMoving();
            Vector2 lastPositon = position;// sử dụng để backup lại vị trí khi di chuyển không hợp lý
            // lấy vị trí của player gần nhất trong tầm nhìn
            this.moveByAI();

            currentDirection = position - lastPositon;
            // xác định xem enemy có dịch chuyển hay không
            if (currentDirection != Vector2.Zero)
                isRunning = true;
            else
                isRunning = false;
            currentDirection.Normalize();

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

        public void moveStraightTo(Vector2 destination)//TODO replace this with Character.MoveTo(destination)
        {
            Vector2 lastPosition = this.position;
            Vector2 direction = destination - this.position;

            if (direction.Length() < velocity)
            {
                this.isRunning = false;
                this.currentState = CharacterState.DUNGYEN;
            }
            else
            {
                direction.Normalize();
                this.currentState = CharacterState.DICHUYEN;
            }
            this.position += direction * this.velocity;
            Vector2 speed = direction * this.velocity;

            // lấy giá trị của hướng di chuyển của sprite sau 1 frame bằng cách sử dụng lastPosition đã lưu trước đó.
            this.currentDirection = position - lastPosition;
            this.currentDirection.Normalize();
        }

        public abstract void moveByAI(); //Inheritance class should override this, position should be changed
    }
}
