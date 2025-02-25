﻿/*
 * Copyright 2020 James Courtney
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Threading;

namespace FlatSharp.Internal;

/// <summary>
/// An implementation of IIndexedVector for use after deserializing an object. This class is not intended to be used
/// directly -- only from code generated by FlatSharp.
/// </summary>
public class FlatBufferIndexedVector<TKey, TValue, TInputBuffer, TVectorItemAccessor> 
    : IIndexedVector<TKey, TValue>

    where TValue : class, ISortableTable<TKey>
    where TKey : notnull
    where TInputBuffer : IInputBuffer
    where TVectorItemAccessor : IVectorItemAccessor<TValue, TInputBuffer>
{
    private FlatBufferDeserializationOption deserializationOption;
    private FlatBufferVectorBase<TValue, TInputBuffer, TVectorItemAccessor> vector;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private FlatBufferIndexedVector()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private void Initialize(FlatBufferVectorBase<TValue, TInputBuffer, TVectorItemAccessor> vector)
    {
        this.deserializationOption = vector.DeserializationOption;
        this.vector = vector;
    }

    public static FlatBufferIndexedVector<TKey, TValue, TInputBuffer, TVectorItemAccessor> GetOrCreate(
        FlatBufferVectorBase<TValue, TInputBuffer, TVectorItemAccessor> vector)
    {
        if (!ObjectPool.TryGet<FlatBufferIndexedVector<TKey, TValue, TInputBuffer, TVectorItemAccessor>>(out var item))
        {
            item = new();
        }

        item.Initialize(vector);
        return item;
    }

    public TValue this[TKey key]
    {
        get
        {
            if (this.TryGetValue(key, out var value))
            {
                return value;
            }

            throw new KeyNotFoundException();
        }
    }

    /// <summary>
    /// Always readonly.
    /// </summary>
    public bool IsReadOnly => true;

    /// <summary>
    /// Gets the count of items.
    /// </summary>
    public int Count => this.vector.Count;

    /// <summary>
    /// No-op. We are already immutable.
    /// </summary>
    public void Freeze()
    {
    }

    public void AddOrReplace(TValue value)
    {
        throw new NotMutableException();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (var value in this.vector)
        {
            yield return new KeyValuePair<TKey, TValue>(IndexedVector<TKey, TValue>.GetKey(value), value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public bool Add(TValue value)
    {
        throw new NotMutableException();
    }

    public bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue? value)
    {
        value = SortedVectorHelpers.BinarySearchByFlatBufferKey(this.vector, key);
        return value is not null;
    }

    public bool ContainsKey(TKey key)
    {
        return this.TryGetValue(key, out _);
    }

    public void Clear()
    {
        throw new NotMutableException();
    }

    public bool Remove(TKey key)
    {
        throw new NotMutableException();
    }

    public void ReturnToPool(bool force = false)
    {
        if (this.deserializationOption.ShouldReturnToPool(force))
        {
            var vec = Interlocked.Exchange(ref this.vector!, null);
            if (vec is not null)
            {
                vec.ReturnToPool(true);
                this.vector = null!;
                ObjectPool.Return(this);
            }
        }
    }
}
