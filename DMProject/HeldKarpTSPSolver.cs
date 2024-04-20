using System;
using System.Collections.Generic;

public class HeldKarpTSPSolver
{
    private Graph graph;
    private Dictionary<(int, int), int> memo;

    public HeldKarpTSPSolver(Graph graph)
    {
        this.graph = graph;
        memo = new Dictionary<(int, int), int>();
    }

    public int[] Solve()
    {
        int n = graph.Size();
        int finalState = (1 << n) - 1;

        return SolveDP(0, 1, finalState);
    }

    private int[] SolveDP(int node, int mask, int finalState)
    {
        if (mask == finalState)
        {
            return new int[] { node, graph.GetWeight(node, 0) }; // Return to starting vertex
        }

        if (memo.ContainsKey((node, mask)))
        {
            return DecodePath(memo[(node, mask)]);
        }

        int[] minPath = new int[] { -1, int.MaxValue };

        for (int nextNode = 0; nextNode < graph.Size(); nextNode++)
        {
            if ((mask & (1 << nextNode)) == 0)
            {
                int[] path = SolveDP(nextNode, mask | (1 << nextNode), finalState);
                int cost = path[1] + graph.GetWeight(node, nextNode);
                if (cost < minPath[1])
                {
                    minPath[0] = nextNode;
                    minPath[1] = cost;
                }
            }
        }

        memo[(node, mask)] = EncodePath(minPath);

        return minPath;
    }

    private int[] DecodePath(int encodedPath)
    {
        return new int[] { encodedPath >> 12, encodedPath & 0xFFF };
    }

    private int EncodePath(int[] path)
    {
        return (path[0] << 12) | path[1];
    }
}