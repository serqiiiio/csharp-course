using Generics;

namespace Test.Generics
{
    public class TestSafeBox
    {
        public class SafeBoxTests
        {
            [Fact]
            public void Constructor_SetsValue_WhenValueIsNotNull()
            {
                // Arrange
                var expected = "test";

                // Act
                var box = new SafeBox<string>(expected);

                // Assert
                Assert.Equal(expected, box.Value);
            }

            [Fact]
            public void Constructor_AllowsDefaultValue_ForValueType()
            {
                // Arrange
                int defaultValue = default;

                // Act
                var box = new SafeBox<int>(defaultValue);

                // Assert
                Assert.Equal(defaultValue, box.Value);
            }

            [Fact]
            public void SafeBox_ShouldThrowIfNull()
            {
                Assert.Throws<ArgumentNullException>(() => new SafeBox<string>(null));
            }
        }
    }
}
