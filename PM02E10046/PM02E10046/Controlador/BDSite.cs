using PM02E10304.Entidades;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM02E10304.Controlador
{
    public class BDSite
    {
        readonly SQLiteAsyncConnection db;

        public BDSite(string pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Sitios>().Wait();
        }

        public Task<List<Sitios>> ObtenerListaSitios()
        {
            return db.Table<Sitios>().ToListAsync();
        }

        public Task<Sitios> ObtenerSitios(int pcodigo)
        {
            return db.Table<Sitios>()
                .Where(i => i.id == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<Sitios> ObtenerSitioByid(int id) {
            return db.Table<Sitios>().Where(a => a.id == id).FirstOrDefaultAsync();
        }

        public Task<int> GrabarSitios(Sitios sitios)
        {
            if (sitios.id != 0)
            {
                return db.UpdateAsync(sitios);
            }
            else
            {
                return db.InsertAsync(sitios);
            }
        }

        public Task<int> EliminarSitios(Sitios sitios)
        {
            return db.DeleteAsync(sitios);
        }

    }
}
