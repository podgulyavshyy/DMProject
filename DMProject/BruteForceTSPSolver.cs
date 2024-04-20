public class BruteForceTSPSolver
{
    private Graph graph;
    private int[] bestTour;
    private int bestCost;

    public BruteForceTSPSolver(Graph graph)
    {
        this.graph = graph;
        bestTour = new int[graph.Size()];
        bestCost = int.MaxValue;
    }

    public int[] Solve()
    {
        int[] tour = new int[graph.Size()];
        for (int i = 0; i < tour.Length; i++)
        {
            tour[i] = i; // Initialize tour with vertices in order
        }

        Permute(tour, 0, tour.Length - 1);

        return bestTour;
    }

    private void Permute(int[] tour, int left, int right)
    {
        if (left == right)
        {
            int currentCost = CalculateTourCost(tour);
            if (currentCost < bestCost)
            {
                bestCost = currentCost;
                Array.Copy(tour, bestTour, tour.Length);
            }
        }
        else
        {
            for (int i = left; i <= right; i++)
            {
                Swap(tour, left, i);
                Permute(tour, left + 1, right);
                Swap(tour, left, i); // Backtrack
            }
        }
    }

    private void Swap(int[] tour, int i, int j)
    {
        int temp = tour[i];
        tour[i] = tour[j];
        tour[j] = temp;
    }

    private int CalculateTourCost(int[] tour)
    {
        int cost = 0;
        for (int i = 0; i < tour.Length - 1; i++)
        {
            cost += graph.GetWeight(tour[i], tour[i + 1]);
        }
        cost += graph.GetWeight(tour[tour.Length - 1], tour[0]); // Return to starting vertex
        return cost;
    }
}