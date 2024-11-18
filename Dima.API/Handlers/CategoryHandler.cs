using Dima.API.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.API.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICaterogyHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category{
                    userID = request.userId,
                    title = request.title,
                    description = request.description
                };

                await context.Categorias.AddAsync(category);
                await context.SaveChangesAsync();
                
                return new Response<Category?>(category, 201, "Categoria criada com sucesso.");
            }
            catch (System.Exception ex)
            {
                //Pesquisar SERILOG -> bom logger de erros
               return new Response<Category?>(null, 500, "Não foi possível criar a categoria.");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categorias.FirstOrDefaultAsync(c => c.id == request.id && c.userID == request.userId);
                if (category == null) return new Response<Category?>(null, 404, "Categoria não encontrada.");

                category.title = request.title;
                category.description = request.description;

                context.Categorias.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria atualizada com sucesso.");
            }
            catch (System.Exception)
            {
                return new Response<Category?>(null, 500, "Não foi possível atualizar a Categoria.");
            }
        }
        public async Task<Response<Category?>> DeleteteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categorias.FirstOrDefaultAsync(c => c.id == request.id && c.userID == request.userId);
                if (category == null) return new Response<Category?>(null, 404, "Categoria não encontrada.");

                context.Categorias.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria excluida com sucesso.");
            }
            catch (System.Exception)
            {
                return new Response<Category?>(null, 500, "Não foi possível excluir a Categoria.");
            }
        }

        public Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            throw new NotImplementedException();
        }

    }
}