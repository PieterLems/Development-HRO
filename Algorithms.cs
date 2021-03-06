﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tester;

namespace Csharp_Algorithms {

    public class Algorithms {

        //SEARCHING ALGROTIHMS
        public int SequentialSearch (int[] array) {
            System.Console.WriteLine ("Please fill in the number you want to find, it will return -1 if number is not found");
            int input = Convert.ToInt32 (Console.ReadLine ());
            for (int i = 0; i < array.Length; i++) {
                if (array[i] == input) {
                    return array[i];
                }
            }
            return -1;
        }

        public int BinarySearch (int[] array) {
            System.Console.WriteLine ("Please fill in the number you want to find, it will return -1 if number is not found");
            int input = Convert.ToInt32 (Console.ReadLine ());
            var low = 0;
            var high = array.Length - 1;

            while (low <= high) {
                var middle = (low + high) / 2;
                if (input < array[middle]) {
                    high = middle - 1;
                } else if (input > array[middle]) {
                    low = middle + 1;
                } else {
                    return middle;
                }
            }
            return -1;
        }

        //SORTING ALGORITHMS
        public int[] InsertionSort (int[] array) {
            for (int i = 0; i < array.Length; i++) {
                var key = array[i];
                var j = i - 1;

                while (j >= 0 && array[j] > key) {
                    array[j + 1] = array[j]; // adding the number to the next place in the list
                    j = j - 1; // going to the next element
                }
                array[j + 1] = key;
            }
            return array;
        }

        public int[] BubbleSort (int[] array) {
            var somethingChanged = true;
            while (somethingChanged) {
                somethingChanged = false;
                for (int i = 0; i < array.Length - 1; i++) {
                    if (array[i].CompareTo (array[i + 1]) > 0) {
                        var temp = array[i + 1];
                        array[i + 1] = array[i];
                        array[i] = temp;

                        somethingChanged = true;
                    }
                }
            }
            return array;
        }

        static void Merge (int[] arr, int left, int middle, int right) {
            int i, j, k;
            int[] arr1 = new int[middle - left + 1];
            int[] arr2 = new int[right - middle];
            for (i = 0, k = left; i < arr1.Length; i++, k++) {
                //TODO: EX 3.1 arr1[i] = ?;
                arr1[i] = arr[k];
            }
            for (j = 0, k = middle + 1; j < arr2.Length; j++, k++) {
                //TODO: EX 3.2 arr2[j] = ?;
                arr2[j] = arr[k];
            }
            i = j = 0;
            k = left;
            while (i < arr1.Length && j < arr2.Length) {
                if (arr1[i].CompareTo (arr2[j]) <= 0)
                    arr[k++] = arr1[i++];
                else
                    arr[k++] = arr2[j++];
            }
            if (i == arr1.Length) {

                i = i + 1;
            } else {
                //TODO: EX 3.4

                j = j + 1;

            }
        }

        public int[] MergeSort (int[] arr, int leftBoundary, int rightBoundary) {
            if (leftBoundary < rightBoundary) {
                int middle = (leftBoundary + rightBoundary) / 2;

                MergeSort (arr, leftBoundary, middle);
                MergeSort (arr, middle + 1, rightBoundary);
                return Sorting<int>.Merge (arr, leftBoundary, middle, rightBoundary);

            }
            return null;
        }

        //LINKED LIST ALGORITHMS
        public Node<int> SortedListFind (SortedLinkedList<int> list, int value) {
            var curr = list.start;

            while (curr.Value != value) {
                if (curr.Value > value)
                    return null;

                curr = curr.Next;
            }

            return curr;
        }

        public void SortedListAdd (SortedLinkedList<int> list, int value) {
            if (list.start == null || list.start.Value.CompareTo (value) >= 0) {
                list.start = new Node<int> (value, list.start);
                return;
            }

            Node<int> curr = list.start;
            while (curr.Next != null && curr.Next.Value.CompareTo (value) < 0) {
                curr = curr.Next;

            }
            curr.Next = new Node<int> (value, curr.Next);
        }

        public void SortedListDelete (SortedLinkedList<int> list, int value) {
            if (list.start == null || list.start.Value.CompareTo (value) >= 0) {
                list.start = null;
                return;
            }

            Node<int> curr = list.start;
            while (curr.Next != null && curr.Next.Value.CompareTo (value) < 0) {
                curr = curr.Next;

            }
            curr.Next = null;
        }

        static void DoublyLinkedListInsertAfter (DoublyLinkedList<int> list, DoubleNode<int> node, int value) {
            DoubleNode<int> newNode = new DoubleNode<int> (value, node, node.Next);
            node.Next = newNode;

            if (list.Last == node) {
                node.Next = new DoubleNode<int> (value, node, null);
            } else {
                node.Next = newNode;
                newNode.Prev.Next = newNode;
            }
        }

        static void DoublyLinkedListRemove (DoublyLinkedList<int> list, DoubleNode<int> node) {

            list.Size--;
            if (node.Prev != null) {
                node.Prev.Next = node.Next;
            }
            if (node.Next != null) {
                node.Next.Prev = node.Prev;
            }
            if (list.First == node)
                list.First = node.Next;
            if (list.Last == node)
                list.Last = node.Prev;
        }

        //HASHTABLE ALGORITHMS
        static int getIndex (string key, int size) {
            int hashCode = Math.Abs (key.GetHashCode ());
            int index = hashCode % size;
            return index;
        }
        public Nullable<int> HashTableFind (HashTable<string, int> table, string key) {
            var arraySize = table.buckets.Length;
            int index = getIndex (key, arraySize);
            var values = table.buckets;
            if (values[index] == null)
                return null;
            else {
                if (values[index].Key.Equals (key)) {
                    return values[index].Value;
                } else {
                    var potentialIndex = index;

                    while (values[potentialIndex] != null) {
                        if (values[potentialIndex].Key.Equals (key)) {
                            return values[potentialIndex].Value;
                        }

                        potentialIndex++;
                        if (potentialIndex >= arraySize)
                            potentialIndex = 0;
                        if (potentialIndex == index)
                            return null;
                    }
                }
            }
            return null;
        }
        public void HashTableAdd (HashTable<string, int> table, string key, int value) {
            var arraySize = table.buckets.Length;
            int index = getIndex (key, arraySize);
            var values = table.buckets;
            if (values[index] == null)
                values[index] = new Entry<string, int> (key, value);
            else {
                if (values[index].Key.Equals (key)) {
                    throw new ArgumentException ("Key already exists");
                } else {
                    var potentialIndex = index;

                    while (values[potentialIndex] != null) {
                        potentialIndex++;
                        if (potentialIndex >= arraySize)
                            potentialIndex = 0;
                        if (potentialIndex == index)
                            return;
                    }
                    values[potentialIndex] = new Entry<string, int> (key, value);
                }
            }
        }

        public void HashTableDelete (HashTable<string, int> table, string key) {
            var arraySize = table.buckets.Length;
            int index = getIndex (key, arraySize);
            var values = table.buckets;
            if (values[index] == null)
                return;
            else {
                if (values[index].Key.Equals (key)) {
                    values[index] = null;
                } else {
                    var potentialIndex = getIndex (key, arraySize);
                    while (values[potentialIndex] != null) {
                        if (values[potentialIndex].Key.Equals (key)) {
                            values[potentialIndex] = null;
                            return;
                        }

                        potentialIndex++;
                        if (potentialIndex >= arraySize)
                            potentialIndex = 0;
                        if (potentialIndex == index)
                            return;
                    }
                }
            }
        }

        //BINARY SEARCH TREE ALGORITHMS
        static TreeNode<int> BSTFind (BSTree<int> tree, int value) {
            return searchStartingFrom (tree.root, value);
        }

        private static TreeNode<int> searchStartingFrom (TreeNode<int> node, int value) {
            if (node == null)
                throw new Exception ("value not found");

            if (node.value.CompareTo (value) == 0)
                return node;

            if (node.value.CompareTo (value) < 0)
                return searchStartingFrom (node.rightChild, value);

            return searchStartingFrom (node.leftChild, value);
        }
        public void BSTAdd (BSTree<int> tree, int value) {
            if (tree.root == null) {
                tree.root = new TreeNode<int> (value, tree.root, null, null);
                return;
            } else
                BSTInsertStartingFrom (tree.root, value);
        }

        static void BSTInsertStartingFrom (TreeNode<int> node, int value) {
            if (node.value.CompareTo (value) < 0) {
                if (node.rightChild == null) {
                    node.rightChild = new TreeNode<int> (value, node, null, null);
                } else {
                    BSTInsertStartingFrom (node.rightChild, value);
                }
            } else if (node.value.CompareTo (value) > 0) {
                if (node.leftChild == null) {
                    node.leftChild = new TreeNode<int> (value, node, null, null);
                } else {
                    BSTInsertStartingFrom (node.leftChild, value);
                }
            }
        }

        public void BSTDelete (BSTree<int> tree, int value) {
            if (tree.root == null) {
                return;
            } else
                BSTDeleteStartingFrom (tree.root, value);
        }

        static void BSTDeleteStartingFrom (TreeNode<int> node, int value) {
            if (node.value.CompareTo (value) < 0) {
                if (node.rightChild == null) {
                    return;
                } else {
                    node.rightChild = null;
                }
            } else if (node.value.CompareTo (value) > 0) {
                if (node.leftChild == null) {
                    return;
                } else {
                    node.leftChild = null;
                }
            }
        }

        //NEEDS TO BE DONE
        public void Stack () {

        }

        public void Enqueue (Queue<int> queue, int element) {
            // if (queue.IsEmpty) {
            //     queue.data[0] = element;
            //     queue.front = queue.back = 0;
            //     queue.Count++;
            // } else if ((queue.back + 1) % queue.data.Length == queue.front) {
            //     throw new QueueException ("The queue is full");
            // } else {
            //     queue.back = (queue.back + 1) % queue.data.Length;
            //     queue.data[queue.back] = element;
            //     queue.front = queue.front % queue.data.Length;
            //     queue.Count++;
            // }
        }

        //ADVANCED ALGORITHMS
        static Tuple<double[, ], int[, ]> FloydWarshall (double[, ] adjacencyMatrix) {
            var numVertices = adjacencyMatrix.GetLength (0);
            double[, ] dist = new double[numVertices, numVertices];
            int[, ] next = new int[numVertices, numVertices];

            for (int i = 0; i < numVertices; i++)
                for (int j = 0; j < numVertices; j++) {
                    if (i == j) {
                        dist[i, j] = 0;
                    } else {
                        dist[i, j] = adjacencyMatrix[i, j];
                    }

                    if (adjacencyMatrix[i, j] != Double.PositiveInfinity) {
                        next[i, j] = i;
                    } else {
                        next[i, j] = -1;
                    }
                }

            for (int k = 0; k < numVertices; k++)
                for (int i = 0; i < numVertices; i++)
                    for (int j = 0; j < numVertices; j++) {
                        if (dist[i, k] + dist[k, j] < dist[i, j]) {
                            dist[i, j] = dist[i, k] + dist[k, j];
                            next[i, j] = next[i, k];
                        }

                    }

            return new Tuple<double[, ], int[, ]> (dist, next);
        }

        static Tuple<double[], int[]> Dijkstra (double[, ] adjacencyMatrix, int source) {
            int Count = adjacencyMatrix.GetLength (0);
            double[] distance = new double[Count];
            int[] prev = new int[Count];
            List<int> vertexSet = new List<int> (Count);

            for (int i = 0; i < Count; i++) {

                distance[i] = double.PositiveInfinity;

                prev[i] = -1;

                vertexSet.Add (source);
            }

            distance[source] = 0;
            while (vertexSet.Count > 0) {
                int firstUnvisited = vertexSet.First ();
                double min = distance[firstUnvisited];
                int minIndex = firstUnvisited;
                foreach (var v in vertexSet) {
                    if (distance[v] < min) {

                        min = distance[v];
                    }
                }

                vertexSet.Remove (minIndex);
                List<int> neighbors = new List<int> ();
                for (int i = 0; i < Count; i++) {
                    if (adjacencyMatrix[minIndex, i] < Double.PositiveInfinity)
                        neighbors.Add (i);
                }

                foreach (var n in neighbors) {
                    double alternativeDist = distance[minIndex] + adjacencyMatrix[minIndex, n];
                    if (alternativeDist < distance[n]) {
                        distance[n] = alternativeDist;
                        prev[n] = minIndex;

                    }
                }
            }

            return new Tuple<double[], int[]> (distance, prev);

        }
        static string BSTTraversal (TreeNode<int> currNode) {
            if (currNode == null)
                return "";

            string s = currNode.value.ToString () + " ";
            s += BSTTraversal (currNode.leftChild);
            s += BSTTraversal (currNode.rightChild);

            return s;
        }

        static string BFS (Graph graph, int root) {
            string s = "";
            bool[] visited = new bool[graph.Count];
            visited[root] = true;

            Queue<int> nodeQueue = new Queue<int> ();

            nodeQueue.Enqueue (root);

            while (nodeQueue.Count > 0) {
                int current = nodeQueue.Dequeue ();
                List<int> neighbors = graph.Neighbors (current);
                s += current + " ";
                for (int i = 0; i < neighbors.Count; i++) {
                    if (!visited[neighbors[i]]) {
                        visited[neighbors[i]] = true;
                        nodeQueue.Enqueue (neighbors[i]);
                    }
                }
            }
            return s;
        }

        static string DFS (Graph graph, int root) {
            string s = "";
            bool[] visited = new bool[graph.Count];
            Stack<int> nodeStack = new Stack<int> ();
 
            nodeStack.Push (root);
            while (nodeStack.Count > 0) {
                int current = nodeStack.Pop ();
                int[] unvisitedNeighbors = graph.Neighbors (current).Where (x => !visited[x]).Reverse ().ToArray ();
                if (!visited[current])
                    s += current + " ";
                visited[current] = true;
                for (int i = 0; i < unvisitedNeighbors.Length; i++) {
                    nodeStack.Push (unvisitedNeighbors[i]);
                }
            }
            return s;
        }

    }

    public class PracAlgorithms {

        static void SortedListAdd (SortedLinkedList<int> list, int value) {
            if (list.start == null || list.start.Value.CompareTo (value) >= 0) {
                // TODO: EX 1.1 list.start = ?

                return;
            }

            Node<int> curr = list.start;
            while (curr.Next != null && curr.Next.Value.CompareTo (value) < 0) {

                // TODO: EX 1.2 PLACEHOLDER, REPLACE break WITH YOUR CODE 
                break;

            }

            // TODO: EX 1.3 curr.Next = ?
        }

        static void RunMerge (int[] arr) {
            MergeSort (arr, 0, arr.Length - 1);
        }

        static void MergeSort (int[] arr, int leftBoundary, int rightBoundary) {
            if (leftBoundary < rightBoundary) {
                int middle = 0; //TODO: EX 2.1 PLACEHOLDER, REPLACE 0 WITH YOUR CODE

                //TODO: EX 2.2

            }
        }

        static int getIndex (string key, int size) {
            int hashCode = Math.Abs (key.GetHashCode ());
            int index = 0; //TODO: EX 3.4 PLACEHOLDER, REPLACE 0 WITH YOUR CODE
            return index;
        }
        static void HashTableDelete (HashTable<string, int> table, string key) {
            var arraySize = table.buckets.Length;
            int index = getIndex (key, arraySize);
            var values = table.buckets;
            if (values[index] == null)
                return;
            else {
                if (values[index].Key.Equals (key)) {
                    //TODO: EX 3.1 values[index] = ?;

                } else {
                    var potentialIndex = getIndex (key, arraySize); //TODO: EX 3.2 PLACEHOLDER, REPLACE 0 WITH YOUR CODE
                    while (values[potentialIndex] != null) {
                        if (values[potentialIndex].Key.Equals (key)) {
                            //TODO: EX 3.3 values[potentialIndex] = ?;

                            return;
                        }

                        potentialIndex++;
                        if (potentialIndex >= arraySize)
                            potentialIndex = 0;
                        if (potentialIndex == index)
                            return;
                    }
                }
            }
        }

        private static TreeNode<int> searchStartingFrom (TreeNode<int> node, int value) {
            if (node == null)
                throw new Exception ("value not found");

            if (node.value.CompareTo (value) == 0)
                return null; // TODO: Ex 4.2 PLACEHOLDER: REPLACE null WITH YOUR CODE

            if (node.value.CompareTo (value) < 0)
                return null; // TODO: Ex 4.3 PLACEHOLDER: REPLACE null WITH YOUR CODE

            return null; // TODO: Ex 4.4 PLACEHOLDER: REPLACE null WITH YOUR CODE
        }

        static TreeNode<int> BSTFind (BSTree<int> tree, int value) {
            return null; // TODO: Ex 4.1 PLACEHOLDER: REPLACE null WITH YOUR CODE
        }

        static void DoublyLinkedListInsertAfter (DoublyLinkedList<int> list, DoubleNode<int> node, int value) {
            DoubleNode<int> newNode = new DoubleNode<int> (value, node, node.Next);
            node.Next = newNode;

            if (list.Last == node) {
                // TODO: Ex 1.1 
            } else {
                // TODO: Ex 1.2
            }
        }

        static void HashTableAdd (HashTable<string, int> table, string key, int value) {
            var arraySize = table.buckets.Length;
            int index = getIndex (key, arraySize);
            var values = table.buckets;
            if (values[index] == null)
                values[index] = null; //TODO: Ex 2.2 PLACEHOLDER: REPLACE null WITH YOUR CODE
            else {
                if (values[index].Key.Equals (key)) {
                    throw new ArgumentException ("Key already exists");
                } else {
                    // TODO: Ex2.3; potentialIndex = ?
                    var potentialIndex = 0; // PLACEHOLDER: REMOVE AND REPLACE WITH YOUR CODE

                    while (values[potentialIndex] != null) {
                        potentialIndex++;
                        if (potentialIndex >= arraySize)
                            potentialIndex = 0;
                        if (potentialIndex == index)
                            return;
                    }
                    values[potentialIndex] = null; //TODO: Ex 2.4 PLACEHOLDER: REPLACE null WITH YOUR CODE
                }
            }
        }

        static void BubbleSort (int[] array) {
            var somethingChanged = true;
            // TODO: Ex3.1; while( ? )
            {
                somethingChanged = false;
                for (int i = 0; i < array.Length - 1; i++) {
                    if (array[i].CompareTo (array[i + 1]) > 0) {
                        var temp = array[i + 1];
                        // TODO: Ex3.2;
                        somethingChanged = true;
                    }
                }
            }
        }

        static Node<int> SortedListFind (SortedLinkedList<int> list, int value) {
            var curr = list.start;

            // TODO: Ex1.1; while( ? )
            {
                if (curr.Value > value)
                    return null;

                // TODO: Ex1.2; curr = ?
            }

            return curr;
        }
        static Nullable<int> HashTableFind (HashTable<string, int> table, string key) {
            var arraySize = table.buckets.Length;
            int index = getIndex (key, arraySize);
            var values = table.buckets;
            if (values[index] == null)
                return null;
            else {
                if (values[index].Key.Equals (key)) {
                    return values[index].Value;
                } else {
                    // TODO: Ex2.1; potentialIndex = ?
                    var potentialIndex = 0; // PLACEHOLDER: REMOVE AND REPLACE WITH YOUR CODE

                    while (values[potentialIndex] != null) {
                        if (values[potentialIndex].Key.Equals (key)) {
                            return values[potentialIndex].Value;
                        }

                        potentialIndex++;
                        if (potentialIndex >= arraySize)
                            potentialIndex = 0;
                        if (potentialIndex == index)
                            return null;
                    }
                }
            }
            return null;
        }
        static void InsertionSort (int[] sequence) {
            for (int j = 1; j < sequence.Length; j++) {
                var key = sequence[j];
                var i = j - 1;

                // TODO: Ex3.1; while( ? )
                {
                    // TODO: Ex3.2 
                    i = i - 1;
                }

                // TODO: Ex3.3
            }
        }

        static void BSTAdd (BSTree<int> tree, int value) {
            if (tree.root == null) {
                // TODO: EX4.1; tree.root = ?

                return;
            } else
                // TODO: Ex4.2
                return; // PLACEHOLDER; REMOVE AND REPLACE WITH YOUR CODE
        }
        static void BSTInsertStartingFrom (TreeNode<int> node, int value) {
            if (node.value.CompareTo (value) < 0) {
                if (node.rightChild == null) {
                    // TODO: Ex4.3; node.rightChild = ?
                } else {
                    // TODO: Ex4.4
                }
            } else if (node.value.CompareTo (value) > 0) {
                if (node.leftChild == null) {
                    // TODO: Ex4.5; node.leftChild = ?
                } else {
                    // TODO: Ex4.6
                }
            }
        }

        static Tuple<double[, ], int[, ]> FloydWarshall (double[, ] adjacencyMatrix) {
            var numVertices = adjacencyMatrix.GetLength (0);
            double[, ] dist = new double[numVertices, numVertices];
            int[, ] next = new int[numVertices, numVertices];

            for (int i = 0; i < numVertices; i++)
                for (int j = 0; j < numVertices; j++) {
                    if (i == j) {
                        // TODO: Ex5.1; dist[i, j] = ?
                    } else {
                        // TODO: EX5.2; dist[i, j] = ?
                    }

                    if (adjacencyMatrix[i, j] != Double.PositiveInfinity) {
                        // TODO: Ex5.3; next[i, j] = ?
                    } else {
                        next[i, j] = -1;
                    }
                }

            for (int k = 0; k < numVertices; k++)
                for (int i = 0; i < numVertices; i++)
                    for (int j = 0; j < numVertices; j++) {
                        //if (? < ?) { ... }
                        // TODO: Ex5.4

                    }

            return new Tuple<double[, ], int[, ]> (dist, next);
        }

        static Tuple<double[], int[]> Dijkstra (double[, ] adjacencyMatrix, int source) {
            int Count = adjacencyMatrix.GetLength (0);
            double[] distance = new double[Count];
            int[] prev = new int[Count];
            List<int> vertexSet = new List<int> (Count);

            for (int i = 0; i < Count; i++) {
                // TODO: Ex 5.1; distance[i] = ?
                // TODO: Ex 5.2; prev[i] = ?;
                // TODO: Ex 5.3; vertexSet.Add(?);
            }

            distance[source] = 0;
            while (vertexSet.Count > 0) {
                int firstUnvisited = vertexSet.First ();
                double min = distance[firstUnvisited];
                int minIndex = firstUnvisited;
                foreach (var v in vertexSet) {
                    if (distance[v] < min) {
                        // TODO: Ex 5.4 
                    }
                }

                vertexSet.Remove (minIndex);
                List<int> neighbors = new List<int> ();
                for (int i = 0; i < Count; i++) {
                    if (adjacencyMatrix[minIndex, i] < Double.PositiveInfinity)
                        neighbors.Add (i);
                }

                foreach (var n in neighbors) {
                    double alternativeDist = distance[minIndex] + adjacencyMatrix[minIndex, n];
                    if (alternativeDist < distance[n]) {
                        // TODO: Ex 5.5 
                    }
                }
            }

            return new Tuple<double[], int[]> (distance, prev);

        }

        static string BFS (Graph graph, int root) {
            string s = "";
            bool[] visited = new bool[graph.Count];
            visited[root] = true;

            Queue<int> nodeQueue = new Queue<int> ();
            //TODO: EX 5.1
            nodeQueue.Enqueue (root);

            while (nodeQueue.Count > 0) {
                int current = nodeQueue.Dequeue (); //TODO: EX 5.2 PLACEHOLDER, REPLACE 0 WITH YOUR CODE
                List<int> neighbors = graph.Neighbors (current);
                s += current + " ";
                for (int i = 0; i < neighbors.Count; i++) {
                    if (!visited[neighbors[i]]) {
                        //TODO: EX 5.3
                    }
                }
            }
            return s;
        }

        static void Enqueue (Queue<int> queue, int element) {
            // if (queue.IsEmpty) {
            //     queue.data[0] = element;
            //     queue.front = queue.back = 0;
            //     queue.Count++;
            // } else if ((queue.back + 1) % queue.data.Length == queue.front) {
            //     throw new QueueException ("The queue is full");
            // } else {
            //     queue.back = 0 //TODO: Ex 2.1 PLACEHOLDER: REPLACE 0 WITH YOUR CODE
            //     queue.data[queue.back] = element;
            //     //TODO Ex 2.2
            // }
        }
    }

}