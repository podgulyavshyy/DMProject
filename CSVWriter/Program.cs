using System;
using System.Diagnostics;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        Stopwatch stopwatch = new Stopwatch();
        StringBuilder csvContent = new StringBuilder();

        // Header for CSV file
        csvContent.AppendLine("attempt,time,density,size");
        
        for (double density = 0.05; density <= 1.0; density += 0.4)
        {
            for (int attempt = 1; attempt <= 2000; attempt++)
            {
                int size = random.Next(500, 501); // Random size between 20 and 200
                Console.WriteLine($"Density: {density * 100:F2}%, Attempt: {attempt}, Size: {size}");

                Graph graph = new Graph(size);
                graph.GenerateRandomGraph(density, 10, 20); // Density, MinWeight = 10, MaxWeight = 20

                TSPSolver solver = new TSPSolver(graph);

                stopwatch.Restart();
                var bestTour = solver.ApproximateTSP();
                stopwatch.Stop();

                int timeTakenInSeconds = (int)(stopwatch.Elapsed.TotalMilliseconds*1000);
                int dens = (int)(density * 100);
                // Append data to CSV content
                csvContent.AppendLine($"{attempt},{timeTakenInSeconds},{dens}%,{size}");
            }
        }

        // Write CSV content to file
        File.WriteAllText("TSP_results.csv", csvContent.ToString());

        Console.WriteLine("CSV file generated successfully.");
    }
}