using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeBFS
{
    public int x;
    public int y;

    public NodeBFS Parent;

    public float g_Cost;
    public float fTerrainCost;
    public bool bWalkable; //Para saber si se puede caminar sobre el nodo

    public NodeBFS(int in_x, int in_y)
    {
        this.x = in_x;
        this.y = in_y;
        this.Parent = null;
        this.g_Cost = int.MaxValue;
        this.fTerrainCost = 1;
        this.bWalkable = true;
    }

    public override string ToString()
    {
        return x.ToString() + " , " + y.ToString();
    }
}
public class Grafo
{
    public List<NodeBFS> Nodes;
}
public class ClassBFS
{
    public int iHeight;
    public int iWidth;

    //Dibujar nuestro grid
    private float fTileSize;
    private Vector3 v3OriginPosition;

    public NodeBFS[,] Nodes;
    public TextMesh[,] debugTextArray;

    public bool bShowDebug = true;
    public GameObject debugGO = null;

    public ClassBFS(int in_height, int in_width, float in_fTileSize = 10.0f, Vector3 in_v3OriginPosition = default)
    {
        iHeight = in_height;
        iWidth = in_width;

        InitGrid();
        this.fTileSize = in_fTileSize;
        this.v3OriginPosition = in_v3OriginPosition;

        if (bShowDebug)
        {
            debugGO = new GameObject("GridDebugParent");
            debugTextArray = new TextMesh[iHeight, iWidth];
            for (int y = 0; y < iHeight; y++)
            {
                for (int x = 0; x < iWidth; x++)
                {
                    debugTextArray[y, x] = CreateWorldText(Nodes[y, x].ToString(),
                    debugGO.transform, GetWorldPosition(x, y) + new Vector3(fTileSize * 0.5f, fTileSize * 0.5f),
                    30, Color.white, TextAnchor.MiddleCenter);

                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

                }
            }
            Debug.DrawLine(GetWorldPosition(0, iHeight), GetWorldPosition(iWidth, iHeight), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(iWidth, 0), GetWorldPosition(iWidth, iHeight), Color.white, 100f);
        }
    }
    public void InitGrid()
    {
        Nodes = new NodeBFS[iHeight, iWidth];

        for (int y = 0; y < iHeight; y++)
        {
            for (int x = 0; x < iWidth; x++)
            {
                Nodes[y, x] = new NodeBFS(x, y);
            }
        }
    }

    public NodeBFS GetNode(int x, int y)
    {
        //Checamos si las coordenadas dadas son validas dentro de nuestra cuadricula
        if (x < iWidth && x >= 0 && y < iHeight && y >= 0)
        {
            return Nodes[x, y];
        }
        //Debug.LogError("Invalid coordinates in GetNode");
        return null;
    }

    public List<NodeBFS> GetNeighbors(NodeBFS in_currentNode)
    {
        List<NodeBFS> out_Neighbors = new List<NodeBFS>();
        //visitamos al nodo de arriba
        int x = in_currentNode.x;
        int y = in_currentNode.y;
        if (GetNode(y + 1, x) != null)
        {
            out_Neighbors.Add(Nodes[y + 1, x]);
        }
        if (GetNode(y, x - 1) != null)
        {
            out_Neighbors.Add(Nodes[y, x - 1]);
        }
        // Checamos a la derecha
        if (GetNode(y, x + 1) != null)
        {
            out_Neighbors.Add(Nodes[y, x + 1]);
        }
        if (GetNode(y - 1, x) != null)
        {
            out_Neighbors.Add(Nodes[y - 1, x]);
        }


        return out_Neighbors;
    }

    public List<NodeBFS> Backtrack(NodeBFS in_node)
    {
        List<NodeBFS> out_Path = new List<NodeBFS>();
        NodeBFS current = in_node;
        while (current.Parent != null)
        {
            out_Path.Add(current);
            current = current.Parent;
        }
        out_Path.Add(current);
        out_Path.Reverse();

        return out_Path;
    }

    public void EnumeratePath(List<NodeBFS> in_path)
    {
        int iCounter = 0;

        foreach (NodeBFS n in in_path)
        {
            iCounter++;
            debugTextArray[n.y, n.x].text = n.ToString() +
                 Environment.NewLine + "Step: " + iCounter.ToString();
        }
    }
    public static TextMesh CreateWorldText(string in_text, Transform in_parent = null,
        Vector3 in_localPosition = default, int in_iFontSize = 32, Color in_color = default,
        TextAnchor in_textAnchor = TextAnchor.UpperLeft, TextAlignment in_textAlignment = TextAlignment.Left)
    {
        //if (in_color == null)
        //{
        //    in_color = Color.white;
        //}
        GameObject MyObject = new GameObject(in_text, typeof(TextMesh));
        MyObject.transform.parent = in_parent;
        MyObject.transform.localPosition = in_localPosition;

        TextMesh myTM = MyObject.GetComponent<TextMesh>();
        myTM.text = in_text;
        myTM.anchor = in_textAnchor;
        myTM.alignment = in_textAlignment;
        myTM.fontSize = in_iFontSize;
        myTM.color = in_color;


        return myTM;
    }


    public Vector3 GetWorldPosition(int x, int y)
    {
        //Nos regresa la posicion en mundo del tile/cuadro especificado por x y y
        //Por eso lo multiplicamos por el ftilesize
        //Dado que tienen lo mismo de alto y ancho por cuadro
        return new Vector3(x, y) * fTileSize + v3OriginPosition;
    }
    //Euclidiana (hasta el momento)
    public int GetDistance(NodeBFS in_a, NodeBFS in_b)
    {
        int x_diff = (in_a.x - in_b.x);
        int y_diff = (in_b.y - in_a.y);
        return (int)Mathf.Sqrt(Mathf.Pow(x_diff, 2) + Mathf.Pow(y_diff, 2)); //Usamos la formula general para sacar la distancia
    }

    public List<NodeBFS> BreadthFirstSearch(int in_startX, int in_startY, int in_endX, int in_endY)
    {

        NodeBFS StartNode = GetNode(in_startY, in_startX);
        NodeBFS EndNode = GetNode(in_endY, in_endX);

        if (StartNode == null || EndNode == null)
        {
            //Mensaje de error
            Debug.LogError("Invalid coordinates in DeepthFirstSearch");
            return null;
        }

        Queue<NodeBFS> OpenList = new Queue<NodeBFS>();
        List<NodeBFS> ClosedList = new List<NodeBFS>();

        OpenList.Enqueue(StartNode);

        //Prioridad
        int iP = 0;

        while (OpenList.Count > 0)
        {
            //Si hay nodos en la lista abierta,buscamos un camino.
            //obtenemos el primer nodo de la lista abierta
            NodeBFS currentNode = OpenList.Dequeue();
            Debug.Log("Current Node is: " + currentNode.x + ", " + currentNode.y);

            //Revisamos si llega al destino
            if (currentNode == EndNode)
            {
                //Enncuetra el camino
                Debug.Log("Camino encontrado");

                //Creamos el camino con backtracking
                List<NodeBFS> path = Backtrack(currentNode);
                EnumeratePath(path);

                return path;
            }

            //Otra posible solución, con caminos pequeños
            if (ClosedList.Contains(currentNode))
            {
                continue;
            }

            ClosedList.Add(currentNode);

            //Visitamos los vecinos de la derecha y arriba
            List<NodeBFS> currentNeighbors = GetNeighbors(currentNode);

            foreach (NodeBFS neighbor in currentNeighbors)
            {
                if (ClosedList.Contains(neighbor))
                    continue;

                //Si no lo contiene, entonces lo agregamos a la lista Abierta
                neighbor.Parent = currentNode;

                //Lo mandamos a llamar para cada vecino
                OpenList.Enqueue(neighbor);
                //Ajustamos la prioridad
                iP++;
            }

            string RemainingNodes = "Nodes in open list are: ";
            foreach (NodeBFS n in OpenList)
                RemainingNodes += "(" + n.x + ", " + n.y + ") - ";
            Debug.Log(RemainingNodes);
        }

        Debug.LogError("No path found between start and end.");

        return null;
    }
}
