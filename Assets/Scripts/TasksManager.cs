using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksManager : MonoBehaviour
{
    // static reference to task manager so can be called from other scripts directly (not just through gameobject component)
    public static TasksManager tm;
    public GameObject[] tasks;
    // Start is called before the first frame update
    void Awake()
    {
        // setup reference to game manager
        if (tm == null)
            tm = this.GetComponent<TasksManager>();
    }

    public void TaskCompleted(int index)
    {
        tasks[index].GetComponent<Text>().color = new Color(0f, 0.5f, 0f);
    }
}
