using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.BlobStorage
{
  
        public class BlobDto
        {
            public byte[] Content { get; set; }

            public string Name { get; set; }
    }



        public class GetBlobRequestDto
        {
            [Required]
            public string Name { get; set; }
    }




        public class SaveBlobInputDto
        {
            public string? Content { get; set; }
       
            [Required]
            public string Name { get; set; }
    }
    

        public interface IFileAppService : IApplicationService
        {
            Task SaveBlobAsync(SaveBlobInputDto input);

            Task<BlobDto> GetBlobAsync(GetBlobRequestDto input);
            Task DeleteBlobAsync(string input);
            Task<bool> IsExist(string input);
        }
    

}
