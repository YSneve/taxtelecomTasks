using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taxTelecomTasks.Core
{
    public static class TreeSort
    {
        public class TreeNode
        {
            public TreeNode(char data)
            {
                Data = data;
            }

            // Данные
            public char Data { get; set; }

            // Левая ветка дерева
            public TreeNode Left { get; set; }

            // Правая ветка дерева
            public TreeNode Right { get; set; }

            // Рекурсивное добавление узла в дерево
            public void Insert(TreeNode node)
            {
                if (node.Data < Data)
                {
                    if (Left == null)
                    {
                        Left = node;
                    }
                    else
                    {
                        Left.Insert(node);
                    }
                }
                else
                {
                    if (Right == null)
                    {
                        Right = node;
                    }
                    else
                    {
                        Right.Insert(node);
                    }
                }
            }

            // Преобразование дерева в отсортированный массив
            public char[] Transform(List<char> elements = null)
            {
                if (elements == null)
                {
                    elements = new List<char>();
                }

                if (Left != null)
                {
                    Left.Transform(elements);
                }

                elements.Add(Data);

                if (Right != null)
                {
                    Right.Transform(elements);
                }

                return elements.ToArray();
            }
        }
        // Метод для сортировки с помощью двоичного дерева
        public static string SortString(string array)
        {
            var treeNode = new TreeNode(array[0]);

            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode(array[i]));
            }

            return new string(treeNode.Transform());
        }
    }
}
