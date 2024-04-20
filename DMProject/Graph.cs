using System;
using System.Collections.Generic;

public class Graph
{
    private int[,] adjacencyMatrix;
    private List<List<(int, int)>> adjacencyList;
    private Random random = new Random();

    public Graph(int size)
    {
        adjacencyMatrix = new int[size, size];
        adjacencyList = new List<List<(int, int)>>(size);
        for (int i = 0; i < size; i++)
        {
            adjacencyList.Add(new List<(int, int)>());
        }
    }

    public void AddEdge(int source, int destination, int weight)
    {
        adjacencyMatrix[source, destination] = weight;
        adjacencyMatrix[destination, source] = weight; // Since the graph is undirected
        adjacencyList[source].Add((destination, weight));
        adjacencyList[destination].Add((source, weight)); // Since the graph is undirected
    }

    public void PrintAdjacencyMatrix()
    {
        Console.WriteLine("Adjacency Matrix:");
        for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
            {
                Console.Write(adjacencyMatrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    public void PrintAdjacencyList()
    {
        Console.WriteLine("Adjacency List:");
        for (int i = 0; i < adjacencyList.Count; i++)
        {
            Console.Write(i + ": ");
            foreach (var neighbor in adjacencyList[i])
            {
                Console.Write($"({neighbor.Item1}, {neighbor.Item2}) ");
            }
            Console.WriteLine();
        }
    }

    public int Size()
    {
        return adjacencyList.Count;
    }

    public int GetWeight(int source, int destination)
    {
        return adjacencyMatrix[source, destination];
    }

    public void GenerateRandomGraph(double density, int minWeight, int maxWeight)
    {
        int n = adjacencyList.Count;
        int maxEdges = n * (n - 1) / 2; // Maximum number of edges in an undirected graph
        int numEdges = (int)Math.Round(density * maxEdges);

        while (numEdges > 0)
        {
            int source = random.Next(0, n);
            int destination = random.Next(0, n);
            if (source != destination && adjacencyMatrix[source, destination] == 0)
            {
                int weight = random.Next(minWeight, maxWeight + 1);
                AddEdge(source, destination, weight);
                numEdges--;
            }
        }
    }
}

