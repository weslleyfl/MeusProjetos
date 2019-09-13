using PaymentContext.Shared.Commands;

namespace PaymentContext.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        // O comand de entrada nem sempre é os mesmos valores de saida (saida ICommandResult) entrada  (Command)
         ICommandResult Handler(T command);
    }
}