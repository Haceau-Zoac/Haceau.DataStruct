using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Haceau.DataStruct
{
    public class LinkedList<T>
    {
        private ulong length = 1;

        /// <summary>
        /// 长度
        /// </summary>
        public ulong Length
        {
            get => length;

            set
            {
                if (value < length)
                    Remove(value);
                else
                {
                    LinkedList<T> end = tail;
                    for (ulong i = 0; i < value; ++i)
                    {
                        end.next = new LinkedList<T>();
                        LinkedList<T> temp = end;
                        end = end.next;
                        end.last = temp;
                    }
                    tail = end;
                }
                length = value;
            }
        }

        /// <summary>
        /// 数据
        /// </summary>
        private T Data
        {
            get;
            set;
        }

        public LinkedList<T> head;

        /// <summary>
        /// 尾链表
        /// </summary>
        private LinkedList<T> tail;

        /// <summary>
        /// 下一个
        /// </summary>
        private LinkedList<T> next = null;
        /// <summary>
        /// 上一个
        /// </summary>
        private LinkedList<T> last = null;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="data">数据</param>
        public LinkedList(T[] data)
        {
            head = this;
            tail = this;
            Length = (ulong)data.Length;
            LinkedList<T> list = head;
            for (ulong i = 0; i < Length; ++i)
            {
                list.Data = data[i];
                list = list.next;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public LinkedList(T data)
        {
            head = this;
            tail = this;
            Data = data;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public LinkedList()
        {
            head = this;
            tail = this;
        }

        /// <summary>
        /// 删除data
        /// </summary>
        /// <param name="data">数据</param>
        public void Remove(T data)
        {
            if (head == null && tail == null)
                throw new Exception("链表只有一个数据。");

            LinkedList<T> i = head;
            // 遇到data
            for (; i != null && (!i.Data.Equals(data)); i = i.next) ;

            if (i == null)
                return;
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
        }

        /// <summary>
        /// 删除index之后的所有数据
        /// </summary>
        /// <param name="index">索引</param>
        public void Remove(ulong index)
        {
            if (index > Length)
                throw new Exception("索引过大。");

            LinkedList<T> list = head;
            // 到达index之前的一个链表
            for (ulong i = 0; i < index; list = list.next, ++i) ;
            list.next = null;
            tail = list;
            length = index;
        }

        /// <summary>
        /// 删除索引所在的数据
        /// </summary>
        /// <param name="index">索引</param>
        public void RemoveAt(ulong index)
        {
            if (index > Length)
                throw new Exception("索引过大。");

            LinkedList<T> list = head;
            for (ulong i = 0; i < index; ++i)
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
            if (Length == 1)
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
            if (Length == 1)
                throw new Exception("数据过少。");
            --Length;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        public void Append(T data)
        {
            ++Length;
            if (head.next == null)
                head.next = tail;
            head[Length - 1] = data;
        }

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>get到的值</returns>
        public T this[ulong index]
        {
            get
            {
                if (index >= Length)
                    throw new Exception("索引过大。");
                LinkedList<T> list = head;
                for (ulong i = 0; i < index; ++i)
                    list = list.next;
                return list.Data;
            }
            set
            {
                if (index >= Length)
                    throw new Exception("索引过大。");
                LinkedList<T> list = head;
                for (ulong i = 0; i < index; ++i)
                    list = list.next;
                list.Data = value;
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
            LinkedList<T> linked = head;
            for (ulong i = 0; i < Length; ++i)
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
        /// <param name="end">长度</param>
        /// <returns>截取到的List</returns>
        public List<T> Sub(ulong start, ulong length)
        {
            if (start + length > Length + 1)
                throw new Exception("参数错误");
            LinkedList<T> list = head;
            for (ulong i = 0; i < start; ++i)
                list = list.next;
            List<T> result = new List<T>();
            for (ulong i = 0; i < length; ++i)
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
        public void Insert(ulong index, T data)
        {
            if (index >= Length - 1)
                Length = index + 2;
            LinkedList<T> list = head;
            for (ulong i = 0; i < index; ++i)
                list = list.next;
            LinkedList<T> insert = new LinkedList<T>();
            insert.Data = data;
            insert.next = list.next;
            insert.last = list;
            list.next = insert;
        }

        /// <summary>
        /// 查找数据并返回索引，如未查找到就返回false
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="index">索引</param>
        /// <returns>是否查找到</returns>
        public bool Search(T data, out ulong index)
        {
            LinkedList<T> list = head;
            for (ulong i = 0; i < Length; ++i)
            {
                if (list.Data.Equals(data))
                {
                    index = i;
                    return true;
                }
                list = list.next;
            }
            index = 0;
            return false;
        }
    }
}