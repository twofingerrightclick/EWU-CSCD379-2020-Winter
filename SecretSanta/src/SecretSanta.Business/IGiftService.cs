using SecretSanta.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business
{
    public interface IGiftService
    {

        public abstract Task<List<Gift>> FetchAllAsync();
        abstract Task<Gift> FetchByIdAsync(int id);
        abstract Task<Gift> InsertAsync(Gift gift);
        abstract Task<Gift[]> InsertAsync(params Gift[] gift);
        abstract Task<Gift> UpdateAsync(int id, Gift gift);
        abstract Task<bool> DeleteAsync(int id);
    }
}