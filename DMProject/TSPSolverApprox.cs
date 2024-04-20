using System;
using System.Collections.Generic;

public class TSPSolver
{
    private Graph graph;

    public TSPSolver(Graph graph)
    {
        this.graph = graph;
    }

    public List<int> ApproximateTSP()
    {
        int numVertices = graph.Size();
        bool[] visited = new bool[numVertices];
        List<int> tour = new List<int>();

        // Start with the first vertex
        int currentVertex = 0;
        tour.Add(currentVertex);
        visited[currentVertex] = true;

        // Repeat until all vertices are visited
        while (tour.Count < numVertices)
        {
            int nearestVertex = FindNearestNeighbor(currentVertex, visited);
            tour.Add(nearestVertex);
            visited[nearestVertex] = true;
            currentVertex = nearestVertex;
        }

        // Complete the tour by connecting back to the starting vertex
        tour.Add(0);

        return tour;
    }

    private int FindNearestNeighbor(int vertex, bool[] visited)
    {
        int nearestVertex = -1;
        int minDistance = int.MaxValue;

        for (int i = 0; i < graph.Size(); i++)
        {
            if (!visited[i] && i != vertex)
            {
                int weight = graph.GetWeight(vertex, i);
                if (weight < minDistance)
                {
                    minDistance = weight;
                    nearestVertex = i;
                }
            }
        }

        return nearestVertex;
    }
}