using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

// make sure to name your C# file the same as your class name. In this case Move.cs 
public class Move : MonoBehaviour
{
    // change your serial port
    SerialPort sp = new SerialPort("COM3", 9600);
    public GameObject[] objectsToMove;
    public float xAngle, yAngle, zAngle;

    // Start is called before the first frame update
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 100; // In my case, 100 was a good amount to allow quite smooth transition. 
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                var line = sp.ReadLine();
                Debug.Log(line);
                // When left button is pushed
                if (line == "left")
                {
                    for (int i = 0; i < objectsToMove.Length; i++)
                    {
                        objectsToMove[i].transform.Translate(Vector3.left * Time.deltaTime * 5);
                    }
                }
                // When right button is pushed
                if (line == "right")
                {
                    for (int i = 0; i < objectsToMove.Length; i++)
                    {
                        objectsToMove[i].transform.Translate(Vector3.right * Time.deltaTime * 5);
                    }
                }
                else if (line == "rotate")
                {
                    for (int i = 0; i < objectsToMove.Length; i++)
                    {

                        objectsToMove[i].transform.Rotate(90, 5 * Time.deltaTime, 0);
                        
                    }
                }
            }
            catch (System.Exception)
            {

            }

        }
    }
    private void OnApplicationQuit()
    {
        sp.Close();
    }
}
