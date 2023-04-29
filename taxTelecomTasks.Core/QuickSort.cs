using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taxTelecomTasks.Core
{
    public static class QuickSort
    {
        // Метод для обмена элементов массива
        static void Swap(ref char x, ref char y)
        {
            (x, y) = (y, x);
        }

        // Метод возвращающий индекс опорного элемента
        static int Partition(char[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        // Быстрая сортировка
        private static char[] Sort(char[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            Sort(array, minIndex, pivotIndex - 1);
            Sort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        public static string SortString(string array)
        {
            return new string(Sort(array.ToCharArray(), 0, array.Length - 1));
        }
    }
}
