using System;
using System.Reflection.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using Document = PaymentContext.Domain.ValueObjects.Document;

namespace PaymentContext.Tests.Entities {

    [TestClass]
    public class StudentTests {

        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;


        public StudentTests()
        {
            _name = new Name ("weslley", "lopes");
            _document = new Document ("35111507795", EDocumentType.CPF);
            _email = new Email ("batman@dc.com");
            _student = new Student (_name, _document, _email);
            _subscription = new Subscription (null);
            _address = new Address ("rua 1", "10", "Legal", "Gotham", "MG", "Brasil", "32071228");
           
        }


      
        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription() 
        {
            // Configurar
             var payment = new PayPalPayment ("1134566", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _document, _address, _email);
          
            // Exercita
            _subscription.AddPayment(payment); 
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            // Verifica
            Assert.IsTrue(_student.Invalid); 

            // Finaliza

        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment() 
        {
            // Configurar           

            // Exercita
            _student.AddSubscription(_subscription);
            // Verifica
            Assert.IsTrue(_student.Invalid);             

            // Finaliza

        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription() 
        {
              // Configurar
             var payment = new PayPalPayment ("1134566", DateTime.Now, DateTime.Now.AddDays (5), 10, 10, "Wayne Corp", _document, _address, _email);
          
            // Exercita
             _subscription.AddPayment(payment); 
            _student.AddSubscription(_subscription);            

            // Verifica
            Assert.IsTrue(_student.Valid); 

        }

    }
}