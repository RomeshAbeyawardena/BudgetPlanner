using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetTags();
        Task<Tag> SaveTag(Tag tag, bool saveChanges = true);
        
        Task<IEnumerable<TransactionTag>> GetTransactionTags(int transactionId);
        Task<TransactionTag> SaveTransactionTag(TransactionTag transactionTag, bool saveChanges = true);
        Tag GetTag(IEnumerable<Tag> tags, string tagName);
        Tag GetTag(IEnumerable<Tag> tags, int tagId);
        IEnumerable<Tag> ParseTags(IEnumerable<Tag> tags, IEnumerable<string> delimitedTags); 
    }
}
