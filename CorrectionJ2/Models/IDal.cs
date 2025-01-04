namespace CorrectionJ2.Models
{
    public interface IDal : IDisposable
    {
        public void DeleteCreateDatabase();
        public List<Film> ObtenirTousLesFilms();
        public int CreerFilm(string titre, int annee, string realisateur, bool visionne);
        public void ModifierFilm(int id, string titre, int annee, string realisateur, bool visionne);
        public void SupprimerFilm(int id);
        public void initializeBdd();
        public Film ChercherFilmParTitre(string titre);

    }
}
