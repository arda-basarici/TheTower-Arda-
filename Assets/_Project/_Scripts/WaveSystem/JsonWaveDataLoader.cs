//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Game
//{
//    public class JsonWaveDataLoader : IWaveDataLoader
//    {

//        public WaveDataProcessor WaveDataProcessor { private get; set; }

//        public List<WaveData> LoadWaveDataAndReturn()
//        {
//            TextAsset jsonFile = Resources.Load<TextAsset>(ResourcePaths.WaveData);
//            if (jsonFile == null)
//            {
//                Debug.LogError("Json file not found");
//                return null;
//            }


//            return DeserializeWaveData(jsonFile.text, WaveDataProcessor);
//        }

//        private List<WaveData> DeserializeWaveData(string json, WaveDataProcessor waveDataProcessor)
//        {
//            var jsonData = SimpleJSON.JSON.Parse(json);
//            Debug.Log("Parsed JSON: " + jsonData.ToString());

//                List<WaveData> waves = new List<WaveData>();

//            //foreach (var waveJson in jsonData["waves"].AsArray)
//            //{
//            //    var waveNode = waveJson.Value;
//            //    int waveNumber = waveNode["WaveNumber"].AsInt;


//            //    var enemyBulks = new List<EnemyBulk>();
//            //    foreach (var enemyBulkJson in waveNode["EnemyBulks"].AsArray)
//            //    {

//            //        var enemyType = (EnemyType)enemyBulkJson.Value["EnemyType"].AsInt;

//            //        (int, int) amountRange = (enemyBulkJson.Value["AmountRange"]["min"].AsInt, enemyBulkJson.Value["AmountRange"]["max"].AsInt);
//            //        (int, int) spawnTimeRange = (enemyBulkJson.Value["SpawnTimeRange"]["min"].AsInt, enemyBulkJson.Value["SpawnTimeRange"]["max"].AsInt);


//            //        enemyBulks.Add(new EnemyBulk(enemyType, amountRange, spawnTimeRange));
//            //    }

//            //    var enemiesToSpawn = waveDataProcessor.GenerateCircularSpawnInfo(enemyBulks);

//            //    waves.Add(new WaveData(waveNumber, enemyBulks, enemiesToSpawn));
//            //}

//            return waves;
//        }



//    }
//}