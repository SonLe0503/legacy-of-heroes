using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    [System.Serializable]
    public class PlayerSaveData
    {
        public int health;
        public int lives;
        public int score;
        public float[] position; // [x, y, z]
        public string checkpointID;
    }

}
