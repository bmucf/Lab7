using UnityEngine;

public class ArrayOfNumbers : MonoBehaviour
{
    public int[] numbers = new int[10000];
    public int randomNumber;
    public int currentElement = 0;

    private void Start()
    {
        foreach (int number in numbers)
        {
            randomNumber = Random.Range(100000, 100000000);
            numbers.SetValue(randomNumber, currentElement);
            currentElement++;
        }
    }
}
