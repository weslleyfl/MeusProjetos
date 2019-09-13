using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repository;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers {
    public class SubscriptionHandler : 
            Notifiable, 
            IHandler<CreateBoletoSubscriptionCommand>, 
            IHandler<CreatePayPalSubscriptionCommand>
        {

        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        // Gerar dependencia -DI
        public SubscriptionHandler (IStudentRepository repository, IEmailService emailService) 
        {
            _repository = repository;
            _emailService = emailService;
        }

        // Espera um
        /// CommandResult para saida ()
        // e um Command para entrada (dto CreateBoletoSubscriptionCommand)
        public ICommandResult Handler (CreateBoletoSubscriptionCommand command) 
        {
            // Fail Fast Validation
            command.Validate ();
            if (command.Invalid == true) {
                AddNotifications (command);
                return new CommandResult (false, "Não foi possivel realizar sua assinatura");
            }

            // Validaçoes
            // Verififcar se documento ja esta cadastrado
            if (_repository.DocumentExists (command.Document))
                AddNotification ("Document", "Este CPF ja esta em uso");

            //AddNotifications (new Contract () { });

            // Verififcar se email ja esta cadastrado
            if (_repository.EmailExists (command.Email))
                AddNotification ("Email", "Este Email ja esta em uso");

            // Gerar os VOs
            var name = new Name (command.FirstName, command.LastName);
            var document = new Document (command.Document, EDocumentType.CPF);
            var email = new Email (command.Email);
            var address = new Address (command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.Zipcode);

            // Gerar as Entidades
            var student = new Student (name, document, email);
            var subscription = new Subscription (DateTime.Now.AddMonths (1));
            var payment = new BoletoPayment (
                command.BarCode,
                command.BoletoNumber,
                command.PaiDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                document,
                address,
                email
            );

            // Relacionamento
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Aplicar as Validaçoes         
            // Agrupar as validaçoes
            AddNotifications(name,document,email,address,student,subscription,payment);
               
            // Salvar as Informaçoes
            _repository.CreateSubscription(student);
            // Enviar Email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao weslley ltda","Sua assinatura foi criada");
            // Retornar informaçoes

            return new CommandResult (true, "Assinatura realizada com sucesso");

        }

        public ICommandResult Handler(CreatePayPalSubscriptionCommand command)
        {
              // Fail Fast Validation              
            command.Validate();
            if (command.Invalid == true) {
                AddNotifications (command);
                return new CommandResult (false, "Não foi possivel realizar sua assinatura");
            }

            // Validaçoes
            // Verififcar se documento ja esta cadastrado
            if (_repository.DocumentExists (command.Document))
                AddNotification ("Document", "Este CPF ja esta em uso");

            //AddNotifications (new Contract () { });

            // Verififcar se email ja esta cadastrado
            if (_repository.EmailExists (command.Email))
                AddNotification ("Email", "Este Email ja esta em uso");

            // Gerar os VOs
            var name = new Name (command.FirstName, command.LastName);
            var document = new Document (command.Document, EDocumentType.CPF);
            var email = new Email (command.Email);
            var address = new Address (command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.Zipcode);

            // Gerar as Entidades
            var student = new Student (name, document, email);
            var subscription = new Subscription (DateTime.Now.AddMonths (1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaiDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                document,
                address,
                email
            );

            // Relacionamento
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Aplicar as Validaçoes         
            // Agrupar as validaçoes
            AddNotifications(name,document,email,address,student,subscription,payment);
               
            // Salvar as Informaçoes
            _repository.CreateSubscription(student);
            // Enviar Email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao weslley ltda","Sua assinatura foi criada");
            // Retornar informaçoes

            return new CommandResult (true, "Assinatura realizada com sucesso");
        }
    }
}