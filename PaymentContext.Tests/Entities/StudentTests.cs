using System.Reflection.Metadata;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void AdicionarAssinatura()
        {
        //     var student = new Student(
        //         firstName: "weslley",
        //         lastName:"Lopes",
        //         document: EDocumentType.CPF,
        //         email:"weslley@email.com"
        //     );

        //    var subscription = new Subscription(expireDate: DateTime.Now.AddDays(1));

        //    student.AddSubscription(subscription);        

        var nome = new Name("Teste First", "Last name");
        foreach (var not in nome.Notifications)
        {
             //not.Message
        }

        }
    }
}