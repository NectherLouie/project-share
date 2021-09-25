using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mb
{
    public class Utils : MonoBehaviour
    {
        public static int RandomRange(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static float RandomRange(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static T GetRandomWeightedValue<T>(T[] values, float[] weights)
        {
            T output = values[0];
            float totalWeight = 0;

            for (int i = 0; i < weights.Length; ++i)
            {
                totalWeight += weights[i];
            }

            float randomWeight = UnityEngine.Random.Range(1f, totalWeight);

            totalWeight = 0;
            for (int i = 0; i < weights.Length; ++i)
            {
                totalWeight += weights[i];
                if (randomWeight < totalWeight)
                {
                    output = values[i];
                    break;
                }
            }

            return output;
        }

        public static T Slice<T>(List<T> pList, int index)
        {
            T result = pList[index];

            pList.RemoveAt(index);

            return result;
        }

        public static void ThrowError(string message)
        {
            Debug.LogError(message);

            throw new System.Exception(message);
        }

        public static IEnumerator Wait(float pWaitTime, Action pCallback)
        {
            yield return new WaitForSeconds(pWaitTime);
            pCallback?.Invoke();
        }

        public static List<T> Shuffle<T>(List<T> pList)
        {
            for (var i = 0; i < pList.Count - 1; ++i)
            {
                var randomIndex = UnityEngine.Random.Range(i, pList.Count);

                var temp = pList[i];
                pList[i] = pList[randomIndex];
                pList[randomIndex] = temp;
            }

            return pList;
        }

        public static bool CompareTypeArray<T>(T value, T[] args)
        {
            bool result = false;

            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i].Equals(value))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}
