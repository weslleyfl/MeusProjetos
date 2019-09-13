using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities {
    public class Student : Entitie {

        private IList<Subscription> _subscriptions;
        //public Student (string firstName, string lastName, Document document, Email email, Name name) {
         public Student (Name name, Document document, Email email) {
            // FirstName = firstName;
            // LastName = lastName;
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications (name, document, email);            
        }

        public Name Name { get; private set; }
        // public string FirstName { get; private set; }
        // public string LastName { get; private set; }

        // public string Document { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        // public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray (); } }
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.ToArray(); 

        public void AddSubscription (Subscription subscription) 
        {
            // Validation logic...
            // regras de negocio aqui

            // se ja tiver uma assinatura ativa, cancela

            // Cancela todas as outras assinaturas, e coloca esta como principal
            // sera mudado, nao posso alterar minha entidade diretamente so por metodo dela 
            var hasSubScriptionActive = false;

            foreach (var sub in _subscriptions) {
                if (sub.Active)
                    hasSubScriptionActive = true;
            }

            
            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubScriptionActive,"Student.Subscription","Você já tem uma assinatura ativa")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "Essa assinatura não possui pagamentos")
            );

            // Aqui vale fazer um teste, para validar se um usuario ja tem uma assinatura ativa
            // Alternativa
            // if (hasSubScriptionActive)
            //     AddNotification("Student.subscription", "Você já tem uma assinatura ativa");

            if(Valid)
            _subscriptions.Add(subscription);

        }

    }
}