using System;
using Flunt.Notifications;


namespace PaymentContext.Shared.Entities {
    public abstract class Entitie : Notifiable {
        protected Entitie () {
            Id = Guid.NewGuid ();
        }

        public Guid Id { get; set; }
    }
}