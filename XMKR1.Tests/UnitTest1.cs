using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

public class ProgramTests
{
    public static IEnumerable<object[]> TestData => new List<object[]>
    {
        new object[] { 3, new decimal[] { 0m, 70.00m, 75.00m, 72.00m }, new decimal[] { 0m, 80.00m, 78.00m, 76.00m }, "107.14" },
        new object[] { 4, new decimal[] { 0m, 1.00m, 10.00m, 5.53m, 6.00m }, new decimal[] { 0m, 10.00m, 5.53m, 1.25m, 5.00m }, "4000.00" },
        new object[] { 2, new decimal[] { 0m, 60.00m, 61.00m }, new decimal[] { 0m, 70.00m, 69.00m }, "101.67" },
        new object[] { 1, new decimal[] { 0m, 50.00m }, new decimal[] { 0m, 50.00m }, "100.00" },
        new object[] { 5, new decimal[] { 0m, 65.00m, 66.00m, 67.00m, 68.00m, 69.00m }, new decimal[] { 0m, 75.00m, 74.00m, 73.00m, 72.00m, 71.00m }, "106.15" },
    };

    [Theory]
    [MemberData(nameof(TestData))]
    public void TestCalculateMaxRubles(int N, decimal[] USD, decimal[] EUR, string expected)
    {
        // Виклик методу для розрахунків
        decimal result = Program.CalculateMaxRubles(N, USD, EUR);
        string formattedResult = result.ToString("F2", CultureInfo.InvariantCulture);

        // Перевірка результату
        Assert.Equal(expected, formattedResult);
    }
}