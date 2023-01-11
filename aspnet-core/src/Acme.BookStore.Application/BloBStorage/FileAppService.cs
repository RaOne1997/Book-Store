using Abp.Application.Services;
using Acme.BookStore.BlobStorage;
using Acme.BookStore.FileActionsDemo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Volo.Abp.BlobStoring;
using static System.Reflection.Metadata.BlobBuilder;

namespace Acme.BookStore.BloBStorage
{

    public class FileAppService : BookStoreAppService, IFileAppService
    {
        private readonly IBlobContainer<MyFileContainer> _fileContainer;

        public FileAppService(IBlobContainer<MyFileContainer> fileContainer)
        {
            _fileContainer = fileContainer;
        }

        public async Task SaveBlobAsync(SaveBlobInputDto input)
        {
            byte[] bytes = System.Convert.FromBase64String(input.Content.ToString());
            await _fileContainer.SaveAsync(input.Name, bytes, true);
            
        }

        public async Task<BlobDto> GetBlobAsync(GetBlobRequestDto input)
        {
            if (await IsExist(input.Name))
            {
                var blob = await _fileContainer.GetAllBytesAsync(input.Name);

                return new BlobDto
                {
                    Name = input.Name,
                    Content = blob
                };
            }

            return new BlobDto();
        }

        public async Task DeleteBlobAsync(string input)
        {
            
            await _fileContainer.DeleteAsync(input);

        }

        public async Task<bool> IsExist(string input)
        {
           var abc = await  _fileContainer.ExistsAsync(input);
           return abc;  
        }
    }

}

