using MineServer.Models;
using NUnit.Framework;

namespace NunitTests
{
    public class CloningMemoryTest
    {
        [Test]
        public static void MemoryTest()
        {
            Tnt original = new Tnt();
            original.map = new Map();
            original.Id = 1;
            original.bombs = 0;
            original.marked = true;
            original.number = 1;

            Cell shallowClone = original.Clone();
            Cell deepClone = original.DeepClone();

            //GCHandle objHandle = GCHandle.Alloc(original, GCHandleType.WeakTrackResurrection);
            //int address = GCHandle.ToIntPtr(objHandle).ToInt32();

            //GCHandle objHandle2 = GCHandle.Alloc(shallowClone, GCHandleType.WeakTrackResurrection);
            //int address2 = GCHandle.ToIntPtr(objHandle).ToInt32();

            //GCHandle objHandle3 = GCHandle.Alloc(deepClone, GCHandleType.WeakTrackResurrection);
            //int address3 = GCHandle.ToIntPtr(objHandle).ToInt32();

            Assert.Pass();
        }
    }
}
