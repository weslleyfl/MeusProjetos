using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities {
    public class BoletoPayment : Payment {
        public BoletoPayment (
            string barCode,
            string boletoNumber,
            DateTime paiDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            string owner,
            Document document,
            Address address,
            Email email) : base (
            paiDate,
            expireDate,
            total,
            totalPaid,
            owner,
            document,
            address,
            email
        ) {
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }

        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }

    }
}