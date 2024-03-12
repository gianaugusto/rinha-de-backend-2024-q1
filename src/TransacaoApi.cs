using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using rinha_2024_q1.Data;
using rinha_2024_q1.Models;

namespace rinha_2024_q1
{
    public static class TransacaoApi
    {
        public static void MapTransacaoApiEndpoints(this IEndpointRouteBuilder app)

        {

            app.MapGet("/clientes/{clientId}/extrato", GetExtratoByClientId)
            .Produces<ExtratoResult>()
            .Produces(StatusCodes.Status200OK, contentType: "application/json")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetStatementByClient")
            .WithTags("Clients")

            .WithOpenApi();

            app.MapPost("/clientes/{clientId}/transacoes", CriarTransacaoPorCliente)
            .Produces<Transacao>()
            .Produces(StatusCodes.Status201Created, contentType: "application/json")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("AddTransactionByClient")
            .WithTags("Clients")
            .WithOpenApi();

        }

        internal static async Task<IResult> GetExtratoByClientId(
            [FromRoute] int clientId,
            [FromServices] IDbContextFactory<RinhaDbContext> dbContextFactory,
            CancellationToken ct)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var extrato = await dbContext.ClientesExtrato.Include(o => o.Transacaos).FirstOrDefaultAsync(o => o.ClienteId == clientId, ct);

            if (extrato == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new ExtratoResult(new SaldoResult(extrato.Saldo, DateTime.UtcNow, extrato.Limite), extrato.Transacaos.Take(10)));
        }

        internal static async Task<IResult> CriarTransacaoPorCliente(
            [FromRoute] int clientId,
            [FromBody] TransacaoRequest request,
            [FromServices] IDbContextFactory<RinhaDbContext> dbContextFactory,
            CancellationToken ct)
        {
            var validationModel = request.IsValidateModel();

            if (!validationModel.Valid)
            {
                return Results.BadRequest(new { validationModel.Message });
            }

            using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var extrato = await dbContext.ClientesExtrato.Include(o => o.Transacaos).FirstOrDefaultAsync(o => o.ClienteId == clientId, ct);

            if (extrato == null)
            {
                return TypedResults.NotFound();
            }

            var result = extrato.NovaTransacao(new Transacao(clientId, request.Descricao, request.Tipo, request.Valor));

            if (!result.Valid)
                return Results.UnprocessableEntity(new { result.Message });

            await dbContext.SaveChangesAsync(ct);

            return Results.Ok(new { extrato.Limite, extrato.Saldo });
        }
    }
}
