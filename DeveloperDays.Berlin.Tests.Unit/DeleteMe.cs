// ---------------------------------------------------------------------
// Copyright (c) 2024 eBiz Consulting GmbH
// Made w/ love by Mabrouk Mahdhi for all .NET developer days attendees
// ---------------------------------------------------------------------

using FluentAssertions;
using Tynamix.ObjectFiller;

namespace DeveloperDays.Berlin.Tests.Unit
{
    public class DeleteMe
    {
        [Fact]
        public void ShouldBeTrue() => Assert.True(true);

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 3, 5)]
        [InlineData(3, 4, 7)]
        public void ShouldAdd(int a, int b, int expected) =>
            Assert.Equal(expected, Add(a, b));

        [Fact]
        public void ShouldAddCorrectly() // Is that test really good ?
        {
            // given
            var a = GetRandomNumber();
            var b = GetRandomNumber();

            var expected = a + b; 

            // when
            var actual = Add(a, b);

            // then
            actual.Should().Be(expected);
        }

        [Fact]
        public void ShouldAddCorrectlyAndBetter()
        {
            // given
            var expected = GetRandomNumber();
            var a = GetRandomNumber();
            var b = expected - a; // yuppi, no magic number

            // when
            var actual = Add(a, b);

            // then
            actual.Should().Be(expected);
        }

        public static int Add(int a, int b) => a + b;

        private static int GetRandomNumber() =>
            new IntRange(-1000, 1000).GetValue();
    }
}