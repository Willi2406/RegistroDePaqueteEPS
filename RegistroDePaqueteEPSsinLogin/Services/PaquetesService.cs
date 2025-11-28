using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using RegistroDePaqueteEPS.DAL;
using RegistroDePaqueteEPS.Models;
using System.Linq.Expressions;

namespace RegistroDePaqueteEPS.Services;

public class PaquetesService (IDbContextFactory<Contexto> dbContext)
{
    public async Task<bool> Existe(int paqueteId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Paquetes.AnyAsync(p => p.PaqueteId == paqueteId);
    }

    public async Task<bool> Insertar(Paquetes paquete)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.Paquetes.Add(paquete);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Paquetes paquete)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.Paquetes.Update(paquete);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Paquetes paquete)
    {
        if (!await Existe(paquete.PaqueteId))
            return await Insertar(paquete);
        else
            return await Modificar(paquete);
    }

    public async Task<bool> Eliminar(int paqueteId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Paquetes.Where(p => p.PaqueteId == paqueteId).AsNoTracking().ExecuteDeleteAsync() > 0;
    }

    public async Task<Paquetes?> Buscar(int paqueteId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Paquetes.FirstOrDefaultAsync(p => p.PaqueteId == paqueteId);
    }

    public async Task<List<Paquetes>> Listar(Expression<Func<Paquetes, bool>> criterio)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Paquetes.Where(criterio).AsNoTracking().ToListAsync();
    }
}
