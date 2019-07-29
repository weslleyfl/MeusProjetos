using System;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities {
    public abstract class Payment : Entitie {
        protected Payment (DateTime paiDate, DateTime expireDate, decimal total, decimal totalPaid, string owner, Document document, Address address, Email email) {

            Number = Guid.NewGuid ().ToString ().Replace ("-", "").Substring (0, 10).ToUpper ();
            PaiDate = paiDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
            Owner = owner;
            Document = document;
            Address = address;
            Email = email;

            AddNotifications(new Contract ()
                .Requires()
                .IsLowerOrEqualsThan(0, Total, "Payment.Total", "O total nao pode ser zero")
                .IsGreaterOrEqualsThan(Total, TotalPaid, "Payment.TotalPaid", "O valor pago e menor que o valor do pagamento")
            );
        }

        public string Number { get; private set; }
        public DateTime PaiDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
        public string Owner { get; private set; }
        public Document Document { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }
    }
}