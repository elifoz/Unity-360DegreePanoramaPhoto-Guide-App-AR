using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;
using System.IO;

public class Database : MonoBehaviour
{
    public static Database instance;
    private string dbName;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
         CreateDB();
        //DeletePhoto();
        ListPhoto();
    }

    public void CreateDB()
    {
        if (Application.platform != RuntimePlatform.Android)
        {

            dbName = "URI=file:PhotoAR.db";
        }

        else
        {
            string filepath = Application.persistentDataPath + "/" + "PhotoAR1.db";

            if (!File.Exists(filepath))
            {
                // If not found on android will create Tables and database

                // UNITY_ANDROID
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/PhotoAR2.db");
                while (!loadDB.isDone) { }
                // then save to Application.persistentDataPath
                File.WriteAllBytes(filepath, loadDB.bytes);

            }

            dbName = "URI=file:" + filepath;
        }

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS  photoInfo (photoPath VARCHAR(150), objectName VARCHAR(30)); ";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        Debug.Log("createdelif");
    }


    public void AddPhoto()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                
                command.CommandText = "INSERT INTO photoInfo (photoPath,objectName) VALUES ('"+ PhotoCapture.instance.path + "' , '" + PhotoCapture.instance.objectName.text + "');";
                Debug.Log("OLUÞTU");
                //command.ExecuteNonQuery();
                command.ExecuteScalar();

            }

            connection.Close();
        }

        ListPhoto();
    }

    public void ListPhoto()
    {
      
        foreach (Transform child in PhotoManager.instance.content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }



        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM photoInfo;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())

                        //write objects in a scrollview
                        //  a.text += reader["objectName"] + "\t\t"+  reader["photoPath"] + "\n";
                      // (string)reader["objectName"];
                       // photoPath = (string)reader["photoPath"];

                    CreateButton((string)reader["objectName"], (string)reader["photoPath"]);
                   
                    reader.Close();
                }
            }

            connection.Close();
            Debug.Log("listeledi");
        }

    }

    private void CreateButton(string objName,string path)
    {
        PhotoButtonController newButton = Instantiate(PhotoManager.instance.PhotoButton, PhotoManager.instance.PhotoListPanel).GetComponent<PhotoButtonController>();
        newButton.transform.SetParent(PhotoManager.instance.PhotoListPanel);
      //  newButton.Initialized(objName, path);
        Debug.Log("entered");
    }

    public void DeletePhoto()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE  FROM photoInfo;";

                command.ExecuteNonQuery();
                command.Dispose();
            }

            connection.Close();
            Debug.Log("sildi");
        }

    }
}


