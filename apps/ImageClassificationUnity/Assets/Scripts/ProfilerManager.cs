using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ProfilerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Profiler.logFile = Application.persistentDataPath + "/mylog.txt"; 
        Profiler.enableBinaryLog = true;
        Profiler.enabled = true;

        // Optional, if more memory is needed for the buffer
        //Profiler.maxUsedMemory = 256 * 1024 * 1024;

        // ...

        // Optional, to close the file when done
        //Profiler.enabled = false;
        //Profiler.logFile = "";

        // To start writing to a new log file
        //Profiler.logFile = "myOtherLog";
        //Profiler.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
