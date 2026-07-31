using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SupportManager.Api.Validations;

public class MaxFileSizeAttribute(int maxFileSize) : ValidationAttribute
{
    private readonly int _maxFileSize = maxFileSize;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"El tamaño del archivo no puede exceder los {_maxFileSize / (1024.0 * 1024.0):F1} MB.");
            }
        }
        return ValidationResult.Success;
    }
}

public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
{
    private readonly string[] _extensions = extensions;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_extensions.Contains(extension))
            {
                return new ValidationResult($"Solo se permiten archivos con las siguientes extensiones: {string.Join(", ", _extensions)}");
            }
        }
        return ValidationResult.Success;
    }
}
