﻿using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFragments
{
    public class FooPooledObjectPolicy : IPooledObjectPolicy<Foo>
    {
        public Foo Create()
        {
            return new Foo();
        }

        public bool Return(Foo obj)
        {
            return true;
        }
    }
    public class Foo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
    }

    public class ObjectPoolTest
    {
        public void DefalultPolicy()
        {
            var policy = new DefaultPooledObjectPolicy<Foo>();
            DoTest(policy, 1);
        }

        private static void DoTest(IPooledObjectPolicy<Foo> policy, int limit)
        {
            var pool = new DefaultObjectPool<Foo>(policy, limit);
            var item1 = pool.Get();

            pool.Return(item1);
            Console.WriteLine("item 1: {0}", item1.Id);


            var item2 = pool.Get();
            pool.Return(item2);
            Console.WriteLine("item 2: {0}", item2.Id);


            var item3 = pool.Get();

            pool.Return(item1); 
            pool.Return(item2); 
            pool.Return(item3);
            var item4 = pool.Get();
            var item5 = pool.Get();
            var item6 = pool.Get();
            Console.WriteLine("item 3: {0}", item3.Id);
            Console.WriteLine("item 4: {0}", item4.Id);
            Console.WriteLine("item 5: {0}", item5.Id);
            Console.WriteLine("item 6: {0}", item6.Id);
            pool.Return(item4);
            pool.Return(item5);
            pool.Return(item6);
            Console.WriteLine("-------------------------------");
        }

        public void CustomPolicy()
        {
            var policy = new FooPooledObjectPolicy();
            DoTest(policy, 2);
        }
    }
}
