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


        public int GetCharacter(Vector2[] CharacterList) //xác định id của nhân vật gần mình nhất trong list
        {
            //float shortestDistance = 10000;
            int j = -1;
            for (int i = 0; i < CharacterList.Length; i++)
            {
                if (CharacterList[i] != new Vector2(-1, -1)) // neu ma sprite chua chet.
                {
                    Vector2 pos = CharacterList[i];
                    if (position != pos) // kiềm tra xem đó có phải là sprite hiện tại không?
                    {
                        float distance = Vector2.Distance(pos, position);
                        if (distance < visionRange) // nếu trong tầm nhìn của enemy
                        {
                            j = i; // set j
                            //shortestDistance = distance; // set shortesDistance
                        }
                    }
                }

            }
            return j;
        }

        public bool followCharacter(Vector2[] CharacterList)    // đi theo nhân vật gần mình nhất trong list
        {
            Vector2 lastPositon = position;
            int characterIndex = GetCharacter(CharacterList);
            // nếu phát hiện mục tiêu gần nhất trong tầm nhìn
            if (characterIndex != -1)
            {
                Vector2 characterPosition = CharacterList[characterIndex];
                moveStraightTo(characterPosition);
                // restore lại vị trí khi nó đè lên hình sprite khác
                if (!IsSafe)
                {
                    position = lastPositon;
                }
                return true;
            }
            else
            {
                this.currentState = CharacterState.DUNGYEN;
                return false;
            }
        }


    }
    
}
