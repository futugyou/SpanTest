﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Other
{
    public class LinkedList
    {
        public int Value { get; set; }
        public LinkedList(int value)
        {
            Value = value;
        }
        public LinkedList Next { get; set; }
    }
    public class SortLinkedList
    {
        public static void TestSort()
        {
            var head = new LinkedList(5)
            {
                Next = new LinkedList(4)
                {
                    Next = new LinkedList(3)
                    {
                        Next = new LinkedList(2)
                        {
                            Next = new LinkedList(1)
                        }
                    }
                }
            };
            head = QuickSort(head);
        }

        public static LinkedList InsertSort(LinkedList head)
        {
            if (head == null || head.Next == null) return head;
            LinkedList curr = head.Next;
            LinkedList start = head;
            LinkedList end = head;
            while (curr != null)
            {
                var tmp = start;
                var index = 0;
                LinkedList tmpPre = null;
                while (tmp.Value < curr.Value)
                {
                    if (index == 0)
                    {
                        tmpPre = start;
                        index++;
                    }
                    else
                    {
                        tmpPre = tmpPre.Next;
                    }
                    tmp = tmp.Next;
                }

                // Not change. curr is big than end
                if (tmp == curr)
                {
                    end = curr;
                    curr = curr.Next;
                    continue;
                }

                var cn = curr.Next;
                if (tmpPre != null) // this is nomal case
                {
                    tmpPre.Next = curr;
                    curr.Next = tmp;
                }
                else // curr is small than start
                {
                    curr.Next = start;
                    // updtae start and head
                    start = curr;
                    head = curr;
                }
                end.Next = cn;
                curr = cn;
            }
            return head;
        }

        public static LinkedList SelectSort(LinkedList head)
        {
            if (head == null || head.Next == null) return head;

            LinkedList virtualNode = new LinkedList(int.MinValue);
            virtualNode.Next = head;
            LinkedList curr = virtualNode;

            while (curr != null && curr.Next != null)
            {
                var tmp = curr.Next;
                var min = curr.Next;
                LinkedList minPre = virtualNode;
                while (tmp != null && tmp.Next != null)
                {
                    if (tmp.Next.Value < min.Value)
                    {
                        min = tmp.Next;
                        minPre = tmp;
                    }
                    tmp = tmp.Next;
                }
                if (minPre == virtualNode)
                {
                    curr = curr.Next;
                }
                else
                {
                    var minnext = min.Next;
                    minPre.Next = minnext;
                    min.Next = curr.Next;
                    curr.Next = min;
                    curr = curr.Next;
                }
            }
            return virtualNode.Next;
        }

        public static LinkedList QuickSort(LinkedList head)
        {
            if (head == null || head.Next == null) return head;
            LinkedList virtualNode = new LinkedList(int.MinValue);
            virtualNode.Next = head;
            QuickSort(virtualNode, head, null);
            return virtualNode.Next;
        }

        private static void QuickSort(LinkedList preHead, LinkedList head, LinkedList tail)
        {
            if (head != tail && head.Next != tail)
            {
                LinkedList mid = PartitionList(preHead, head, tail);
                QuickSort(preHead, preHead.Next, mid);
                QuickSort(mid, mid.Next, tail);
            }
        }

        private static LinkedList PartitionList(LinkedList prelow, LinkedList low, LinkedList high)
        {
            int key = low.Value;
            LinkedList presmall = new LinkedList(0);
            LinkedList small = presmall;
            LinkedList prebig = new LinkedList(0);
            LinkedList big = prebig;
            for (var i = low.Next; i != high; i = i.Next)
            {
                if (i.Value < key)
                {
                    small.Next = i;
                    small = i;
                }
                else
                {
                    big.Next = i;
                    big = i;
                }
            }
            big.Next = high;
            small.Next = low;
            low.Next = prebig.Next;
            prelow.Next = presmall.Next;
            return low;
        }

        public static LinkedList MergeSort(LinkedList head)
        {
            if (head == null && head.Next == null) return head;
            LinkedList fast = head;
            LinkedList slow = head;
            while (fast.Next != null && fast.Next.Next != null)
            {
                fast = fast.Next.Next;
                slow = slow.Next;
            }
            fast = slow;
            slow = slow.Next;
            fast.Next = null;
            fast = MergeSort(head);
            slow = MergeSort(slow);
            return MergeSort(fast, slow);

        }
        public static LinkedList MergeSort(LinkedList left, LinkedList right)
        {
            if (left == null) return right;
            if (right == null) return left;
            LinkedList result = null;
            if (left.Value < right.Value)
            {
                result = left;
                left = left.Next;
            }
            else
            {
                result = right;
                right = right.Next;
            }

            LinkedList tmp = result;
            while (left != null && right != null)
            {
                if (left.Value < right.Value)
                {
                    tmp.Next = left;
                    left = left.Next;
                }
                else
                {
                    tmp.Next = right;
                    right = right.Next;
                }
                tmp = tmp.Next;
            }
            if (left == null) tmp.Next = right;
            if (right == null) tmp.Next = left;
            return result;
        }
    }
}
