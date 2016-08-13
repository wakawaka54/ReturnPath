using RP_Backend.Models.Sentences;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RP_Backend.Services
{
    public interface ISentenceService
    {
        Task<IEnumerable<StatisticsModel>> Statistics();
        Task<SentencesGetReturnType> Get(int? page);
        Task<SentenceModel> Create(SentenceModel model);
        Task<bool> Delete(string id);
    }	
}
