using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroDePaqueteEPS.Data;
using RegistroDePaqueteEPS.Models;

namespace RegistroDePaqueteEPS.Services;

public class PreavisosService(IDbContextFactory<ApplicationDbContext> dbContext)
{
    public async Task<bool> Existe(int preavisoId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Preavisos.AnyAsync(p => p.PreavisoId == preavisoId);
    }

    public async Task<bool> Insertar(Preavisos preaviso)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.Preavisos.Add(preaviso);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Preavisos preaviso)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.Preavisos.Update(preaviso);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Preavisos preaviso)
    {
        if (!await Existe(preaviso.PreavisoId))
            return await Insertar(preaviso);
        else
            return await Modificar(preaviso);
    }

    public async Task<bool> Eliminar(int preavisoId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Preavisos.Where(p => p.PreavisoId == preavisoId).AsNoTracking().ExecuteDeleteAsync() > 0;
    }

    public async Task<Preavisos?> Buscar(int preavisoId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Preavisos.FirstOrDefaultAsync(p => p.PreavisoId == preavisoId);
    }

    public async Task<List<Preavisos>> Listar(Expression<Func<Preavisos, bool>> criterio)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Preavisos.Where(criterio).AsNoTracking().ToListAsync();
    }
}
