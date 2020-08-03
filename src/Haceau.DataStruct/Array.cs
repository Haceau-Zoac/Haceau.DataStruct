using System;
using System.Collections;
using System.Collections.Generic;

namespace Haceau.DataStruct
{
    public class DynamicArray<T> : IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// 数组
        /// </summary>
        private T[] array = new T[0];
        /// <summary>
        /// 已用长度
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
                if (value <= count)
                    RemoveToEnd(value);
                else
                {
                    T[] arr = array;
                    array = new T[value];
                    for (int i = 0; i < arr.Length; ++i)
                        array[i] = arr[i];
                    count = value;
                }
            }
        }

        /// <summary>
        /// 在尾部添加元素
        /// </summary>
        /// <param name="data">元素</param>
        public void Add(T data)
        {
            ++Count;
            array[count - 1] = data;
        }

        /// <summary>
        /// 清除所有元素
        /// </summary>
        public void Clear() =>
            count = 0;

        /// <summary>
        /// 移除index之后的所有数据（不包括index）
        /// </summary>
        /// <param name="index">索引</param>
        public void RemoveToEnd(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index为负数。");
            else if (index > count)
                throw new ArgumentOutOfRangeException("index大于count。");
            T[] arr = array;
            array = new T[index + 1];
            for (int i = 0; i <= index; ++i)
                array[i] = arr[i];
            count = index;
        }

        /// <summary>
        /// 循环访问集合的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int index = 0; index < count; ++index)
                yield return this[index % count];
        }

        /// <summary>
        /// 循环访问集合的枚举器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int index = 0; index < count; ++index)
                yield return this[index % count];
        }

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>元素</returns>
        public T this[int index]
        {
            get
            {
                // 错误参数检查
                if (index > 0 && index >= count)
                    throw new ArgumentOutOfRangeException("index过大。");
                if (index < 0 && Math.Abs(index) > count)
                    throw new ArgumentOutOfRangeException("index过小。");

                // 负数
                if (index < 0)
                    index += count;
                // 获取元素
                return array[index];
            }
            set
            {

                // 错误参数检查
                if (index > 0 && index >= count)
                    throw new ArgumentOutOfRangeException("index过大。");
                if (index < 0 && Math.Abs(index) > count)
                    throw new ArgumentOutOfRangeException("index过小。");

                // 负数
                if (index < 0)
                    index += count;
                // 设置元素
                array[index] = value;
            }
        }

        /// <summary>
        /// 切片
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <returns>元素值</returns>
        public DynamicArray<T> this[int start, int end]
        {
            get
            {
                // 错误参数检查
                if (start >= end)
                    throw new ArgumentOutOfRangeException("start过大。");
                if (start < 0 && Math.Abs(start) > count)
                    throw new ArgumentOutOfRangeException("start过小。");
                if (end > 0 && end >= count)
                    throw new ArgumentOutOfRangeException("end过大。");

                // 负数
                if (start < 0)
                    start += count;
                if (end < 0)
                    end += count;
                // 获取元素
                DynamicArray<T> result = new DynamicArray<T>();
                for (int i = start; i < end; ++i)
                    result.Add(array[i]);
                return result;
            }
        }
    }
}
