using System;
using System.Collections;
using System.Collections.Generic;

namespace Haceau.DataStruct
{
    public class Stack<T> : IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// 长度
        /// </summary>
        private int count = 0;

        /// <summary>
        /// 长度
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value为0。");
                if (value < count)
                    PopToEnd(value);
                if (value > count)
                {
                    T[] arr = stack;
                    stack = new T[value];
                    for (int i = 0; i < count; ++i)
                        stack[i] = arr[i];
                }
                count = value;
            }
        }

        /// <summary>
        /// 栈
        /// </summary>
        private T[] stack = new T[0];

        /// <summary>
        /// 将index到结尾的数据弹出。
        /// </summary>
        /// <param name="index">下标</param>
        public Stack<T> PopToEnd(int index)
        {
            Stack<T> result = new Stack<T>();
            for (int i = index + 1; i < count; ++i)
                result.Push(Pop());
            return result;
        }

        /// <summary>
        /// 弹出尾部数据
        /// </summary>
        /// <returns>弹出数据</returns>
        public T Pop()
        {
            T[] arr = stack;
            stack = new T[count - 1];
            for (int i = 0; i < count - 2; ++i)
                stack[i] = arr[i];
            --count;
            return arr[count];
        }

        /// <summary>
        /// 将数据压入栈
        /// </summary>
        /// <param name="item">数据</param>
        public void Push(T item)
        {
            T[] arr = stack;
            stack = new T[count + 1];
            for (int i = 0; i < count; ++i)
                stack[i] = arr[i];
            stack[count] = item;
            ++count;
        }

        /// <summary>
        /// 循环访问集合的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int index = 0; index < count; ++index)
                yield return stack[index % count];
        }

        /// <summary>
        /// 循环访问集合的枚举器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int index = 0; index < count; ++index)
                yield return stack[index % count];
        }
    }
}