using System;
using System.Collections;
using System.Collections.Generic;

namespace Haceau.DataStruct
{
    public class Queue<T> : ICollection<T>, IReadOnlyCollection<T>, ICollection, IEnumerable, IEnumerable<T>
    {
        /// <summary>
        /// 拥有的元素数
        /// </summary>
        public int Count
        {
            get => list.Length;
            set => list.Length = value;
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly { get; } = false;

        public bool IsSynchronized { get; set; }

        public object SyncRoot { get; set; }

        protected LinkedList<T> list = new LinkedList<T>();

        /// <summary>
        /// 使用下标获取对象
        /// </summary>
        /// <param name="ix">下标</param>
        /// <returns>对象</returns>
        public T this[int ix]
        {
            get
            {
                if (ix >= Count || ix < 0)
                    throw new ArgumentOutOfRangeException("ix", "未找到元素。");
                return list[ix];
            }
        }

        /// <summary>
        /// 移除所有对象
        /// </summary>
        public void Clear() =>
            list.Clear();

        /// <summary>
        /// 元素是否存在
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否存在</returns>
        public bool Contains(T item) =>
            list.Contains(item);

        /// <summary>
        /// 将Queue元素复制到array
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="arrayIndex">开始下标</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array", "array 为 null");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex 小于 0");
            if (Count > array.Length - arrayIndex)
                throw new ArgumentException("array 的剩余空间不足");

            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 将Queue元素复制到array
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">开始下标</param>
        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array", "array 为 null");
            if (index < 0)
                throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex 小于 0");
            if (Count > array.Length - index)
                throw new ArgumentException("array 的剩余空间不足");

            for (int ix = 0; ix < Count; ++ix)
            {
                array.SetValue(this[ix], ix + index);
            }
        }

        /// <summary>
        /// 删除&返回头部对象
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if (Count < 1)
                throw new InvalidOperationException("queue 中没有数据");
            T result = list[0];
            list.RemoveStart();

            return result;
        }

        /// <summary>
        /// 删除头部对象并将对象存入result
        /// </summary>
        /// <param name="result"></param>
        /// <returns>如果queue拥有数据则为true，否则为false</returns>
        public bool TryDequeue(out T result)
        {
            if (Count < 1)
            {
                result = default;
                return false;
            }
            result = list[0];
            list.RemoveStart();

            return true;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="item">对象</param>
        public void Enqueue(T item) =>
            list.Add(item);

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="item">对象</param>
        public void Add(T item) =>
            list.Add(item);

        /// <summary>
        /// 获取循环访问Queue的枚举数
        /// </summary>
        /// <returns>循环访问Queue的枚举数</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int index = 0; index < Count; ++index)
                yield return this[index % Count];
        }

        /// <summary>
        /// 删除第一个item对象
        /// </summary>
        /// <param name="item">对象</param>
        /// <returns>是否删除</returns>
        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        /// <summary>
        /// 返回最开始的对象
        /// </summary>
        /// <returns>最开始的对象</returns>
        public T Peek()
        {
            if (Count < 1)
                throw new InvalidOperationException("Queue 中没有数据");

            return list[0];
        }

        /// <summary>
        /// 将result的值设置为最开始的对象，获取成功则为true，否则为false
        /// </summary>
        /// <returns>最开始的对象</returns>
        public bool TryPeek(out T result)
        {
            if (Count < 1)
            {
                result = default;
                return false;
            }
            result = list[0];

            return true;
        }

        /// <summary>
        /// 将queue对象复制到数组
        /// </summary>
        /// <returns>数组</returns>
        public T[] ToArray() =>
            list.ToArray();

        /// <summary>
        /// 获取循环访问Queue的枚举数
        /// </summary>
        /// <returns>循环访问Queue的枚举数</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int index = 0; index < Count; ++index)
                yield return this[index % Count];
        }
    }
}
