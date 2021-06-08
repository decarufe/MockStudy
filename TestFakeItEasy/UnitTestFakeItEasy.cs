using System;
using FakeItEasy;
using FluentAssertions;
using Subject;
using Xunit;

namespace TestFakeItEasy
{
    public class UnitTestFakeItEasy
    {
        [Fact]
        public void TestDependency()
        {
            var bar = A.Fake<IBar>();
            var foo = new Foo(bar);
            A.CallTo(() => bar.BarMethod(A<int>.Ignored)).Returns(0);
            A.CallTo(() => bar.BarMethod(3)).Returns(3);
            A.CallTo(() => bar.BarMethod(2)).Returns(2);

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
            var bar = A.Fake<IBar>();
            var foo = new Foo(bar);
            A.CallTo(() => bar.BarMethod(A<int>.Ignored)).Returns(5);

            foo.FooMethod(4);

            foo.Number.Should().Be(5);
        }

        [Fact]
        public void TestInvokingMethod()
        {
            var bar = A.Fake<IBar>();
            var foo = new Foo(bar);

            foo.FooMethod(3);

            A.CallTo(() => bar.BarMethod(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        }

    }
}
