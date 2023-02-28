using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Llamamos a nuestro BreadthFirstSearch y le decimos su punto de inicio y final
        ClassBFS myTest = new ClassBFS(5, 5);
        myTest.BreadthFirstSearch(0, 0, 4, 4);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
