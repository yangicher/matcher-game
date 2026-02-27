using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matcher.Game.Services.Database
{
    public interface IDatabaseService
    {
        Task<string> AddDocumentAsync<T>(string collectionName, T data);
        
        Task<List<Dictionary<string, object>>> GetTopDocumentsAsync(
            string collectionName, string filterField, int filterValue, string orderByField, int limit);
        
        Task SetDocumentAsync<T>(string collectionName, string documentId, T data);
        
        Task DeleteDocumentAsync(string collectionName, string documentId);
    }
}