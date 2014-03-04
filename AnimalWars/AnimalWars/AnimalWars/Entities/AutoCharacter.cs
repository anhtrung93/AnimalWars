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

        // enermy có thêm thuôc tính tầm nhìn
        // ban đầu di chuyển random 
        // nhưng phát hiện quân trong tầm nhìn nó sẽ di chuyển để đánh
        public AutoCharacter(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, Map playingScreen, Texture2D bloodImage, int visionRange)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend, vision, type, isMine, blood, rateImage, live, level, playingScreen)
        {
            this.imagiBlood = bloodImage;
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
                this.currentState = CharacterState.DICHUYEN;
            else
                this.currentState = CharacterState.DUNGYEN;
            currentDirection.Normalize();

            // nếu nó di chuyển thì phải cập nhập Update cũ để cập nhật frame mới => tạo ảnh động di chuyển
            base.Update(gameTime);
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

        public abstract void moveByAI(); //Inheritance class should override this, position should be changed
    }
}
