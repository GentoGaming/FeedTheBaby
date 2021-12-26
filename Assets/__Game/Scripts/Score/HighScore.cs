using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class HighScore : MonoBehaviour
{
	[Serializable]
	public struct HighScoreStruct
	{
		public int score;
		public string name;
	}
	
	public int amountOfScoreToShow = 5;
	
	public List<HighScoreStruct> highScores;
	public List<HighScoreStruct> defaultScores;

	private void Awake()
	{
		LoadFile();
	}

	public void AddScore(int score, string scoreName)
	{
		var newScore = new HighScoreStruct{score = score, name = scoreName};
		highScores.Add(newScore);

		var newArray = highScores.ToArray();
		if (newArray.Length > 1){
			for (int i = 0; i < newArray.Length; i++)
			{
				for (int j = 0; j < newArray.Length; j++)
				{
					if(!(newArray[j].score < newArray[i].score)) continue;
					var temp = newArray[i];
					newArray[i] = newArray[j];
					newArray[j] = temp;
				}
			}
		}
		
		highScores = new List<HighScoreStruct>();
		if (newArray.Length >= amountOfScoreToShow)
		{
			for (int i = 0; i < amountOfScoreToShow; i++)
			{
				highScores.Add(newArray[i]);
			}
		}
		else
		{
			highScores = new List<HighScoreStruct>();
			foreach (var item in newArray)
			{
				highScores.Add(item);
			}
		}
		
		SaveFile();
	}
	
	public void SaveFile()
	{
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if (File.Exists(destination)) file = File.OpenWrite(destination);
		else file = File.Create(destination);

		int[] scores = new int[amountOfScoreToShow];
		string[] names = new string[amountOfScoreToShow];
		for (int i = 0; i < highScores.Count; i++){
			scores[i] = highScores[i].score;
			names[i] = highScores[i].name;
		}
		GameData data = new GameData(scores, names);

		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, data);
		file.Close();
	}

	public void LoadFile()
	{
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if(File.Exists(destination)) file = File.OpenRead(destination);
		else
		{
			Debug.Log("Save file not found");
			file = File.Create(destination);
			
			int[] scores = new int[amountOfScoreToShow];
			string[] names = new string[amountOfScoreToShow];
			
			for (int i = 0; i < amountOfScoreToShow; i++){
				scores[i] = defaultScores[i].score;
				names[i] = defaultScores[i].name;
			}
			GameData dataDefault = new GameData(scores, names);
			
			BinaryFormatter bfDefault = new BinaryFormatter();
			bfDefault.Serialize(file, dataDefault);
			file.Close();
			
			for (int i = 0; i < dataDefault.Scores.Length; i++)
			{
				AddScore(dataDefault.Scores[i], dataDefault.Names[i]);
				print($"Added {dataDefault.Scores[i]}, {dataDefault.Names[i]}");
			}
			
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		GameData data = (GameData)bf.Deserialize(file);
		file.Close();

		if(data.Scores.Length <= 0) return;
 		
		for (int i = 0; i < data.Scores.Length; i++)
		{
			AddScore(data.Scores[i], data.Names[i]);
			print($"Added {data.Scores[i]}, {data.Names[i]}");
		}
	}
	
	public void DeleteSaveFile()
	{
		string destination = Application.persistentDataPath + "/save.dat";
        
		if (File.Exists(destination))
		{
			File.Delete(destination);
		}
	}
}

[Serializable]
public class GameData
{
	public int[] Scores;
	public string[] Names;

	public GameData(int[] scores, string[] names)
	{
		Scores = scores;
		Names = names;
	}
}
