// Adapted from https://dotnetcodr.com/2020/10/13/roll-your-own-custom-list-with-c-net/ and https://stackoverflow.com/questions/11313373/how-to-implement-ienumerablet-with-getenumerator.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDeserializer
{
	public class SortedStack<T, P, V> : IEnumerable<T>
	{
		private T[] Type;
		private P[] Property;
		private V[] Value;

		private int _SIZE = 0;
		private int Capacity;

		public SortedStack(int initialCapacity = 1)
		{
			this.Capacity = initialCapacity;

			Type = new T[initialCapacity];
			Property = new P[initialCapacity];
			Value = new V[initialCapacity];
		}

		private IEnumerable<T> Events()
		{
			foreach (T entity in Type)
			{
				yield return entity;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Events().GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Type.GetEnumerator();
		}

		public int Size { get { return _SIZE; } }
		public bool IsEmpty { get; set; }

		public T TypeAt(int index)
		{
			ThrowIfIndexOutOfRangeException(index);
			return Type[index];
		}

		public void Push(T type, P property, V value)
		{
			Type[0] = type;
			Property[0] = property;
			Value[0] = value;
			this._SIZE++;
		}

		public void Pop()
		{
			if (Type.Count() >= 1)
			{
				//Type[this._SIZE - 1] = default(T);
				this._SIZE--;
			}
		}

		public P PropertyAt(int index)
		{
			ThrowIfIndexOutOfRangeException(index);
			return Property[index];
		}

		public V ValueAt(int index)
		{
			ThrowIfIndexOutOfRangeException(index);
			return Value[index];
		}

		public void SetAt(T type, P property, V value, int index)
		{
			ThrowIfIndexOutOfRangeException(index);
			Type[index] = type;
			Property[index] = property;
			Value[index] = value;
		}

		public void InsertAt(T type, P property, V value, int index)
		{
			ThrowIfIndexOutOfRangeException(index);
			Type[index] = type;
			Property[index] = property;
			Value[index] = value;
		}

		public void Insert(T type, P property, V value)
		{
			//ThrowIfIndexOutOfRangeException(1);
			Type[this.Size + 1] = type;
			Property[this.Size + 1] = property;
			Value[this.Size + 1] = value;
			this._SIZE++;
		}

		public void DeleteAt(int index)
		{
			throw new NotImplementedException("This feature has not yet been implemented.");
		}

		private void ThrowIfIndexOutOfRangeException(int index)
		{
			if (index > _SIZE - 1 || index < 0)
			{
				throw new ArgumentOutOfRangeException(String.Format("The current size of the array is {0}", _SIZE));
			}
		}

	}
}
