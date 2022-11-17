using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

namespace App2.Scoreboards
{
    public class Scoreboard: MonoBehaviour
    {
        [SerializeField] private int maxScoreEntries = 5;
        [SerializeField] private Transform ScoreBox = null;
        [SerializeField] private GameObject scoreBoardEntryObject = null;
        public GameObject LeaderboardInput;
        public GameObject ReplayGame;
        public Button submitButton;
        public TMP_InputField submitName;


        private string savePath => $"{Directory.GetCurrentDirectory()}/highscores.json";

        private void Start()
        {
            ScoreboardSaveData savedScores = GetSavedScores();
            UpdateUI(savedScores);
        } 

        public void AddEntryFromUI()
        {
            ScoreboardEntryData newEntryData = new ScoreboardEntryData();
            string username = submitName.text;
            newEntryData.entryName = username;
            var score = PlayerPrefs.GetInt("time");
            if (score != 0){
                newEntryData.entryScore = score;
            }
            AddEntry(newEntryData);
            LeaderboardInput.SetActive(false);
            ReplayGame.SetActive(true);
        }

        private ScoreboardSaveData GetSavedScores()
        {
            if (!File.Exists(savePath))
            {
                File.Create(savePath).Dispose();
                using (StreamWriter stream = new StreamWriter(savePath))
                {
                    string json = JsonUtility.ToJson("\"{\\\"highscores\\\":[]}\"", true);
                    stream.Write(json);
                 }
                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(savePath))
            {
                string json = stream.ReadToEnd();
      
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<ScoreboardSaveData>(json);
             
                return list;

            }

        }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
         
            ScoreboardSaveData savedScores = GetSavedScores();
            bool scoreAdded = false;

            
            for (int i = 0; i < savedScores.highscores.Count; i++)
            {
                if (scoreboardEntryData.entryScore < savedScores.highscores[i].entryScore)
                {
                     savedScores.highscores.Insert(i, scoreboardEntryData);
                     scoreAdded = true;
                    break;
                 }
             }
         

            if (!scoreAdded && savedScores.highscores.Count < maxScoreEntries)
            {
                savedScores.highscores.Add(scoreboardEntryData);
            }

            if (savedScores.highscores.Count > maxScoreEntries)
            {
                savedScores.highscores.RemoveRange(maxScoreEntries, savedScores.highscores.Count - maxScoreEntries);
            }

            UpdateUI(savedScores);
            SaveScores(savedScores);
        }

        private void UpdateUI(ScoreboardSaveData savedScores)
        {
            foreach(Transform child in ScoreBox)
            {
                Destroy(child.gameObject);
            }

            foreach(ScoreboardEntryData highscore in savedScores.highscores)
            {
                Instantiate(scoreBoardEntryObject, ScoreBox).GetComponent<ScoreboardEntryUI>().Initialise(highscore);
            }
        }


        private void SaveScores(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(savePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }
    }
}
