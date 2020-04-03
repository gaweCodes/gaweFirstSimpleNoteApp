using System;
using System.ComponentModel.DataAnnotations;

namespace GaweNotesApi.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class DateTimeOffsetNotInFutureValidationAttribute : ValidationAttribute
    {
        public DateTimeOffsetNotInFutureValidationAttribute() { }
        public override bool IsValid(object value)
        {
            try
            {
                var dateTimeOffset = (DateTimeOffset)value;
                return dateTimeOffset <= DateTimeOffset.Now;
            }
            catch (Exception e)
            {
                Console.Write("Error during DateTime Parsing" + e.Message);
                return false;
            }
        }
    }
}
