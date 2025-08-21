// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.Linq;
using System.Collections;
using CommonLibrary.Enums;
using CommonLibrary.Helpers;
using System.ComponentModel;
using CommonLibrary.Attributes;
using System.Collections.Generic;

namespace CommonLibrary.Collections
{
    /// <summary>
    /// 
    ///  EN:
    ///    Associative array of key value pairs, sorted by default.
    ///    Default capacity is 2 pairs and maximum capacity is 1 000 000 000 pairs.
    ///    The KeyValueCollection like others associative arrays is indexed by a key.
    ///    The indexer return the value associated to that key in the internal key-value pair.
    ///  
    ///  BG:
    ///    KeyValueCollection представлява асоциативен масив от двойки ключ-стойност, който по подразбиране е сортиран.
    ///    Стандартен капацитет: 2 двойки ключ-стойност.
    ///    Максимален капацитет: 1 000 000 000 двойки ключ-стойност.
    ///    Подобно на други асоциативни структури от данни, KeyValueCollection използва ключ за достъп до стойностите.
    ///    Чрез индексатора се връща стойността, асоциирана с дадения ключ.
    ///  
    /// </summary>
    /// 
    /// <typeparam name="KeyType">
    ///  EN: The data type of the keys.
    ///  BG: Типа данни на ключовете.
    /// </typeparam>
    /// 
    ///<typeparam name="ValueType">
    /// EN: The data type of the values.
    /// BG: Типа данни на асоцийраните стойности.
    /// </typeparam>
    [Author("Tsvetelin Marinov")]
    [Description("Collection of key-value pairs")]
    public class KeyValueCollection<KeyType, ValueType> : IEnumerable<KeyValuePair<KeyType, ValueType>>, ICloneable
    {
        //The key-value pairs of the collection.
        //Двойките ключ-стойност на колекцията(в колекция разбира се..).
        private Collection<KeyValuePair<KeyType, ValueType>>? _pairs;

        //The default capacity of the collection.
        //Капацитет по подразбиране от 2 двойки ключ-стойност.
        private const int DEFCAP = 2;

        //Maximum capacity of the collection.
        //Максимален капацитет от един милиард двойки ключ-стойност.
        private const int MAXCAP = 1_000_000_000;


        /// <summary>
        /// 
        /// EN: Indexer.
        /// 
        /// BG: Индексатор
        /// 
        /// </summary>
        /// 
        /// <param name="key">
        /// EN: The key of the key-value pair
        /// BG: Ключа от двойката ключ-стойност
        /// </param>
        /// 
        /// <returns>
        /// EN: The value associated with that key
        /// BG: Стойността към този ключ
        /// </returns>
        public ValueType this[KeyType key]
        {
            get => GetValueByKey(key);
        }


        /// <summary>
        /// 
        /// EN: Get the count of the key-value pairs.
        /// 
        /// BG: Връща броя на двойките ключ-стойност.
        /// 
        /// </summary>
        public int Count
        {
            get => _pairs!.Count;
        }

        /// <summary>
        /// 
        /// EN: Get or set the capacity of the collection.
        /// 
        /// BG: Достъпва капацитета на колекцията.
        /// 
        /// </summary>
        public int Capacity
        {
            get => _pairs!.Capacity;
            set => _pairs!.Capacity = value; // EN: No validation here, because the inner collection already handles it.
                                             // BG: Няма валидация тук, защото вътрешната колекция вече има.
        }

        /// <summary>
        /// 
        /// EN: Get the keys of the collection in a collection of keys.
        /// 
        /// BG: Връща всички ключове от колекцията в колекция от ключове.
        /// 
        /// </summary>
        public Collection<KeyType> Keys
            => GetAllKeys();

        /// <summary>
        /// 
        /// EN: Get the associated values of the collection in a collection of values.
        /// 
        /// BG: Връща всички асоциирани стойности от колекцията в колекция от стойности.
        /// 
        /// </summary>
        public Collection<ValueType> Values
            => GetAssociatedValues();

        /// <summary>
        /// 
        /// EN: Create a new empty collection of key-value pairs with default capacity.
        /// 
        /// BG: Създава нова празна колекция от двойки ключ-стойност със стандартен капацитет.
        /// 
        /// </summary>
        public KeyValueCollection()
            => _pairs = new Collection<KeyValuePair<KeyType, ValueType>>(DEFCAP);

        /// <summary>
        /// 
        /// EN: Create a new empty collection of key-value pairs with specified capacity.
        /// 
        /// BG: Създава нова празна колекция от двойки ключ-стойност със зададен капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="capacity">
        /// EN: The capacity  
        /// BG: Капацитетът
        /// </param>
        public KeyValueCollection(int capacity)
            => _pairs = new Collection<KeyValuePair<KeyType, ValueType>>(capacity);

        /// <summary>
        /// 
        /// EN: Create a new empty collection of key-value pairs with maximum capacity if specified.
        /// 
        /// BG: Създава нова празна колекция от двойки ключ-стойност с максимален капацитет, ако е указано.
        /// 
        /// </summary>
        /// 
        /// <param name="maxCapacity">
        /// EN: Indicates whether to use maximum capacity  
        /// BG: Указва дали да се използва максимален капацитет
        /// </param>
        public KeyValueCollection(bool maxCapacity = false)
            : this()
        {
            if (maxCapacity)
            {
                Capacity = MAXCAP;
            }
        }

        /// <summary>
        /// 
        /// EN: Create a collection of key-value pairs by copying pairs from another collection.  
        /// Capacity is set to the number of copied pairs.
        /// 
        /// BG: Създава колекция от двойки ключ-стойност чрез копиране от друга колекция.  
        /// Капацитетът се задава според броя на копираните двойки.
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        /// EN: The collection from which key-value pairs will be copied  
        /// BG: Колекцията, от която ще се копират двойките ключ-стойност
        /// </param>
        public KeyValueCollection(IEnumerable<KeyValuePair<KeyType, ValueType>> collection)
            => CreateFromExternalCollection(collection);

        /// <summary>
        /// 
        /// EN: Create a collection of key-value pairs by copying pairs from another collection  
        /// and setting a specified capacity.
        /// 
        /// BG: Създава колекция от двойки ключ-стойност чрез копиране от друга колекция  
        /// и със зададен капацитет.
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        /// EN: The collection from which key-value pairs will be copied  
        /// BG: Колекцията, от която ще се копират двойките ключ-стойност
        /// </param>
        /// 
        /// <param name="capacity">
        /// EN: The new capacity  
        /// BG: Новият капацитет
        /// </param>
        public KeyValueCollection(IEnumerable<KeyValuePair<KeyType, ValueType>> collection, int capacity)
            : this(collection)
        {
            Capacity = capacity;
        }

        /// <summary>
        /// 
        /// EN: Create a collection of key-value pairs by copying pairs from another collection.  
        /// If maxCapacity is true, maximum capacity is used.  
        /// Otherwise, capacity is set to the number of copied pairs.
        /// 
        /// BG: Създава колекция от двойки ключ-стойност чрез копиране от друга колекция.  
        /// Ако maxCapacity е true, се използва максимален капацитет.  
        /// В противен случай капацитетът се задава според броя на копираните двойки.
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        /// EN: The collection from which key-value pairs will be copied  
        /// BG: Колекцията, от която ще се копират двойките ключ-стойност
        /// </param>
        /// 
        /// <param name="maxCapacity">
        /// EN: Indicates whether to use maximum capacity  
        /// BG: Указва дали да се използва максимален капацитет
        /// </param>
        public KeyValueCollection(IEnumerable<KeyValuePair<KeyType, ValueType>> collection, bool maxCapacity)
            : this(collection)
        {
            if (maxCapacity)
            {
                Capacity = MAXCAP;
            }
        }



        /// <summary>
        /// 
        /// EN: Adds key-value pair in the collection with specified key and associated value to that key.
        /// 
        /// BG: Добавя двойка ключ-стойност в колекцията със зададен ключ и съответната стойност.
        /// 
        /// </summary>
        /// 
        /// <param name="key">
        /// EN: The key of the pair  
        /// BG: Ключът на двойката
        /// </param>
        /// 
        /// <param name="value">
        /// EN: The associated value of the key  
        /// BG: Стойността, асоциирана с ключа
        /// </param>
        public void Add(KeyType key, ValueType value)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(value);

            _pairs!.Add(new KeyValuePair<KeyType, ValueType>(key, value));
        }

        /// <summary>
        /// 
        /// EN: Adds the specified key-value pair in the collection.
        /// 
        /// BG: Добавя зададената двойка ключ-стойност в колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="pair">
        /// EN: The key-value pair  
        /// BG: Двойката ключ-стойност
        /// </param>
        public void Add(KeyValuePair<KeyType, ValueType> pair)
        {
            ArgumentNullException.ThrowIfNull(pair);
            _pairs!.Add(pair);
        }

        /// <summary>
        /// 
        /// EN: Adds a collection (array) of key-value pairs to the collection.
        /// 
        /// BG: Добавя колекция (масив) от двойки ключ-стойност към колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="collection">
        /// EN: The collection whose key-value pairs will be added  
        /// BG: Колекцията, чиито двойки ключ-стойност ще бъдат добавени
        /// </param>
        public void AddCollection(IEnumerable<KeyValuePair<KeyType, ValueType>> collection)
            => _pairs!.AddCollection(collection.ToList());

        /// <summary>
        /// 
        /// EN: Removes the key-value pair at the specified index in the collection.
        /// 
        /// BG: Премахва двойката ключ-стойност на зададения индекс в колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="index">
        /// EN: The index of the key-value pair  
        /// BG: Индексът на двойката ключ-стойност
        /// </param>
        public void RemoveByIndex(int index)
            => _pairs!.RemoveElementAt(index);

        /// <summary>
        /// 
        /// EN: Removes the key-value pair that contains the specified key.
        /// 
        /// BG: Премахва двойката ключ-стойност, която съдържа зададения ключ.
        /// 
        /// </summary>
        /// 
        /// <param name="key">
        /// EN: The key of the pair  
        /// BG: Ключът на двойката
        /// </param>
        public void RemoveByKey(KeyType key)
        {
            for (int i = 0; i < _pairs!.Count; i++)
            {
                if (_pairs![i].Key!.Equals(key))
                {
                    _pairs!.RemoveElementAt(i);
                }
            }
        }

        /// <summary>
        /// 
        /// EN: Removes all key-value pairs in the specified range in the collection.
        /// 
        /// BG: Премахва всички двойки ключ-стойност в зададения диапазон от колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="startIndex">
        /// EN: The starting index in the collection  
        /// BG: Началният индекс в колекцията
        /// </param>
        /// 
        /// <param name="endIndex">
        /// EN: The ending index in the collection  
        /// BG: Крайният индекс в колекцията
        /// </param>
        public void RemoveAllInRange(int startIndex, int endIndex)
            => _pairs!.RemoveInDiapason(startIndex, endIndex);

        /// <summary>
        /// 
        /// EN: Removes all key-value pairs from the collection.
        /// 
        /// BG: Премахва всички двойки ключ-стойност от колекцията.
        /// 
        /// </summary>
        public void RemoveAll()
            => _pairs!.TruncateCollection();

        /// <summary>
        /// 
        /// EN: Checks if the specified key exists in the collection.
        /// 
        /// BG: Проверява дали зададеният ключ съществува в колекцията.
        /// 
        /// </summary>
        /// 
        /// <param name="key">
        /// EN: The key  
        /// BG: Ключът
        /// </param>
        /// 
        /// <returns>
        /// EN: True if the key exists in the collection, otherwise false  
        /// BG: Връща true, ако ключът съществува в колекцията, иначе false
        /// </returns>
        public bool HasKey(KeyType key)
        {
            foreach (KeyValuePair<KeyType, ValueType> pair in _pairs!)
            {
                if (pair.Key!.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// EN: Reduces the capacity of the collection to match the number of pairs.
        /// 
        /// BG: Намалява капацитета на колекцията до броя на двойките.
        /// 
        /// </summary>
        public void TrimCapacity()
            => _pairs!.RemoveExcessCapacity();

        /// <summary>
        /// 
        /// EN: Reduces the capacity of the collection to the specified value.
        /// 
        /// BG: Намалява капацитета на колекцията до зададената стойност.
        /// 
        /// </summary>
        /// 
        /// <param name="capacity">
        /// EN: The new capacity  
        /// BG: Новият капацитет
        /// </param>
        public void ReduceCapacityTo(int capacity)
            => _pairs!.ReduceCapacityTo(capacity);

        /// <summary>
        /// 
        /// EN: Sorts the collection by keys in the specified order (default is ascending).
        /// 
        /// BG: Сортира колекцията по ключове в зададения ред (по подразбиране възходящ).
        /// 
        /// </summary>
        /// 
        /// <param name="options">
        /// EN: The sorting options  
        /// BG: Опциите за сортиране
        /// </param>
        public void SortCollectionByKey(SortingOptions options = SortingOptions.Ascending)
        {
            if (options == SortingOptions.Ascending)
            {
                List<KeyValuePair<KeyType, ValueType>> localPairs = _pairs!.ToList();

                localPairs = localPairs
                    .OrderBy(pair => pair.Key)
                    .ToList();

                _pairs = CollectionHelper.ReturnAsCollection(localPairs);
            }
            else if (options == SortingOptions.Descending)
            {
                List<KeyValuePair<KeyType, ValueType>> pairs = _pairs!.ToList();

                pairs = pairs
                    .OrderByDescending(pair => pair.Key)
                    .ToList();

                _pairs = CollectionHelper.ReturnAsCollection(pairs);
            }
        }

        /// <summary>
        /// 
        /// EN: Sorts the collection by values in the specified order (default is ascending).
        /// 
        /// BG: Сортира колекцията по стойности в зададения ред (по подразбиране възходящ).
        /// 
        /// </summary>
        /// 
        /// <param name="options">
        /// EN: The sorting options  
        /// BG: Опциите за сортиране
        /// </param>
        public void SortCollectionByValue(SortingOptions options = SortingOptions.Ascending)
        {
            if (options == SortingOptions.Ascending)
            {
                List<KeyValuePair<KeyType, ValueType>> pairs = _pairs!.ToList();

                pairs = pairs
                    .OrderBy(pair => pair.Value)
                    .ToList();

                _pairs = CollectionHelper.ReturnAsCollection(pairs);
            }
            else if (options == SortingOptions.Descending)
            {
                List<KeyValuePair<KeyType, ValueType>> pairs = _pairs!.ToList();

                pairs = pairs
                    .OrderByDescending(pair => pair.Value)
                    .ToList();

                _pairs = CollectionHelper.ReturnAsCollection(pairs);
            }
        }

        /// <summary>
        /// 
        /// EN: Clones the collection.
        /// 
        /// BG: Клонира колекцията.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// EN: The collection as an object  
        /// BG: Колекцията като обект
        /// </returns>
        public object Clone()
            => this;



        //Extracts all the keys from the collections key-value pairs.
        //
        //Извлича вкички ключове от двойките ключ-стойност на колекцията в колекция от ключове.
        private Collection<KeyType> GetAllKeys()
        {
            Collection<KeyType> keys = new(Count);

            foreach (KeyValuePair<KeyType, ValueType> pair in _pairs!)
            {
                keys.Add(pair.Key);
            }

            return keys;
        }

        //Extracts all the values form the collection key-value pairs.
        //
        //Извлича вкички асоциирани стойности от двойките ключ-стойност на колекцията в колекция от стойности.
        private Collection<ValueType> GetAssociatedValues()
        {
            Collection<ValueType> values = new(Count);

            foreach (KeyValuePair<KeyType, ValueType> pair in _pairs!)
            {
                values.Add(pair.Value);
            }

            return values;
        }

        //Copies the key-value pairs from another collection to the current collection.
        //
        //Копира двойките ключ-стойност от външна колекция в локалната.
        //Тази команда е предназначена да създаде вътрешната колекция от двойки ключ-стойност с копие на
        //дойките ключ-стойност на външна колекция и се изпълнява, когато се извика конструктора
        //с параметър IEnumerable (външна колекция).
        private void CreateFromExternalCollection(IEnumerable<KeyValuePair<KeyType, ValueType>> externalCollection)
        {
            _pairs = new Collection<KeyValuePair<KeyType, ValueType>>(externalCollection.Count());

            foreach (KeyValuePair<KeyType, ValueType> pair in externalCollection)
            {
                _pairs!.Add(pair);
            }

            _pairs!.RemoveExcessCapacity();
        }

        //Get the value associated with the specified key, but first validates the key
        //and checks if the key is not already in the collection.
        //That methods is called by the indexer accessor and returns the value associated to the key.
        //My return the default value for the value data type, when the collection does not contains 
        //key-value pair with the specified key.
        //
        //Този метод е предназначен за аксесора на Индексатора и достъпва асоциираната стойност към дадения ключ,
        //като първо валидира ключа и проверява дали изобщо ключа се съдържа в колекцията. 
        private ValueType GetValueByKey(KeyType key)
        {
            ArgumentNullException.ThrowIfNull(key);

            foreach (KeyValuePair<KeyType, ValueType> pair in _pairs!)
            {
                if (pair.Key!.Equals(key))
                {
                    return pair.Value;
                }
            }

            return default!;
        }


        #region Enumerator

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<KeyType, ValueType>> GetEnumerator()
            => _pairs!.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion
    }
}
