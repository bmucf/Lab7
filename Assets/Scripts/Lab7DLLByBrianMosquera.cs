using UnityEngine;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class Lab7DLLByBrianMosquera : MonoBehaviour
{
    // The imported function
    [DllImport("Lab7DLLByBrianMosquera", EntryPoint = "TestSort")]
    public static extern void TestSort(int[] a, int length);

    public int[] numbers = new int[10000];
    public int randomNumber;
    public int currentElement = 0;

    void Start()
    {

        MeasureFunctionExecutionTime();

        foreach (int number in numbers)
        {
            randomNumber = Random.Range(100000, 100000000);
            numbers.SetValue(randomNumber, currentElement);
            currentElement++;
        }

        TestSort(numbers, numbers.Length);
    }

    void MeasureFunctionExecutionTime()
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start(); // Start timing
        MyFunctionToTest(); // Call the function you want to measure
        stopwatch.Stop();  // Stop timing

        // Get the elapsed time
        long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        UnityEngine.Debug.Log($"MyFunctionToTest took {elapsedMilliseconds} ms to execute.");
    }
    void MyFunctionToTest()
    {
        TestSort(numbers, numbers.Length); //Make sure you have the code that imports your native DLL and populates the array somewhere
    }
}
