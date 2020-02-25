using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<TransactionTag> _budgetTagRepository;
        private IQueryable<Tag> DefaultTagQuery => _tagRepository.Query(tag => tag.Active, enableTracking: false);
        private IQueryable<TransactionTag> DefaultBudgetTagQuery => _budgetTagRepository.Query(enableTracking: false);
        public async Task<IEnumerable<TransactionTag>> GetTransactionTags(int transactionId)
        {
            var query = from budgetTag in DefaultBudgetTagQuery
                        where budgetTag.TransactionId == transactionId
                        select budgetTag;

            return await query.ToArrayAsync();
        }

        public Tag GetTag(IEnumerable<Tag> tags, string tagName)
        {
            var query = from tag in tags
                        where tag.Name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase)
                        select tag;

            return query.SingleOrDefault();
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            return await DefaultTagQuery.ToArrayAsync();
        }

        public async Task<TransactionTag> SaveTransactionTag(TransactionTag budgetTag, bool saveChanges = true)
        {
            return await _budgetTagRepository.SaveChanges(budgetTag, saveChanges);
        }

        public async Task<Tag> SaveTag(Tag tag, bool saveChanges = true)
        {
            return await _tagRepository.SaveChanges(tag, saveChanges);
        }

        public IEnumerable<Tag> ParseTags(IEnumerable<Tag> tags, IEnumerable<string> delimitedTags)
        {
            var tagList = new List<Tag>();
            foreach(var tag in delimitedTags)
            {
                var foundTag = GetTag(tags, tag);
                if(foundTag == null)
                    foundTag = new Tag { Name = tag, Active = true };

                tagList.Add(foundTag);
            }

            return tagList;
        }

        public Tag GetTag(IEnumerable<Tag> tags, int tagId)
        {
            var query = from tag in tags
                   where tag.Id == tagId
                   select tag;

            return query.SingleOrDefault();
        }

        public IEnumerable<Tag> SearchTags(IEnumerable<Tag> tags, string searchTerm)
        {
            var query = from tag in tags
                        where tag.Name
                            .ToLower()
                            .Contains(searchTerm.ToLower())
                        select tag;

            return tags;
        }

        public async Task<int> GetMax()
        {
            if(DefaultTagQuery.Any())
                return await DefaultTagQuery.MaxAsync(tag => tag.Id);

            return 0;
        }

        public TagService(IRepository<Tag> tagRepository, IRepository<TransactionTag> budgetTagRepository)
        {
            _tagRepository = tagRepository;
            _budgetTagRepository = budgetTagRepository;
        }
    }
}
