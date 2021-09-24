using Backtrace.Unity;
using Backtrace.Unity.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
public class StartupScript : MonoBehaviour
{
    public AudioSource audioSource;
    private int[] arr = { };

    // Backtrace client instance
    public BacktraceClient _backtraceClient;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            try
            {
                // throw an exception here
                //int n = arr[0];
                //n++;
                throw new Exception();
            }
            catch (Exception exception)
            {
                BacktraceReport report = new BacktraceReport(exception);
                _backtraceClient.Send(report);
            }
        }
    }
}