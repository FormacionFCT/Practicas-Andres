using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;


namespace app_inventario_andres_navarro.Modelos
{
    public class ProductoValidationRule : ValidationRule
    {
        public required string Campo { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value as string))
            {
                return new ValidationResult(false, $"{Campo} es obligatorio.");
            }

            switch (Campo)
            {
                case "Id":
                case "Cantidad":
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                        return new ValidationResult(false, $"El {Campo} es obligatorio.");

                    if (!int.TryParse(value.ToString(), out int intValue))
                        return new ValidationResult(false, $"{Campo} debe ser un número entero.");
                    if (intValue <= 0)
                        return new ValidationResult(false, $"{Campo} debe ser mayor que cero.");
                    break;

                case "Precio":
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                        return new ValidationResult(false, "El precio es obligatorio.");

                    string precioInput = value.ToString().Replace(',', '.');
                    if (!float.TryParse(precioInput, NumberStyles.Any, CultureInfo.InvariantCulture, out float floatValue))
                        return new ValidationResult(false, "Precio debe ser un número decimal válido.");
                    if (floatValue < 0)
                        return new ValidationResult(false, "Precio no puede ser negativo.");
                    break;
            }

            return ValidationResult.ValidResult;
        }
    }
}
