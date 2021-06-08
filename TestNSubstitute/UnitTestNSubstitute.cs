using System;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Subject;
using Xunit;

namespace TestNSubstitute
{
    public class UnitTestNSubstitute
    {
        [Fact]
        public void TestDependency()
        {
            var bar = Substitute.For<IBar>();
            var foo = new Foo(bar);
            bar.BarMethod(Arg.Any<int>()).Returns(0);
            bar.BarMethod(Arg.Is(2)).Returns(2);
            bar.BarMethod(Arg.Is(3)).Returns(3);

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
            var bar = Substitute.For<IBar>();
            var foo = new Foo(bar);
            bar.BarMethod(Arg.Any<int>()).Returns(5);

            foo.FooMethod(4);

            foo.Number.Should().Be(5);
        }

        [Fact]
        public void TestInvokingMethod()
        {
            var bar = Substitute.For<IBar>();
            var foo = new Foo(bar);

            foo.FooMethod(3);
            foo.FooMethod(4);
            foo.FooMethod(2);
            foo.FooMethod(2);

            bar.Received(Quantity.Exactly(1)).BarMethod(Arg.Is<int>(4));
            bar.Received(Quantity.Exactly(2)).BarMethod(Arg.Is<int>(2));
        }
    }
}
