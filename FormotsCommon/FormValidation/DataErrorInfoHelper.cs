using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace FormotsCommon.FormValidation
{
    public static class DataErrorInfoHelper
    {
        public static string GetErrorInfo(object entity)
        {
            var type = entity.GetType();
            var validator =
                ValidationFactory.CreateValidator(type);
            var results = validator.Validate(entity);
            return string.Join(" ",
                results.Select(r => r.Message).ToArray());
        }

        public static string GetErrorInfo(object entity,
            string propertyName)
        {
            var property =
                entity.GetType().GetProperty(propertyName);

            return GetErrorInfo(entity, property);
        }

        public static string GetErrorInfo(object entity,
            PropertyInfo property)
        {
            var validator = GetPropertyValidator(entity,
                property);

            if (validator != null)
            {
                var results = validator.Validate(entity);

                return string.Join(" ",
                    results.Select(r => r.Message).ToArray());
            }

            return string.Empty;
        }

        private static Validator GetPropertyValidator(
            object entity, PropertyInfo property)
        {
            var ruleset = string.Empty;
            var source = ValidationSpecificationSource.All;
            var builder = new ReflectionMemberValueAccessBuilder();

            return PropertyValidationFactory.GetPropertyValidator(
                entity.GetType(), property, ruleset, source, builder);
        }
    }
}