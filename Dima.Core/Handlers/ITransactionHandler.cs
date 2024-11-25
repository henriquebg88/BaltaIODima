using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Core.Handlers
{
    //Serve para limitar que o frontend terá acesso
    // API <--> Handler <--> WEBAPP
    public interface ITransactionHandler // Multiplas implementações. Servirá para o frontend e backend
    {
        Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<PagedResponse<List<Transaction>>> GetListByPeriodAsync(GetTransactionsByPeriodRequest request);
        Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
        Task<Response<Transaction?>> DeleteteAsync(DeleteTransactionRequest request);
    }
}