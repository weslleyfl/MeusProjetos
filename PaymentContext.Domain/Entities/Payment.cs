using System;

namespace PaymentContext.Domain.Entities {
    public abstract class Payment {
        protected Payment (DateTime paiDate, DateTime expireDate, decimal total, decimal totalPaid, string owner, string document, string address, string email) {

            Number = Guid.NewGuid().ToString ().Replace ("-", "").Substring (0, 10).ToUpper ();
            PaiDate = paiDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
            Owner = owner;
            Document = document;
            Address = address;
            Email = email;
        }

        public string Number { get; private set; }
        public DateTime PaiDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
        public string Owner { get; private set; }
        public string Document { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
    }
}