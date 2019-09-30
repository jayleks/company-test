using System;
using System.Linq;
using System.Collections.Generic;
using CompanyTest.Data;

namespace CompanyTest.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _dbContext;

        public CompanyService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Company> GetList() => _dbContext.Companies.ToList();

        public bool IsExist(string id) => _dbContext.Companies.Any(e => e.Id == id);

        public Company Add(string name)
        {
            var company = new Company
            {
                Id = GenerateId(name),
                Name = name
            };

            _dbContext.Add(company);
            _dbContext.SaveChanges();

            return company;
        }

        public void Delete(string id)
        {
            _dbContext.Companies.Remove(new Company { Id = id });
            _dbContext.SaveChanges();
        }

        private string GenerateId(string name)
        {
            var id = string.Empty;
            var wordIndex = 0;
            var words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var lengths = Enumerable.Repeat(1, words.Length).ToArray();

            do
            {
                id = string.Join("", words.Select((s, i) => lengths[i] >= s.Length ? s : s.Substring(0, lengths[i])).ToArray());
                if (!IsExist(id))
                    break;

                wordIndex = GenerateNext(words, lengths, wordIndex);
            } while (true);

            return id;
        }

        private int GenerateNext(string[] words, int[] lengths, int index, int skip = 0)
        {
            if (skip == words.Length)
                throw new Exception("All versions of Company ID are busy");

            if (index >= words.Length)
                return GenerateNext(words, lengths, 0, 0);

            if (lengths[index] >= words[index].Length)
                return GenerateNext(words, lengths, index + 1, skip + 1);

            lengths[index] += 1;
            return index + 1;
        }
    }
}
