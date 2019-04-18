using System;

namespace Heap
{
    public class Heap<T> where T : IComparable
    {
        private enum HeapType { minHeap, maxHeap }

        private readonly HeapType heapType; //is the heap a Min Heap or Max Heap?
        private int heapPointer; //current index on the Heap.

        /// <summary>
        /// Test to see if the Heap is empty.
        /// </summary>
        public bool IsEmpty { get { return heapPointer == 0; } }

        /// <summary>
        /// Test to see if the Heap is full.
        /// </summary>
        public bool IsFull { get { return heapPointer == GetHeap.Length; } }

        /// <summary>
        /// Gets the get Heap as an array.
        /// </summary>
        /// <value>The Heap array.</value>
        public T[] GetHeap { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Heap.Heap`1"/> class.
        /// </summary>
        /// <param name="maxSize">Size of the Heap.</param>
        /// <param name="isMaxHeap">If set to <c>true</c> the largest value will be the root value, instead of the smallest.</param>
        public Heap(int maxSize, bool isMaxHeap = false)
        {
            heapType = isMaxHeap ? HeapType.maxHeap : HeapType.minHeap;
            GetHeap = new T[maxSize];
        }

        /// <summary>
        /// Inserts a value onto the heap. Returns false if the heap is full, true if not.
        /// </summary>
        /// <returns>Bool determining whether the value was inserted</returns>
        /// <param name="val">Value.</param>
        public bool Insert(T val)
        {
            if (heapPointer == GetHeap.Length) return false;
            GetHeap[heapPointer] = val;
            SiftUp(heapPointer);
            heapPointer++;
            return true;
        }

        /// <summary>
        /// Remove the value currently at the top of the Heap and return it.
        /// </summary>
        /// <returns>Value at the top of the Heap.</returns>
        public T Remove()
        {
            T output = Peek();
            heapPointer--;
            GetHeap[0] = GetHeap[heapPointer];
            SiftDown(0);
            return output;
        }

        /// <summary>
        /// Moves an element at <paramref name="index"/> up the Heap.
        /// </summary>
        /// <param name="index">Index of element to sift.</param>
        private void SiftUp(int index)
        {
            if (index == 0) return;

            int parentIndex = (index - 1) / 2;

            bool shift = CompareValues(index, parentIndex)>0;
            if (!shift) return;

            Swap(index, parentIndex);

            SiftUp(parentIndex);

        }

        /// <summary>
        /// Moves an element at <paramref name="index"/> down the Heap.
        /// </summary>
        /// <param name="index">Index of element to sift.</param>
        private void SiftDown(int index)
        {
            int child1 = index * 2 + 1;
            if (child1 >= heapPointer) return;
            int child2 = child1 + 1;

            int childIndexToSift = (child2 >= heapPointer || CompareValues(child1, child2) <= 0) ? child1 : child2;
            if (CompareValues(childIndexToSift, index) > 0) return;

            Swap(index, childIndexToSift);

            SiftDown(childIndexToSift);
        }

        /// <summary>
        /// Compares the values <paramref name="index"/> and <paramref name="indexToCompare"/> using <see cref="IComparable.CompareTo"/>
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="index">Index.</param>
        /// <param name="indexToCompare">Index to compare.</param>
        private int CompareValues(int index, int indexToCompare)
        {
            T val = GetHeap[index];
            T compVal = GetHeap[indexToCompare];
            int comparison = val.CompareTo(compVal);
            if (heapType == HeapType.maxHeap) comparison = -comparison;
            return comparison;
        }

        /// <summary>
        /// Swaps two values at index <paramref name="i"/> and <paramref name="j"/>.
        /// </summary>
        /// <param name="i">First index to swap.</param>
        /// <param name="j">Second index to swap.</param>
        private void Swap(int i, int j)
        {
            T temp = GetHeap[i];
            GetHeap[i] = GetHeap[j];
            GetHeap[i] = temp;
        }

        /// <summary>
        /// View the value currently at the top of the Heap.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (GetHeap.Length == 0) throw new ArgumentOutOfRangeException("The Heap is empty.");
            return GetHeap[0];
        }
    }
}
