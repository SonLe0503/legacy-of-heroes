using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    internal class SaveManager
    {
        private static string savePath = Application.persistentDataPath + "/player_save.json";

        public static void SavePlayer(GameObject player, string checkpointID, int score)
        {
            var health = player.GetComponent<Health>();
            PlayerSaveData data = new PlayerSaveData
            {
                health = health.currentHealth,
                lives = health.currentLives,
                score = score,
                position = new float[] {
                player.transform.position.x,
                player.transform.position.y,
                player.transform.position.z
            },
                checkpointID = checkpointID
            };

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(savePath, json);
            Debug.Log("Game Saved!");
        }

        public static PlayerSaveData LoadPlayer()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                return JsonUtility.FromJson<PlayerSaveData>(json);
            }

            return null;
        }

        public static void DeleteSave()
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
        }
    }
}
