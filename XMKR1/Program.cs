using System;
using System.Globalization;
using System.IO;

public class Program
{
    public static void Main()
    {
        int N;
        decimal[] USD;
        decimal[] EUR;

        ReadInput(out N, out USD, out EUR);

        decimal finalRubles = CalculateMaxRubles(N, USD, EUR);

        WriteOutput(finalRubles);
    }

    // Метод для читання вхідних даних з файлу
    static void ReadInput(out int N, out decimal[] USD, out decimal[] EUR)
    {
        using (StreamReader sr = new StreamReader("INPUT.txt"))
        {
            N = int.Parse(sr.ReadLine());

            USD = new decimal[N + 1];
            EUR = new decimal[N + 1];

            for (int i = 1; i <= N; i++)
            {
                string[] inputs = sr.ReadLine().Split();
                USD[i] = decimal.Parse(inputs[0], CultureInfo.InvariantCulture);
                EUR[i] = decimal.Parse(inputs[1], CultureInfo.InvariantCulture);
            }
        }
    }

    // Метод для виконання розрахунків
    public static decimal CalculateMaxRubles(int N, decimal[] USD, decimal[] EUR)
    {
        decimal[] r = new decimal[N + 1];
        decimal[] d = new decimal[N + 1];
        decimal[] e = new decimal[N + 1];

        r[0] = 100m;
        d[0] = 0m;
        e[0] = 0m;

        for (int i = 1; i <= N; i++)
        {
            decimal rate_USD_to_EUR = USD[i] / EUR[i];
            decimal rate_EUR_to_USD = EUR[i] / USD[i];

            decimal option1_r = r[i - 1];
            decimal option2_r = d[i - 1] * USD[i];
            decimal option3_r = e[i - 1] * EUR[i];
            r[i] = Max(option1_r, option2_r, option3_r);

            decimal option1_d = d[i - 1];
            decimal option2_d = r[i - 1] / USD[i];
            decimal option3_d = e[i - 1] * rate_EUR_to_USD;
            d[i] = Max(option1_d, option2_d, option3_d);

            decimal option1_e = e[i - 1];
            decimal option2_e = r[i - 1] / EUR[i];
            decimal option3_e = d[i - 1] * rate_USD_to_EUR;
            e[i] = Max(option1_e, option2_e, option3_e);
        }

        decimal finalRubles = Max(
            r[N],
            d[N] * USD[N],
            e[N] * EUR[N]
        );

        return finalRubles;
    }

    // Метод для запису результату у файл
    static void WriteOutput(decimal finalRubles)
    {
        using (StreamWriter sw = new StreamWriter("OUTPUT.txt"))
        {
            sw.WriteLine(finalRubles.ToString("F2", CultureInfo.InvariantCulture));
        }
    }

    // Метод для знаходження максимального з трьох чисел
    static decimal Max(decimal a, decimal b, decimal c)
    {
        return Math.Max(a, Math.Max(b, c));
    }
}
