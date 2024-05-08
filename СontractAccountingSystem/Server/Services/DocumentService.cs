using Microsoft.EntityFrameworkCore;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Data;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly Repository _repository;

        public DocumentService(Repository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateRelateDocumentAsync(RelateDocumentModel parent, RelateDocumentModel relateDocumentModel)
        {
            RelateDocuments newRelation = new RelateDocuments()
            {
                Id = relateDocumentModel.Id,
                Document1Id = parent.RelatedDocumentId,
                Document2Id = relateDocumentModel.RelatedDocumentId,
                Document2Name = relateDocumentModel.DocumentName,
                Document2Number = relateDocumentModel.DocumentNumber,
                Document1 = null,
                Document2 = null
            };
            RelateDocuments newRelationReverse = new RelateDocuments()
            {
                Id = Guid.NewGuid(),
                Document1Id = relateDocumentModel.RelatedDocumentId,
                Document2Id = parent.RelatedDocumentId,
                Document2Name = parent.DocumentName,
                Document2Number = parent.DocumentNumber,
                Document1 = null,
                Document2 = null
            };

            var createdRelation = await _repository.CreateAsync(newRelation);
            if (createdRelation == null)
                return false;

            var createdReveerseRelation = await _repository.CreateAsync(newRelationReverse);
            if (createdReveerseRelation == null)
                return false;

            return true;
        }

        public async Task<List<RelateDocuments>> GetAllRelateDocumentListById(Guid id)
        {
            var relateList = await _repository.FindListByFilterAsync<RelateDocuments,Guid>("Document1Id", id);
            var reverseRelateList = await _repository.FindListByFilterAsync<RelateDocuments, Guid>("Document2Id", id);
            relateList.AddRange(reverseRelateList);
            return relateList;
        }

        public async Task<bool> RemoveRelateDocumentAsync(RelateDocuments relateDocuments)
        {
            //var list = await _repository.FindListByFilterAsync<RelateDocuments,Guid>("Document2Id", relateDocuments.Document1Id);
            //var createdRelation = await _repository.CreateAsync(newRelationReverse);
            //if (createdRelation == null)
            //    return false;

            return true;
        }
    }
}
