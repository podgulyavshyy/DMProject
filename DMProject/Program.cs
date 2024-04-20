using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        Stopwatch stopwatch = new Stopwatch();

        for (int i = 0; i < 10; i++)
        {
            int size = random.Next(3, 4); // Random size between 20 and 200
            Console.WriteLine($"Graph {i + 1}: Size = {size}");

            int density = 1;
            string dens_str = "0," + density.ToString();
            double new_dens = Double.Parse(dens_str);
            Console.WriteLine("Density " + new_dens);
            Graph graph = new Graph(size);
            graph.GenerateRandomGraph(new_dens, 5, 10); // Density = 0.5, minWeight = 1, maxWeight = 10

            TSPSolver solver = new TSPSolver(graph);

            stopwatch.Restart();
            var bestTour = solver.ApproximateTSP();
            stopwatch.Stop();

            Console.WriteLine($"Time Taken = {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine("Best Tour:");
            foreach (var vertex in bestTour)
            {
                Console.Write(vertex + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}