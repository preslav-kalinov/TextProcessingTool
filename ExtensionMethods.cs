using System.Collections.Generic;

namespace WordProcessingApplication
{
    public static class ExtensionMethods
    {
        public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
    }
}
