using Narya.Sms.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Narya.Email.Core.UnitTests
{
    public class SmsTests
    {
        [Fact]
        public void SMSFactoryMethod_ReturnsSms()
        {
            // Arrange
            var result = SmsOptions.Create("", "+201016171819");
            // Assert
            Assert.IsType<SmsOptions>(result.Value);
        }

        [Fact]
        public void SMSFactoryMethod_InvalidPhoneNumberReturnsError()
        {
            // Arrange
            var result = SmsOptions.Create("", "aaafda");
            var errors = result.Errors;

            // Act
            int actualErrors = 1;
            // Assert
            Assert.Equal(errors.Count, actualErrors);
        }

        [Fact]
        public void SMSFactoryMethod_InvalidPhoneNumberReturnsError_ErrorMessage()
        {
            // Arrange
            string number = "aaafda";
            var result = SmsOptions.Create("", number);
            var errors = result.Errors;

            // Act
            var actualError = $"Invalid Phone Number {number}.";
            // Assert
            Assert.Equal(errors.First(), actualError);
        }


        [Fact]
        public void SMSFactoryMethod_InvalidPhoneNumberReturnsMultipleErrorsIfMultipleInvalidNumbers()
        {
            // Arrange
            var numbers = new string[] { "aaafda", "+2010101010101010101" };
            var result = SmsOptions.Create("", numbers);
            var errors = result.Errors;

            // Act
            var actualErrors = 2;
            // Assert
            Assert.Equal(errors.Count, actualErrors);
        }
    }
}