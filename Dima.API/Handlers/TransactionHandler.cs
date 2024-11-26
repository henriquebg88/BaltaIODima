using Dima.API.Data;
using Dima.Core.Common.Extensions;
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
            try
            {
                var transaction = await context.Transactions
                                .AsNoTracking()
                                .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                return transaction is null
                    ? new Response<Transaction?>(null, 404, "Transação não encontrada")
                    : new Response<Transaction?>(transaction);
                
            }
            catch (System.Exception)
            {
                return new Response<Transaction?>(null, 500, "Ocorreu um erro ao tentar retornar a Transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetListByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch (System.Exception)
            {
                
                return new PagedResponse<List<Transaction>?>(null, 500, "Houve uma falha ao tentar converter a data de Início e Fim");
            }


            try
            {

                var _query = context.Transactions
                                .AsNoTracking()
                                .Where(c => 
                                    c.UserId == request.UserId &&
                                    c.CreatedAt >= request.StartDate && 
                                    c.CreatedAt <= request.EndDate
                                ).OrderBy(x => x.CreatedAt);
                
                var transactions = await _query
                                .Skip((request.pageNumber -1) * request.pageSize)
                                .Take(request.pageSize)
                                .ToListAsync();

                var count = await _query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.pageNumber, request.pageSize);
                
            }
            catch (System.Exception)
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Ocorreu um erro ao tentar retornar a Transação");
            }
        }

    }
}