using System;

namespace Chef.Common.Test
{
    public class TestFixture<T> : IDisposable
    {
        public T TestItem { get; private set; }

        public bool IsIntialized { get; private set; }

        public TestFixture()
        {
            // Do "global" initialization here; Only called once.

        }
        public void Initialize(T item)
        {
            TestItem = item;
            IsIntialized = true;
        }
        //public void SetTestItem(T item)
        //{
        //    this.TestItem = item;
        //}
        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
        }
    }
}
