using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Acme.BookStore.Currency_code
{
    internal class InsertCurrencycode
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<CurrencyCodes, Guid> _bookRepository;


        public InsertCurrencycode(IRepository<CurrencyCodes, Guid> bookRepository)
        {
            _bookRepository = bookRepository;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() <= 0)
            {


                string filePath =
                    @"C:\Users\AbhijeetWarade\Downloads\currencies.csv";
                StreamReader reader = null;
                if (File.Exists(filePath))
                {
                    reader = new StreamReader(File.OpenRead(filePath));
                    List<CurrencyCodes> listA = new List<CurrencyCodes>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        var ccc = new CurrencyCodes();
                        ccc.CurrencyCode = values[0];
                        ccc.CounteryName = values[1];
                        await _bookRepository.InsertAsync(ccc,
                            autoSave: true
                        );



                    }

                }
            }
        }
    }
}
        
    


