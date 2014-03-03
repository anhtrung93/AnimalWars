using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AnimalWars.Screens.Maps;

namespace AnimalWars.Entities
{
    class Enemy : AutoCharacter
    {
        public Vector2[] destinationList = null;
        public int nextIdDestination;

        // enermy có thêm thuôc tính tầm nhìn
        // ban đầu di chuyển random 
        // nhưng phát hiện quân trong tầm nhìn nó sẽ di chuyển để đánh
        public Enemy(Texture2D image, Point currentFrame, int timeSinceLastFrame, Vector2 position, float velocity,
                                    int attack, int defend, int vision, int type, bool isMine,
                                    float blood, float rateImage, bool live, int level, Map spriteManager, Texture2D bloodImage, int visionRange)
            : base(image, currentFrame, timeSinceLastFrame, position, velocity, attack, defend, vision, type, isMine,
                                    blood, rateImage, live, level, spriteManager, bloodImage, visionRange)
        {
        }

        public override void moveByAI()
        {
            //Neu khong danh duoi theo player thi di tuan
            if (!followPlayer())
            {
                patrolOverPoints();//Cach di tuan duoc set voi Map qua ham setPatrolPath();
            }
        }

        public void setPatrolPath(Vector2[] destinationList)
        {
            //Dat danh sach cac diem di chuyen de enemy di lan luot den cac diem nay
            this.destinationList = destinationList;
            this.nextIdDestination = -1;
        }

        public bool patrolOverPoints()
        {
            bool result = false;
            Vector2 lastPositon = position;
            if (this.destinationList != null && this.destinationList.Length > 1)
            {
                if (this.currentState == CharacterState.DUNGYEN)
                {
                    ++this.nextIdDestination;
                    if (this.nextIdDestination >= this.destinationList.Length)
                    {
                        this.nextIdDestination = 0;
                    }
                }
                this.moveStraightTo(this.destinationList[this.nextIdDestination]);
                result = true;
            }
            if (!IsSafe)
            {
                position = lastPositon;
            }
            return result;
        }

        public int GetPlayer(Vector2[] playerPositionList)
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

        public bool followPlayer()
        {
            Vector2 lastPositon = position;
            int playerIndex = GetPlayer(spriteManager.GetPositionList);
            // nếu phát hiện mục tiêu gần nhất trong tầm nhìn
            if (playerIndex != -1)
            {
                Vector2 playerPosition = spriteManager.GetPositionList[playerIndex];
                moveStraightTo(playerPosition);
                // restore lại vị trí khi nó đè lên hình sprite khác
                if (!IsSafe)
                {
                    position = lastPositon;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


    }

}
