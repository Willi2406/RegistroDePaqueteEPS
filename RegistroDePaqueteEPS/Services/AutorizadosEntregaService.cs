using Microsoft.EntityFrameworkCore;
using RegistroDePaqueteEPS.Data;
using RegistroDePaqueteEPS.Models;
using System.Linq.Expressions;

namespace RegistroDePaqueteEPS.Services;

public class AutorizadosEntregaService(IDbContextFactory<ApplicationDbContext> dbContext)
{
    public async Task<bool> Existe(int autorizadoEntregaId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.AutorizadosEntrega.AnyAsync(a => a.AutorizadoEntregaId == autorizadoEntregaId);
    }

    public async Task<bool> Insertar(AutorizadosEntrega autorizadoEntrega)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.AutorizadosEntrega.Add(autorizadoEntrega);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(AutorizadosEntrega autorizadoEntrega)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.AutorizadosEntrega.Update(autorizadoEntrega);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(AutorizadosEntrega autorizadoEntrega)
    {
        if (!await Existe(autorizadoEntrega.AutorizadoEntregaId))
            return await Insertar(autorizadoEntrega);
        else
            return await Modificar(autorizadoEntrega);
    }

    public async Task<bool> Eliminar(int autorizadoEntregaId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.AutorizadosEntrega.Where(a => a.AutorizadoEntregaId == autorizadoEntregaId).AsNoTracking().ExecuteDeleteAsync() > 0;
    }

    public async Task<AutorizadosEntrega?> Buscar(int autorizadoEntregaId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.AutorizadosEntrega.FirstOrDefaultAsync(a => a.AutorizadoEntregaId == autorizadoEntregaId);
    }

    public async Task<List<AutorizadosEntrega>> Listar(Expression<Func<AutorizadosEntrega, bool>> criterio)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.AutorizadosEntrega.Where(criterio).AsNoTracking().ToListAsync();
    }
}
