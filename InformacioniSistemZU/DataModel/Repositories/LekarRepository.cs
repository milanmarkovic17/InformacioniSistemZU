using InformacioniSistemZU.MainDbContext;
using InformacioniSistemZU.Models;
using Microsoft.EntityFrameworkCore;

namespace InformacioniSistemZU.DataModel.Repositories
{
    public class LekarRepository : ILekarRepository
    {
        private readonly MyDbContext _dbContext;

        public LekarRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Lekar IzbrisiLekara(int id)
        {
            if (id <= 0) 
            {
                throw new ArgumentOutOfRangeException();
            }

            var dataLekar = _dbContext.Lekari.FirstOrDefault(x => x.Id == id);
            if (dataLekar == null)
            {
                return null;
            }
            _dbContext.Remove(dataLekar);
            _dbContext.SaveChanges();
            return dataLekar; 
        }

        public Lekar IzmeniLekara(int id, Lekar lekar)
        {
            ArgumentNullException.ThrowIfNull(lekar); 

            var dataLekar = _dbContext.Lekari.FirstOrDefault(x => x.Id == id);
            if (dataLekar == null)
            {
                return null; 
            }
            dataLekar.Ime = lekar.Ime; 
            dataLekar.Prezime = lekar.Prezime;
            dataLekar.Jmbg = lekar.Jmbg;
            dataLekar.DatumRodjenja = lekar.DatumRodjenja;
            dataLekar.Opis = lekar.Opis;
            dataLekar.IsActive = lekar.IsActive;
            dataLekar.Specijalnost = lekar.Specijalnost;
            
            _dbContext.SaveChanges();
            return dataLekar;
        }

        public IEnumerable<Lekar> VratiSveLekare() 
        {
            return _dbContext.Lekari.ToList();
        }

        public Lekar VratiLekaraPoId(int id)
        {
            return _dbContext.Lekari.Include(l => l.Pacijenti).FirstOrDefault(x => x.Id == id);
        }

        public Lekar UnesiLekara(Lekar lekar)
        {
            ArgumentNullException.ThrowIfNull(lekar); 

            _dbContext.Lekari.Add(lekar);
            _dbContext.SaveChanges();
            return lekar;
        }

     
    }
}
