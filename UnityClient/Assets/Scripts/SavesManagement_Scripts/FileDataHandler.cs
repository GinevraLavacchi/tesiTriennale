using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Rendering;

public class FileDataHandler
{
    private string directoryPath = "";
    private string fileName = "";

    public FileDataHandler(string directoryPath, string fileName)
    {
        this.directoryPath = directoryPath;
        this.fileName = fileName;
    }
    public GameData Load(string profile)
    {
        string fullPath = Path.Combine(directoryPath, profile, fileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            //try
            {
                //Load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //the data needs to be deserialized from json bakc into the c object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            } 
            /*catch (Exception e)
            {
                Debug.LogError("Error occured while trying to save data to file: " + fullPath + e);
            }*/
        }
        return loadedData;
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();
        //loop over alll directory names in the data directory path
        IEnumerable<DirectoryInfo> diretoryInfos = new DirectoryInfo(directoryPath).EnumerateDirectories();
        foreach(DirectoryInfo info in diretoryInfos)
        {
            string profile = info.Name;

            //check if th edata file exists 
            //if it does not, then this folder isn't a profile and should be skipped 
            string fullPath = Path.Combine(directoryPath, profile, fileName);
            if(!File.Exists(fullPath))
            {
                continue;
            }

            //load the game data for this profile and put it in the dictionary
            GameData profileData = Load(profile);
            //ensure the profile data isnt null, if it is something went veryyyyy wrong
            if(profileData != null)
            {
                profileDictionary.Add(profile, profileData);
            } else
            {
                Debug.LogError("Tried to load profile but something went veryyy wrong. Profile ID: "+ profile);
            }
        }
        return profileDictionary;
    }
    public void Save(GameData data, string profile)
    {
        string fullPath = Path.Combine(directoryPath, profile, fileName); //this could be done by dirPath + "/" + filename, but it would be tied to the operating system
        //try
        {
            //Create the directory path in case it doesnt exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize the C game objet into Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //Write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) //using block ensure that the connection to the file is closed once we are done reading or writing to it
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        } 
        /*catch (Exception e)
        {
            Debug.LogError("Error occured while trying to save data to file: "+ fullPath + e);
        }*/
    }
}
