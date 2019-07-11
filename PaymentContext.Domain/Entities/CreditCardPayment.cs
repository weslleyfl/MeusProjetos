using System;

namespace PaymentContext.Domain.Entities {
    public class CreditCardPayment : Payment {
        public CreditCardPayment (
            string cardHolderName,
            string cardNumber,
            string lastTransactionNumber,
            DateTime paiDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            string owner,
            string document,
            string address,
            string email) : base (
            paiDate,
            expireDate,
            total,
            totalPaid,
            owner,
            document,
            address,
            email
        )

        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}