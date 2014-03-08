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
    abstract class SemiAuto : AutoCharacter
    {
        
        public Vector2[] destinationList = null;
        
        // tập trung về tướng khi có lệnh follow phân tán khi có lệnh move
        // follow: di chuyển tới khi tướng trong tầm nhìn / 2 thì dừng,
        //nếu tướng trong tầm nhìn thì có thể tự động đánh địch khi địch cách tướng ko xa và trong tầm nhì của mình
        // nhưng phát hiện quân trong tầm nhìn nó sẽ di chuyển để đánh
        //move: di tản xung quanh tìm địch và đánh
        public SemiAuto(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    int blood, float rateImage, bool live, int level, Map spriteManager, Texture2D bloodImage, int visionRange)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend, vision, type, isMine,
                                    blood, rateImage, live, level, spriteManager, bloodImage, visionRange)
        {
        }

        // đi theo tướng

        
        // kiểm tra xem sprite có giao nhau hay đè hình lên sprite khác
        public override void moveByAI()
        {
            //theo tướng khi có lệnh
            if (Map.controlState == Map.ControlState.FOLLOW) 
                protectMainCharacter();
            //tìm và tc địch
            if (Map.controlState == Map.ControlState.MOVE)
            {
                if (!followOpponentCharacter(getOpponentCharacter()))
                {
                    AutoMove();//Cach di tuan duoc set voi Map qua ham se
                }
            }
        }

        public bool protectMainCharacter()
        {
            if (Map.controlState == Map.ControlState.FOLLOW)
            {
                Character mainCharacter = spriteManager.getMainCharacter();   //trả về vị trí nhân vật chính
                if (Vector2.Distance(mainCharacter.position, this.position) > visionRange)   //nếu xa tướng thì di chuyển tới gần
                {
                    moveStraightTo(mainCharacter.position);
                    return true;
                }
                else
                {
                    Vector2 lastPositon = position;                             //nếu gần thì sẽ tấn công địch nếu phát hiện nó gần tướng
                    Character enemy = getOpponentCharacter();

                    // nếu phát hiện mục tiêu gần nhất trong tầm nhìn
                    if (enemy != null &&
                        Vector2.Distance(mainCharacter.position, enemy.position) < visionRange)
                    {
                        followOpponentCharacter(enemy);
                        return true;
                    }
                    else if (Vector2.Distance(mainCharacter.position, this.position) > visionRange / 2)
                    {
                        moveStraightTo(mainCharacter.position);
                        return true;
                    }
                    else
                    {
                        setStandStill();
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        
        public void AutoMove()
        {}
    }
}
