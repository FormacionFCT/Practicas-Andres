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
            
            switch (Campo)
            {
                case "IdProducto":                
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                        return new ValidationResult(false, "El id es obligatorio.");
                    if (!int.TryParse(value.ToString(), out int intValue))
                        return new ValidationResult(false, "El id debe ser un número entero.");
                    if (intValue <= 0 && intValue == 0)
                        return new ValidationResult(false, "El id debe ser mayor que cero.");
                    // Aquí se agrega la validación de ID único
                    var productoExistente = productoList.FirstOrDefault(p => p.IdProducto == intValue);
                    if (productoExistente != null)
                        return new ValidationResult(false, "El ID ya existe.");

                    break;

                case "NombreProducto":
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                        return new ValidationResult(false, "El nombre es obligatorio.");
                    break;

                case "Descripcion":
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                        return new ValidationResult(false, "La descripción es obligatoria.");
                    break;

                case "Cantidad":
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                        return new ValidationResult(false, "El cantidad es obligatoria.");
                    if (!int.TryParse(value.ToString(), out int intValuee))
                        return new ValidationResult(false, "La cantidad debe ser un número entero.");
                    if (intValuee <= 0 && intValuee == 0)
                        return new ValidationResult(false, "La cantidad debe ser mayor que cero.");
                    break;

                case "Precio":
                    if ( string.IsNullOrWhiteSpace(value.ToString()))
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
