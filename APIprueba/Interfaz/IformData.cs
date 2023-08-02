using APIprueba.DB;
using APIprueba.models;

namespace APIprueba.Interfaz
{
    public interface IformData
    {
        Task<bool> datos(formsData data); 
        Task<List<Estado>> Estados();
    }
}
