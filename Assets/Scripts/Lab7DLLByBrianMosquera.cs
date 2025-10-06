using UnityEngine;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class Lab7DLLBenchmark : MonoBehaviour
{
    // Native DLL import
    [DllImport("Lab7DLLByBrianMosquera", EntryPoint = "TestSort")]
    public static extern void TestSort(int[] a, int length);

    public int[] originalArray;
    public int arraySize = 10000;
    public int trials = 10;

    void Start()
    {
        // Generate base array
        originalArray = GenerateRandomArray(arraySize);

        // Run benchmark
        BenchmarkSorts(originalArray, trials);
    }

    // Generate random array
    int[] GenerateRandomArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = Random.Range(100000, 100000000);
        }
        return array;
    }

    // Managed sort using built-in Array.Sort
    void ManagedSort(int[] array)
    {
        System.Array.Sort(array);
    }

    // Benchmark both sorts
    void BenchmarkSorts(int[] baseArray, int trialCount)
    {
        Stopwatch stopwatch = new Stopwatch();
        long managedTotal = 0;
        long nativeTotal = 0;

        for (int t = 0; t < trialCount; t++)
        {
            int[] managedArray = (int[])baseArray.Clone();
            int[] nativeArray = (int[])baseArray.Clone();

            // Managed
            stopwatch.Restart();
            ManagedSort(managedArray);
            stopwatch.Stop();
            managedTotal += stopwatch.ElapsedMilliseconds;

            // Native
            stopwatch.Restart();
            TestSort(nativeArray, nativeArray.Length);
            stopwatch.Stop();
            nativeTotal += stopwatch.ElapsedMilliseconds;
        }

        float managedAvg = managedTotal / (float)trialCount;
        float nativeAvg = nativeTotal / (float)trialCount;

        UnityEngine.Debug.Log("Managed sort average over " + trialCount + " trials: " + managedAvg + " ms");
        UnityEngine.Debug.Log("Native sort average over " + trialCount + " trials: " + nativeAvg + " ms");

        ComparePerformance(managedAvg, nativeAvg);
    }

    // Compare and comment
    void ComparePerformance(float managed, float native)
    {
        if (native < managed)
        {
            UnityEngine.Debug.Log("Native sort is faster by " + (managed - native) + " ms on average.");
        }
        else if (managed < native)
        {
            UnityEngine.Debug.Log("Managed sort is faster by " + (native - managed) + " ms on average.");
        }
        else
        {
            UnityEngine.Debug.Log("Both sorts performed equally on average.");
        }
    }
}