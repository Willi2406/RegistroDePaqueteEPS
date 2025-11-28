using Microsoft.EntityFrameworkCore;
using RegistroDePaqueteEPS.DAL;
using RegistroDePaqueteEPS.Models;
using System.Linq.Expressions;

namespace RegistroDePaqueteEPS.Services;

public class DireccionesDeliveryService(IDbContextFactory<Contexto> dbContext)
{
    public async Task<bool> Existe(int direccionDeliveryId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.DireccionesDelivery.AnyAsync(d => d.DireccionDeliveryId == direccionDeliveryId);
    }

    public async Task<bool> Insertar(DireccionesDelivery direccionDelivery)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.DireccionesDelivery.Add(direccionDelivery);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(DireccionesDelivery direccionDelivery)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.DireccionesDelivery.Update(direccionDelivery);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(DireccionesDelivery direccionDelivery)
    {
        if (!await Existe(direccionDelivery.DireccionDeliveryId))
            return await Insertar(direccionDelivery);
        else
            return await Modificar(direccionDelivery);
    }

    public async Task<bool> Eliminar(int direccionDeliveryId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.DireccionesDelivery.Where(d => d.DireccionDeliveryId == direccionDeliveryId).AsNoTracking().ExecuteDeleteAsync() > 0;
    }

    public async Task<DireccionesDelivery?> Buscar(int direccionDeliveryId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.DireccionesDelivery.FirstOrDefaultAsync(d => d.DireccionDeliveryId == direccionDeliveryId);
    }

    public async Task<List<DireccionesDelivery>> Listar(Expression<Func<DireccionesDelivery, bool>> criterio)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.DireccionesDelivery.Where(criterio).AsNoTracking().ToListAsync();
    }
}
