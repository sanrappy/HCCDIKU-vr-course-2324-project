using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLogger : MonoBehaviour
{
    private bool isLogging = false;
    private List<string> rows;

    public string filenameBase;
    public string separator;
    public string[] headerParams;

    public void StartLogging() 
    {
        if (isLogging == false) { 
            isLogging = true;
            rows = new List<string>();
        }
    }

    public void StopLogging() 
    {
        if (isLogging == true) {
            isLogging = false;
            string filePath = Application.persistentDataPath + "/" + filenameBase + System.DateTime.Now.ToString("ddmmyyyy-HHmmss") + ".csv";
            print(filePath);
            string header = $"userId{separator}trialId{separator}timestamp{separator}eventName{separator}{string.Join(separator, headerParams)}";
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                outputFile.WriteLine(header);
                foreach (string row in rows)
                {
                    outputFile.WriteLine(row);
                }
            }
        }
    }

    public void Log(int userId, int trialId, long timestamp, string eventName, params string[] items) 
    {
        if (isLogging == true) 
        {
            string logMessage = $"{userId}{separator}{trialId}{separator}{timestamp}{separator}{eventName}{separator}{string.Join(separator, items)}";
            this.rows.Add(logMessage);
        }
    }
}
