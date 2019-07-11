using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void AdicionarAssinatura()
        {
            var student = new Student(
                firstName: "weslley",
                lastName:"Lopes",
                document:"1234565656",
                email:"weslley@email.com"
            );

           var subscription = new Subscription(expireDate: DateTime.Now.AddDays(1));

           student.AddSubscription(subscription);

            

        }
    }
}