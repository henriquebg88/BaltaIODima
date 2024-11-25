using Dima.API.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.API.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction{
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Title = request.Title,
                    Type = request.Type,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt 
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();
                
                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso.");
            }
            catch (System.Exception ex)
            {
                //Pesquisar SERILOG -> bom logger de erros
               return new Response<Transaction?>(null, 500, "Não foi possível criar a Transação.");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(T => T.Id == request.Id && T.UserId == request.UserId);
                if (transaction == null) return new Response<Transaction?>(null, 404, "Transação não encontrada.");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, message: "Transação atualizada com sucesso.");
            }
            catch (System.Exception)
            {
                return new Response<Transaction?>(null, 500, "Não foi possível atualizar a Transação.");
            }
        }
        public async Task<Response<Transaction?>> DeleteteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(T => T.Id == request.Id && T.UserId == request.UserId);
                if (transaction == null) return new Response<Transaction?>(null, 404, "Transação não encontrada.");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, message: "Transação atualizada com sucesso.");
            }
            catch (System.Exception)
            {
                return new Response<Transaction?>(null, 500, "Não foi possível atualizar a Transação.");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<Transaction>>> GetListByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            throw new NotImplementedException();
        }

    }
}