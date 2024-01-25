using System;
using PsychoUnity.Manager;
using UnityEngine;

namespace Samples.HighResolutionTimer
{
    public class Logic : MonoBehaviour
    {
        private void Start()
        {
            TimerManager.Instance.SetSchedule("001", 2000, 0, 5, TaskOne);
            TimerManager.Instance.AddTask("001", TaskTwo);
            TimerManager.Instance.Start("001");
        }
        private static void TaskOne()
        {
            print("TaskOne");
        }

        private static void TaskTwo()
        {
            print("TaskTwo");
        }

        private void OnDestroy()
        {
            TimerManager.Instance.Stop("001");
        }
    }
}
