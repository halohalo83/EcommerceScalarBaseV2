using Domain.Entities.Customers;
using FluentAssertions;

namespace UnitTests.Entities.Customers
{
    [TestFixture]
    class CustomerTests
    {
        [Test]
        public void Create_ShouldSetTheValue()
        {
            const string firstName = "John";
            const string lastName = "Doe";
            const string email = "";
            const string phoneNumber = "";

            var result = Customer.Create(firstName, lastName, email, phoneNumber);

            result.FirstName.Should().Be(firstName);
            result.LastName.Should().Be(lastName);
        }
    }
}
