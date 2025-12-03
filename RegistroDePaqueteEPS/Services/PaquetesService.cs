using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroDePaqueteEPS.Data;
using RegistroDePaqueteEPS.Models;

namespace RegistroDePaqueteEPS.Services;

public class PaquetesService (IDbContextFactory<ApplicationDbContext> dbContext)
{
    public async Task<bool> Existe(int paqueteId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Paquetes.AnyAsync(p => p.PaqueteId == paqueteId);
    }

    public async Task AfectarExistencia(EstatusPaqueteDetalles[] detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        var componente = await contexto.EstatusPaquete.SingleAsync(e => e.EstatusPaqueteId == detalle[detalle.Length-1].EstatusPaqueteId);
        if (tipoOperacion == TipoOperacion.Suma)
            componente.Existencia++;
        else
            componente.Existencia--;
        await contexto.SaveChangesAsync();
    }

    public async Task<bool> Insertar(Paquetes paquete)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.Paquetes.Add(paquete);
        await AfectarExistencia(paquete.EstatusPaquete.ToArray(), TipoOperacion.Suma);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Paquetes paquete)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        var original = await contexto.Paquetes
            .Include(e => e.EstatusPaquete)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.PaqueteId == paquete.PaqueteId);

        if(original != null) return true;

        await AfectarExistencia(original.EstatusPaquete.ToArray(), TipoOperacion.Resta);

        contexto.EstatusPaqueteDetalles.RemoveRange(original.EstatusPaquete);

        contexto.Update(paquete);

        await AfectarExistencia(paquete.EstatusPaquete.ToArray(), TipoOperacion.Suma);

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
        var paquete = await Buscar(paqueteId);

        await AfectarExistencia(paquete.EstatusPaquete.ToArray(), TipoOperacion.Resta);
        contexto.EstatusPaqueteDetalles.RemoveRange(paquete.EstatusPaquete);
        contexto.Paquetes.Remove(paquete);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Paquetes?> Buscar(int paqueteId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Paquetes.Include(p => p.EstatusPaquete).FirstOrDefaultAsync(p => p.PaqueteId == paqueteId);
    }

    public async Task<List<Paquetes>> Listar(Expression<Func<Paquetes, bool>> criterio)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Paquetes.Include(p => p.EstatusPaquete).Where(criterio).AsNoTracking().ToListAsync();
    }

    public async Task<List<EstatusPaquete>> ListarEstatus()
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.EstatusPaquete.Where(e => e.EstatusPaqueteId > 0).AsNoTracking().ToListAsync();
    }
}
public enum TipoOperacion
{
    Suma = 1,
    Resta = 2
}
