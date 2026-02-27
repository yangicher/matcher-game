using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using Matcher.Game.Services.Database;

namespace Matcher.Game.Services.Firebase
{
    public class FirestoreService : IDatabaseService
    {
        private readonly FirebaseFirestore _db;

        public FirestoreService()
        {
            _db = FirebaseFirestore.DefaultInstance;
        }

        public async Task<string> AddDocumentAsync<T>(string collectionName, T data)
        {
            try
            {
                CollectionReference collection = _db.Collection(collectionName);
                DocumentReference docRef = await collection.AddAsync(data);
                return docRef.Id;
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[Firestore] AddDocumentAsync {collectionName}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Dictionary<string, object>>> GetTopDocumentsAsync(string collectionName, string filterField, int filterValue, string orderByField, int limit)
        {
            try
            {
                Query query = _db.Collection(collectionName)
                    .WhereEqualTo(filterField, filterValue)
                    .OrderByDescending(orderByField)
                    .Limit(limit);

                QuerySnapshot snapshot = await query.GetSnapshotAsync();
        
                var results = new List<Dictionary<string, object>>();
                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (doc.Exists) results.Add(doc.ToDictionary());
                }
        
                return results;
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"[Firestore] GetTopDocumentsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task SetDocumentAsync<T>(string collectionName, string documentId, T data)
        {
            try
            {
                DocumentReference docRef = _db.Collection(collectionName).Document(documentId);
                await docRef.SetAsync(data, SetOptions.MergeAll);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[Firestore] SetDocumentAsync {collectionName}/{documentId}: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteDocumentAsync(string collectionName, string documentId)
        {
            try
            {
                DocumentReference docRef = _db.Collection(collectionName).Document(documentId);
                await docRef.DeleteAsync();
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[Firestore] DeleteDocumentAsync {collectionName}/{documentId}: {ex.Message}");
                throw;
            }
        }
    }
}