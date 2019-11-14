using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MineServer;
using MineServer.Models;
using MineServer.Resources;
using NUnit.Framework;

namespace NunitTests
{
    public class MapTests
    {
        Map map;
        [SetUp]
        public void Setup()
        {
            map = new Map();
        }
        
        [TearDown]
        public void TearDown()
        {
            map = null;
        }
        
        [Test]
        public void MapCreationTest()
        {
            map = new Map();
            foreach (var cell in map._cells)
            {
                Assert.IsTrue(cell is Unknown);
            }
        }
        

        [TestCase(0, 0,'e', GameStatus.Lost)]
        [TestCase(0, 1,'e', GameStatus.Lost)]
        [TestCase(1, 0,'2')]
        [TestCase(0, 9,'e', GameStatus.Lost)]
        [TestCase(9, 9,'0', GameStatus.Won)]
        [TestCase(9, 0,'0',GameStatus.Won)]
        public void CellRevealTest()
        {
            
        }
        
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 9)]
        [TestCase(9, 9)]
        [TestCase(9, 0)]
        public void CellMarkTest(int x, int y)
        {
            for (int i = 0; i < 10; i++)
            {
                map._cells[i] = new Tnt();
                map._cells[i].number = i;
            }

            var result = map.MarkCell(x,y);
            
            Assert.IsNotNull(result.map);
            Assert.AreEqual('m', result.map[x, y]);
            Assert.IsTrue(map._cells[x*10+y].marked);
        }
        
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 9)]
        [TestCase(9, 9)]
        [TestCase(9, 0)]
        public void MineSetTest(int x, int y)
        {
            
        }
        
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 9)]
        [TestCase(9, 9)]
        [TestCase(9, 0)]        
        public void MineUnsetTest()
        {
            
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 9)]
        [TestCase(9, 9)]
        [TestCase(9, 0)]
        public void UpdateTest()
        {
            
        }

        public void SurrenderTest()
        {
            
        }
    }
}