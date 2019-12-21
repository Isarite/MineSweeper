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
        public void UnknownDeepCloneTest()
        {
            var original = new Unknown { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.DeepClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreNotEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void RevealedDeepCloneTest()
        {
            var original = new Revealed { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.DeepClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreNotEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void TntDeepCloneTest()
        {
            var original = new Tnt { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.DeepClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreNotEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void WrongTntDeepCloneTest()
        {
            var original = new WrongTnt { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.DeepClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreNotEqual(original.map, clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }

        [Test]
        public void ExplodedDeepCloneTest()
        {
            var original = new ExplodedTnt { Id = 1, map = new Map(), marked = true, number = 1 };
            var clone = original.DeepClone();

            Assert.AreEqual(original.Id, clone.Id);
            Assert.AreEqual(original.bombs, clone.bombs);
            Assert.AreNotEqual(original.map,clone.map);
            Assert.AreEqual(original.marked, clone.marked);
            Assert.AreEqual(original.number, clone.number);
        }
    }
}
