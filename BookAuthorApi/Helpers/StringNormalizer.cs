using System.Text;
using System.Text.RegularExpressions;

namespace BookAuthorApi.Helpers
{
    public static class StringNormalizer
    {
        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            // Convertir a mayúsculas
            input = input.ToUpperInvariant();
            // Eliminar números
            input = Regex.Replace(input, "[0-9]", "");
            // Reemplazar caracteres especiales (tildes, ñ)
            input = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in input)
            {
                var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c == 'Ñ' ? 'N' : c);
                }
            }
            input = sb.ToString().Normalize(NormalizationForm.FormC);
            // Reemplazar espacios consecutivos por uno solo
            input = Regex.Replace(input, "\\s+", " ").Trim();
            return input;
        }
    }
}
