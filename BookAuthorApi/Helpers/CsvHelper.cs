using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookAuthorApi.Helpers
{
    public static class CsvHelper
    {
        public static IEnumerable<Dictionary<string, string>> ParseCsv(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            var header = reader.ReadLine()?.Split(',');
            if (header == null) yield break;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var values = line.Split(',');
                var dict = header.Zip(values, (h, v) => new { h, v })
                    .ToDictionary(x => x.h.Trim(), x => x.v.Trim());
                yield return dict;
            }
        }
    }
}
