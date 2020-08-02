using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Haceau.DataStruct
{
    public class LinkedList<T> : ICollection<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, IEnumerable<T>, IList<T>
    {
        private int length = 0;

        /// <summary>
        /// 长度
        /// </summary>
        public int Length
        {
            get => length;

            set
            {
                int rmValue = 0;
                if (value < length)
                    Remove(value);
                else
                {
                    Linked list;
                    if (length == 0)
                    {
                        head = new Linked();
                        if (value > 1)
                        {
                            tail = new Linked
                            {
                                last = head
                            };
                            head.next = tail;
                            list = tail;
                            rmValue += 2;
                            value -= 2;
                        }
                        else
                        {
                            ++length;
                            return;
                        }
                    }
                    else if (length == 1)
                    {
                        tail = new Linked
                        {
                            last = head
                        };
                        head.next = tail;
                        list = tail;
                        if (value == 2)
                        {
                            ++length;
                            return;
                        }
                        value -= 1;
                        ++rmValue;
                    }
                    else
                        list = tail;
                    for (int i = length; i < value; ++i)
                    {
                        list.next = new Linked
                        {
                            last = list
                        };
                        list = list.next;
                    }
                    tail = list;
                }
                length = value + rmValue;
            }
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int Count => Length;

        /// <summary>
        /// 是否为只读
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// 头链表
        /// </summary>
        private Linked head;

        /// <summary>
        /// 尾链表
        /// </summary>
        private Linked tail;


        /// <summary>
        /// 添加数据
        /// </summary>
        public void Add(T data) =>
            this[++Length - 1] = data;

        /// <summary>
        /// 添加集合的数据
        /// </summary>
        /// <param name="datas"></param>
        public void AddRange(IEnumerable<T> datas)
        {
            if (datas == null)
                throw new ArgumentNullException("datas为null。");
            for (int i = 0; i < datas.ToArray().Length; ++i)
                Add(datas.ToList()[i]);
        }

        /// <summary>
        /// 是否存在data
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public bool Contains(T data) =>
            (IndexOf(data) != -1) ? true : false;

        /// <summary>
        /// 将LinkedList复制到array的arrayIndex后
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="arrayIndex">索引</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array为null。");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex为负数。");
            if (Length > array.Length - arrayIndex)
                throw new ArgumentException("数组空间不足。");
            for (int i = 0; i < Length; ++i)
                array[arrayIndex - 1 + i] = this[i];
        }

        /// <summary>
        /// 将链表第index之后的count个元素复制到array的arrayIndex后
        /// </summary>
        /// <param name="index">链表索引</param>
        /// <param name="array">数组</param>
        /// <param name="arrayIndex">数组索引</param>
        /// <param name="count">元素个数</param>
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            if (array == null)
                throw new ArgumentNullException("array为null。");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index小于零。");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex小于零。");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count小于零。");
            LinkedList<T> list = Sub(index, count);
            for (int i = 0; i < Length; ++i)
                array[arrayIndex - 1 + i] = list[i];
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public LinkedList()
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public LinkedList(int length)
        {
            if (length < 0)
                throw new ArgumentException("长度过小。");
            Length = length;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public LinkedList(int length, T data)
        {
            Length = length;
            head.Data = data;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="list">链表</param>
        public LinkedList(int length, LinkedList<T> list)
        {
            if (length < list.Length)
                throw new ArgumentException("长度过小。");
            Length = length;
            for (int i = 0; i < list.Length; ++i)
                this[i] = list[i];
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="data">数据</param>
        public LinkedList(int length, IEnumerable<T> data)
        {
            if (length < data.ToArray().Length)
                throw new ArgumentException("长度过短。");
            Length = length;
            Linked list = head;
            for (int i = 0; i < Length; ++i)
            {
                list.Data = data.ToArray()[i];
                list = list.next;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="list">链表</param>
        public LinkedList(LinkedList<T> list)
        {
            foreach (T item in list)
                Add(item);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="data">数据</param>
        public LinkedList(IEnumerable<T> data)
        {
            foreach (var item in data)
                Add(item);
        }

        /// <summary>
        /// 删除data
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>是否成功删除</returns>
        public bool Remove(T data)
        {
            Linked i = head;
            // 遇到data
            for (; !i.Data.Equals(data) && i != null; i = i.next) ;

            if (i == null)
                return false;
            // 在中间
            if (i.next != null && i.last != null)
            {
                i.last.next = i.next;
                i.next.last = i.last;
            }
            // 在尾部
            else if (i.next != null && i.last == null)
                head = i.next;
            // 在头部
            else if (i.next == null && i.last != null)
                tail = i.last;

            --length;
            return true;
        }

        /// <summary>
        /// 删除index之后的data
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="index">索引</param>
        public void Remove(T data, int index) =>
            Sub(index).Remove(data);

        /// <summary>
        /// 删除index之后的所有数据
        /// </summary>
        /// <param name="index">索引</param>
        public void Remove(int index)
        {
            if (index > Length)
                throw new ArgumentException("索引过大。");
            if (index < 0)
                throw new ArgumentException("索引过小。");

            Linked list = head;
            // 到达index之前的一个链表
            for (int i = 0; i < index; list = list.next, ++i) ;
            list.next = null;
            tail = list;
            length = index;
        }

        /// <summary>
        /// 删除索引所在的数据
        /// </summary>
        /// <param name="index">索引</param>
        public void RemoveAt(int index)
        {
            if (index > Length)
                throw new Exception("索引过大。");
            if (index < 0)
                throw new ArgumentException("索引过小。");

            Linked list = head;
            for (int i = 0; i < index; ++i)
                list = list.next;
            list.last.next = list.next;
            list.next.last = list.last;
            --length;
        }

        /// <summary>
        /// 删除最开始的数据
        /// </summary>
        public void RemoveStart()
        {
            if (Length == 0)
                throw new Exception("数据过少。");
            head.next.last = null;
            head = head.next;
            --length;
        }

        /// <summary>
        /// 删除最尾部的数据
        /// </summary>
        public void RemoveEnd()
        {
            if (Length == 0)
                throw new Exception("数据过少。");
            --Length;
        }

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>get到的值</returns>
        public T this[int index]
        {
            get
            {
                if ((index < 0 && Math.Abs(index) > Length) ||
                    (index > 0 && index >= Length))
                    throw new ArgumentOutOfRangeException("索引过大或过小。");
                if (index >= 0)
                {
                    Linked list = head;
                    for (int i = 0; i < index; ++i)
                        list = list.next;
                    return list.Data;
                }
                else
                {
                    Linked list = tail;
                    for (int i = -1; i > index; --i)
                        list = list.last;
                    return list.Data;
                }
            }
            set
            {
                if ((index < 0 && Math.Abs(index) > Length) ||
                    (index > 0 && index >= Length))
                    throw new ArgumentOutOfRangeException("索引过大或过小。");
                if (index >= 0)
                {
                    Linked list = head;
                    for (int i = 0; i < index; ++i)
                        list = list.next;
                    list.Data = value;
                }
                else
                {
                    Linked list = tail;
                    for (int i = -1; i > index; --i)
                        list = list.last;
                    list.Data = value;
                }
            }
        }

        /// <summary>
        /// 切片
        /// </summary>
        /// <param name="start">开始索引</param>
        /// <param name="end">结束</param>
        /// <returns>值</returns>
        public LinkedList<T> this[int start, int end]
        {
            get
            {
                if ((start < 0 && Math.Abs(start) > Length) ||
                    (start > 0 && start >= Length) ||
                    (end < 0 && Math.Abs(end) > Length) ||
                    (end > 0 && end >= Length))
                    throw new ArgumentOutOfRangeException("索引过大或过小。");
                if (start >= end)
                    throw new ArgumentOutOfRangeException("end过小或start过大。");
                LinkedList<T> result = new LinkedList<T>();
                for (int i = start; i < end; ++i)
                    result.Add(this[i]);
                return result;
            }
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <returns>数组</returns>
        public T[] ToArray() =>
            ToList().ToArray();

        /// <summary>
        /// 转换为列表
        /// </summary>
        /// <returns>列表</returns>
        public List<T> ToList()
        {
            List<T> list = new List<T>();
            Linked linked = head;
            for (int i = 0; i < Length; ++i)
            {
                list.Add(linked.Data);
                linked = linked.next;
            }
            return list;
        }

        /// <summary>
        /// 截取数据
        /// </summary>
        /// <param name="start">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns>截取到的链表</returns>
        public LinkedList<T> Sub(int start, int length)
        {
            if (start + length > Length + 1)
                throw new Exception("索引过大。");
            if (start < 0 || length <= 0)
                throw new Exception("索引过小。");
            Linked list = head;
            for (int i = 0; i < start; ++i)
                list = list.next;
            LinkedList<T> result = new LinkedList<T>();
            for (int i = 0; i < length; ++i)
            {
                result.Add(list.Data);
                list = list.next;
            }
            return result;
        }

        /// <summary>
        /// 截取数据
        /// </summary>
        /// <param name="start">开始索引</param>
        /// <returns>截取到的链表</returns>
        public LinkedList<T> Sub(int start)
        {
            if (start > Length)
                throw new Exception("参数错误");
            if (start < 0)
                throw new Exception("索引过小。");
            Linked list = head;
            for (int i = 0; i < start; ++i)
                list = list.next;
            LinkedList<T> result = new LinkedList<T>();
            for (int i = 0; i < length; ++i)
            {
                result.Add(list.Data);
                list = list.next;
            }
            return result;
        }

        /// <summary>
        /// 清空链表
        /// </summary>
        public void Clear() =>
            Length = 0;

        /// <summary>
        /// 插入数据在索引后面
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="data">数据</param>
        public void Insert(int index, T data)
        {
            if (index < 0)
                throw new Exception("索引过小。");
            if (index >= Length - 1)
                throw new Exception("索引过大。");
            Linked list = head;
            for (int i = 0; i < index; ++i)
                list = list.next;
            Linked insert = new Linked();
            insert.Data = data;
            insert.next = list.next;
            insert.last = list;
            list.next = insert;
        }

        /// <summary>
        /// 插入集合在索引后面
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="data">数据</param>
        public void Insert(int index, IEnumerable<T> data)
        {
            if (index < 0)
                throw new Exception("索引过小。");
            if (index >= Length - 1)
                throw new Exception("索引过大。");
            Linked list = head;
            for (int i = 0; i < index; ++i)
                list = list.next;
            for (int i = index; i < data.Count(); ++i)
            {
                list.next = new Linked()
                {
                    last = list,
                    Data = data.ToArray()[i - index]
                };
                list = list.next;
            }
        }

        /// <summary>
        /// 查找数据并返回第一个数据的索引，如未查找到就返回-1
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>索引</returns>
        public int IndexOf(T data)
        {
            Linked list = head;
            for (int i = 0; i < Length; ++i)
            {
                if (list.Data.Equals(data))
                    return i;
                list = list.next;
            }
            return -1;
        }

        /// <summary>
        /// 查找index之后的数据并返回第一个数据的索引，如未查找到就返回-1
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="index">索引</param>
        /// <returns>索引</returns>
        public int IndexOf(T data, int index)
        {
            Linked list = head;
            for (int i = index; i < Length; ++i)
            {
                if (list.Data.Equals(data))
                    return i;
                list = list.next;
            }
            return -1;
        }

        /// <summary>
        /// 查找index之后的count个数据并返回第一个数据的索引，如未查找到就返回-1
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="index">索引</param>
        /// <param name="count">个数</param>
        /// <returns>索引</returns>
        public int IndexOf(T data, int index, int count)
        {
            Linked list = head;
            for (int i = index; i < index + count; ++i)
            {
                if (list.Data.Equals(data))
                    return i;
                list = list.next;
            }
            return -1;
        }

        /// <summary>
        /// 使用默认比较器在链表中使用二分查找算法查找数据并返回索引，如未查找到就返回-1
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>索引</returns>
        public int BinarySerach(T data)
        {
            int start = 0;
            int end = Length;
            Comparer<T> comparer = Comparer<T>.Default;

            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (comparer.Compare(data, this[mid]) == -1)
                    end = mid - 1;
                else if (comparer.Compare(data, this[mid]) == 0)
                    return mid;
                else
                    start = mid + 1;
            }
            return -1;
        }

        /// <summary>
        /// 使用指定比较器在链表中使用二分查找算法查找数据并返回索引，如未查找到就返回-1
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="comparer">比较器</param>
        /// <returns>索引</returns>
        public int BinarySerach(T data, IComparer<T> comparer)
        {
            if (comparer == null)
                comparer = Comparer<T>.Default;

            int start = 0;
            int end = Length;

            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (comparer.Compare(data, this[mid]) == -1)
                    end = mid - 1;
                else if (comparer.Compare(data, this[mid]) == 0)
                    return mid;
                else
                    start = mid + 1;
            }
            return -1;
        }

        /// <summary>
        /// 使用指定比较器在链表的某个范围中使用二分查找算法查找数据并返回索引，如未查找到就返回-1
        /// </summary>
        /// <param name="index">开始下标</param>
        /// <param name="comparer">比较器</param>
        /// <param name="data">数据</param>
        /// <param name="count">数量</param>
        /// <returns>索引</returns>
        public int BinarySerach(int index, int count, T data, IComparer<T> comparer)
        {
            if (comparer == null)
                comparer = Comparer<T>.Default;

            int start = index;
            int end = index + count;

            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (comparer.Compare(data, this[mid]) == -1)
                    end = mid - 1;
                else if (comparer.Compare(data, this[mid]) == 0)
                    return mid;
                else
                    start = mid + 1;
            }
            return -1;
        }

        /// <summary>
        /// 将链表的所有数据填充为data
        /// </summary>
        /// <param name="data">数据</param>
        public void FillAll(T data)
        {
            for (int i = 0; i < length; ++i)
                this[i] = data;
        }

        /// <summary>
        /// 将链表从index开始的count个数据更改为data
        /// </summary>
        /// <param name="index">开始索引</param>
        /// <param name="count">个数</param>
        /// <param name="data">数据</param>
        public void Fill(int index, int count, T data)
        {
            for (int i = index; i < index + count; ++i)
                this[i] = data;
        }

        /// <summary>
        /// 链表中是否有与match条件匹配的元素
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>是否有s</returns>
        public bool Exists(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为null。");
            for (int i = 0; i < length; ++i)
                if (match(this[i]))
                    return true;
            return false;
        }

        /// <summary>
         /// 链表从index开始的count个元素中是否有与match条件匹配的元素
         /// </summary>
         /// <param name="match">条件</param>
         /// <returns>是否有满足的元素</returns>
        public bool Exists(int index, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为null。");
            if (index < 0 || count < 1)
                throw new ArgumentOutOfRangeException("index或count过小。");
            for (int i = index; i < index + count; ++i)
                if (match(this[i]))
                    return true;
            return false;
        }

        /// <summary>
        /// 如果链表中有与match匹配的元素，则返回该元素，否则返回T的默认值
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>元素</returns>
        public T Find(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为空。");
            for (int i = 0; i < length; ++i)
                if (match(this[i]))
                    return this[i];
            return default;
        }

        /// <summary>
        /// 如果链表从index开始的count个元素中有与match匹配的元素，则返回该元素，否则返回T的默认值
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>元素</returns>
        public T Find(int index, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为空。");
            if (count < 0 ||
                index < 0)
                throw new ArgumentOutOfRangeException("index或count过小。");
            for (int i = index; i < index + count; ++i)
                if (match(this[i]))
                    return this[i];
            return default;
        }

        /// <summary>
        /// 返回链表中所有满足match条件的元素
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>所有元素</returns>
        public List<T> FindAll(Predicate<T> match)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < length; ++i)
                if (match(this[i]))
                    result.Add(this[i]);
            return result;
        }

        /// <summary>
        /// 返回链表中从index开始之后的count个元素中所有满足match条件的元素
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>所有元素</returns>
        public List<T> FindAll(int index, int count, Predicate<T> match)
        {
            List<T> result = new List<T>();
            for (int i = index; i < count; ++i)
                if (match(this[i]))
                    result.Add(this[i]);
            return result;
        }

        /// <summary>
        /// 如果链表中有与match匹配的元素，则返回该元素的索引，否则返回-1
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>索引</returns>
        public int FindIndex(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为空。");
            for (int i = 0; i < length; ++i)
                if (match(this[i]))
                    return i;
            return -1;

        }

        /// <summary>
        /// 如果链表从index开始的count个元素中有与match匹配的元素，则返回该元素的索引，否则返回-1
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>索引</returns>
        public int FindIndex(int index, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为空。");
            if (count < 0 ||
                index < 0)
                throw new ArgumentOutOfRangeException("index或count过小。");
            for (int i = index; i < index + count; ++i)
                if (match(this[i]))
                    return i;
            return -1;
        }

        /// <summary>
        /// 如果链表中有与match匹配的元素，则返回最后一个匹配的元素，否则返回T的默认值
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>元素</returns>
        public T FindLast(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为空。");
            for (int i = length - 1; i >= 0; --i)
                if (match(this[i]))
                    return this[i];
            return default;
        }

        /// <summary>
        /// 如果链表从index开始的count个元素中有与match匹配的元素，则返回最后一个匹配元素，否则返回T的默认值
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>元素</returns>
        public T FindLast(int index, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为空。");
            if (count < 0 ||
                index < 0)
                throw new ArgumentOutOfRangeException("index或count过小。");
            for (int i = index + count - 1; i > index; --i)
                if (match(this[i]))
                    return this[i];
            return default;
        }

        /// <summary>
        /// 如果链表中有与match匹配的元素，则返回最后一个匹配的元素的索引，否则返回-1
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>索引</returns>
        public int FindLastIndex(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为空。");
            for (int i = length; i > 0; --i)
                if (match(this[i]))
                    return i;
            return -1;
        }

        /// <summary>
        /// 如果链表从index开始的count个元素中有与match匹配的元素，则返回最后一个匹配的元素的索引，否则返回-1
        /// </summary>
        /// <param name="match">条件</param>
        /// <returns>索引</returns>
        public int FindLastIndex(int index, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match为空。");
            if (count < 0 ||
                index < 0)
                throw new ArgumentOutOfRangeException("index或count过小。");
            for (int i = index + count - 1; i > index; --i)
                if (match(this[i]))
                    return i;
            return -1;
        }

        /// <summary>
        /// 让链表中的所有元素执行func方法。
        /// </summary>
        /// <param name="func">方法</param>
        public void ForEach(Action<T> func)
        {
            if (func == null)
                throw new ArgumentNullException("func为null。");
            foreach (T item in this)
                func(item);
        }

        /// <summary>
        /// 让链表中从index开始的count个元素执行func方法。
        /// </summary>
        /// <param name="func">方法</param>
        public void ForEach(int index, int count, Action<T> func)
        {
            if (func == null)
                throw new ArgumentNullException("func为null。");
            for (int i = index; i < index + count; ++i)
            {
                T temp = this[i];
                func(temp);
                if (!temp.Equals(this[i]))
                    throw new InvalidOperationException("func改变了元素的值。");
            }
        }

        /// <summary>
        /// 获取index之后的count个元素。
        /// </summary>
        /// <param name="index">下标</param>
        /// <param name="count">个数</param>
        /// <returns>链表</returns>
        public LinkedList<T> GetRange(int index, int count)
        {
            LinkedList<T> list = new LinkedList<T>();
            for (int i = index; i < index + count; ++i)
                list.Add(this[index]);
            return list;
        }

        public override string ToString()
        {
            string result = "[";
            foreach (var item in this)
            {
                result += item + ",";
            }
            result.Remove(result.Length - 1);
            result += "]";
            return result;
        }

        /// <summary>
        /// 获取链表的只读 ReadOnlyCollection<T> 包装器。
        /// </summary>
        /// <returns>只读包装器</returns>
        public ReadOnlyCollection<T> AsReadOnly() =>
            new ReadOnlyCollection<T>(this);

        /// <summary>
        /// 转换类型
        /// </summary>
        /// <typeparam name="TOutput">类型</typeparam>
        /// <param name="converter">转换器</param>
        /// <returns>转换后的链表</returns>
        public LinkedList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            LinkedList<TOutput> list = new LinkedList<TOutput>();
            foreach (T item in this)
                list.Add(converter(item));
            return list;
        }

        /// <summary>
        /// 循环访问集合的枚举器
        /// </summary>
        /// <returns>数据</returns>
        public IEnumerator GetEnumerator()
        {
            for (int index = 0; index < Length; ++index)
                yield return this[index % Length];
        }

        /// <summary>
        /// 循环访问集合的枚举器
        /// </summary>
        /// <returns>数据</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int index = 0; index < Length; ++index)
                yield return this[index % Length];
        }

        /// <summary>
        /// 节点
        /// </summary>
        private class Linked
        {
            /// <summary>
            /// 下一个
            /// </summary>
            public Linked next;
            /// <summary>
            /// 上一个
            /// </summary>
            public Linked last;

            /// <summary>
            /// 数据
            /// </summary>
            public T Data
            {
                get;
                set;
            }
        }
    }
}