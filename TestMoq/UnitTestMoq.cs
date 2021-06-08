using System;
using FluentAssertions;
using Moq;
using Subject;
using Xunit;

namespace TestMoq
{
    public class UnitTestMoq
    {
        [Fact]
        public void TestDependency()
        {
            var barMock = new Mock<IBar>();
            var foo = new Foo(barMock.Object);
            barMock.Setup(b => b.BarMethod(It.IsAny<int>())).Returns(0);
            barMock.Setup(b => b.BarMethod(3)).Returns(3);
            barMock.Setup(b => b.BarMethod(2)).Returns(2);

            var r1 = foo.FooMethod(3);
            var r2 = foo.FooMethod(2);
            var r3 = foo.FooMethod(1);

            r1.Should().Be(3);
            r2.Should().Be(2);
            r3.Should().Be(0);
        }



        [Fact]
        public void TestDependency2()
        {
            var bar = Mock.Of<IBar>();
            var foo = new Foo(bar);
            Mock.Get(bar).Setup(m => m.BarMethod(It.IsAny<int>())).Returns(0);
            Mock.Get(bar).Setup(m => m.BarMethod(3)).Returns(3);
            Mock.Get(bar).Setup(m => m.BarMethod(2)).Returns(2);

            var r1 = foo.FooMethod(3);
            var r2 = foo.FooMethod(2);
            var r3 = foo.FooMethod(1);

            r1.Should().Be(3);
            r2.Should().Be(2);
            r3.Should().Be(0);
        }
        
        [Fact]
        public void TestProperties()
        {
            var bar = Mock.Of<IBar>();
            var foo = new Foo(bar);
            Mock.Get(bar).Setup(m => m.BarMethod(It.IsAny<int>())).Returns(5);

            foo.FooMethod(4);

            foo.Number.Should().Be(5);
        }

        [Fact]
        public void TestInvokingMethod()
        {
            var bar = Mock.Of<IBar>();
            var foo = new Foo(bar);

            foo.FooMethod(3);
            foo.FooMethod(4);
            foo.FooMethod(2);
            foo.FooMethod(2);

            Mock.Get(bar).Verify(m => m.BarMethod(3), Times.Exactly(1));
        }
    }
}
