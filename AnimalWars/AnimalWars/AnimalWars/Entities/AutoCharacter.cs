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
        const int NO_CHARACTER = -1;

        public int visionRange;

        // enermy có thêm thuôc tính tầm nhìn
        // ban đầu di chuyển random 
        // nhưng phát hiện quân trong tầm nhìn nó sẽ di chuyển để đánh
        public AutoCharacter(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    int blood, float rateImage, bool live, int level, Map playingScreen, Texture2D bloodImage, int visionRange)
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
            currentDirection.Normalize();

            // nếu nó di chuyển thì phải cập nhập Update cũ để cập nhật frame mới => tạo ảnh động di chuyển
            base.Update(gameTime);
        }

        public Character getMainCharacter()
        {
            return spriteManager.getMainCharacter();
        }

        public Character getOpponentCharacter() //xác định id của nhân vật gần mình nhất trong list
        {
            //TODO update this to higher level
            List<Character> characterList;
            if (this.isMine == true)
            {
                characterList = spriteManager.getEnemyList();
            }
            else
            {
                characterList = spriteManager.getMyCharacterList();
            }
            
            Character opponentToFollow = null;
            float minDistance = (float)Statics.INFINITE;
            for (int idCharacterList = 0; idCharacterList < characterList.Count; idCharacterList++)
            {
                Vector2 opponentPosition = characterList[idCharacterList].position;
                if (characterList[idCharacterList].isMine != this.isMine) // kiềm tra xem đó có phải là sprite hiện tại không?
                {
                    float distance = Vector2.Distance(opponentPosition, position);
                    if (distance < visionRange && distance < minDistance) // nếu trong tầm nhìn của enemy
                    {
                        opponentToFollow = characterList[idCharacterList];
                        minDistance = distance;
                    }
                }
            }
            return opponentToFollow;
        }

        public bool followOpponentCharacter(Character opponentCharacter)    // đi theo nhân vật gần mình nhất trong list
        {
            Vector2 lastPositon = position;
            // nếu phát hiện mục tiêu gần nhất trong tầm nhìn
            if (opponentCharacter != null)
            {
                if (!this.isAbleToHit(opponentCharacter))
                {
                    moveStraightTo(opponentCharacter.position);
                }
                else
                {
                    setAttack();
                    this.hit(opponentCharacter);
                }
                return true;
            }
            else 
            {
                return false;
            }
        }

        public abstract void moveByAI(); //Inheritance class should override this, position should be changed

    }
    
}
