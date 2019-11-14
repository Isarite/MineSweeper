using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MineServer.Models;
using NUnit.Framework;

namespace NunitTests
{
    public class CloneTests
    {

        [Test]
        public void UnknownCloneTest()
        {
            var original = new Unknown{Id = 1,map = new Map(),marked = true, number = 1};
            var clone = original.Clone();

            //Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void RevealedCloneTest()
        {
            var original = new Revealed { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.Clone();

            //Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void TntCloneTest()
        {
            var original = new Tnt { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.Clone();

            //Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void ExplodedTntCloneTest()
        {
            var original = new ExplodedTnt() { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.Clone();

            //Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void WrongTntCloneTest()
        {
            var original = new WrongTnt { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.Clone();

            //Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void UnknownShallowCloneTest()
        {
            var original = new Unknown { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.ShallowClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void RevealedShallowCloneTest()
        {
            var original = new Revealed { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.ShallowClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void TntShallowCloneTest()
        {
            var original = new Tnt { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.ShallowClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void WrongTntShallowCloneTest()
        {
            var original = new WrongTnt { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.ShallowClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void ExplodedShallowCloneTest()
        {
            var original = new ExplodedTnt { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.ShallowClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }
    }
}
