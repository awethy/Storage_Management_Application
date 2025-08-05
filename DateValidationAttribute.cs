using System.ComponentModel.DataAnnotations;

namespace Storage_Management_Application
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTime)
            {
                // Проверка, что дата не в будущем
                if (dateTime > DateTime.Now)
                {
                    return false;
                }
                // Проверка, что дата не раньше 1 января 2000 года
                if (dateTime < new DateTime(2000, 1, 1))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
