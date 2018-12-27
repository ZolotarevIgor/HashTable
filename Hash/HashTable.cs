using System;
using System.Collections.Generic;
using static System.Math;
using System.Linq;

namespace HashTable
{
    class Hash
    {
        public interface IHash<T>
        {
            int Count { get; }
            int Size { get; }
            T HashSearch(T element);
            T[][] Hash { get; }
            T[] AllElements { get; }
            int[] AllKeys { get; }
            bool Add(T element);
            bool Remove(T element);

        }
        public enum TypeHash { Open, Closed };
        public enum HashFunction { Module, Mult, Center } //метод деления, мультипликативный, середины квадрата
        public enum ClosedHashType { Linear, Square, Double } //линейное, квадратичное опробование, двойное хеширование

        public static TypeHash nTypeHash = TypeHash.Open;
        public static HashFunction nOpenHashType = HashFunction.Module;
        public static ClosedHashType nClosedHashType = ClosedHashType.Linear;
        public static HashFunction nDoubleHashing = HashFunction.Mult;

        public class HashClass<T>
        {
            protected int size;
            protected int count;
            protected HashFunction HFunction;
            protected T[] Array;
            protected delegate int ComputeHashFunction(T Element, int Size);
            protected static int FindPrimeNumber(int LeftBorder) //функция поиска первого простого числа на отрезке [LeftBorder; +inf]
            {
                if (LeftBorder < 0)
                    return -1;
                for (int i = LeftBorder; i < int.MaxValue; i++)
                    if (IsSimple(i))
                        return i;
                return -1;
            }
            protected static bool IsSimple(int N)//проверяет, является ли число простым
            {
                if (N <= 0)
                    return false;
                //чтоб убедиться простое число или нет достаточно проверить, делится ли число на числа до его корня
                for (int i = 2; i <= Sqrt(N); i++)
                {
                    if (N % i == 0)
                        return false;
                }
                return true;
            }
            protected static int ComputeHashModule(T Element, int Module) //функция вычисляет хеш методом деления на перевода Element в int и деления на Module
            {
                try
                {
                    if (Element as string == "Пусто")
                        return -1;
                    if (typeof(T) == "".GetType())
                        return Abs(int.Parse(Element as string)) % Module;
                    return Abs(Element.GetHashCode()) % Module;
                }
                catch
                {
                    return -1;
                }
            }
            protected static int ComputeHashMult(T Element, int Size) //вычисление хеш мультипликативным методом: 2^M*{Element*(sqrt(5)-1)/2}
            {
                try
                {
                    if (Element as string == "Пусто")
                        return -1;
                    const double K = 0.618033988749895; //золотое сечение
                    if (typeof(T) == "".GetType())
                        return (int)(Size * ((Abs(int.Parse(Element as string)) * K) % 1));

                    return (int)(Size * ((Abs(Element.GetHashCode()) * K) % 1));
                }
                catch
                {
                    return -1;
                }
            }
            protected static int ComputeHashCenter(T Element, int Size) //метод середины квадрата
            {
                if (Element as string == "Пусто")
                    return -1;
                long key = Element.GetHashCode();
                if (typeof(T) == "".GetType())
                {
                    if (Element as string == "Пусто")
                        return -1;
                    key = int.Parse(Element as string);
                }
                key *= key;//возводим в квадрат
                key >>= (int)Abs((Log(key, 2) - Log(Size, 2)) / 2);
                return (int)(key % Size); // Возвращаем младших бит
            }

        }
        public class OpenHashTable<T> : HashClass<T>, IHash<T> where T: class //открытое хеширование
        {
            List<T>[] HashArray;
            public int Count { get { return count; } }
            public int Size { get { return size; } }
            public T[][] Hash
            {
                get
                {
                    T[][] H = new T[HashArray.Length][];
                    for (int i = 0; i < HashArray.Length; i++)
                    {
                        if (HashArray[i] != null)
                            H[i] = HashArray[i].ToArray();
                        else
                        {
                            H[i] = new T[1];
                            H[i][0] = Default<T>();
                        }
                    }
                    return H;
                }
            }
            public T[] AllElements
            {
                get
                {
                    return Array;
                }
            }
            public int[] AllKeys
            {
                get
                {
                    int[] Arr = new int[Array.Length];
                    ComputeHashFunction ComputeHash = ComputeHashModule; //устанавливаем значение по умолчанию
                    switch (HFunction)
                    {
                        case HashFunction.Module: ComputeHash = ComputeHashModule; break;
                        case HashFunction.Mult: ComputeHash = ComputeHashMult; break;
                        case HashFunction.Center: ComputeHash = ComputeHashCenter; break;
                    }
                    for (int i = 0; i < Array.Length; i++)
                        Arr[i] = ComputeHash(Array[i], Size);
                    return Arr;
                }
            }
            public new HashFunction GetType
            {
                get { return HFunction; }
            }
            public OpenHashTable(T[] Array, HashFunction Type = HashFunction.Module)
            {
                this.Array = Array;
                HFunction = Type;
                count = Array.Length;
                ComputeHashFunction ComputeHash = ComputeHashModule; //устанавливаем значение по умолчанию
                try
                {
                    switch (Type)
                    {
                        case HashFunction.Module:
                            {
                                int Module = FindPrimeNumber(Array.Length);
                                size = Module;
                                ComputeHash = ComputeHashModule;
                            }
                            break;
                        case HashFunction.Mult:
                            {
                                int N = (int)Truncate(Log(Array.Length, 2.0)) + 1; //N=log2(M)+1
                                size = (int)Pow(2, N); //здесь получается размер, равный 2^N для того, чтобы поместились все члены исходного массива
                                ComputeHash = ComputeHashMult;
                            }
                            break;
                        case HashFunction.Center:
                            {
                                int N = (int)Truncate(Log(Array.Length, 2.0)) + 1; //N=log2(M)+1
                                size = (int)Pow(2, N);
                                ComputeHash = ComputeHashCenter;
                            }
                            break;
                    }
                    HashArray = new List<T>[size];
                    foreach (T Element in Array)
                    {
                        int hash = ComputeHash(Element, size);
                        if (hash < 0)
                            break;
                        if (HashArray[hash] == null)
                            HashArray[hash] = new List<T>();
                        HashArray[hash].Add(Element);
                    }
                    HashSearch(Array[0]);
                }
                catch (Exception ex)
                {
                   string error = ex.Message;
                }
            }
            public T HashSearch(T element)
            {
                try
                {
                    ComputeHashFunction ComputeHash = ComputeHashModule; //устанавливаем значение по умолчанию
                    switch (HFunction)
                    {
                        case HashFunction.Module: ComputeHash = ComputeHashModule; break;
                        case HashFunction.Mult: ComputeHash = ComputeHashMult; break;
                        case HashFunction.Center: ComputeHash = ComputeHashCenter; break;
                    }

                    int hash = ComputeHash(element, size);
                    if (hash < 0)
                        throw new Exception("Отрицательный хеш");
                    if (HashArray[hash] == null)
                        return default(T);
                    foreach (T el in HashArray[hash])
                        if (el.Equals(element))
                            return el;
                    return default(T);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return default(T);
                }

            }
            public bool Remove(T element)
            {
                ComputeHashFunction ComputeHash = null;
                switch (HFunction)
                {
                    case HashFunction.Module: ComputeHash = ComputeHashModule; break;
                    case HashFunction.Mult: ComputeHash = ComputeHashMult; break;
                    case HashFunction.Center: ComputeHash = ComputeHashCenter; break;
                }
                
                int Hash = ComputeHash(element, size);
                if (Hash < 0)
                    return false;
                if (HashArray[Hash] == null || Hash < 0)
                    return false;
                else
                {
                    count--;
                    if (HashArray[Hash].Remove(element))
                    {
                        int numIndex = System.Array.IndexOf(Array, element);
                        Array = Array.Where((val, idx) => idx != numIndex).ToArray();

                        if (HashArray[Hash].Count == 0)
                        {
                            HashArray[Hash].Add(Default<T>());
                            return true;
                        }

                    }
                    return false;
                }
            }
            public bool Add(T element)
            {
                ComputeHashFunction ComputeHash = null;
                int NSize = 0;
                switch (HFunction)
                {
                    case HashFunction.Module:
                        {
                            NSize = FindPrimeNumber(count + 1);
                            ComputeHash = ComputeHashModule;
                        }
                        break;
                    case HashFunction.Mult:
                        {
                            int N = (int)Truncate(Log(count + 1, 2.0)) + 1; //N=log2(M)+1
                            NSize = (int)Pow(2, N); //здесь получается размер, равный 2^N для того, чтобы поместились все члены исходного массива
                            ComputeHash = ComputeHashMult;
                        }
                        break;
                    case HashFunction.Center:
                        {
                            int N = (int)Truncate(Log(count + 1, 2.0)) + 1; //N=log2(M)+1
                            NSize = (int)Pow(2, N);
                            ComputeHash = ComputeHashCenter;
                        }
                        break;
                }
                int hash = ComputeHash(element, size);
                if (hash < 0)
                    return false;
                if (size < NSize)
                {
                    Resize(element);
                    return true;
                }
                System.Array.Resize(ref Array, Array.Length + 1);
                Array[Array.Length - 1] = element;

                if (HashArray[hash] == null)
                    HashArray[hash] = new List<T>();
                HashArray[hash].Add(element);
                count++;
                return true;
            }
            private void Resize(params T[] elements)
            {
                if (elements.Length != 0)
                    count += elements.Length;
                int oldLen = Array.Length;
                System.Array.Resize(ref Array, Array.Length + elements.Length);
                for (int i = oldLen; i < Array.Length; i++)
                    Array[i] = elements[i - oldLen];
                
                List<T> TList = new List<T>();
                foreach (T el in elements)
                    TList.Add(el);
                foreach (List<T> L in HashArray)
                    if (L != null)
                        foreach (T el in L)
                            if (el != null)
                                TList.Add(el);
                ComputeHashFunction ComputeHash = null;
                switch (HFunction)
                {
                    case HashFunction.Module:
                        {
                            int Module = FindPrimeNumber(count);
                            size = Module;
                            ComputeHash = ComputeHashModule;
                        }
                        break;
                    case HashFunction.Mult:
                        {
                            int N = (int)Truncate(Log(count, 2.0)) + 1; //N=log2(M)+1
                            size = (int)Pow(2, N); //здесь получается размер, равный 2^N для того, чтобы поместились все члены исходного массива
                            ComputeHash = ComputeHashMult;
                        }
                        break;
                    case HashFunction.Center:
                        {
                            int N = (int)Truncate(Log(count, 2.0)) + 1; //N=log2(M)+1
                            size = (int)Pow(2, N);
                            ComputeHash = ComputeHashCenter;
                        }
                        break;
                }
                HashArray = new List<T>[size];
                foreach (T Element in TList)
                {
                    if (Element as string != "Пусто")
                    {
                        int hash = ComputeHash(Element, size);
                        if (HashArray[hash] == null)
                            HashArray[hash] = new List<T>();
                        HashArray[hash].Add(Element);
                    }
                }
                HashSearch(TList[0]);
            }
        }
        public class ClosedHashTable<T> : HashClass<T>, IHash<T> where T: class
        {
            private ClosedHashType Type { get; }
            T[] HashArray;
            bool[] Deleted;
            private readonly HashFunction DoubleHashing;
            public int Count { get { return count; } }
            public int Size { get { return size; } }
            public T[][] Hash
            {
                get
                {
                    T[][] H = new T[HashArray.Length][];
                    for (int i = 0; i < HashArray.Length; i++)
                    {
                        H[i] = new T[1];
                        if (HashArray[i] != null)
                            H[i][0] = HashArray[i];
                        else H[i][0] = Default<T>();
                    }
                    return H;
                }
            }
            public T[] AllElements
            {
                get
                {
                    return Array;
                }
            }
            public int[] AllKeys
            {
                get
                {
                    int[] Arr = new int[Array.Length];
                    ComputeHashFunction ComputeHash = ComputeHashModule; //устанавливаем значение по умолчанию
                    switch (HFunction)
                    {
                        case HashFunction.Module: ComputeHash = ComputeHashModule; break;
                        case HashFunction.Mult: ComputeHash = ComputeHashMult; break;
                        case HashFunction.Center: ComputeHash = ComputeHashCenter; break;
                    }
                    for (int i = 0; i < Array.Length; i++)
                        Arr[i] = ComputeHash(Array[i], Size);
                    return Arr;
                }
            }
            public ClosedHashTable(T[] Array, ClosedHashType Type, HashFunction HF = HashFunction.Module, HashFunction DH = HashFunction.Mult)
            {
                this.Array = Array;
                HFunction = HF;
                DoubleHashing = DH;
                this.Type = Type;
                count = Array.Length;
                ComputeHashFunction ComputeHash = ComputeHashModule; //устанавливаем значение по умолчанию
                ComputeHashFunction DoubleHash = ComputeHashMult;
                try
                {
                    switch (HFunction)
                    {
                        case HashFunction.Module:
                            {
                                int Module = FindPrimeNumber((int)(Array.Length*1.5));
                                size = Module;
                                ComputeHash = ComputeHashModule;
                            }
                            break;
                        case HashFunction.Mult:
                            {
                                int N = (int)Truncate(Log(1.5*Array.Length, 2.0)) + 1; //N=log2(M)+1
                                size = (int)Pow(2, N); //здесь получается размер, равный 2^N для того, чтобы поместились все члены исходного массива
                                ComputeHash = ComputeHashMult;
                            }
                            break;
                        case HashFunction.Center:
                            {
                                int N = (int)Truncate(Log(1.5*Array.Length, 2.0)) + 1; //N=log2(M)+1
                                size = (int)Pow(2, N);
                                ComputeHash = ComputeHashCenter;
                            }
                            break;
                    }
                    switch (DoubleHashing)
                    {
                        case HashFunction.Module:
                            DoubleHash = ComputeHashModule;
                            break;
                        case HashFunction.Mult:
                            DoubleHash = ComputeHashMult;
                            break;
                        case HashFunction.Center:
                            DoubleHash = ComputeHashCenter;
                            break;
                    }
                    Deleted = new bool[size];
                    HashArray = new T[size];
                    foreach (T el in Array)
                    {
                        int h = ComputeHashModule(el, size);
                        if (h < 0)
                            throw new Exception("Отрицательный хеш");
                        int i = 0;
                        bool end = false;
                        while (!end)
                        {
                            if (HashArray[h] == null || Deleted[h])
                            {
                                HashArray[h] = el;
                                end = true;
                            }
                            else switch (Type)
                                {
                                    case ClosedHashType.Linear:
                                        {
                                            i+=2;
                                            h = (h + i) % size;
                                        }
                                        break;
                                    case ClosedHashType.Square:
                                        {
                                            i++;
                                            h = (h + i * i) % size;
                                        }
                                        break;
                                    case ClosedHashType.Double:
                                        {
                                            i++;
                                            h = (h + i * (DoubleHash(el, size) + 1));
                                            if (h >= size)
                                                h = (h % size) + 1;
                                        }
                                        break;
                                }
                        }
                    }
                    HashSearch(Array[0]);
                }
                catch (Exception ex)
                {
                    string Error = ex.Message;
                }

            }
            public T HashSearch(T element)
            {
                try
                {
                    ComputeHashFunction ComputeHash = ComputeHashModule; //устанавливаем значение по умолчанию
                    ComputeHashFunction DoubleHash = ComputeHashMult;
                    switch (HFunction)
                    {
                        case HashFunction.Module:
                            ComputeHash = ComputeHashModule;
                            break;
                        case HashFunction.Mult:
                            ComputeHash = ComputeHashMult;
                            break;
                        case HashFunction.Center:
                            ComputeHash = ComputeHashCenter;
                            break;
                    }
                    switch (DoubleHashing)
                    {
                        case HashFunction.Module:
                            DoubleHash = ComputeHashModule;
                            break;
                        case HashFunction.Mult:
                            DoubleHash = ComputeHashMult;
                            break;
                        case HashFunction.Center:
                            DoubleHash = ComputeHashCenter;
                            break;
                    }

                    int h = ComputeHash(element, size);//значение хеша
                    if (HashArray[h] == null)
                        return default(T);
                    if (h < 0)
                        throw new Exception("Отрицательный хеш");
                    int i = 0; //шаг
                    while (HashArray[h] != null || Deleted[h])
                    {
                        if (HashArray[h].Equals(element))
                            return element;
                        else switch (Type)
                            {
                                case ClosedHashType.Linear:
                                    {
                                        i += 2;
                                        h = (h + i) % size;
                                    }
                                    break;
                                case ClosedHashType.Square:
                                    {
                                        i++;
                                        h = (h + i * i) % size;
                                    }
                                    break;
                                case ClosedHashType.Double:
                                    {
                                        i++;
                                        h = (h + i * DoubleHash(element, size)) % size;
                                    }
                                    break;
                            }
                    }
                    return default(T);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return default(T);
                }
            }
            public bool Remove(T element)
            {
                ComputeHashFunction ComputeHash = ComputeHashModule; //устанавливаем значение по умолчанию
                ComputeHashFunction DoubleHash = ComputeHashMult;
                switch (HFunction)
                {
                    case HashFunction.Module:
                        ComputeHash = ComputeHashModule;
                        break;
                    case HashFunction.Mult:
                        ComputeHash = ComputeHashMult;
                        break;
                    case HashFunction.Center:
                        ComputeHash = ComputeHashCenter;
                        break;
                }
                switch (DoubleHashing)
                {
                    case HashFunction.Module:
                        DoubleHash = ComputeHashModule;
                        break;
                    case HashFunction.Mult:
                        DoubleHash = ComputeHashMult;
                        break;
                    case HashFunction.Center:
                        DoubleHash = ComputeHashCenter;
                        break;
                }
                int h = ComputeHash(element, size);//значение хеша
                if (h < 0)
                    return false;
                if (HashArray[h] == null)
                    return false;
                else
                {
                    HashArray[h] = Default<T>();
                    Deleted[h] = true;
                    count--;
                    int numIndex = System.Array.IndexOf(Array, element);
                    Array = Array.Where((val, idx) => idx != numIndex).ToArray();
                }
                return true;
            }
            public bool Add(T element)
            {
                try
                {
                    
                    ComputeHashFunction ComputeHash = ComputeHashModule; //устанавливаем значение по умолчанию
                    ComputeHashFunction DoubleHash = ComputeHashMult;
                    switch (HFunction)
                    {
                        case HashFunction.Module:
                            {
                                ComputeHash = ComputeHashModule;
                                if (FindPrimeNumber((int)(1.5 * (count + 1))) >= size)
                                {
                                    Resize(element);
                                    return true;
                                }
                            }
                            break;
                        case HashFunction.Mult:
                            {
                                ComputeHash = ComputeHashMult;
                                int N = (int)Truncate(Log(1.5 * (count+1), 2.0)) + 1; //N=log2(M)+1
                                if((int)Pow(2, N)>=size)
                                {
                                    Resize(element);
                                    return true;
                                }
                            }
                            break;
                        case HashFunction.Center:
                            {
                                ComputeHash = ComputeHashCenter;

                                int N = (int)Truncate(Log(1.5 * (count + 1), 2.0)) + 1; //N=log2(M)+1
                                if ((int)Pow(2, N) >= size)
                                {
                                    Resize(element);
                                    return true;
                                }
                            }
                            break;
                    }
                    switch (DoubleHashing)
                    {
                        case HashFunction.Module:
                            DoubleHash = ComputeHashModule;
                            break;
                        case HashFunction.Mult:
                            DoubleHash = ComputeHashMult;
                            break;
                        case HashFunction.Center:
                            DoubleHash = ComputeHashCenter;
                            break;
                    }
                    System.Array.Resize(ref Array, Array.Length + 1);
                    Array[Array.Length - 1] = element;
                    int h = ComputeHash(element, size);//значение хеша
                    if (h < 0)
                        return false;
                    int i = 0;
                    bool end = false;
                    while (!end)
                    {
                        if (HashArray[h] == null || Deleted[h])
                        {
                            HashArray[h] = element;
                            end = true;
                        }
                        else switch (Type)
                            {
                                case ClosedHashType.Linear:
                                    i += 2;
                                    h = (h + i) % size;
                                    break;
                                case ClosedHashType.Square:
                                    {
                                        i++;
                                        h = (h + i * i) % size;
                                    }
                                    break;
                                case ClosedHashType.Double:
                                    {
                                        i++;
                                        h = (h + i * (DoubleHash(element, size))+1) % size;
                                        if (h >= size)
                                            h = (h % size) + 1;
                                    }
                                    break;
                            }
                    }
                    count++;
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    Resize(element);
                    return false;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }
            private void Resize(params T[] elements)
            {
                if (elements.Length != 0)
                    count += elements.Length;
                int oldLen = Array.Length;
                System.Array.Resize(ref Array, Array.Length + elements.Length);
                for (int i = oldLen; i < Array.Length; i++)
                    Array[i] = elements[i - oldLen];

                List<T> TList = new List<T>();
                foreach (T el in elements)
                    TList.Add(el);
                foreach (T el in HashArray)
                    if (el != null)
                        TList.Add(el);

                ComputeHashFunction ComputeHash = ComputeHashModule; //устанавливаем значение по умолчанию
                ComputeHashFunction DoubleHash = ComputeHashMult;
                switch (HFunction)
                {
                    case HashFunction.Module:
                        {
                            int Module = FindPrimeNumber((int)(count * 1.5));
                            size = Module;
                            ComputeHash = ComputeHashModule;
                        }
                        break;
                    case HashFunction.Mult:
                        {
                            int N = (int)Truncate(Log(1.5 * count, 2.0)) + 1; //N=log2(M)+1
                            size = (int)Pow(2, N); //здесь получается размер, равный 2^N для того, чтобы поместились все члены исходного массива
                            ComputeHash = ComputeHashMult;
                        }
                        break;
                    case HashFunction.Center:
                        {
                            int N = (int)Truncate(Log(1.5 * count, 2.0)) + 1; //N=log2(M)+1
                            size = (int)Pow(2, N);
                            ComputeHash = ComputeHashCenter;
                        }
                        break;
                }
                switch (DoubleHashing)
                {
                    case HashFunction.Module:
                        DoubleHash = ComputeHashModule;
                        break;
                    case HashFunction.Mult:
                        DoubleHash = ComputeHashMult;
                        break;
                    case HashFunction.Center:
                        DoubleHash = ComputeHashCenter;
                        break;
                }
                
                Deleted = new bool[size];
                HashArray = new T[size];
                foreach (T el in TList)
                {
                    int h = ComputeHash(el, size);
                    if (h < 0)
                        throw new Exception("Отрицательный хеш");
                    int i = 0;
                    bool end = false;
                    while (!end)
                    {
                        if (HashArray[h] == null || Deleted[h])
                        {
                            HashArray[h] = el;
                            end = true;
                        }
                        else switch (Type)
                            {
                                case ClosedHashType.Linear:
                                    i += 2;
                                    h = (h + i) % size;
                                    break;
                                case ClosedHashType.Square:
                                    {
                                        i++;
                                        h = (h + i * i) % size;
                                    }
                                    break;
                                case ClosedHashType.Double:
                                    {
                                        i++;
                                        h = (h + i * (DoubleHash(el, size)) + 1) % size;
                                        if (h >= size)
                                            h = (h % size) + 1;
                                    }
                                    break;
                            }
                    }
                }
                HashSearch(TList[0]);
            }
        }
        public static IHash<T> CreateHashTable<T>(T[] Array) where T: class
        {
            switch (nTypeHash)
            {
                case TypeHash.Open: return new OpenHashTable<T>(Array, nOpenHashType);
                case TypeHash.Closed: return new ClosedHashTable<T>(Array, nClosedHashType, nOpenHashType, nDoubleHashing);
            }
            return null;
        }
        public static T Default<T>() where T:class
        {
            if (typeof(T)=="".GetType())
                return "Пусто" as T;
            else
                return default(T);
        }
    }
}